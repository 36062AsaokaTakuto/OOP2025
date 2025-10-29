
namespace Exercize01 {
    internal class Program {
        static void Main(string[] args) {
            Exercise1_2();
            Console.WriteLine();
            Exercise1_3();
            Console.WriteLine();
            Exercise1_4();
            Console.WriteLine();
            Exercise1_5();
            Console.WriteLine();
            Exercise1_6();
            Console.WriteLine();
            Exercise1_7();
            Console.WriteLine();
            Exercise1_8();

            Console.ReadLine();



        }

        private static void Exercise1_2() {
            var book = Library.Books.MaxBy(b => b.Price);
            Console.WriteLine(book);
        }

        private static void Exercise1_3() {
            var result = Library.Books
                .GroupBy(b => b.PublishedYear)
                .OrderBy(b => b.Key)
                .Select(b => new {
                     PublishedYear = b.Key,
                     Count = b.Count()
                 });
            foreach (var item in result) {
                Console.WriteLine($"{item.PublishedYear}年:{item.Count}");
            }
        }

        private static void Exercise1_4() {
            var selected = Library.Books
                .OrderByDescending(b => b.PublishedYear)
                .ThenByDescending(b => b.Price);
            foreach (var book in selected) {
                Console.WriteLine($"{book.PublishedYear}年 {book.Price}円 {book.Title}");
            }
        }

        private static void Exercise1_5() {
            var books = Library.Books
                            .Where(b => b.PublishedYear == 2022)
                            .Join(Library.Categories,
                                    book => book.CategoryId,
                                    category => category.Id,
                                    (book, category) => new {
                                        Category = category.Name,
                                    }
                            );
            foreach (var book in books.Distinct()) {
                Console.WriteLine($"{book.Category}");
            }
        }

        private static void Exercise1_6() {
            var books = Library.Books
                            .Join(Library.Categories,
                                    book => book.CategoryId,
                                    category => category.Id,
                                    (book, category) => new {
                                        book.Title,
                                        Category = category.Name,
                                    }
                            )
                            .GroupBy(c => c.Category)
                            .OrderBy(c => c.Key);
            foreach (var book in books) {
                Console.WriteLine($"# {book.Key}");
                foreach (var item in book) {
                    Console.WriteLine($"   {item.Title}");
                }
            }
        }

        private static void Exercise1_7() {
            var books = Library.Books
                            .Where(c => c.CategoryId == 1)
                            .Join(Library.Categories,
                                    book => book.CategoryId,
                                    category => category.Id,
                                    (book, category) => new {
                                        book.Title,
                                        Category = category.Name,
                                        book.PublishedYear,
                                    }
                            )
                            .GroupBy(b => b.PublishedYear)
                            .OrderBy(b => b.Key);
            foreach (var book in books) {
                Console.WriteLine($"# {book.Key}");
                foreach (var item in book) {
                    Console.WriteLine($"   {item.Title}");
                }
            }
        }

        private static void Exercise1_8() {
            
        }
    }
}
