using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HR
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // KHÔNG AI HARD-CODE ĐƯỜNG DẪN connectionString vào trong code cả, 
            // vì không thể thay đổi được khi chương trình đã được compile trên máy khác
            // CHO NÊN TA CẤU HÌNH VÀO TRONG "CONFIGURATION"
            // string connectionString = "Server=LAPTOP-UALL54OF\\HUYNGUYEN;Database=HR;Uid=sa;Pwd=12345;TrustServerCertificate=True";

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();

            using var conn = new SqlConnection(configuration.GetConnectionString("HRDB"));
            conn.Open();

            //TRANSACTION
            var trans = conn.BeginTransaction();

            // ListCountries(conn);
            // DisplayCountryCount(conn);

            CreateEmployee(
                "Nhu1",
                "Nguyen",
                "nguyenhuy@gmail.com",
                "0912345678",
                DateTime.Today,
                9,
                10000,
                100,
                6,
                conn, trans
                );

            CreateEmployee(
                "Nhu2",
                "Nguyen",
                "nguyenhuy@gmail.com",
                "0912345678",
                DateTime.Today,
                9,
                10000,
                100,
                6,
                conn, trans
                );

            // trans.Commit();  // OK, cho qua
            trans.Rollback();  // NOPE, hoàn lại các câu lệnh có gán transaction, không chạy nữa

            ListEmployees(conn);

            conn.Close();
        }

        private static void DisplayCountryCount(SqlConnection conn)
        {
            //NẾU ĐỂ CHẠY KHI ĐÃ CÓ ExecuteReader() THÌ KHÔNG HIỆU QUẢ BẰNG COUNT TRONG WHILE Ở TRÊN
            //CÒN NẾU KHÔNG CÓ ExecuteReader() THÌ CHẠY ỔN
            var cmd = new SqlCommand("SELECT COUNT(*) FROM countries", conn);
            var count = (int)cmd.ExecuteScalar();
            Console.WriteLine("=========================================");
            Console.WriteLine($"TotalV2: {count} rows");
        }

        private static void ListCountries(SqlConnection conn)
        {
            var cmd = new SqlCommand("SELECT * FROM countries", conn);
            using var reader = cmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                //Console.WriteLine($"country_id: {reader.GetString(0)} | country_name: {reader.GetString(1)}");
                Console.WriteLine($"country_id: {reader["country_id"]} | country_name: {reader["country_name"]}");
                count++;
            }
            reader.Close();
            Console.WriteLine("=========================================");
            Console.WriteLine($"Total: {count} rows");
        }

        private static void ListEmployees(SqlConnection conn)
        {
            var cmd = new SqlCommand("SELECT * FROM employees", conn);
            using var reader = cmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                Console.WriteLine($"id: {reader["id"]} | display_name: {reader["last_name"]} {reader["first_name"]} | email: {reader["email"]}");
                count++;
            }
            reader.Close();
            Console.WriteLine("===============================================");
            Console.WriteLine($"Total: {count} rows");
        }

        private static int CreateEmployee(string first_name, string last_name, string email, string phone_number, DateTime hire_date, int job_id, double salary, int manager_id, int department_id, SqlConnection conn, SqlTransaction trans)
        {
            var cmd = new SqlCommand(@"INSERT INTO employees (
                    first_name, 
                    last_name, 
                    email, 
                    phone_number, 
                    hire_date, 
                    job_id, 
                    salary, 
                    manager_id, 
                    department_id
                ) VALUES (
                    @first_name, 
                    @last_name, 
                    @email, 
                    @phone_number, 
                    @hire_date, 
                    @job_id, 
                    @salary, 
                    @manager_id, 
                    @department_id
                    )", conn, trans);
            cmd.Parameters.Add(new SqlParameter("@first_name", System.Data.SqlDbType.VarChar, 25)).Value = first_name;
            cmd.Parameters.Add(new SqlParameter("@last_name", System.Data.SqlDbType.VarChar, 25)).Value = last_name;
            cmd.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar, 100)).Value = email;
            cmd.Parameters.Add(new SqlParameter("@phone_number", System.Data.SqlDbType.VarChar, 10)).Value = phone_number;
            cmd.Parameters.Add(new SqlParameter("@hire_date", System.Data.SqlDbType.Date)).Value = hire_date;
            cmd.Parameters.Add(new SqlParameter("@job_id", System.Data.SqlDbType.Int)).Value = job_id;
            cmd.Parameters.Add(new SqlParameter("@salary", System.Data.SqlDbType.Decimal)).Value = salary;
            cmd.Parameters.Add(new SqlParameter("@manager_id", System.Data.SqlDbType.Int)).Value = manager_id;
            cmd.Parameters.Add(new SqlParameter("@department_id", System.Data.SqlDbType.Int)).Value = department_id;

            var result = cmd.ExecuteNonQuery();

            return result;
        }
    }
}
