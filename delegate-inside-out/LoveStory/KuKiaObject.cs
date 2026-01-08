namespace LoveStory
{
    internal class KuKiaObject
    {
        private SendLoveMessageDelegate? _globalMessages;

        public KuKiaObject() => _globalMessages = null;

        public KuKiaObject(SendLoveMessageDelegate message) => _globalMessages += message;

        public void AddMessage(SendLoveMessageDelegate message) => _globalMessages += message;

        public void MeetSweetHeart()
        {
            if (_globalMessages == null)
            {
                Console.WriteLine("Nothing message!");
                return;
            }

            _globalMessages.Invoke();
        }
    }
}
