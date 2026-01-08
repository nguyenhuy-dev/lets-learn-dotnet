namespace LoveStory
{
    public delegate void SendLoveMessageDelegate();

    internal class KuKia
    {
        private static SendLoveMessageDelegate _globalMessage = null;

        public static void MeetSweetHeart()
        {
            Console.WriteLine("Hey baby, oh my sweet heart");

            SendLoveMessageDelegate messages = Tui.TellHer;
            messages += Ban.NhanEm;

            Console.WriteLine("By the way, you have messages from...");
            
            messages.Invoke();

            Console.WriteLine("==============================");

            _globalMessage?.Invoke();
        }
    }
}
