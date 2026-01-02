namespace SemaphoreDemo
{
    internal static class Program
    {
        private static readonly Random _random = new();
        private static int _itemsInBox = 0;
        private const int MAX = 10;

        private static readonly Semaphore _semaphore = new(MAX, MAX);

        private static readonly AutoResetEvent _autoResetEvent = new(false);

        static void Main(string[] args)
        {
            for (int i = 1; i <= 8; i++)
            {
                var t = new Thread(new ParameterizedThreadStart(MoveItemThread02))
                {
                    IsBackground = true
                };
                t.Start(i.ToString());
            }

            var t2 = new Thread(ReplaceBox) { IsBackground = true };
            t2.Start();

            Console.ReadLine();
        }

        private static void ReplaceBox()
        {
            while (true)
            {
                _autoResetEvent.WaitOne();

                Console.WriteLine("Replace with a new box");

                _itemsInBox = 0;
                _semaphore.Release(MAX);
            }
        }

        private static void MoveItemThread02(object? obj)
        {
            string armNumber = obj as string ?? "Unknown";

            while (true)
            {
                _semaphore.WaitOne();

                Console.WriteLine($"{armNumber} - Moving item...");

                MoveItem();

                Thread.Sleep(_random.Next(1000, 2000));

                Console.WriteLine($"{armNumber} - Done");

            }
        }

        private static void MoveItemThread(object? obj)
        {
            string armNumber = obj as string ?? "Unknown";

            while (true)
            {
                if (_itemsInBox < MAX)
                {
                    Console.WriteLine($"{armNumber} - Moving item...");

                    Thread.Sleep(_random.Next(100, 200));

                    MoveItem();

                    Thread.Sleep(_random.Next(1000, 2000));

                    Console.WriteLine($"{armNumber} - Done");
                }
            }
        }

        private static void MoveItem()
        {
            _itemsInBox++;

            Console.WriteLine($"Current quantity: {_itemsInBox}");

            if (_itemsInBox == MAX)
                _autoResetEvent.Set();
        }
    }
}
