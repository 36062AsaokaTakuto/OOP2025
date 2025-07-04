using System.ComponentModel;
using static CarReportSystem.CarReport;

namespace CarReportSystem {
    public partial class Form1 : Form {
        //カーレポート管理用リスト
        BindingList<CarReport> listCarReports = new BindingList<CarReport>();

        public Form1() {
            InitializeComponent();
            dgvRecord.DataSource = listCarReports;
        }

        private void btPicOpen_Click(object sender, EventArgs e) {
            if (ofdPicFileOpen.ShowDialog() == DialogResult.OK) {
                pbPicture.Image = Image.FromFile(ofdPicFileOpen.FileName);
            }
        }

        private void btPicDelete_Click(object sender, EventArgs e) {
            pbPicture.Image = null;
        }

        //記録者の履歴をコンボボックスへ登録（重複なし）
        private void setCbAuthor(string author) {
            //既に登録済みか確認
            if (cbAuthor.Items.Contains(author)) {

            } else {
                //未登録なら登録【登録済みなら何もしない】
                cbAuthor.Items.Add(author);
            }
                 
        }

        //記録者の履歴をコンボボックスへ登録（重複なし）
        private void setCbCarName(string CarName) {
            if (cbCarName.Items.Contains(CarName)) {

            } else {
                //未登録なら登録【登録済みなら何もしない】
                cbCarName.Items.Add(CarName);
            }
        }

        private void btRecordAdd_Click(object sender, EventArgs e) {
            var carReport = new CarReport {
                Author = cbAuthor.Text,
                CarName = cbCarName.Text,
                Date = dtpDate.Value.Date,
                Report = tbReport.Text,
                Picture = pbPicture.Image,
                Maker = GetRadioButtonMaker()
            };
            listCarReports.Add(carReport);
            setCbAuthor(carReport.Author);
            setCbCarName(carReport.CarName);
            InputItemsAllClear();//登録後は項目をクリア
        }

        //入力項目をすべてクリア
        private void InputItemsAllClear() {
            cbAuthor.Text = string.Empty;
            cbCarName.Text = string.Empty;
            dtpDate.Value = DateTime.Today;
            tbReport.Text = string.Empty;
            pbPicture.Image = null;
            rbOther.Checked = true;
        }

        private CarReport.MakerGroup GetRadioButtonMaker() {
            if (rbToyota.Checked) {
                return MakerGroup.トヨタ;
            } else if (rbNissan.Checked) {
                return MakerGroup.日産;
            } else if (rbSubaru.Checked) {
                return MakerGroup.スバル;
            } else if (rbDaihatsu.Checked) {
                return MakerGroup.ダイハツ;
            } else if (rbImport.Checked) {
                return MakerGroup.輸入車;
            }
            return MakerGroup.その他;
        }

        private void dgvRecord_Click(object sender, EventArgs e) {
            if (dgvRecord.CurrentRow is null) {
                return;
            } else {
                dtpDate.Value = (DateTime)dgvRecord.CurrentRow.Cells["Date"].Value;
                cbAuthor.Text = (string)dgvRecord.CurrentRow.Cells["Author"].Value;
                cbCarName.Text = (string)dgvRecord.CurrentRow.Cells["CarName"].Value;
                tbReport.Text = (string)dgvRecord.CurrentRow.Cells["Report"].Value;
                pbPicture.Image = (Image)dgvRecord.CurrentRow.Cells["Picture"].Value;
                setRadioButtonMaker((MakerGroup)dgvRecord.CurrentRow.Cells["Maker"].Value);
            }

        }

        //指定したメーカーのラジオボタンをセット
        private void setRadioButtonMaker(MakerGroup targetMaker) {
            switch (targetMaker) {
                case MakerGroup.トヨタ:
                    rbToyota.Checked = true;
                    break;
                case MakerGroup.日産:
                    rbNissan.Checked = true;
                    break;
                case MakerGroup.スバル:
                    rbSubaru.Checked = true;
                    break;
                case MakerGroup.ダイハツ:
                    rbDaihatsu.Checked = true;
                    break;
                case MakerGroup.輸入車:
                    rbImport.Checked = true;
                    break;
                default:
                    rbOther.Checked = true;
                    break;
            }
        }

        private void btNewRecord_Click(object sender, EventArgs e) {
            InputItemsAllClear();//登録後は項目をクリア
        }
    }
}
