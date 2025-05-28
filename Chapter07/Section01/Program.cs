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
            var minPrice = books.Min(x => x.Price);
            var minbook = books.Where(x => x.Price == minPrice);
            foreach (var book in minbook) {
                Console.WriteLine(book.Title + ":" + minPrice + "円");
            }
            Console.WriteLine("-----");
            //④ページが多い書籍名とページ数を表示
            var maxPages = books.Max(x => x.Pages);
            var maxbook = books.Where(x => x.Pages == maxPages);
            foreach (var book in maxbook) {
                Console.WriteLine(book.Title + ":" + maxPages + "ページ");
            }
            Console.WriteLine("-----");
            //⑤タイトルに「物語」が含まれている書籍名をすべて表示
            var story = books.Where(s =>s.Title.Contains("物語"));
            foreach (var book in story) {
                Console.WriteLine(book.Title);
            }


        }
    }
}
