using System.Diagnostics;

namespace Task_Samples
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var t1 = Delay01Async();
            var t2 = Delay02Async();
            //var t3 = Delay03Async();
            Delay03Async();

            await t1;
            await t2;
            //await t3;

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

        static async void Delay03Async()
        {
            Console.WriteLine("Delay3...");
            await Task.Delay(3000);
            Console.WriteLine("Delay3 done...");
        }
    }
}
