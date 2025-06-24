using System.Globalization;
using System.Security.Cryptography;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            //var today = new DateTime(2025,7,12);//日付    不変オブジェクト
            //var now = DateTime.Now;             //日付と時刻    不変オブジェクト

            //Console.WriteLine($"Today:{today}");//{}の中をtoday.Monthとかにすると月が返る
            //Console.WriteLine($"Now:{now}");

            //①自分の生年月日は何曜日かをプログラムを書いて調べる
            Console.Write("西暦：");
            var year = int.Parse(Console.ReadLine());
            Console.Write("月：");
            var month = int.Parse(Console.ReadLine());
            Console.Write("日：");
            var day = int.Parse(Console.ReadLine());

            var birth = new DateTime(year, month, day);

            var culture = new CultureInfo("jp-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            var str = birth.ToString("ggyy年y月d日",culture);

            var dayOfWeek = culture.DateTimeFormat.GetShortestDayName(birth.DayOfWeek); ;
            Console.WriteLine(str + dayOfWeek + "曜日");

            //自分の
            //string[] japaneseDays = { "日", "月", "火", "水", "木", "金", "土" };
            //if (DateTime.TryParse($"{ad}/{month}/{day}",out var born)) {
            //    DayOfWeek dayOfWeek = born.DayOfWeek;
            //    for (int i = 0; i < 7; i++) {
            //        if ((int)dayOfWeek == i) {
            //            Console.WriteLine($"平成{(int.Parse(ad) + 12) % 100}年{month}月{day}日は{japaneseDays[i]}曜日です");

            //        }
            //    }
                
            //}

            //②うるう年の判定プログラム
            var LeapYear = DateTime.IsLeapYear(2006);

        }
    }
}
