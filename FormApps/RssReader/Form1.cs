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

        }

        private void btGoBack_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();

        }

        private void wvRssLink_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e) {
            GoForwardBtEnableSet();
        }

        private void Form1_Load(object sender, EventArgs e) {
            cbUrl.DataSource = rssUrlDict.Select(k => k.Key).ToList();
            GoForwardBtEnableSet();
        }

        private void GoForwardBtEnableSet() {
            btGoBack.Enabled = wvRssLink.CanGoBack;
            btGoForward.Enabled = wvRssLink.CanGoForward;
        }

        private void btGet(object sender, EventArgs e) {

        }
    }
}
