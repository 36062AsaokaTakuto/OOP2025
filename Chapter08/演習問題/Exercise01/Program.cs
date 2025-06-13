
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Cozy lummox gives smart squid who asks for job pen";

            Exercise1(text);
            Console.WriteLine();

            Exercise2(text);

        }

        private static void Exercise1(string text) {
            //①ディクショナリインスタンスの作成
            var dict = new Dictionary<char, int>();
            //②1文字取り出す
            //'A'…0x41
            //③大文字に変換
            foreach (var uc in text.ToUpper()) {
                if ('A' <= uc && uc <= 'Z') {
                    //④アルファベットならディクショナリに登録
                    if (dict.ContainsKey(uc)) {
                        //登録済み：valueをインクリメント
                        dict[uc]++;
                    } else {
                        //未登録：valueに１を設定
                        dict[uc] = 1;
                    }
                }
                //⑤②へ戻る
            }
            //⑥すべての文字が読み終えたら、アルファベット順に並び替えて出力
            foreach (var c in dict.OrderBy(x => x.Key)) {
                Console.WriteLine($"{0}:{1}",c.Key,c.Value);
            }


        }

        private static void Exercise2(string text) {
            var dict = new SortedDictionary<char, int>();
            foreach (var c in text.ToUpper()) {
                if ('A' <= c && c <= 'Z') {
                    if (dict.ContainsKey(c)) {
                        dict[c]++;
                    } else {
                        dict[c] = 1;
                    }
                }
            }
            foreach (var c in dict) {
                Console.WriteLine($"{c.Key}:{c.Value}");
            }
        }
    }
}
