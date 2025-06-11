
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
            var dict = new SortedDictionary<char, int>();
            //②1文字取り出す
            //'A'…0x41
            //③大文字に変換
            foreach (var c in text.ToUpper()) {
                if ('A' <= c && c <= 'Z') {
                    //④アルファベットならディクショナリに登録
                    if (dict.ContainsKey(c)) {
                        //登録済み：valueをインクリメント
                        dict[c]++;
                    } else {
                        //未登録：valueに１を設定
                        dict[c] = 1;
                    }
                }
                //⑤②へ戻る
            }
            //⑥すべての文字が読み終えたら、アルファベット順に並び替えて出力
            foreach (var c in dict) {
                Console.WriteLine($"{c.Key}:{c.Value}");
            }


        }

        private static void Exercise2(string text) {
            
        }
    }
}
