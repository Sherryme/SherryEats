using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace 雪莉轻食
{
    public partial class xiangqincs : Form
    {
        public xiangqincs()
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
        private void xiangqincs_Load(object sender, EventArgs e)
        {
           
            panel2.Visible = false;
            DataTable objDataTable = DBHelper.GetDataTable("select * from  MerchantsTable where MerchantsID=" + UserMain.DA);
            label158.Text = objDataTable.Rows[0]["MerchantsName"].ToString(); //显示店铺名称
            string temp = objDataTable.Rows[0]["MerchantsAddress"].ToString();
            label161.Text = temp.Length > 12 ? temp.Substring(0, 12) + "..." : temp; //截取地址12位长度后面以省略号代替
            label162.Text = objDataTable.Rows[0]["SPhone"].ToString(); //显示电话
            pictureBox74.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[0]["SPhotoName"].ToString()); //显示商家图片
            int aa = 0;
            int bb = 0;
            DataTable objkDataTable = DBHelper.GetDataTable("select * from  FoodTable where MerchantsID=" + objDataTable.Rows[0]["MerchantsID"].ToString());
            for (int i = 0; i < objkDataTable.Rows.Count; i++)
            {
                Label Va = new Label();
                Label Dw = new Label();
                Label DY = new Label();
                PictureBox Db = new PictureBox(); //实例化一个控件
                PictureBox DD = new PictureBox(); //实例化一个控件
                Button DG = new Button();
                Db.Name = "Db_PictureBox" + i;//设定名称
                Dw.Name = "Dw_Label" + i;//设定名称
                DG.Name =objkDataTable.Rows[i]["FoodID"].ToString();//设定名称
                DY.Name = "Dw_Label" + i;//设定名称
                DD.Name = "Db_PictureBox" + i;//设定名称
                Va.Name = "Db_PictureBox" + i;//设定名称
                Db.Text = objkDataTable.Rows[i]["FoodID"].ToString();
                Dw.Text = "¥" + objkDataTable.Rows[i]["FoodPrice"].ToString() + "/份";
                DY.Text = objkDataTable.Rows[i]["FoodName"].ToString();
                Va.Text = objkDataTable.Rows[i]["FoodID"].ToString();
                DG.Text = "加入购物车";
                Dw.AutoSize = true;
                Va.AutoSize = true;
                DY.AutoSize = true;
                Dw.Font = new Font("微软雅黑", 11);
                DG.Font = new Font("微软雅黑", 11);
                DY.Font = new Font("微软雅黑", 15);
                //Dw.Size = new System.Drawing.Size(121, 25);
                DG.BackColor = Color.DarkOrange;
                DG.ForeColor = Color.Transparent;
                DG.FlatStyle = FlatStyle.Flat;
                DG.Size = new System.Drawing.Size(270, 30);
                Db.Location = new Point(150 * aa, 20 + bb);//设定位置
                Dw.Location = new Point(150 * aa, 210 + bb);//设定位置
                DG.Location = new Point(150 * aa, 238 + bb);//设定位置
                DY.Location = new Point(150 * aa, 180 + bb);//设定位置
                DD.Location = new Point(150 * aa, 240 + bb);//设定位置
                Db.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\StudentPhoto\" + objkDataTable.Rows[i]["PhotoName"].ToString());
                DD.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\StudentPhoto\" + objkDataTable.Rows[i]["HPhotoName"].ToString());
                Db.SizeMode = PictureBoxSizeMode.StretchImage; //设定图像如何显示
                Db.Size = new Size(270, 150);
                Db.BorderStyle = BorderStyle.FixedSingle; //设置边框样式
                //Db.Cursor = Cursors.Hand;
                DG.Cursor = Cursors.Hand;
                DG.Click += new EventHandler(this.Button_Click); //添加单击事件
                Db.MouseEnter += new EventHandler(this.PictureBox_Enter);
                Db.MouseLeave +=new EventHandler(this.PictureBox_MouseLeave);
                //this.Controls.Add(Db);
                panel1.Controls.Add(Db); //绑定到panel1上
                panel1.Controls.Add(Dw);
                panel1.Controls.Add(DG);
                panel1.Controls.Add(DY);
                panel1.Controls.Add(DD);

                aa++;
                if (aa++ >= 4)
                {
                    aa = 0;
                    bb += 270;
                }

            }
        }
        private void PictureBox_Enter(object sender, EventArgs e)
        {
            PictureBox b = (PictureBox)sender;//将触发此事件的对象转换为该对象
            MouseEen(b);
        }
        private void PictureBox_MouseLeave(object sender, EventArgs e)
        {
            PictureBox b = (PictureBox)sender;//将触发此事件的对象转换为该对象
            MouseLeav(b);
        }
        public static int HH = 0; //菜品ID
        private void Button_Click(object sender, EventArgs e)
        {
            Button b2 = (Button)sender;//将触发此事件的对象转换为该对象
            HH = int.Parse(b2.Name.ToString());
            DataTable objDataTable = DBHelper.GetDataTable("select * from  ShoppingTable where FoodID="+HH);
            if (objDataTable.Rows.Count > 0)
            {
                MessageBox.Show("购物车已存在此商品");
                return;
            }
            string SQL = "insert into ShoppingTable(MerchantsID,FoodID,ShoppingCount,ShoppingPrice)values(" + UserMain.DA + "," + HH + ",1,0)";
           if (DBHelper.SaveData(SQL) > 0)
           {
             
               MessageBox.Show("添加成功");
           }
           else
           {
               MessageBox.Show("添加失败");
           }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void Haa(Form d)
        {
            panel2.Controls.Clear();
            d.TopLevel = false;
            d.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //d.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Controls.Add(d);
            d.Show();
        }
        int a = 1;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (a == 1)
            {
               
                AnimateWindow(this.panel2.Handle, 200, AW_HOR_POSITIVE);
                panel2.Visible = true;
                a++;
                GWC ah = new GWC();
                Haa(ah);
                return;
            }
            if (a == 2)
            {
                
                panel2.Visible = false;
                GWC ah = new GWC();
                Haa(ah);
                a = 1;
                return;
            }
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserMain.form1.panel9.Visible = false;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            MouseEen(pictureBox1);
        }
        private void MouseEen(PictureBox name)
        {
            name.Location = new Point(name.Location.X - 2, name.Location.Y - 2);
        }
        private void MouseLeav(PictureBox name)
        {
            name.Location = new Point(name.Location.X + 2, name.Location.Y + 2);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            MouseLeav(pictureBox1);
        }

        



    }
}
