namespace Exerciza01 {
    public class Program {
        static void Main(string[] args) {
            var songs = new List<Song>();

            Console.WriteLine("***** 曲の登録 *****");

            // 何件入力あるかわからないので無限ループ
            while (true) {
                Console.Write("曲名:");
                string title = Console.ReadLine();

                //endが入力されたら登録終了
                if (title.Equals("end",StringComparison.OrdinalIgnoreCase)) return;

                Console.Write("アーティスト名:");
                string artistName = Console.ReadLine();

                Console.Write("演奏時間(秒):");
                int length = int.Parse(Console.ReadLine());


                //Song song = new Song(title,artistName,length);
                Song song = new Song() {
                    Title = title,
                    ArtistName = artistName,
                    Length = length
                };

                songs.Add(song);

                Console.WriteLine();
            
            printSongs(songs);
            }
        }

        //2.1.4
        private static void printSongs(List<Song> songs) {
#if false
            foreach (var song in songs) {
                var minutes = song.Length / 60;
                var seconds = song.Length % 60;
                Console.WriteLine($"{song.Title}, {song.ArtistName} {minutes}:{seconds:00}");
            }
#else 
            //TimeSpan構造体を使った場合
            foreach (var song in songs) {
                var timespan = TimeSpan.FromSeconds(song.Length);
                Console.WriteLine($"{song.Title}, {song.ArtistName} {timespan.Minutes}:{timespan.Seconds:00}");
            }
            /*
            //または、以下でも可
            foreach (var song in songs) {
                Console.WriteLine(@"{0}, {1} {2:m\:ss}",
                    song.Title, song.ArtistName, TimeSpan.FromSeconds(song.Length));
            }
            */          
#endif
            Console.WriteLine();            

        }
    }
}
