﻿namespace ProductSample {
    internal class Program {
        static void Main(string[] args) {

            Product karinto = new Product(123, "かりんとう", 180);

            Product daifuku = new Product(124, "大福", 320);


            //税抜き価格を表示
            Console.WriteLine(karinto.Name + "の税抜き価格は" + karinto.Price + "円です");
            //消費税額の表示
            Console.WriteLine(karinto.Name + "の消費税額は" + karinto.GetTax() +"円です");
            //税込み価格の表示
            Console.WriteLine(karinto.Name + "の税込み価格は" + karinto.GetPriceIncludingTax() +"円です");

            Console.WriteLine("");

            //税抜き価格を表示
            Console.WriteLine(daifuku.Name + "の税抜き価格は" + daifuku.Price + "円です");
            //消費税額の表示
            Console.WriteLine(daifuku.Name + "の消費税額は" + daifuku.GetTax() + "円です");
            //税込み価格の表示
            Console.WriteLine(daifuku.Name + "の税込み価格は" + daifuku.GetPriceIncludingTax() + "円です");





        }
    }
}
