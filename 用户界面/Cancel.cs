using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace 雪莉轻食
{
    public partial class XQ : Form
    {
        public XQ()
        {
            InitializeComponent();
        }

        private void XQ_Load(object sender, EventArgs e)
        {
            int aa = 0;
            int bb = 0;
            DataTable objDataTable = DBHelper.GetDataTable("select * from  TheOrderTable a inner join DetailedTheOrderTable b on a.OrderID=b.OrderID  where a.OrderID=" + didanguanli.GG);
            for (int i = 0; i < objDataTable.Rows.Count; i++)
            {
               
                Label DK = new Label();
                DK.Name = "Label" + i;//设定名称
                DK.Text = objDataTable.Rows[i]["SFoodName"].ToString();
                DK.Location = new Point(10, 5 + bb);//设定位置
                DK.AutoSize = true;
                DK.Font = new Font("微软雅黑", 15);
                panel2.Controls.Add(DK);
                Label DD = new Label();
                DD.Name = "Label" + i;//设定名称
                DD.Text = objDataTable.Rows[i]["OrderCount"].ToString();
                DD.Location = new Point(250, 5 + bb);//设定位置
                DD.AutoSize = true;
                DD.Font = new Font("微软雅黑", 15);
                panel2.Controls.Add(DD);
                Label SS = new Label();
                SS.Name = "Label" + i;//设定名称
                SS.Text = "¥" + objDataTable.Rows[i]["FoodPrice"].ToString();
                SS.Location = new Point(400, 5 + bb);//设定位置
                SS.AutoSize = true;
                SS.Font = new Font("微软雅黑", 15);
                panel2.Controls.Add(SS);
                aa++;
                if (aa++ >= 1)
                {
                    bb += 40;
                    //aa += 10;
                }
            }
            label7.Text = "¥" + objDataTable.Rows[0]["ShoppingPrice"].ToString();
            string temp1 = objDataTable.Rows[0]["Address0"].ToString();
            label10.Text = temp1.Length > 30 ? temp1.Substring(0, 30) + "..." : temp1;
            label8.Text = objDataTable.Rows[0]["XPhone"].ToString();
            label9.Text = objDataTable.Rows[0]["Name"].ToString();
            if (objDataTable.Rows[0]["ShoppingNote"].ToString() != "")
            {
                string temp = objDataTable.Rows[0]["ShoppingNote"].ToString();
                label11.Text = temp.Length > 30 ? temp.Substring(0, 30) + "..." : temp;
            }
            else
            {
                label11.Text = "无";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 200, AW_CENTER | AW_HIDE);
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
             DataTable objDataTable = DBHelper.GetDataTable("select * from  TheOrderTable where OrderID="+ didanguanli.GG);
             DataTable objDataTable1 = DBHelper.GetDataTable("select * from  OrderStateTable where OrderNumber='"+objDataTable.Rows[0]["OrderNumber"].ToString()+"'" );
             if (objDataTable1.Rows[0]["OrderState"].ToString() == "等待确认")
             {
                 if (DBHelper.SaveData("delete from OrderStateTable where OrderNumber='" + objDataTable.Rows[0]["OrderNumber"].ToString() + "'") > 0)
                 {
                     if (DBHelper.SaveData("delete from DetailedTheOrderTable where OrderID=" + didanguanli.GG) > 0)
                     {
                         if(DBHelper.SaveData("delete from TheOrderTable where OrderID=" + didanguanli.GG) > 0 )
                         {
                             MessageBox.Show("取消成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                             Close();
                             didanguanli jj = new didanguanli();
                             UserMain.form1.Haa(jj);
                             return;
                         }
                     }
                 }
                 else
                 {
                     MessageBox.Show("取消失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                     return;
                 }
             }
             else
             {
                 MessageBox.Show("订单正在派送中或以完成不能取消","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                 return;
             }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
    }
}
