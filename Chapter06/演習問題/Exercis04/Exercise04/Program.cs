using System.Runtime.CompilerServices;

namespace Exercise04 {
    internal class Program {
        static void Main(string[] args) {
            var line = "Novelist=谷崎潤一郎;BestWork=春琴抄;Born=1886";
            ToJapanese(line);

            var wards = line.Split(';');
            foreach (var item in wards) {
                var word = item.Split('=');
                Console.WriteLine(ToJapanese(word[0]) + ':' + ToJapanese(word[1]));
            }

        }

        /// <summary>
        /// 引数の単語を日本語へ変換します
        /// </summary>
        /// <param name="key">"Novelist","BestWork","Born"</param>
        /// <returns>"「作家」,「代表作」,「誕生年」</returns>
        static string ToJapanese(string key) {
            switch (key) {
                case "Novelist": return "作家";
                case "BestWork": return "代表作";
                case "Born": return "誕生年";
                default: return key;//←どのcaseにも当てはまらない時に実行される処理
            }

        }
    }
}