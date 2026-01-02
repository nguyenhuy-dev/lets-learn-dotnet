namespace AsyncDemo
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Main - 1 - Thread ID = {Environment.CurrentManagedThreadId}");

            // Chạy hàm AsyncFunc1 trên 1 thread mới, có nghĩa là chạy song song, multi threads đó.
            Task t = Task.Run(AsyncFunc1);

            // Nếu hàm là async và có await bên trong nó, thì coi như là thao tác bất đồng bộ.
            await AsyncFunc2();

            Console.WriteLine($"Main - 2 - Thread ID = {Environment.CurrentManagedThreadId}");

            t.Wait();
        }

        private static int AsyncFunc1()
        {
            Console.WriteLine($"AsyncFunc1 - Thread ID = {Environment.CurrentManagedThreadId}");

            return 0;
        }

        // Đánh dấu 1 hàm là async, là 1 thao tác tốn kém. Vì phải sinh ra cấu trúc máy trạng thái (state machine).
        // Quản lý các kết quả sinh ra từ các thao tác bất đồng bộ. Vì hàm async 2 có thể được gọi ở nhiều nơi.
        private static async Task<int> AsyncFunc2()
        {
            Console.WriteLine($"Async2 - 1 - Thread ID = {Environment.CurrentManagedThreadId}");

            // Hàm async chỉ thật sự là async khi chạm đến câu lệnh await bên trong nó.
            // var r = (await File.ReadAllTextAsync("sample.txt")).Length;

            await Task.Delay(1000);

            Console.WriteLine($"Async2 - 2 - Thread ID = {Environment.CurrentManagedThreadId}");

            return 0;
        }
    }
}
