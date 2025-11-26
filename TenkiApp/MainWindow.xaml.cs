using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace TenkiApp {
    public partial class MainWindow : Window {
        private readonly HttpClient _httpClient = new HttpClient();

        // 全都道府県リスト（県庁所在地の座標）
        private readonly Dictionary<string, (double lat, double lon)> PrefectureCoords =
            new Dictionary<string, (double lat, double lon)>
        {
            {"北海道",(43.06417,141.34694)}, {"青森県",(40.82444,140.74)}, {"岩手県",(39.70361,141.1525)},
            {"宮城県",(38.26889,140.87194)}, {"秋田県",(39.71861,140.1025)}, {"山形県",(38.24056,140.36333)},
            {"福島県",(37.75,140.46778)}, {"茨城県",(36.34139,140.44667)}, {"栃木県",(36.56583,139.88361)},
            {"群馬県",(36.39111,139.06083)}, {"埼玉県",(35.85694,139.64889)}, {"千葉県",(35.60472,140.12333)},
            {"東京都",(35.68944,139.69167)}, {"神奈川県",(35.44778,139.6425)}, {"新潟県",(37.90222,139.02361)},
            {"富山県",(36.69528,137.21139)}, {"石川県",(36.59444,136.62556)}, {"福井県",(36.06528,136.22194)},
            {"山梨県",(35.66389,138.56833)}, {"長野県",(36.65139,138.18111)}, {"岐阜県",(35.39111,136.72222)},
            {"静岡県",(34.97694,138.38306)}, {"愛知県",(35.18028,136.90667)}, {"三重県",(34.73028,136.50861)},
            {"滋賀県",(35.00444,135.86833)}, {"京都府",(35.02139,135.75556)}, {"大阪府",(34.68639,135.52)},
            {"兵庫県",(34.69139,135.18306)}, {"奈良県",(34.68528,135.83278)}, {"和歌山県",(34.22611,135.1675)},
            {"鳥取県",(35.50361,134.23833)}, {"島根県",(35.47222,133.05056)}, {"岡山県",(34.66167,133.935)},
            {"広島県",(34.39639,132.45944)}, {"山口県",(34.18583,131.47139)}, {"徳島県",(34.06583,134.55944)},
            {"香川県",(34.34028,134.04333)}, {"愛媛県",(33.84167,132.76611)}, {"高知県",(33.55972,133.53111)},
            {"福岡県",(33.60639,130.41806)}, {"佐賀県",(33.24944,130.29889)}, {"長崎県",(32.74472,129.87361)},
            {"熊本県",(32.78972,130.74167)}, {"大分県",(33.23806,131.6125)}, {"宮崎県",(31.91111,131.42389)},
            {"鹿児島県",(31.56028,130.55806)}, {"沖縄県",(26.2125,127.68111)}
        };

        // 漢字→ひらがな読み
        private readonly Dictionary<string, string> PrefectureReadings = new()
        {
            {"北海道","ほっかいどう"},{"青森県","あおもりけん"},{"岩手県","いわてけん"},
            {"宮城県","みやぎけん"},{"秋田県","あきたけん"},{"山形県","やまがたけん"},
            {"福島県","ふくしまけん"},{"茨城県","いばらきけん"},{"栃木県","とちぎけん"},
            {"群馬県","ぐんまけん"},{"埼玉県","さいたまけん"},{"千葉県","ちばけん"},
            {"東京都","とうきょうと"},{"神奈川県","かながわけん"},{"新潟県","にいがたけん"},
            {"富山県","とやまけん"},{"石川県","いしかわけん"},{"福井県","ふくいけん"},
            {"山梨県","やまなしけん"},{"長野県","ながのけん"},{"岐阜県","ぎふけん"},
            {"静岡県","しずおかけん"},{"愛知県","あいちけん"},{"三重県","みえけん"},{"滋賀県","しがけん"},
            {"京都府","きょうとふ"},{"大阪府","おおさかふ"},{"兵庫県","ひょうごけん"},
            {"奈良県","ならけん"},{"和歌山県","わかやまけん"},{"鳥取県","とっとりけん"},
            {"島根県","しまねけん"},{"岡山県","おかやまけん"},{"広島県","ひろしまけん"},
            {"山口県","やまぐちけん"},{"徳島県","とくしまけん"},{"香川県","かがわけん"},
            {"愛媛県","えひめけん"},{"高知県","こうちけん"},{"福岡県","ふくおかけん"},
            {"佐賀県","さがけん"},{"長崎県","ながさきけん"},{"熊本県","くまもとけん"},
            {"大分県","おおいたけん"},{"宮崎県","みやざきけん"},{"鹿児島県","かごしまけん"},
            {"沖縄県","おきなわけん"}
        };

        public MainWindow() {
            InitializeComponent();
            PrefectureList.ItemsSource = PrefectureCoords.Keys.ToList();
            SearchBox.Text = "都道府県を入力してください";
            SearchBox.Foreground = Brushes.Gray;
        }

        // 🔎 検索ボックスのテキスト変更イベント
        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            if (PrefectureList == null) return;
            string keyword = SearchBox.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword) || keyword == "都道府県を入力してください") {
                PrefectureList.ItemsSource = PrefectureCoords.Keys.ToList();
            } else {
                PrefectureList.ItemsSource = PrefectureCoords.Keys
                    .Where(p => p.Contains(keyword) || PrefectureReadings[p].Contains(keyword))
                    .ToList();
            }
        }

        // 都道府県選択時の処理
        private async void PrefectureList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            if (PrefectureList.SelectedItem == null) return;
            string prefecture = PrefectureList.SelectedItem.ToString();
            var (lat, lon) = PrefectureCoords[prefecture];

            try {
                string weatherUrl =
                    $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&hourly=temperature_2m,precipitation,wind_speed_10m,winddirection_10m,weathercode,relativehumidity_2m&timezone=auto";

                var weatherResponse = await _httpClient.GetStringAsync(weatherUrl);
                using var weatherDoc = JsonDocument.Parse(weatherResponse);

                var hourly = weatherDoc.RootElement.GetProperty("hourly");
                var times = hourly.GetProperty("time");
                var temps = hourly.GetProperty("temperature_2m");
                var precs = hourly.GetProperty("precipitation");
                var winds = hourly.GetProperty("wind_speed_10m");
                var dirs = hourly.GetProperty("winddirection_10m");
                var codes = hourly.GetProperty("weathercode");
                var hums = hourly.GetProperty("relativehumidity_2m");

                var hourlyList = new List<WeatherData>();
                for (int i = 0; i < times.GetArrayLength(); i++) {
                    hourlyList.Add(new WeatherData {
                        Time = DateTime.Parse(times[i].GetString()),
                        Temperature = temps[i].GetDouble(),
                        Precipitation = precs[i].GetDouble(),
                        WindSpeed = winds[i].GetDouble(),
                        WindDirection = dirs[i].GetDouble(),
                        WeatherCode = codes[i].GetInt32(),
                        Humidity = hums[i].GetDouble()
                    });
                }

                TempGraph.ItemsSource = hourlyList;

                if (hourlyList.Count > 0) {
                    var now = DateTime.Now;
                    var current = hourlyList.OrderBy(h => Math.Abs((h.Time - now).TotalMinutes)).First();
                    DataContext = new CurrentWeatherView {
                        CurrentWeatherIcon = current.WeatherIcon,
                        CurrentTemperature = $"{current.Temperature:F1} ℃",
                        CurrentCondition = current.ConditionText,
                        CurrentHumidity = $"湿度 {current.Humidity:F0} %",
                        CurrentWind = $"風速 {current.WindSpeed:F1} m/s"
                    };
                    HumidityText.Text = $"{current.Humidity:F0}%";
                    WindSpeedText.Text = $"風速 {current.WindSpeed:F1} m/s";
                    WindDirectionText.Text = $"風向 {current.WindDirection:F0}°";

                    // 背景色切り替え
                    MainGrid.Background = current.WeatherCode switch {
                        0 => new LinearGradientBrush(Colors.SkyBlue, Colors.LightYellow, 90),
                        1 or 2 or 3 => new LinearGradientBrush(Colors.LightGray, Colors.WhiteSmoke, 90),
                        61 or 63 or 65 => new LinearGradientBrush(Colors.Gray, Colors.LightBlue, 90),
                        71 or 73 or 75 => new LinearGradientBrush(Colors.LightGray, Colors.White, 90),
                        95 or 96 or 99 => new LinearGradientBrush(Colors.DarkSlateGray, Colors.LightGray, 90),
                        _ => new LinearGradientBrush(Colors.LightSlateGray, Colors.WhiteSmoke, 90)
                    };

                    // アニメーション背景切り替え
                    AnimationCanvas.Children.Clear();
                    switch (current.WeatherCode) {
                        case 0: AddSunAnimation(); break;
                        case 1:
                        case 2:
                        case 3: AddCloudAnimation(); break;
                        case 61:
                        case 63:
                        case 65: AddRainAnimation(); break;
                        case 71:
                        case 73:
                        case 75: AddSnowAnimation(); break;
                        case 95:
                        case 96:
                        case 99: AddThunderAnimation(); break;
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"エラー: {ex.Message}");
            }
        }

        // ☀️ 太陽アニメーション
        private void AddSunAnimation() {
            // 太陽コンテナ
            var sunGroup = new Canvas();

            // 太陽本体
            var sunBrush = new RadialGradientBrush();
            sunBrush.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 220), 0.0));
            sunBrush.GradientStops.Add(new GradientStop(Color.FromRgb(255, 230, 100), 0.5));
            sunBrush.GradientStops.Add(new GradientStop(Color.FromRgb(255, 180, 0), 1.0));

            var sun = new Ellipse {
                Width = 100,
                Height = 100,
                Fill = sunBrush
            };
            Canvas.SetLeft(sun, 0);
            Canvas.SetTop(sun, 0);
            sunGroup.Children.Add(sun);

            // 光線
            for (int i = 0; i < 12; i++) {
                var ray = new Line {
                    X1 = 40,
                    Y1 = 40,
                    X2 = 70,
                    Y2 = 40,
                    Stroke = Brushes.Gold,
                    StrokeThickness = 3
                };
                ray.RenderTransform = new RotateTransform(i * 30, 40, 40);
                sunGroup.Children.Add(ray);
            }

            // コンテナをキャンバスに配置
            Canvas.SetLeft(sunGroup, 100);
            Canvas.SetTop(sunGroup, 50);
            AnimationCanvas.Children.Add(sunGroup);

            // コンテナ全体を上下に動かす
            var moveAnim = new DoubleAnimation {
                From = 50,
                To = 150,
                Duration = TimeSpan.FromSeconds(10),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            sunGroup.BeginAnimation(Canvas.TopProperty, moveAnim);

            // 光線の回転アニメーション（輝き）
            var rotate = new RotateTransform(0, 40, 40);
            sunGroup.RenderTransform = rotate;

            var rotateAnim = new DoubleAnimation {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(20),
                RepeatBehavior = RepeatBehavior.Forever
            };
            rotate.BeginAnimation(RotateTransform.AngleProperty, rotateAnim);
        }

        // ☁️ 曇りアニメーション
        private void AddCloudAnimation() {
            var cloudGeometry = new GeometryGroup();
            var rand = new Random();

            // 中心の大きな楕円
            cloudGeometry.Children.Add(new EllipseGeometry(new Point(120, 90), 70, 45));

            // 周囲に小さめの楕円をランダム配置
            for (int i = 0; i < 6; i++) {
                double x = 120 + rand.Next(-50, 50);
                double y = 90 + rand.Next(-25, 25);
                double w = rand.Next(30, 50);
                double h = rand.Next(20, 35);

                cloudGeometry.Children.Add(new EllipseGeometry(new Point(x, y), w, h));
            }

            var cloudBrush = new RadialGradientBrush {
                GradientOrigin = new Point(0.3, 0.3),
                Center = new Point(0.5, 0.5),
                RadiusX = 0.8,
                RadiusY = 0.8
            };
            cloudBrush.GradientStops.Add(new GradientStop(Colors.WhiteSmoke, 0.0));
            cloudBrush.GradientStops.Add(new GradientStop(Colors.LightGray, 0.6));
            cloudBrush.GradientStops.Add(new GradientStop(Colors.Gray, 1.0));

            var cloudPath = new Path {
                Fill = cloudBrush,
                StrokeThickness = 0,
                Data = cloudGeometry,
                Effect = new BlurEffect { Radius = 6 }
            };

            AnimationCanvas.Children.Add(cloudPath);

            // 横移動
            var moveAnim = new DoubleAnimation {
                From = -200,
                To = AnimationCanvas.ActualWidth + 200,
                Duration = TimeSpan.FromSeconds(40),
                RepeatBehavior = RepeatBehavior.Forever
            };
            cloudPath.BeginAnimation(Canvas.LeftProperty, moveAnim);

            // 濃淡変化
            var opacityAnim = new DoubleAnimation {
                From = 0.8,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(10),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            cloudPath.BeginAnimation(UIElement.OpacityProperty, opacityAnim);

            // 縦方向の揺れ
            var topAnim = new DoubleAnimation {
                From = 80,
                To = 90,
                Duration = TimeSpan.FromSeconds(6),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            cloudPath.BeginAnimation(Canvas.TopProperty, topAnim);
        }


        // 🌧️ 雨アニメーション（斜め＆ランダム）
        private void AddRainAnimation() {
            var rand = new Random();
            for (int i = 0; i < 30; i++) {
                double x = rand.Next(0, 400);
                var drop = new Line {
                    X1 = x,
                    Y1 = 0,
                    X2 = x + rand.Next(-3, 3),
                    Y2 = rand.Next(10, 20),
                    Stroke = Brushes.LightBlue,
                    StrokeThickness = 1.5,
                    Effect = new BlurEffect { Radius = 2 }
                };
                AnimationCanvas.Children.Add(drop);

                var anim = new DoubleAnimation {
                    From = 0,
                    To = 400,
                    Duration = TimeSpan.FromSeconds(rand.NextDouble() * 1.5 + 1.0),
                    RepeatBehavior = RepeatBehavior.Forever
                };
                drop.BeginAnimation(Canvas.TopProperty, anim);
            }
        }

        // ❄️ 雪アニメーション（サイズ・横揺れ・透明度ランダム）
        private void AddSnowAnimation() {
            var rand = new Random();
            for (int i = 0; i < 20; i++) {
                var snow = new Ellipse {
                    Width = rand.Next(5, 12),
                    Height = rand.Next(5, 12),
                    Fill = Brushes.White,
                    Opacity = rand.NextDouble() * 0.8 + 0.2
                };
                Canvas.SetLeft(snow, rand.Next(0, 400));
                Canvas.SetTop(snow, 0);
                AnimationCanvas.Children.Add(snow);

                var fallAnim = new DoubleAnimation {
                    From = 0,
                    To = 400,
                    Duration = TimeSpan.FromSeconds(rand.Next(5, 10)),
                    RepeatBehavior = RepeatBehavior.Forever
                };
                snow.BeginAnimation(Canvas.TopProperty, fallAnim);

                var swayAnim = new DoubleAnimation {
                    From = Canvas.GetLeft(snow),
                    To = Canvas.GetLeft(snow) + rand.Next(-20, 20),
                    Duration = TimeSpan.FromSeconds(rand.Next(3, 6)),
                    AutoReverse = true,
                    RepeatBehavior = RepeatBehavior.Forever
                };
                snow.BeginAnimation(Canvas.LeftProperty, swayAnim);
            }
        }

        // ⚡ 雷アニメーション（発光・不規則点滅）
        private void AddThunderAnimation() {
            var lightning = new Polygon {
                Fill = Brushes.Yellow,
                Points = new PointCollection {
            new Point(10,0), new Point(30,40), new Point(20,40),
            new Point(40,80), new Point(0,40), new Point(20,40)
        },
                Effect = new DropShadowEffect {
                    Color = Colors.White,
                    BlurRadius = 20,
                    ShadowDepth = 0,
                    Opacity = 0.8
                }
            };
            Canvas.SetLeft(lightning, 100);
            Canvas.SetTop(lightning, 50);
            AnimationCanvas.Children.Add(lightning);

            var blink = new DoubleAnimation {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.3 + new Random().NextDouble()),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            lightning.BeginAnimation(UIElement.OpacityProperty, blink);
        }


        // 🔎 検索ボックスのプレースホルダー制御
        private void SearchBox_GotFocus(object sender, RoutedEventArgs e) {
            if (SearchBox.Text == "都道府県を入力してください") {
                SearchBox.Text = "";
                SearchBox.Foreground = Brushes.Black;
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e) {
            if (string.IsNullOrWhiteSpace(SearchBox.Text)) {
                SearchBox.Text = "都道府県を入力してください";
                SearchBox.Foreground = Brushes.Gray;
            }
        }

        private void ForecastDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {

        }
    }

    // 天気データクラス
    public class WeatherData {
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
        public double Precipitation { get; set; }
        public double WindSpeed { get; set; }
        public double WindDirection { get; set; }
        public int WeatherCode { get; set; }
        public double Humidity { get; set; }

        public string DisplayTime => $"{Time:MM月dd日 HH時}";
        public string TemperatureDisplay => $"{Temperature:F1} ℃";
        public string PrecipitationDisplay => $"{Precipitation:F1} mm";
        public string WindSpeedDisplay => $"{WindSpeed:F1} m/s";

        public string WeatherIcon => WeatherCode switch {
            0 => "☀️",
            1 or 2 or 3 => "☁️",
            45 or 48 => "🌫️",
            51 or 53 or 55 => "🌦️",
            61 or 63 or 65 => "🌧️",
            71 or 73 or 75 => "❄️",
            95 or 96 or 99 => "⛈️",
            _ => "❓"
        };

        public string ConditionText => WeatherCode switch {
            0 => "晴れ",
            1 or 2 or 3 => "曇り",
            45 or 48 => "霧",
            51 or 53 or 55 => "弱い雨",
            61 or 63 or 65 => "雨",
            71 or 73 or 75 => "雪",
            95 or 96 or 99 => "雷雨",
            _ => "不明"
        };
    }

    // 現在の天気表示用クラス
    public class CurrentWeatherView {
        public string CurrentWeatherIcon { get; set; }
        public string CurrentTemperature { get; set; }
        public string CurrentCondition { get; set; }
        public string CurrentHumidity { get; set; }
        public string CurrentWind { get; set; }
    }
}
