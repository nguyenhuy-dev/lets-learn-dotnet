namespace EventWaitHandleDemo
{
    internal static class Program
    {
        static readonly BlockingQueue<string> _queue = new();

        static void Main(string[] args)
        {
            // Test();
            // Test02();
            Test03();
        }

        static void Test03()
        {
            var t = new Thread(DeQueueThread02) { IsBackground = false };
            t.Start();

            t = new Thread(DeQueueThread02) { IsBackground = false };
            t.Start();

            for (int i = 0; i < 10000; i++)
                _queue.EnQueueEventWaitHandle(i.ToString());
        }

        static void DeQueueThread02()
        {
            while (true)
            {
                var s = _queue.DeQueueAutoReset();
                Console.WriteLine($"Q: {s}");
            }
        }

        static void Test02()
        {
            var t = new Thread(DeQueueThread02) { IsBackground = true };
            t.Start();

            t = new Thread(DeQueueThread02) { IsBackground = true };
            t.Start();

            string? s = null;
            do
            {
                Console.Write("S: ");
                s = Console.ReadLine();

                if (!string.IsNullOrEmpty(s))
                    _queue.EnQueueEventWaitHandle(s);

                Thread.Sleep(100);
            } while (!string.IsNullOrEmpty(s));
        }

        static void Test()
        {
            var t = new Thread(DeQueueThread02) { IsBackground = true };
            t.Start();

            t = new Thread(DeQueueThread02) { IsBackground = true };
            t.Start();

            string? s = null;
            do
            {
                Console.Write("S: ");
                s = Console.ReadLine();

                if (!string.IsNullOrEmpty(s))
                    _queue.EnQueue02(s);

                Thread.Sleep(100);
            } while (!string.IsNullOrEmpty(s));
        }

        static void DeQueueThread()
        {
            while (true)
            {
                if (!_queue.IsEmpty())
                {
                    var s = _queue.DeQueue();
                    Console.WriteLine($"Q: {s}");
                }
            }
        }
    }
}
