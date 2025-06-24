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
            //Console.WriteLine(str + dayOfWeek + "曜日");
            Console.WriteLine(str + birth.ToString("ddd曜日",culture));

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
            var LeapYear = DateTime.IsLeapYear(year);
            if (LeapYear) {
                Console.WriteLine("閏年です");
            } else {
                Console.WriteLine("閏年ではありません");
            }

            //③生まれてから〇日目
            var today = DateTime.Today;
            TimeSpan df = DateTime.Today - birth;
            Console.WriteLine($"生まれてから{df.Days}日目");

            //TimeSpan diff;
            //while (true) {
            //    diff = DateTime.Now - birth;
            //    Console.WriteLine($"\r{diff.TotalSeconds}秒");//生まれてからの経過秒数
            //}//無限ループ

            //④あなたは〇歳です！
            int age = GetAge(birth, today);
            Console.WriteLine($"あなたは{age}歳です！");

            //⑤1月1日から何日目か？
            int dayOfYear = today.DayOfYear;
            Console.WriteLine($"1月1日から{dayOfYear}日経過");
        }
        static int GetAge(DateTime birthday,DateTime targetDay) {
            var age = targetDay.Year - birthday.Year;
            if (targetDay < birthday.AddYears(age)) {
                age--;
            }
            return age;
        }
    }
}
