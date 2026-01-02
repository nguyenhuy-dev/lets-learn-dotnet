namespace MultiThreadDemo
{
    internal static class Program
    {
        static bool b = false;

        static void Main(string[] args)
        {
            // Print();
            // Print02();
            // Print03();
            // Print04();
            PausedByCancellationToken();
        }

        static void PausedByCancellationToken()
        {
            CancellationTokenSource cts = new();

            var t1 = new Thread(new ParameterizedThreadStart(PrintHelloWithHelloObject02));
            var t2 = new Thread(new ParameterizedThreadStart(PrintHelloWithHelloObject02));
            var t3 = new Thread(new ParameterizedThreadStart(PrintHelloWithHelloObject02));
            var t4 = new Thread(new ParameterizedThreadStart(PrintHelloWithHelloObject02));


            t1.Start(new HelloParam { Message = "1", CancellationToken = cts.Token });
            t2.Start(new HelloParam { Message = "2", Delay = 2000, CancellationToken = cts.Token });
            t3.Start(new HelloParam { Message = "3", Delay = 3000, CancellationToken = cts.Token });
            t4.Start("t4");

            // Console.ReadLine();

            // cts.Cancel();
            
            cts.CancelAfter(10000);

            // cts.Dispose();
        }

        static void PrintHelloWithHelloObject02(object? obj)
        {
            var helloObj = obj as HelloParam;
            while (!helloObj?.CancellationToken.IsCancellationRequested ?? false)
            {
                Console.WriteLine($"Hello {helloObj?.Message ?? "NAME"}!");
                Thread.Sleep(helloObj?.Delay ?? 1000);
            }
        }

        static void Print04()
        {
            var t1 = new Thread(new ParameterizedThreadStart(PrintHelloWithHelloObject));
            var t2 = new Thread(new ParameterizedThreadStart(PrintHelloWithHelloObject));
            var t3 = new Thread(new ParameterizedThreadStart(PrintHelloWithHelloObject));
            var t4 = new Thread(new ParameterizedThreadStart(PrintHelloWithHelloObject));

            t1.Start(new HelloParam { Message = "1" });
            t2.Start(new HelloParam { Message = "2", Delay = 2000 });
            t3.Start(new HelloParam { Message = "3", Delay = 3000 });
            t4.Start("t4");

            Console.ReadLine();

            b = true;
        }

        static void PrintHelloWithHelloObject(object? obj)
        {
            var helloObj = obj as HelloParam;
            while (!b)
            {
                Console.WriteLine($"Hello {helloObj?.Message ?? "NAME"}!");
                Thread.Sleep(helloObj?.Delay ?? 1000);
            }
        }

        static void Print03()
        {
            var t1 = new Thread(new ParameterizedThreadStart(PrintHelloWithParameter));
            var t2 = new Thread(new ParameterizedThreadStart(PrintHelloWithParameter));
            var t3 = new Thread(new ParameterizedThreadStart(PrintHelloWithParameter));

            t1.Start("1");
            t2.Start("2");
            t3.Start("3");

            Console.ReadLine();
        }

        static void PrintHelloWithParameter(object? message)
        {
            while (true)
            {
                Console.WriteLine($"Hello {message}!");
                Thread.Sleep(1000);
            }
        }

        static void Print02()
        {
            var t1 = new Thread(new ThreadStart(PrintHello));
            var t2 = new Thread(new ThreadStart(PrintHello));
            var t3 = new Thread(new ThreadStart(PrintHello));

            t1.Start();
            t2.Start();
            t3.Start();

            Console.ReadLine();
        }

        static void PrintHello()
        {
            while (true)
            {
                Console.WriteLine("Hello!");
                Thread.Sleep(1000);
            }
        }

        static void Print()
        {
            var b = false;

            var t1 = new Thread(() =>
            {
                while (!b)
                {
                    Console.WriteLine("Hello 1!");
                    Thread.Sleep(1000);
                }
            });

            var t2 = new Thread(() =>
            {
                int n = 3;

                while (!b)
                {
                    if (n == 3)
                        Console.WriteLine("Hello 2!");
                    n--;
                    if (n == 0)
                        n = 3;

                    Thread.Sleep(1000);
                }
            });

            // Not best practice to shut down threads
            // t1.IsBackground = true;
            // t2.IsBackground = true;

            t1.Start();
            t2.Start();

            Console.ReadLine();

            b = true;
        }
    }
}
