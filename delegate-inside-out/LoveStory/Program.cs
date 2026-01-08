namespace LoveStory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Test();
            Test02();
        }

        static void Test02()
        {
            KuKiaObject kuKiaObject = new(Tui.TellHer);

            kuKiaObject.AddMessage(Ban.NhanEm);
            
            kuKiaObject.MeetSweetHeart();
        }

        static void Test()
        {
            KuKia.MeetSweetHeart();
        }
    }
}
