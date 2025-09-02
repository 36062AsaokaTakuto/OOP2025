using System.Text.RegularExpressions;

namespace Exercize02 {
    internal class Program {
        static void Main(string[] args) {
            string filePath = "sample.txt";
            Pickup3DigitNumber(filePath);
        }

        private static void Pickup3DigitNumber(string filePath) {
            foreach (var line in File.ReadLines(filePath)) {
                var matches = Regex.Matches(line,@"\b\d{3,}\b" );//\b = 境界
                foreach (Match m in matches) {
                    //結果を出力
                    Console.WriteLine(m);
                }
            }
        }
    }
}
