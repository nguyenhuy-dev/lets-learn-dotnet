namespace LambdaExpression
{
    delegate void PlayNumberDelegate(int x);
    internal class Program
    {
        static void Main(string[] args)
        {
            // HÀM VÔ DANH
            PlayNumberDelegate playNumber = delegate (int x) { Console.WriteLine($"{x}{x}{x}"); };
            playNumber(3);

            // CÒN CÁI DÂY NỊT
            // BIỂU THỨC LAMBDA
            playNumber = (int x) => Console.WriteLine($"{x}{x}");
            playNumber(2);

            // CÓ CÂU: "HÀM VÔ DANH" RÚT GỌN CHỈ CÒN CÁI DÂY NỊT => BIẾN THÀNH "BIỂU THỨC LAMBDA"

            // CÒN CÁI DÂY NỊT MỎNG
            playNumber = (x) => Console.WriteLine($"{x}{x}{x}{x}");
            playNumber(4);

            // CÒN CÁI DÂY NỊT MỎNG HƠN NỮA
            playNumber = x => Console.WriteLine($"{x}-{x}");
            playNumber(6789);
        }
    }
}
