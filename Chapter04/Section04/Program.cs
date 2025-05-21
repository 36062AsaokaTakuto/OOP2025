
using System;

namespace Section04 {
    internal class Program {
        static void Main(string[] args) {

            #region nullの判定
            string? name = null;//string? name = null　null許容型

            if (name is null) 
                Console.WriteLine("nameはnullです");
            #endregion


            #region null合体演算子
            string code = "12345";
            var message = GetMessage(code) ?? DefaultMassage();//GetMessage(code)がNULLだったらDefaultMassage()に行く
            Console.WriteLine(message);
            #endregion

            #region null条件演算子
            //Sale? sale = new Sale {
            //    ShopName = "新宿店",
            //    ProductCategory = "洋菓子",
            //    Amount = 523100
            //};
            Sale? sale = null;

            int? amount = sale?.Amount;

            Console.WriteLine(amount);

            #endregion

            #region 値の入れ方
            int a = 10;
            int b = 20;

            Console.WriteLine("a = " + a + ",b = " + b);

            (b, a) = (a, b);//swapのアルゴリズム（新しいやり方）
            /*swapのアルゴリズム（古いやり方）
            int box = a;
            a = b;
            b = box;*/

            Console.WriteLine("a = " + a + ",b = " + b);
            #endregion

            string? inputData = Console.ReadLine();

            if(int.TryParse(inputData,out var number)) {
                Console.WriteLine(number);
            } else {
                Console.WriteLine("エラー");
            }

            /*try {
                int num = int.Parse(inputData);
                Console.WriteLine(num);
            }
            catch (FormatException e) {//intなのに文字が入力された（stringなのに数字が入力された）
                //Console.WriteLine(e.Message);
                Console.WriteLine("フォーマットエラー");
                throw;
            }
            catch(OverflowException e) {//桁が多い（文字数が多い）
                Console.WriteLine("入力値が多きすぎます");
            }
            finally {
                Console.WriteLine("処理完了");
            }
            Console.WriteLine("メソッド終了");*/

        }

        private static object? GetMessage(string code) {
            return code;
        }

        private static object? DefaultMassage() {
            return "DefaultMassage";
        }
    }

    //売り上げクラス
    public class Sale {
        //店舗名
        public string ShopName { get; set; } = string.Empty;//(= string.Empty)空を入れてる。("")と一緒
        //商品カテゴリ
        public string ProductCategory { get; set; } = string.Empty;
        //売上高
        public int Amount { get; set; }

    }
}
