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
            tbUrl = new TextBox();
            btRssGet = new Button();
            lbTitles = new ListBox();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            btMove = new Button();
            btReturn = new Button();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            SuspendLayout();
            // 
            // tbUrl
            // 
            tbUrl.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            tbUrl.Location = new Point(178, 11);
            tbUrl.Name = "tbUrl";
            tbUrl.Size = new Size(473, 33);
            tbUrl.TabIndex = 0;
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
            lbTitles.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lbTitles.FormattingEnabled = true;
            lbTitles.ItemHeight = 21;
            lbTitles.Location = new Point(12, 51);
            lbTitles.Name = "lbTitles";
            lbTitles.Size = new Size(450, 592);
            lbTitles.TabIndex = 2;
            lbTitles.Click += lbTitles_Click;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(491, 51);
            webView21.Name = "webView21";
            webView21.Size = new Size(612, 592);
            webView21.TabIndex = 3;
            webView21.ZoomFactor = 1D;
            // 
            // btMove
            // 
            btMove.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btMove.Location = new Point(97, 11);
            btMove.Name = "btMove";
            btMove.Size = new Size(75, 33);
            btMove.TabIndex = 4;
            btMove.Text = "進む";
            btMove.UseVisualStyleBackColor = true;
            btMove.Click += btMove_Click;
            // 
            // btReturn
            // 
            btReturn.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btReturn.Location = new Point(16, 11);
            btReturn.Name = "btReturn";
            btReturn.Size = new Size(75, 33);
            btReturn.TabIndex = 5;
            btReturn.Text = "戻る";
            btReturn.UseVisualStyleBackColor = true;
            btReturn.Click += btReturn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1115, 658);
            Controls.Add(btReturn);
            Controls.Add(btMove);
            Controls.Add(webView21);
            Controls.Add(lbTitles);
            Controls.Add(btRssGet);
            Controls.Add(tbUrl);
            Name = "Form1";
            Text = "RSSリーダー";
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbUrl;
        private Button btRssGet;
        private ListBox lbTitles;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private Button btMove;
        private Button btReturn;
    }
}
