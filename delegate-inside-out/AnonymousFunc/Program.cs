namespace AnonymousFunc
{
    delegate void PlayNumberDelegate(int x);

    internal class Program
    {
        static void Main(string[] args)
        {
            // Test();
            // Test02();
            Test03();
        }

        static void Test()
        {
            Console.WriteLine("Play methods");
            PlayNumberDelegate playNumber = CloneNumbers; // nhân bản 3 lần

            playNumber(5);
            playNumber.Invoke(3);

            Console.WriteLine("In ra bình phương");
            playNumber = PowerBy2Number;
            playNumber.Invoke(10);
            playNumber(5);

            // CHƠI HỆ MULTICAST
            Console.WriteLine("Hệ multicast...");
            playNumber += CloneNumbers; // Power() trước rồi Clone() sau
            playNumber(6);
        }

        static void Test02()
        {
            // C2: DÙNG ANONYMOUS FUNCTION: THIẾT KẾ 1 HÀM KHÔNG THÈM CÓ TÊN, CHỈ CẦN ĐẦU VÀO TUÂN THEO ĐỊNH DẠNG CỦA DELEGATE ĐÃ KHAI BÁO; VIẾT CODE NGAY TRÊN CÂU LỆNH GÁN HỢP ĐỒNG ỦY QUYỀN
            // HÀM ON THE GO - TAKE A WAY
            PlayNumberDelegate playNumber = delegate (int n) { Console.WriteLine($"{n}{n}{n}{n}"); };
            playNumber(9);
        }

        static void Test03()
        {
            PlayNumberDelegate playNumber = delegate (int x) { Console.WriteLine($"The {x}^ 5 = {Math.Pow(x, 5)}"); };

            playNumber.Invoke(10);
        }

        static void CloneNumbers(int n) => Console.WriteLine($"{n}{n}{n}");

        static void PowerBy2Number(int x) => Console.WriteLine($"The {x}^ 2 = {x * x}");

        // TUI MÚN CÓ HÀM NHẬN VÀO 1 CON SỐ NGUYÊN NHƯNG: IN RA, LẶP LẠI THÀNH 4 SỐ
        static void CloneNumbersLikeGoldFormat(int n) => Console.WriteLine($"{n}{n}{n}{n}");
    }
}
