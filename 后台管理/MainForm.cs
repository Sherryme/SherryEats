using System;
using System.Drawing;
using System.Windows.Forms;
using CCWin;


namespace 雪莉轻食
{
    public partial class Main_Form : CCSkinMain
    {
        public static Main_Form GTY;
        public Main_Form()
        {
            InitializeComponent();
            GTY = this;
        }
        
        #region 窗体显示的动画效果制作，使用Windows API完成
        /// <summary>
        /// 函数功能：该函数能在显示与隐藏窗口时能产生特殊的效果。有两种类型的动画效果：滚动动画和滑动动画。
        /// <para>函数原型：BOOL AnimateWindow（HWND hWnd，DWORD dwTime，DWORD dwFlags）</para>
        /// <para>hWnd：指定产生动画的窗口的句柄。</para>
        /// <para>dwTime：指明动画持续的时间（以微秒计），完成一个动画的标准时间为200微秒。</para>
        /// <para>dwFags：指定动画类型。这个参数可以是一个或多个下列标志的组合。</para>
        /// </summary>
        /// <param name="hwnd">指定产生动画的窗口的句柄。</param>
        /// <param name="dwTime">指明动画持续的时间（以微秒计），完成一个动画的标准时间为200微秒。</param>
        /// <param name="dwFlags">指定动画类型。这个参数可以是一个或多个下列标志的组合。</param>
        /// <returns>如果函数成功，返回值为非零；如果函数失败，返回值为零。</returns>
        /// <remarks>
        ///在下列情况下函数将失败：窗口使用了窗口边界；窗口已经可见仍要显示窗口；窗口已经隐藏仍要隐藏窗口。若想获得更多错误信息，请调用GetLastError函数。
        ///备注：可以将AW_HOR_POSITIVE或AW_HOR_NEGTVE与AW_VER_POSITVE或AW_VER_NEGATIVE组合来激活一个窗口。
        ///可能需要在该窗口的窗口过程和它的子窗口的窗口过程中处理WM_PRINT或WM_PRINTCLIENT消息。对话框，控制，及共用控制已处理WM_PRINTCLIENT消息，缺省窗口过程也已处理WM_PRINT消息。
        ///速查：WIDDOWS NT：5.0以上版本：Windows：98以上版本；Windows CE：不支持；头文件：Winuser.h；库文件：user32.lib。
        /// </remarks>
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        /// <summary>
        /// 使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略。
        /// </summary>
        public const int AW_SLIDE = 0x40000;

        /// <summary>
        /// 激活窗口。在使用了AW_HIDE标志后不要使用这个标志。
        /// </summary>
        public const int AW_ACTIVATE = 0x20000;

        /// <summary>
        /// 使用淡出效果。只有当hWnd为顶层窗口的时候才可以使用此标志。
        /// </summary>
        public const int AW_BLEND = 0x80000;

        /// <summary>
        /// 隐藏窗口，缺省则显示窗口。(关闭窗口用)
        /// </summary>
        public const int AW_HIDE = 0x10000;

        /// <summary>
        /// 若使用了AW_HIDE标志，则使窗口向内重叠；若未使用AW_HIDE标志，则使窗口向外扩展。
        /// </summary>
        public const int AW_CENTER = 0x0010;

        /// <summary>
        /// 自左向右显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略。
        /// </summary>
        public const int AW_HOR_POSITIVE = 0x0001;

        /// <summary>
        /// 自顶向下显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略。
        /// </summary>
        public const int AW_VER_POSITIVE = 0x0004;

        /// <summary>
        /// 自右向左显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略。
        /// </summary>
        public const int AW_HOR_NEGATIVE = 0x0002;

        /// <summary>
        /// 自下向上显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略。
        /// </summary>
        public const int AW_VER_NEGATIVE = 0x0008;

        #endregion
        private void Main_Form_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 200, AW_CENTER);
            panel2.Visible = true;
            Form5 a = new Form5();
            Daa(a);
        }
        private void Daa(Form d)
        {
            panel1.Controls.Clear();
            d.TopLevel = false;
            d.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            d.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(d);
            d.Show();
        }
       

        private void label1_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel2.Visible = true;
            button1_Click(null,null);

           
        }

        private void label2_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = true;
            button6_Click(null,null);
        }

        public void button6_Click(object sender, EventArgs e)
        {
            Quren kk = new Quren();
            Daa(kk);
        }

        public void button1_Click(object sender, EventArgs e)
        {
            Form5 a = new Form5();
            Daa(a);

        }

        public void button2_Click(object sender, EventArgs e)
        {
            Form7 a = new Form7();
            Daa(a);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 a = new Form6();
            Daa(a);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
            Login aa = new Login();
            aa.Show();
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.White;
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Red;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.White;
        }

        public void button5_Click(object sender, EventArgs e)
        {
            Qurpaisong jj = new Qurpaisong();
            Daa(jj);
        }

        public void button4_Click(object sender, EventArgs e)
        {
            PaiWanChen Dd = new PaiWanChen();
            Daa(Dd);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form1 nn = new Form1();
            Daa(nn);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel2.Visible = false;
        }
    }
}
