namespace RssReader {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            btRssGet = new Button();
            lbTitles = new ListBox();
            wvRssLink = new Microsoft.Web.WebView2.WinForms.WebView2();
            btGoForward = new Button();
            btGoBack = new Button();
            cbUrl = new ComboBox();
            cbFavorite = new ComboBox();
            label1 = new Label();
            button1 = new Button();
            btDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)wvRssLink).BeginInit();
            SuspendLayout();
            // 
            // btRssGet
            // 
            btRssGet.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btRssGet.Location = new Point(657, 11);
            btRssGet.Name = "btRssGet";
            btRssGet.Size = new Size(77, 33);
            btRssGet.TabIndex = 1;
            btRssGet.Text = "取得";
            btRssGet.UseVisualStyleBackColor = true;
            btRssGet.Click += btRssGet_Click;
            // 
            // lbTitles
            // 
            lbTitles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lbTitles.DrawMode = DrawMode.OwnerDrawFixed;
            lbTitles.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lbTitles.FormattingEnabled = true;
            lbTitles.ItemHeight = 21;
            lbTitles.Location = new Point(12, 105);
            lbTitles.Name = "lbTitles";
            lbTitles.Size = new Size(450, 592);
            lbTitles.TabIndex = 2;
            lbTitles.Click += lbTitles_Click;
            lbTitles.DrawItem += lbTitles_DrawItem;
            lbTitles.SelectedIndexChanged += lbTitles_Click;
            // 
            // wvRssLink
            // 
            wvRssLink.AllowExternalDrop = true;
            wvRssLink.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            wvRssLink.CreationProperties = null;
            wvRssLink.DefaultBackgroundColor = Color.White;
            wvRssLink.Location = new Point(491, 105);
            wvRssLink.Name = "wvRssLink";
            wvRssLink.Size = new Size(612, 589);
            wvRssLink.TabIndex = 3;
            wvRssLink.ZoomFactor = 1D;
            wvRssLink.SourceChanged += wvRssLink_SourceChanged;
            // 
            // btGoForward
            // 
            btGoForward.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btGoForward.Location = new Point(97, 11);
            btGoForward.Name = "btGoForward";
            btGoForward.Size = new Size(75, 33);
            btGoForward.TabIndex = 4;
            btGoForward.Text = "進む";
            btGoForward.UseVisualStyleBackColor = true;
            btGoForward.Click += btGoForward_Click;
            // 
            // btGoBack
            // 
            btGoBack.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btGoBack.Location = new Point(16, 11);
            btGoBack.Name = "btGoBack";
            btGoBack.Size = new Size(75, 33);
            btGoBack.TabIndex = 5;
            btGoBack.Text = "戻る";
            btGoBack.UseVisualStyleBackColor = true;
            btGoBack.Click += btGoBack_Click;
            // 
            // cbUrl
            // 
            cbUrl.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            cbUrl.FormattingEnabled = true;
            cbUrl.Location = new Point(178, 11);
            cbUrl.Name = "cbUrl";
            cbUrl.Size = new Size(473, 33);
            cbUrl.TabIndex = 6;
            // 
            // cbFavorite
            // 
            cbFavorite.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            cbFavorite.FormattingEnabled = true;
            cbFavorite.Location = new Point(178, 52);
            cbFavorite.Name = "cbFavorite";
            cbFavorite.Size = new Size(473, 33);
            cbFavorite.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.Location = new Point(27, 60);
            label1.Name = "label1";
            label1.Size = new Size(131, 25);
            label1.TabIndex = 7;
            label1.Text = "お気に入り登録";
            // 
            // button1
            // 
            button1.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            button1.Location = new Point(657, 52);
            button1.Name = "button1";
            button1.Size = new Size(77, 33);
            button1.TabIndex = 8;
            button1.Text = "登録";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btGet;
            // 
            // btDelete
            // 
            btDelete.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btDelete.Location = new Point(740, 52);
            btDelete.Name = "btDelete";
            btDelete.Size = new Size(75, 33);
            btDelete.TabIndex = 9;
            btDelete.Text = "削除";
            btDelete.UseVisualStyleBackColor = true;
            btDelete.Click += btDelete_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1115, 709);
            Controls.Add(btDelete);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(cbFavorite);
            Controls.Add(cbUrl);
            Controls.Add(btGoBack);
            Controls.Add(btGoForward);
            Controls.Add(wvRssLink);
            Controls.Add(lbTitles);
            Controls.Add(btRssGet);
            Name = "Form1";
            Text = "RSSリーダー";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)wvRssLink).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btRssGet;
        private ListBox lbTitles;
        private Microsoft.Web.WebView2.WinForms.WebView2 wvRssLink;
        private Button btGoForward;
        private Button btGoBack;
        private ComboBox cbUrl;
        private ComboBox cbFavorite;
        private Label label1;
        private Button button1;
        private Button btDelete;
    }
}
