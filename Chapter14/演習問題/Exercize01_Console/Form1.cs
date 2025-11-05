using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercize01_Console {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            // TextBox の設定
            textBox1.Multiline = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Dock = DockStyle.Fill;

            // 起動時に読み込み処理を実行
            this.Load += async (s, e) => await LoadFileLineByLineAsync();
        }

        private async Task LoadFileLineByLineAsync() {
            using (OpenFileDialog dialog = new OpenFileDialog()) {
                dialog.Filter = "テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*";
                dialog.Title = "読み込むファイルを選択してください";

                if (dialog.ShowDialog() == DialogResult.OK) {
                    try {
                        var sb = new StringBuilder();

                        // Shift_JIS の場合はこちらを有効に
                        // Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        // var encoding = Encoding.GetEncoding("shift_jis");

                        using (var reader = new StreamReader(dialog.FileName, Encoding.UTF8)) {
                            string? line;
                            while ((line = await reader.ReadLineAsync()) != null) {
                                sb.AppendLine(line);
                            }
                        }

                        textBox1.Text = sb.ToString();
                    }
                    catch (Exception ex) {
                        MessageBox.Show("読み込みエラー: " + ex.Message);
                    }
                }
            }
        }
    }
}
