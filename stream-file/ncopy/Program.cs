namespace ncopy
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Test();
        }

        static void Test()
        {
            var source = @"D:\test\HelloWorld.txt";
            var dist = @"D:\test\HelloWorld-copy.txt";

            var buffer = new byte[256];
            using var instream = File.OpenRead(source);
            using var outstream = File.OpenWrite(dist);

            int n = instream.Read(buffer, 0, buffer.Length);
            while (n > 0)
            {
                Console.WriteLine(n.ToString());

                outstream.Write(buffer, 0, n);

                n = instream.Read(buffer, 0, buffer.Length);
            }
            
            // instream.Close();
            // outstream.Close(); //nếu không chịu đóng file out lại thì coi như chưa copy dữ liệu vô
            // viết lệnh "using" trước var thì câu lệnh đóng sẽ tự động được gọi
        }
    }
}
