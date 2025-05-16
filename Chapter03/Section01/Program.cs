namespace Section01 {
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

            //全部小文字
            var lowerList = cities.ConvertAll(s => s.ToLower());

            lowerList.ForEach(s => Console.WriteLine(s));

            Console.WriteLine("");

            //全部大文字
            var upperList = cities.ConvertAll(s => s.ToUpper());

            upperList.ForEach(s => Console.WriteLine(s));
        }
    }
}
