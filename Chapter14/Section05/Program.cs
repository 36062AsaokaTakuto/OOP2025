using Section01;

namespace Section05 {
    internal class Program {
        static void Main(string[] args) {
            var selected = Library.Books
                .AsParallel()//これをつけるだけで並列化を行うことができる
                .AsOrdered()//順序を保証したい場合はAsOrderedを追加
                .Where(b => b.Price > 500 && b.Price < 2000)
                .Select(b => new { b.Title });
            foreach (var item in selected) {
                Console.WriteLine(item.Title);
            }


        }
    }
}
