namespace OpenCVSharpTest
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.PictureBox_Source = new System.Windows.Forms.PictureBox();
            this.openBtn = new System.Windows.Forms.Button();
            this.PictureBox_Target = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Source)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Target)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.Black;
            this.listBox1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.listBox1.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 19;
            this.listBox1.Items.AddRange(new object[] {
            "转化为灰度图",
            "反色",
            "图像二值化",
            "高斯滤波",
            "均值滤波",
            "中值滤波",
            "双边滤波",
            "边缘检测(sobel)",
            "边缘检测(scharr)",
            "边缘检测(canny)",
            "边缘检测(Laplacian)",
            "浮雕效果",
            "反转（左右）",
            "反转（上下）",
            "毛玻璃效果",
            "锐化",
            "膨胀",
            "腐蚀",
            "顶帽",
            "黑帽",
            "gamma暗部增强",
            "log亮部增强",
            "显示直方图",
            "轮廓绘制",
            "模板匹配",
            "绘制轮廓图"});
            this.listBox1.Location = new System.Drawing.Point(2, 1);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(160, 498);
            this.listBox1.TabIndex = 7;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // PictureBox_Source
            // 
            this.PictureBox_Source.Location = new System.Drawing.Point(168, 44);
            this.PictureBox_Source.Name = "PictureBox_Source";
            this.PictureBox_Source.Size = new System.Drawing.Size(524, 449);
            this.PictureBox_Source.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox_Source.TabIndex = 9;
            this.PictureBox_Source.TabStop = false;
            // 
            // openBtn
            // 
            this.openBtn.AutoSize = true;
            this.openBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.openBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.openBtn.Location = new System.Drawing.Point(168, 1);
            this.openBtn.Name = "openBtn";
            this.openBtn.Size = new System.Drawing.Size(94, 30);
            this.openBtn.TabIndex = 10;
            this.openBtn.Text = "打开图片";
            this.openBtn.UseVisualStyleBackColor = true;
            this.openBtn.Click += new System.EventHandler(this.openBtn_Click);
            // 
            // PictureBox_Target
            // 
            this.PictureBox_Target.Location = new System.Drawing.Point(700, 44);
            this.PictureBox_Target.Name = "PictureBox_Target";
            this.PictureBox_Target.Size = new System.Drawing.Size(524, 449);
            this.PictureBox_Target.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox_Target.TabIndex = 11;
            this.PictureBox_Target.TabStop = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(268, 1);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(947, 45);
            this.trackBar1.TabIndex = 14;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 504);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.PictureBox_Target);
            this.Controls.Add(this.openBtn);
            this.Controls.Add(this.PictureBox_Source);
            this.Controls.Add(this.listBox1);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Source)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Target)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.PictureBox PictureBox_Source;
        private System.Windows.Forms.Button openBtn;
        private System.Windows.Forms.PictureBox PictureBox_Target;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}

