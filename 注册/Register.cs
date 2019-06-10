using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CCWin;

namespace 雪莉轻食
{
    public partial class Form4 : CCSkinMain
    {
        public Form4()
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
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://blog.sherry.cf");
        } 

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            Login a = new Login();
            a.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
            Login aa = new Login();
            aa.Show();
        }

        public bool StrSQl(string SQL)
        {
            SqlConnection objCon = new SqlConnection(@"Data Source=(local);database=SherryEats;Integrated Security=true;");
            //定义数据库的连接，括号里面是数据库的连接参数包括
            //数据库的地址、使用的数据库的名称、账号密码
            try
            {
                //if (objCon.State == ConnectionState.Closed)
                //判断数据库的连接状态是不是关闭状态
                objCon.Open();
                //如果是关闭状态就打开数据库
                SqlCommand objCom = new SqlCommand(SQL, objCon);
                objCom.ExecuteNonQuery();
                //执行数据库查询语句返回受影响行数
                return true;
            }
            catch (Exception ex)
            {//捕获程序运行中的错误
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                //if (objCon.State == ConnectionState.Open)
                objCon.Close();
                //如果数据的状态是打开的则关闭数据库
            }
        }

        public DataSet GetDataSet(string strSQL)
        {
            SqlConnection objMyCon = new SqlConnection(@"Data Source=(local);database=SherryEats;Integrated Security=true;");
            try
            {
                //if (objMyCon.State == ConnectionState.Closed)
                objMyCon.Open();
                SqlCommand objMyCom = new SqlCommand(strSQL, objMyCon);
                SqlDataAdapter da = new SqlDataAdapter(objMyCom);
                //创建数据匹配中心
                DataSet dt = new DataSet();
                //创建数据表集
                da.Fill(dt);
                //填充数据表集
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return new DataSet();
            }
            finally
            {
                if (objMyCon.State == ConnectionState.Open)
                    objMyCon.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Faa() == false)
                return;
            //Form5 aa = new Form5();
            string SQl = " insert into UserTable(UserNum,UserName,UserPassWord,Phone,UserType)values('" + this.textBox1.Text.ToString().Trim() + "','" + this.textBox5.Text.ToString().Trim() + "','" + this.textBox2.Text.ToString().Trim() + "','" + this.textBox4.Text.ToString().Trim() + "',1)";
           if (StrSQl(SQl) == true)
           {
               MessageBox.Show("注册成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
           }
           else
           {
               MessageBox.Show("注册失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
           }
        }
        private bool Faa()
        {
            if (this.textBox1.Text.ToString().Trim() == "")
            {
                MessageBox.Show("请填写用户名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                string strSQL = "select * from UserTable where UserNum='"+this.textBox1.Text.ToString().Trim()+"'";
                //Form5 aa = new Form5();
                DataSet dt =GetDataSet(strSQL);
                if (dt.Tables.Count == 0 || dt.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("该用户名已被注册", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
               
            }
            if (this.textBox2.Text.ToString().Trim() == "" )
            {
                MessageBox.Show("请填写密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.textBox3.Text.ToString().Trim() == "")
            {
                MessageBox.Show("确认密码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.textBox3.Text.ToString().Trim() != this.textBox2.Text.ToString().Trim())
            {
                MessageBox.Show("前后输入的密码不一致", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.textBox4.Text.ToString().Trim() == "" || this.textBox4.Text.ToString().Trim().Length!=11)
            {
                MessageBox.Show("手机号不能为空且长度必须为11位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.textBox5.Text.ToString().Trim() == "" || this.textBox5.Text.ToString().Trim().Length > 7)
            {
                MessageBox.Show("姓名不能为空且长度不能大于7位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 200, AW_VER_NEGATIVE);
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}