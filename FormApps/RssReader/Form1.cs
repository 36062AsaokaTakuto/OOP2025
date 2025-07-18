using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        public Form1() {
            InitializeComponent();
        }

        private async void btRssGet_Click(object sender, EventArgs e) {

            using (var hc = new HttpClient()) {

                string xml = await hc.GetStringAsync(tbUrl.Text);
                XDocument xdoc = XDocument.Parse(xml);

                //var url = hc.OpenRead(tbUrl.Text);
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

        //�^�C�g����I���i�N���b�N)�����Ƃ��ɌĂ΂��C�x���g�n���h��
        private void lbTitles_Click(object sender, EventArgs e) {
            if (lbTitles.SelectedIndex < 0) {
                MessageBox.Show("�^�C�g��������܂���");
                return;
            }
            wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link ?? "https://www.yahoo.co.jp/");
        }


        private void btGoFoward_Click(object sender, EventArgs e) {
            if (lbTitles.SelectedIndex < 0 || lbTitles.SelectedIndex + 1 >= items.Count) {
                btGoFoward.Enabled = false;
                return;
            }
            wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex + 1].Link ?? "https://www.yahoo.co.jp/");
            lbTitles.SelectedIndex++;

        }

        private void btGoBack_Click(object sender, EventArgs e) {
            if (lbTitles.SelectedIndex < 0 || lbTitles.SelectedIndex - 1 < 0) {
                btGoBack.Enabled = false;
                return;
            }
            wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex - 1].Link ?? "https://www.yahoo.co.jp/");
            lbTitles.SelectedIndex--;

        }

        private void wvRssLink_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e) {
            btGoFoward.Enabled = true;
            btGoBack.Enabled = true;
        }
    }
}
