using System.Globalization;

namespace Exercis01 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("文字を入力してください。");
            var s1 = Console.ReadLine();
            Console.WriteLine("文字を入力してください。");
            var s2 = Console.ReadLine();

            var cultureinfo = new CultureInfo("ja-JP");
            if (String.Compare(s1, s2, cultureinfo, CompareOptions.None) == 0)
                Console.WriteLine("等しい");
            else
                Console.WriteLine("等しくない");
            
        }
    }
}
