namespace EventWaitHandleDemo
{
    internal class BlockingQueue<T>
    {
        private readonly List<T> _queue = [];

        private bool b = false;

        private readonly EventWaitHandle _ewh = new(false, EventResetMode.AutoReset);

        public void EnQueueEventWaitHandle(T item)
        {
            _queue.Add(item);
            _ewh.Set();
        }

        public T? DeQueueAutoReset()
        {
            _ewh.WaitOne();

            var item = _queue.FirstOrDefault();
            _queue.RemoveAt(0);

            return item;
        }

        public void EnQueue(T item) => _queue.Add(item);

        public bool IsEmpty() => _queue.Count == 0;

        public void EnQueue02(T item)
        {
            _queue.Add(item);
            b = true;
        }

        public T? DeQueue02()
        {
            while (!b) ;
            Console.WriteLine($"{Environment.CurrentManagedThreadId} - b = {b}");

            b = false;

            Console.WriteLine($"{Environment.CurrentManagedThreadId} - b = {b}");

            var item = _queue.FirstOrDefault();

            _queue.RemoveAt(0);

            Console.WriteLine($"{Environment.CurrentManagedThreadId} - win");

            return item;
        }

        public T? DeQueue()
        {
            var item = _queue.FirstOrDefault();

            _queue.RemoveAt(0);
            return item;
        }
    }
}
