using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            /// <summary>
            /// 二值化
            /// </summary>
            binary,
            /// <summary>
            /// 高斯滤波
            /// </summary>
            gaussianBlur,
            /// <summary>
            /// 均值滤波
            /// </summary>
            blur,
            /// <summary>
            /// 中值滤波
            /// </summary>
            medianBlur,
            /// <summary>
            /// 双边滤波
            /// </summary>
            bilateralFilter,
            /// <summary>
            /// 边缘检测-sobel
            /// </summary>
            sobel,
            /// <summary>
            /// 边缘检测-scharr
            /// </summary>
            scharr,
            /// <summary>
            /// 边缘检测-canny
            /// </summary>
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
                case (int)lb1_list.binary:
                    #region 二值化处理
                    binary(this.trackBar1.Value+3, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.gaussianBlur:
                    #region 高斯滤波
                    gaussianBlur(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.blur:
                    #region 均值滤波
                    blur(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.medianBlur:
                    #region 中值滤波
                    medianBlur(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.bilateralFilter:
                    #region 双边滤波
                    bilateralFilter(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.sobel:
                    #region sobel边缘检测
                    sobel(tagetMat);
                    #endregion
                    break;
                case (int)lb1_list.scharr:
                    #region scharr边缘检测
                    scharr(tagetMat);
                    #endregion
                    break;
                default:
                    break;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            switch (selectIndex)
            {
                case (int)lb1_list.binary:
                    #region 二值化处理
                    binary(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.gaussianBlur:
                    #region 高斯滤波
                    gaussianBlur(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.blur:
                    #region 均值滤波
                    blur(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.medianBlur:
                    #region 中值滤波
                    medianBlur(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.bilateralFilter:
                    #region 双边滤波
                    bilateralFilter(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                default:
                    break;

            }
          
        }
        /// <summary>
        /// 二值化
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void binary(int pos, Mat mat)
        {
            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);

            Cv2.Threshold(tempMat, tempMat, pos * 10, 255, ThresholdTypes.Binary);

            tempMat.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat.Release();
        }
        /// <summary>
        /// 高斯滤波
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void gaussianBlur(int pos, Mat mat)
        {
            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);

            Cv2.GaussianBlur(tempMat, tempMat, new OpenCvSharp.Size(pos * 2 + 1, pos * 2 + 1), 5);
            tempMat.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat.Release();
        }
        /// <summary>
        /// 均值滤波
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void blur(int pos, Mat mat)
        {
            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);

            Cv2.Blur(tempMat, tempMat, new OpenCvSharp.Size(pos + 1, pos + 1));
            tempMat.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat.Release();
        }
        /// <summary>
        /// 中值滤波
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void medianBlur(int pos, Mat mat)
        {
            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);

            Cv2.MedianBlur(tempMat, tempMat, pos * 2 + 1);
            tempMat.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat.Release();
        }
        /// <summary>
        /// 双边滤波
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void bilateralFilter(int pos, Mat mat)
        {
            Mat tempMat1 = new Mat();
            Mat tempMat2 = new Mat();
            mat.CopyTo(tempMat1);

            Cv2.BilateralFilter(tempMat1, tempMat2, pos * 5 + 1, 100, 100);
            tempMat2.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat1.Release();
            tempMat2.Release();
        }
        /// <summary>
        /// 边缘检测-sobel
        /// </summary>
        /// <param name="mat"></param>
        private void sobel(Mat mat)
        {
            Mat sobelx = new Mat();
            Mat sobely = new Mat();
            Cv2.Sobel(mat, sobelx, -1, 1, 0);
            Cv2.Sobel(mat, sobely, -1, 0, 1);
            Cv2.AddWeighted(sobelx, 0.5, sobely, 0.5, 0, mat);//gamma为加到结果上的值
            PictureBox_Target.Image = mat.ToBitmap();
            sobelx.Release();
            sobelx.Release();
        }
        /// <summary>
        /// 边缘检测-scharr
        /// </summary>
        /// <param name="mat"></param>
        private void scharr(Mat mat)
        {
            Mat scharrx = new Mat();
            Mat scharry = new Mat();
            Cv2.Scharr(mat, scharrx, -1, 1, 0);
            Cv2.Scharr(mat, scharry, -1, 0, 1);
            Cv2.AddWeighted(scharrx, 0.5, scharry, 0.5, 0, mat);
            PictureBox_Target.Image = mat.ToBitmap();
            scharrx.Release();
            scharry.Release();
        }
        /// <summary>
        /// 边缘检测-canny
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="userdata"></param>
        public void canny(int pos, Mat mat)
        {
            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);

            if (tempMat.Channels() == 3)
            {
                Cv2.CvtColor(tempMat, tempMat, ColorConversionCodes.BGR2GRAY);
            }

            Cv2.Canny(tempMat, tempMat, pos * 25, pos * 25);
            tempMat.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat.Release();
            Thread.Sleep(50);
        }
    }
}
