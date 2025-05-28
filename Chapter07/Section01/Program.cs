namespace Section01 {
    internal class Program {
        static void Main(string[] args) {

            var books = Books.GetBooks();

            //①本の平均金額を表示
            Console.WriteLine((int)books.Average(x => x.Price));
            Console.WriteLine("-----");
            //②本のページ合計を出力
            Console.WriteLine(books.Sum(x => x.Pages));
            Console.WriteLine("-----");
            //③金額の安い書籍名と金額を表示
            var minPrice = books.Where(x => x.Price == books.Min(b => b.Price));
            foreach (var book in minPrice) {
                Console.WriteLine(book.Title + ":" + book.Price + "円");
            }
            Console.WriteLine("-----");
            //④ページが多い書籍名とページ数を表示
            var maxPages = books.Where(x => x.Pages == books.Max(b => b.Pages));
            foreach (var book in maxPages) {
                Console.WriteLine(book.Title + ":" + book.Pages + "ページ");
            }
            //一行バージョン
            //books.Where(x => x.Pages == books.Max(b => b.Pages)).ToList().ForEach(x => Console.WriteLine($"{x.Title} : {x.Pages}ページ"));
            Console.WriteLine("-----");
            //⑤タイトルに「物語」が含まれている書籍名をすべて表示
            var stories = books.Where(x =>x.Title.Contains("物語"));
            foreach (var book in stories) {
                Console.WriteLine(book.Title);
            }


        }
    }
}
