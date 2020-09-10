using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCVSharpTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public Mat sourceMat;//当前主窗体图像
        public Mat tagetMat;//当前主窗体图像
        public int selectIndex;
        public enum lb1_list//记录操作方法
        {
            /// <summary>
            /// 灰度
            /// </summary>
            gray,
            /// <summary>
            /// 反色
            /// </summary>
            reverse,
            binary,
            gaussianBlur,
            blur,
            medianBlur,
            bilateralFilter,
            sobel,
            scharr,
            canny,
            Laplacian,
            carve,
            rever_lr,
            rever_tb,
            frost,
            sharp,
            dilate,
            erode,
            tophat,
            blackhat,
            gamma,
            log,
            hist,
            findContours,
            template,
            drawRect,

        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();//创建打开文件夹窗体
            ofd1.Title = "请选择输入的图像";
            ofd1.InitialDirectory = @"";//初始化目录
            ofd1.Multiselect = false;//不可多选
            ofd1.Filter = "图像文件|*.jpg;*.png;*.bmp|全部文件|*.*";
            ofd1.ShowDialog();

            if (ofd1.FileName != string.Empty)
            {
                try
                {
                    this.PictureBox_Source.Load(ofd1.FileName);
                    sourceMat = new Mat(ofd1.FileName);
                    tagetMat = new Mat(ofd1.FileName);
                }
                catch (Exception ex )
                {
                    MessageBox.Show("打开失败，请检查文件格式是否符合");
                }

            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            selectIndex = listBox1.SelectedIndex;
            handle(selectIndex);
        }

        public void handle(int selectIndex)
        {
            switch (selectIndex)
            {
                case (int)lb1_list.gray:
                    #region 转为灰度图
                    if (sourceMat.Channels() == 3)
                    {
                        Cv2.CvtColor(sourceMat, tagetMat, ColorConversionCodes.BGR2GRAY);
                        this.PictureBox_Target.Image = tagetMat.ToBitmap();
                    }
                    else
                    {
                        MessageBox.Show("已是灰度图像，不要重复操作");
                    }
                    #endregion
                    break;
                case (int)lb1_list.reverse:
                    #region 反色
                    tagetMat = ~sourceMat;
                    PictureBox_Target.Image = tagetMat.ToBitmap();
                    #endregion
                    break;
                default:
                    break;
            }
        }








    }
}
