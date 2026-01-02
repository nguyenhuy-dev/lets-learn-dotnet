namespace MultiThreadDemo
{
    internal class HelloParam
    {
        public required string Message { get; set; }

        public int Delay { get; set; } = 1000;

        public CancellationToken CancellationToken { get; set; }
    }
}
