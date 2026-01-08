namespace DelegateIntro
{
    // delegate sẽ khai báo ở bên ngoài class khác, và nằm dưới namespace/package
    // Solution có nhiều project
    // Project có nhiều Namespace
    // Namespace có nhiều Class

    // tạo - delegate là 1 data type cho nhóm object phức tạp là cái đám hàm tao trỏ/quản lí, đại diện, tao bình đẳng với các class/data type khác
    public delegate void X(); // X: tui là class X đại diện cho 1 đám hàm ở dưới hay ở đâu đó có chung style là void
    // CHÍNH LÀ LỆNH KHAI BÁO CLASS VIẾT NGẮN GỌN - VIẾT NGOÀI CLASS KHÁC (NEWBIE)
    // public class X [void X()] Delegate
    // {

    //    public void X(đưa-vào-tên-hàm-cần-cất-trữ) { }
          // do X đại diện cho 1 đám hàm, đưa hàm đây tao cất trữ đại diện trỏ - delegate
    // }
    
    // X f = value là tên hàm ở dưới, là nick name cho tên hàm nào đó ở dưới
    // f là tên gọi khác cho cái hàm ở dưới cùng style X

    internal class Program
    {
        static void Main(string[] args)
        {
            // Test01();
            // Test02();
            Test03();
        }

        static void Test01()
        {
            X f1 = new X(TellHer); // f1 là tên khác, nick name của TellHer
            X f2 = NhanEm;

            // HẾT SỨC LƯU Ý, KHI GÁN BIẾN/NICK NAME CHO 1 HÀM CỤ THỂ NÀO ĐÓ
            // TUYỆT ĐỐI KHÔNG GHI TÊN HÀM KÈM () DẤU NGOẶC!!!
            // VÌ GHI () NGHĨA LÀ RUN HÀM LUÔN RỒI

            // TellHer();
            f1();

            // CÁCH GỌI THỨ 3: gọi qua hàm Invoke() được tạo sẵn
            Console.WriteLine("Call message by using Invoke() method inside the X class");
            f1.Invoke();
            f2.Invoke();
        }

        // Chơi delegate style nhiều hàm được nhồi vào biến thuộc style X
        // 1 CON TRỎ TRỎ ĐA HÀM, VẪN 1 VÙNG NEW X()
        // X CÒN ĐÓNG VAI THÙNG CHỨA
        static void Test02()
        {
            X f = new(TellHer);
            f += NhanEm;
            f += SayHelloToSweetHeart;

            //f();
            f.Invoke();
        }

        static void Test03()
        {
            X f = TellHer;
            f = NhanEm;

            f(); // TẠI 1 THỜI ĐIỂM BIẾN CHỈ LƯU 1 VALUE, TÊN GỌI ỨNG 1 VALUE
        }

        static void TellHer() => Console.WriteLine("Cuộc sống em ổn không. Có giống anh hy vọng!!!");

        static void NhanEm() => Console.WriteLine("Chúc em hạnh phúc bên người...");

        static void SayHelloToSweetHeart() => Console.WriteLine("Em thì quá bận với những toán lo!!!");
    }
}
