using System;
using System.Net;
using System.Security.Policy;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        Dictionary<string, string> rssUrlDict = new Dictionary<string, string>() {
            {"主要", "https://news.yahoo.co.jp/rss/topics/top-picks.xml"},
            {"国内", "https://news.yahoo.co.jp/rss/topics/domestic.xml"},
            {"国際", "https://news.yahoo.co.jp/rss/topics/world.xml"},
            {"経済", "https://news.yahoo.co.jp/rss/topics/business.xml"},
            {"エンタメ", "https://news.yahoo.co.jp/rss/topics/entertainment.xml"},
            {"スポーツ", "https://news.yahoo.co.jp/rss/topics/sports.xml"},
            {"IT", "https://news.yahoo.co.jp/rss/topics/it.xml"},
            {"科学", "https://news.yahoo.co.jp/rss/topics/science.xml"},
            {"地域", "https://news.yahoo.co.jp/rss/topics/local.xml"},
        };

        public Form1() {
            InitializeComponent();
        }

        private async void btRssGet_Click(object sender, EventArgs e) {

            using (var hc = new HttpClient()) {



                string xml = await hc.GetStringAsync(GetRssUrl(cbUrl.Text));
                XDocument xdoc = XDocument.Parse(xml);

                //var url = hc.OpenRead(cbUrl.Text);
                //XDocument xdoc = XDocument.Load(url);//RSSの取得

                //RSSを解析して必要な要素を取得
                items = xdoc.Root.Descendants("item")
                    .Select(x =>
                        new ItemData {
                            Title = (string?)x.Element("title"),
                            Link = (string?)x.Element("link"),
                        }).ToList();

                //リストボックスへタイトルを表示
                lbTitles.Items.Clear();
                items.ForEach(item => lbTitles.Items.Add(item.Title ?? "データなし"));
            }
        }

        //コンボボックスの文字列をチェックしてアクセス可能なURLを返却する
        private string GetRssUrl(string str) {
            if (rssUrlDict.ContainsKey(str)) {
                return rssUrlDict[str];//strがKeyだったらValueが返される。逆もある
            }

            return str;
        }

        //タイトルを選択（クリック)したときに呼ばれるイベントハンドラ
        private void lbTitles_Click(object sender, EventArgs e) {
            if (lbTitles.SelectedIndex < 0) {
                MessageBox.Show("タイトルがありません");
                return;
            }
            wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link ?? "https://www.yahoo.co.jp/");
        }


        private void btGoForward_Click(object sender, EventArgs e) {
            wvRssLink.GoForward();
            GoForwardBtEnableSet();

        }

        private void btGoBack_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();
            GoForwardBtEnableSet();
        }

        private void wvRssLink_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e) {
            GoForwardBtEnableSet();
        }

        private void Form1_Load(object sender, EventArgs e) {
            foreach (var key in rssUrlDict.Keys) {
                cbUrl.Items.Add(key);
            }
            BackColor = Color.AliceBlue;

            GoForwardBtEnableSet();
        }

        private void GoForwardBtEnableSet() {
            btGoBack.Enabled = wvRssLink.CanGoBack;
            btGoForward.Enabled = wvRssLink.CanGoForward;
        }

        private void btGet(object sender, EventArgs e) {
            if (!Uri.IsWellFormedUriString(cbUrl.Text, UriKind.Absolute)) {
                MessageBox.Show("URLではありません");
                return;
            }

            rssUrlDict.Add(cbFavorite.Text, cbUrl.Text);
            cbUrl.Items.Add(cbFavorite.Text);
            cbFavorite.Text = string.Empty;
            MessageBox.Show("お気に入り登録完了");
        }

        private void btDelete_Click(object sender, EventArgs e) {
            cbUrl.Items.Remove(cbUrl.Text);
            cbUrl.Text = string.Empty;
            cbFavorite.Text = string.Empty;
            rssUrlDict.Remove(cbUrl.Text);
            lbTitles.Items.Clear();
            wvRssLink.Source = new Uri("about:blank");
            MessageBox.Show("削除完了");
        }

        //手順
        //①交互に色を変更したいリストボックスのDrawModeプロパティを、OwnerDrawFixedに変更
        //②イベントから「DrawItem」をダブルクリック
        //③以下のイベントハンドラが自動生成されたら中の処理をコピペ
        private void lbTitles_DrawItem(object sender, DrawItemEventArgs e) {
            var idx = e.Index;                                                      //描画対象の行
            if (idx == -1) return;                                                  //範囲外なら何もしない
            var sts = e.State;                                                      //セルの状態
            var fnt = e.Font;                                                       //フォント
            var _bnd = e.Bounds;                                                    //描画範囲(オリジナル)
            var bnd = new RectangleF(_bnd.X, _bnd.Y, _bnd.Width, _bnd.Height);     //描画範囲(描画用)
            var txt = (string)lbTitles.Items[idx];                                  //リストボックス内の文字
            var bsh = new SolidBrush(lbTitles.ForeColor);                           //文字色
            var sel = (DrawItemState.Selected == (sts & DrawItemState.Selected));   //選択行か
            var odd = (idx % 2 == 1);                                               //奇数行か
            var fore = Brushes.WhiteSmoke;                                         //偶数行の背景色
            var bak = Brushes.AliceBlue;                                           //奇数行の背景色

            e.DrawBackground();                                                     //背景描画

            //奇数項目の背景色を変える（選択行は除く）
            if (odd && !sel) {
                e.Graphics.FillRectangle(bak, bnd);
            } else if (!odd && !sel) {
                e.Graphics.FillRectangle(fore, bnd);
            }

            //文字を描画
            e.Graphics.DrawString(txt, fnt, bsh, bnd);
        }
    }
}
