namespace Section04 {
    internal class Program {
        static void Main(string[] args) {
            var cities = new List<string> {
                "Tokyo",
                "New Delhi",
                "Bangkok",
                "London",
                "Paris",
                "Berlin",
                "Canberra",
                "Hong Kong",
            };

            var query = cities.Where(s => s.Length <= 5);    //- query変数に代入・必要になったタイミングでもう一回実行される・配列ToArray（遅延実行されない）
            foreach (var item in query) {
                Console.WriteLine(item);
            }
            Console.WriteLine("------");

            cities[0] = "Osaka";            //- cities[0]を変更 
            foreach (var item in query) {   //- 再度、queryの内容を取り出す[遅延実行]
                Console.WriteLine(item);
            }

        }
    }
}
