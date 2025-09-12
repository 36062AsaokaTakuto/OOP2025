using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorChecker{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window{
        MyColor currentColor;
        public List<MyColor> Colors { get; set; }
        public Color SelectedColor { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            colorSelectComboBox.ItemsSource = GetColorList();
        }

        /// <summary>
        /// すべての色を取得するメソッド
        /// </summary>
        /// <returns></returns>
        private MyColor[] GetColorList() {
            return typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Select(i => new MyColor() { Color = (Color)i.GetValue(null), Name = i.Name }).ToArray();
        }

        //すべてのスライダーから呼ばれるイベントハンドラ
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            //colorAreaの色（背景色）は、スライダーで指定したRGBの色を表示する
            colorArea.Background = new SolidColorBrush(Color.FromRgb((byte)rSlider.Value, (byte)gSlider.Value, (byte)bSlider.Value));
        }

        private void stockButton_Click(object sender, RoutedEventArgs e) {
            Color selectedColor = Color.FromRgb((byte)rSlider.Value, (byte)gSlider.Value, (byte)bSlider.Value);

            foreach (var item in stockList.Items) {
                if (item is MyColor colorList && colorList.Color.Equals(selectedColor)) {
                    MessageBox.Show("すでに登録されています");
                    return;
                }
            }

            MyColor? matchColor = null;

            foreach (MyColor match in colorSelectComboBox.Items) {
                if (match.Color.Equals(selectedColor)) {
                    matchColor = match;
                    break;
                }
            }

            if (matchColor != null) {
                currentColor = new MyColor {
                    Color = selectedColor,
                    Name = matchColor.Value.Name
                };
            } else {
                currentColor = new MyColor {
                    Color = selectedColor,
                    Name = $"R:{selectedColor.R} G:{selectedColor.G} B:{selectedColor.B}"
                };
            }

            stockList.Items.Insert(0,currentColor);

                
        }

        private void stockList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (stockList.SelectedItem is MyColor selectedColorInfo) {
                // ウィンドウの背景色を選択された色に変更
                colorArea.Background = new SolidColorBrush(selectedColorInfo.Color);
                colorSelectComboBox.SelectedValue = selectedColorInfo.Color;
                setSliderValue(selectedColorInfo.Color);
            }
        }

        private void colorSelectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var comboBox = (ComboBox)sender;

            if (comboBox.SelectedItem is MyColor comboSelectMycolor) {
                setSliderValue(comboSelectMycolor.Color);
            }
        }

        //各スライダーの値を設定する
        private void setSliderValue(Color color) {
            rSlider.Value = color.R;
            gSlider.Value = color.G;
            bSlider.Value = color.B;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            colorSelectComboBox.SelectedIndex = 7;
        }
    }
}
