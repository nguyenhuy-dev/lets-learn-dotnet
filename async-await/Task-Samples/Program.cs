using System.Diagnostics;

namespace Task_Samples
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            // await DemoTaskSample();

            // DemoTaskSample2();

            // DemoTaskSample3();

            await DemoTaskSample4();
        }

        static Task<double> CalculateResult(int n)
        {
            if (n == 0) return Task.FromResult(0.0);

            return Task.FromResult(Math.Pow(10, n));
        }

        static async Task DemoTaskSample4()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var t1 = Delay01Async();
            var t2 = Delay02Async();
            var t3 = Delay03Async();

            var t123 = Task.WhenAll(t1, t2, t3);
            await t123;

            var t = CalculateResult(2);
            await t;

            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
        }

        static void RunMethod()
        {
            for (int i =0; i < 10; i++)
            {
                Console.WriteLine($"i = {i}");
                Task.Delay(500).Wait();
            }
        }

        static void DemoTaskSample3()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var t1 = Delay01Async();
            var t2 = Delay02Async();
            var t3 = Delay03Async();

            var tr = Task.Run(RunMethod);

            var t123 = Task.WhenAll(t1, t2, t3);

            Task.WaitAny(t123, tr);

            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
        }

        static void DemoTaskSample2()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var t1 = Delay01Async();
            var t2 = Delay02Async();
            var t3 = Delay03Async();

            var tr = Task.Run(RunMethod);

            // Chờ cho tất cả các Task hoàn thành.
            // Task.WaitAll(t1, t2, t3);

            // Task.WaitAny(t1, t2, t3);

            // Tạo ra 1 Task mới đại diện cho việc chờ các Task trên hoàn thành.
            // Task t123 sẽ hoàn thành khi cả t1, t2, t3 hoàn thành.
            // Áp dụng ở Api Client khi muốn lấy dữ liệu từ 2 api trở lên,
            // và muốn biết khi nào tất cả dữ liệu được lấy về.
            var t123 = Task.WhenAll(t1, t2, t3, tr);
            t123.Wait();

            // Áp dụng khi muốn lấy dữ liệu từ 1 trong 2 loại database trở lên.
            //var t123 = Task.WhenAny(t1, t2, t3);
            //t123.Wait();

            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
        }

        static async Task DemoTaskSample()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var t1 = Delay01Async();
            var t2 = Delay02Async();
            var t3 = Delay03Async();

            // Delay03Async();

            await t1;
            await t2;
            await t3;

            // Kết quả của t3 được tái sử dụng, không chạy lại.
            // Vì đối tượng Task của t3 đã được state machine đánh dấu, lưu lại
            await t3;

            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
        }

        static async Task Delay01Async()
        {
            Console.WriteLine("Delay1...");
            await Task.Delay(1000);
            Console.WriteLine("Delay1 done...");
        }

        static async Task Delay02Async()
        {
            Console.WriteLine("Delay2...");
            await Task.Delay(2000);
            Console.WriteLine("Delay2 done...");
        }

        static async Task Delay03Async()
        {
            Console.WriteLine("Delay3...");
            await Task.Delay(3000).ConfigureAwait(false); // Không cần UI Thread quay lại, vì không có thao tác UI nào được thực thi.
            Console.WriteLine("Delay3 done...");
        }
    }
}
