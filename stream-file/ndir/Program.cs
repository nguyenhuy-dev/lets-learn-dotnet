namespace ndir
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Test();
        }

        // Clone lệnh "dir" trong cmd bằng SYSTEM.IO
        static void Test()
        {
            var path = "C:\\";

            var dir = new DirectoryInfo(path);
            var directories = dir.GetDirectories();
            //var directories = Directory.GetDirectories(path);  // này chỉ là tên của các file, không gồm đủ info

            foreach (var d in directories)
                Console.WriteLine($"{d.LastWriteTime:MM/dd/yyyy} {d.LastWriteTime:HH:mm}    <DIR>   {d.Name}");

            var files = dir.GetFiles();
            foreach (var f in files)
                Console.WriteLine($"{f.LastWriteTime:MM/dd/yyyy} {f.LastWriteTime:HH:mm}          {f.Length:#,###} {f.Name}");
        }
    }
}
