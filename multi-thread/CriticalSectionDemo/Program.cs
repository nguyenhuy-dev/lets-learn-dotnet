
namespace CriticalSectionDemo
{
    internal static class Program
    {
        static int x = 10;
        static int y = 20;

        static readonly object lockObject = new();

        static void Main(string[] args)
        {
            SupplyThread();
        }

        private static void SupplyThread()
        {
            var t1 = new Thread(new ThreadStart(DoPrintXy)) { IsBackground = true };
            t1.Start();

            Swap();

            Console.ReadLine();
        }

        private static void ExeWithoutThread()
        {
            PrintXy();

            Swap();

            PrintXy();
        }

        private static void DoPrintXy()
        {
            while (true)
            {
                PrintXy();
                Thread.Sleep(30);
            }
        }

        private static void DoSwap()
        {
            while (true)
            {
                Swap();
            }
        }

        private static void Swap()
        {
            lock (lockObject)
            {
                Thread.Sleep(150);
                int temp = x;
                x = y;
                Thread.Sleep(200);
                y = temp;
                Thread.Sleep(150);
            }
        }

        private static void PrintXy()
        {
            lock (lockObject)
            {
                Console.WriteLine($"x = {x}, y = {y}");
            }
        }
    }
}
