using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace Exericise01 {
    internal class Program {
        static void Main(string[] args) {
            //問題12.1.1
            var emp = new Employee() {
                Id = 123,
                Name = "山田太郎",
                HireDate = new DateTime(2018, 10, 1),
            };
            var jsonString = Serialize(emp);
            Console.WriteLine(jsonString);
            var obj = Deserialize(jsonString);
            Console.WriteLine(obj);

            //問題12.1.2
            Employee[] employees = [
                new () {
                    Id = 123,
                    Name = "山田太郎",
                    HireDate = new DateTime(2018, 10, 1),
                },
                new () {
                    Id = 198,
                    Name = "田中華子",
                    HireDate = new DateTime(2020, 4, 1),
                },
            ];
            Serialize("employees.json", employees);

            //問題12.1.3
            //var empdata = Deserialize_f("employees.json");
            //foreach (var empd in empdata)
            //    Console.WriteLine(empd);
        }
        //問題12.1.1
        static string Serialize(Employee emp) {
            var options = new JsonSerializerOptions {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            string jsonString = JsonSerializer.Serialize(emp, options);
            return jsonString;
        }

        static Employee? Deserialize(string text) {
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var novelist = JsonSerializer.Deserialize<Employee>(text);
            return novelist;
        }

        //問題12.1.2
        //シリアル化してファイルへ出力する
        static void Serialize(string filePath, IEnumerable<Employee> employees) {
            //var text = File.ReadAllText(filePath);
            //var novelist = JsonSerializer.Deserialize<List<Employee>>(text);
            //novelist?.ForEach(Console.WriteLine);
        }

        //問題12.1.3
        //シリアル化してファイルへ出力する
        //static Employee[] Deserialize_f(string filePath) {



        //}

    }
    public record Employee {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime HireDate { get; set; }
    }
}
