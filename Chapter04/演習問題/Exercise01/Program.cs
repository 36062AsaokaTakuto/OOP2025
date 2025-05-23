
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            List<string> langs = [
            "C#", "Java", "Ruby", "PHP", "Python", "TypeScript",
            "JavaScript", "Swift", "Go",
            ];

            Exercise1(langs);
            Console.WriteLine("---");
            Exercise2(langs);
            Console.WriteLine("---");
            Exercise3(langs);
        }

        private static void Exercise1(List<string> langs) {
            //foreach文
            foreach (var lang in langs) {
                if (lang.Contains('S')) {
                    Console.WriteLine(lang);
                }
            }

            Console.WriteLine("");

            //for文
            for (int i = 0; i < langs.Count; i++) {
                if (langs[i].Contains('S')) 
                    Console.WriteLine(langs[i]);
            }

            Console.WriteLine("");

            //while文
            int index = 0;
            while (index < langs.Count) {
                if (langs[index].Contains('S'))
                    Console.WriteLine(langs[index]);
                index++;
            }
        }

        private static void Exercise2(List<string> langs) {
            //langs.Where(s => s.Contains('S')).ToList().ForEach(s => Console.WriteLine(s));
            var selectrd = langs.Where(x => x.Contains('S'));
            foreach(var lang in selectrd) {
                Console.WriteLine(lang);
            }
        }

        private static void Exercise3(List<string> langs) {
            Console.WriteLine(langs.Find(x => x.Length == 10)　?? "unknown");

        }
    }
}
