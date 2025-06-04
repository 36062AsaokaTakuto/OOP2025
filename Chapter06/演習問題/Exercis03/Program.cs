
using System.Data.SqlTypes;
using System.Text;

namespace Exercis03 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Jackdaws love my big sphinx of quartz";

            Console.WriteLine("6.3.1");
            Exercise1(text);

            Console.WriteLine("6.3.2");
            Exercise2(text);

            Console.WriteLine("6.3.3");
            Exercise3(text);

            Console.WriteLine("6.3.4");
            Exercise4(text);

            Console.WriteLine("6.3.5");
            Exercise5(text);

            Console.WriteLine("6.3.99");
            Exercise6(text);

        }

        private static void Exercise1(string text) {
            Console.WriteLine("空白数:" + text.Count(c => c == ' '));
        }

        private static void Exercise2(string text) {
            Console.WriteLine(text.Replace("big", "small"));
        }

        private static void Exercise3(string text) {
            var array = text.Split([' '], StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder(array[0]);
            foreach (var word in array.Skip(1)) {
                sb.Append(' ');
                sb.Append(word);
            }
            sb.Append('.');
            Console.WriteLine(sb); 
        }

        private static void Exercise4(string text) {
            string[] word = text.Split(' ');
            Console.WriteLine(word.Count());
        }

        private static void Exercise5(string text) {
            string[] word = text.Split(' ');
            var str4 = word.Where(x => x.Length <= 4);
            foreach (var item in str4) {
                Console.WriteLine(item);
            }
        }

        private static void Exercise6(string text) {
            //辞書で集計
            var str = text.ToLower().Replace(" ","");

            var alphDicCount = Enumerable.Range('a', 26)
                .ToDictionary(num => ((char)num).ToString(),num => 0);
            foreach (var alph in str) {
                alphDicCount[alph.ToString()]++;
            }

            foreach (var item in alphDicCount) {
                Console.WriteLine($"{item.Key}:{item.Value}");
            }

            Console.WriteLine("");

            //配列で集計
            var array = Enumerable.Repeat(0, 26).ToArray();
            foreach (var alph in str) {
                array[alph - 'a']++;

            }

            for (char ch = 'a'; ch <= 'z'; ch++) {
                Console.WriteLine($"{ch}:{array[ch - 'a']}");
            }

            Console.WriteLine("");

            //'a'から順にカウントして出力
            for (char ch = 'a'; ch <= 'z'; ch++) {
                Console.WriteLine($"{ch}:{text.Count(tc => tc == ch)}");
            }

            Console.WriteLine("");

            //自分で考えたやつ
            char str1 = 'a';
            while (str1 <= 'z') {
                var count = text.ToLower().Count(x => x == str1);//[ToLower]すべて小文字に変換
                Console.WriteLine($"{str1}:{count}");
                str1++;
            }
        }
    }
}
