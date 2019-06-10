using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using CCWin;

namespace 雪莉轻食
{

    public partial class Login : CCSkinMain
    {
        public Login()
        {
            InitializeComponent();
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
        private void Form1_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 200, AW_VER_POSITIVE);
            //this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }
       
        public static int tt=0;//用户ID
        private void button1_Click(object sender, EventArgs e)
        {
            if (Check() == false)
                return;
            string strUserID = this.textBox1.Text.ToString().Trim();
            string password = this.textBox2.Text.ToString().Trim();

            SqlConnection objCon = new SqlConnection(@"Data Source=(local);database=SherryEatsDB;Integrated Security=true;");
            //定义数据库的连接，括号里面是数据库的连接参数包括
            //数据库的地址、使用的数据库的名称、账号密码
            try
            {
                //if (objCon.State == ConnectionState.Closed)
                //判断数据库的连接状态是不是关闭状态
                objCon.Open();
                ////如果是关闭状态就打开数据库
                SqlCommand objCom = new SqlCommand();
                //定义数据库命令
                objCom.Connection = objCon;
                //设置命令的连接
                objCom.CommandText = "select * from UserTable  where UserNum=@UserNum";
                //设定命令文本
                SqlParameter objUserID = new SqlParameter(@"UserNum", SqlDbType.NVarChar, 11);
                objUserID.Value = strUserID;
                objCom.Parameters.Add(objUserID);
                //定义数据库参数变量，并且制定变量类型长度
                //参数的赋值
                //将参数添加到数据了直行命令中
                SqlDataReader objRead = objCom.ExecuteReader();
                //定义数据库读取，并且指定读取的命令来源
                objRead.Read();
                //读取一行
                if (objRead.HasRows)
                {
                    //判断是否读取数据
                    if (password == objRead["UserPassWord"].ToString())
                    {
                        //如果读取出有数据则用索引读取这一行中所需的数据
                        if ("2" == objRead["UserType"].ToString())
                        {
                            if (textBox3.Text.ToString().Trim().ToUpper() != skinCode1.CodeStr.ToString())
                            {
                                MessageBox.Show("验证码错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                skinCode1.NewCode();
                                return;
                            }
                            tt = int.Parse(objRead["UserID"].ToString());
                            Main_Form aa = new Main_Form();
                            aa.Show();
                            this.Hide();
                        }
                        if ("1" == objRead["UserType"].ToString())
                        {
                            if (textBox3.Text.ToString().Trim().ToUpper() != skinCode1.CodeStr.ToString())
                            {
                                MessageBox.Show("验证码错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                skinCode1.NewCode();
                                return;
                            }
                            tt = int.Parse(objRead["UserID"].ToString());
                            UserMain um = new UserMain();
                            um.Show();
                            this.Hide();
                        }
                    }
                    else
                        MessageBox.Show("账号或者密码错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("账号或者密码错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {//捕获程序运行中的错误
                //MessageBox.Show(ex.ToString());
                MessageBox.Show("无法连接到服务器，登录失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
                //如果数据的状态是打开的则关闭数据库
            }
        }
        private bool Check()
        {
            if (textBox1.Text.ToString().Trim() == "")// || textBox1.Text.ToString().Trim().Length != 6
            {
                MessageBox.Show("请输入用户名", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox2.Text.ToString().Trim() == "" )
            {
                MessageBox.Show("请输入密码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 200, AW_VER_NEGATIVE | AW_HIDE);
            System.Environment.Exit(0); 
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            zhuimima a = new zhuimima();
            a.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form4 a = new Form4();
            a.Show();
            this.Hide();

        }

        private void TheLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnimateWindow(this.Handle, 1000, AW_BLEND | AW_HIDE);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


    }
}
