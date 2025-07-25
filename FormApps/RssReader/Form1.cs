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
            {"��v", "https://news.yahoo.co.jp/rss/topics/top-picks.xml"},
            {"����", "https://news.yahoo.co.jp/rss/topics/domestic.xml"},
            {"����", "https://news.yahoo.co.jp/rss/topics/world.xml"},
            {"�o��", "https://news.yahoo.co.jp/rss/topics/business.xml"},
            {"�G���^��", "https://news.yahoo.co.jp/rss/topics/entertainment.xml"},
            {"�X�|�[�c", "https://news.yahoo.co.jp/rss/topics/sports.xml"},
            {"IT", "https://news.yahoo.co.jp/rss/topics/it.xml"},
            {"�Ȋw", "https://news.yahoo.co.jp/rss/topics/science.xml"},
            {"�n��", "https://news.yahoo.co.jp/rss/topics/local.xml"},
        };

        public Form1() {
            InitializeComponent();
        }

        private async void btRssGet_Click(object sender, EventArgs e) {

            using (var hc = new HttpClient()) {



                string xml = await hc.GetStringAsync(GetRssUrl(cbUrl.Text));
                XDocument xdoc = XDocument.Parse(xml);

                //var url = hc.OpenRead(cbUrl.Text);
                //XDocument xdoc = XDocument.Load(url);//RSS�̎擾

                //RSS����͂��ĕK�v�ȗv�f���擾
                items = xdoc.Root.Descendants("item")
                    .Select(x =>
                        new ItemData {
                            Title = (string?)x.Element("title"),
                            Link = (string?)x.Element("link"),
                        }).ToList();

                //���X�g�{�b�N�X�փ^�C�g����\��
                lbTitles.Items.Clear();
                items.ForEach(item => lbTitles.Items.Add(item.Title ?? "�f�[�^�Ȃ�"));
            }
        }

        //�R���{�{�b�N�X�̕�������`�F�b�N���ăA�N�Z�X�\��URL��ԋp����
        private string GetRssUrl(string str) {
            if (rssUrlDict.ContainsKey(str)) {
                return rssUrlDict[str];//str��Key��������Value���Ԃ����B�t������
            }

            return str;
        }

        //�^�C�g����I���i�N���b�N)�����Ƃ��ɌĂ΂��C�x���g�n���h��
        private void lbTitles_Click(object sender, EventArgs e) {
            if (lbTitles.SelectedIndex < 0) {
                MessageBox.Show("�^�C�g��������܂���");
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
                MessageBox.Show("URL�ł͂���܂���");
                return;
            }

            rssUrlDict.Add(cbFavorite.Text, cbUrl.Text);
            cbUrl.Items.Add(cbFavorite.Text);
            cbFavorite.Text = string.Empty;
            MessageBox.Show("���C�ɓ���o�^����");
        }

        private void btDelete_Click(object sender, EventArgs e) {
            cbUrl.Items.Remove(cbUrl.Text);
            cbUrl.Text = string.Empty;
            cbFavorite.Text = string.Empty;
            rssUrlDict.Remove(cbUrl.Text);
            lbTitles.Items.Clear();
            wvRssLink.Source = new Uri("about:blank");
            MessageBox.Show("�폜����");
        }

        //�菇
        //�@���݂ɐF��ύX���������X�g�{�b�N�X��DrawMode�v���p�e�B���AOwnerDrawFixed�ɕύX
        //�A�C�x���g����uDrawItem�v���_�u���N���b�N
        //�B�ȉ��̃C�x���g�n���h���������������ꂽ�璆�̏������R�s�y
        private void lbTitles_DrawItem(object sender, DrawItemEventArgs e) {
            var idx = e.Index;                                                      //�`��Ώۂ̍s
            if (idx == -1) return;                                                  //�͈͊O�Ȃ牽�����Ȃ�
            var sts = e.State;                                                      //�Z���̏��
            var fnt = e.Font;                                                       //�t�H���g
            var _bnd = e.Bounds;                                                    //�`��͈�(�I���W�i��)
            var bnd = new RectangleF(_bnd.X, _bnd.Y, _bnd.Width, _bnd.Height);     //�`��͈�(�`��p)
            var txt = (string)lbTitles.Items[idx];                                  //���X�g�{�b�N�X���̕���
            var bsh = new SolidBrush(lbTitles.ForeColor);                           //�����F
            var sel = (DrawItemState.Selected == (sts & DrawItemState.Selected));   //�I���s��
            var odd = (idx % 2 == 1);                                               //��s��
            var fore = Brushes.WhiteSmoke;                                         //�����s�̔w�i�F
            var bak = Brushes.AliceBlue;                                           //��s�̔w�i�F

            e.DrawBackground();                                                     //�w�i�`��

            //����ڂ̔w�i�F��ς���i�I���s�͏����j
            if (odd && !sel) {
                e.Graphics.FillRectangle(bak, bnd);
            } else if (!odd && !sel) {
                e.Graphics.FillRectangle(fore, bnd);
            }

            //������`��
            e.Graphics.DrawString(txt, fnt, bsh, bnd);
        }
    }
}
