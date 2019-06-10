using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CCWin;
namespace 雪莉轻食
{
    public partial class UserMain : CCSkinMain
    {
        public static UserMain form1;
        public UserMain()
        {
            InitializeComponent();
            form1 = this;
            timer1_Tick(null, null);
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

        public static string DA = "";
        private void pictureBox_MouseClick(object sender, EventArgs e)
        {
            PictureBox b1 = (PictureBox)sender;//将触发此事件的对象转换为该对象
            DA = b1.Text;
            //panel9.Visible = true;
            //this.panel9.BackColor = Color.Orange;
            xiangqincs hh = new xiangqincs();
            Haa(hh);
            AnimateWindow(this.panel9.Handle, 200, AW_VER_POSITIVE);
            panel9.Visible = true;

        }
        private void yonghu_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 200, AW_CENTER);
            int aa = 0;
            int bb = 0;
            DataTable objDataTable = DBHelper.GetDataTable("select * from  MerchantsTable");
            for (int i = 0; i < objDataTable.Rows.Count; i++)
            {
                Label Dw = new Label();
                PictureBox Df = new PictureBox();
                PictureBox Db = new PictureBox(); //实例化一个控件
                Db.Name = "Db_PictureBox" + i;//设定名称
                Df.Name = "Df_PictureBox" + i;//设定名称
                Dw.Name = "Dw_Label" + i;//设定名称
                Db.Text = objDataTable.Rows[i]["MerchantsID"].ToString();
                Dw.Text = objDataTable.Rows[i]["MerchantsName"].ToString();
                Dw.AutoSize = true;
                Dw.Font = new Font("微软雅黑", 15);
                Dw.Size = new System.Drawing.Size(121, 25);
                Db.Location = new Point(150 * aa, 20 + bb);//设定位置
                Df.Location = new Point(150 * aa, 210 + bb);//设定位置
                Dw.Location = new Point(150 * aa, 180 + bb);//设定位置
                Db.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["SPhotoName"].ToString());
                Df.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["BPhotoName"].ToString());
                Db.SizeMode = PictureBoxSizeMode.StretchImage; //设定图像如何显示
                Df.SizeMode = PictureBoxSizeMode.StretchImage;
                Db.Size = new Size(270, 150);
                Df.Size = new Size(272, 49);
                Db.BorderStyle = BorderStyle.FixedSingle; //设置边框样式
                Db.Cursor = Cursors.Hand;
                //this.Controls.Add(Db);
                panel1.Controls.Add(Db); //绑定到panel1上
                panel1.Controls.Add(Df);
                panel1.Controls.Add(Dw);
                Db.Click += new EventHandler(this.pictureBox_MouseClick); //添加单击事件
                aa++;
                if (aa++ >= 4)
                {
                    aa = 0;
                    bb += 240;
                }
                //int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
                //int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
                //this.Location = new Point(x, y);//设置窗体在屏幕右下角显示

                panel9.HorizontalScroll.Visible = true;//横的
                panel9.VerticalScroll.Visible = false;//竖的
                panel9.Visible = false;
                label2.ForeColor = Color.Red;
                label3.ForeColor = Color.Gray;
                label4.ForeColor = Color.Gray;
                label5.ForeColor = Color.Gray;
                label6.ForeColor = Color.Gray;
                label7.ForeColor = Color.Gray;
                label8.ForeColor = Color.Gray;
                //_1 a = new _1(this);
                //Daa(a);
            }
            label13.Visible = false;
            pictureBox2.Visible = false;
        }

        private void label168_Click(object sender, EventArgs e)
        {

        }

        private void yonghu_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)  //判断是否最小化
            {
                //this.ShowInTaskbar = false;  //不显示在系统任务栏
                notifyIcon1.Visible = true;  //托盘图标可见
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = true;  //显示在系统任务栏
                this.WindowState = FormWindowState.Normal;  //还原窗体
                notifyIcon1.Visible = false;  //托盘图标隐藏
            }
        }
        #region 点击任务栏图标最小化
        // Minimize from taskbar  点击任务栏图标最小化
        const int WS_MINIMIZEBOX = 0x20000;
        const int CS_DBLCLKS = 0x8;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                return cp;
            }
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;//最小化 
            this.ShowInTaskbar = false;  //不显示在系统任务栏
        }

        private void button4_Click(object sender, EventArgs e) //窗体关闭事件
        {
            Close();    
            Login aa = new Login();
            aa.Show();
        }


        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 主菜单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;  //还原窗体
            notifyIcon1.Visible = false;  //托盘图标隐藏   
            this.ShowInTaskbar = true;  //显示在系统任务栏
        }

        private void label3_Click(object sender, EventArgs e) //美食
        {
            label2.ForeColor = Color.Gray;
            label3.ForeColor = Color.Red;
            label4.ForeColor = Color.Gray;
            label5.ForeColor = Color.Gray;
            label6.ForeColor = Color.Gray;
            label7.ForeColor = Color.Gray;
            label8.ForeColor = Color.Gray;
            panel1.Controls.Clear();
            int aa = 0;
            int bb = 0;
            DataTable objDataTable = DBHelper.GetDataTable("select * from  MerchantsTable where  MerchantsType='美食'");
            for (int i = 0; i < objDataTable.Rows.Count; i++)
            {
                label13.Visible = false;
                pictureBox2.Visible = false;
                Label Dw = new Label();
                PictureBox Df = new PictureBox();
                PictureBox Db = new PictureBox(); //实例化一个控件
                Db.Name = "Db_PictureBox" + i;//设定名称
                Df.Name = "Df_PictureBox" + i;//设定名称
                Dw.Name = "Dw_Label" + i;//设定名称
                Db.Text = objDataTable.Rows[i]["MerchantsID"].ToString();
                Dw.Text = objDataTable.Rows[i]["MerchantsName"].ToString();
                Dw.AutoSize = true;
                Dw.Font = new Font("微软雅黑", 15);
                Dw.Size = new System.Drawing.Size(121, 25);
                Db.Location = new Point(150 * aa, 20 + bb);//设定位置
                Df.Location = new Point(150 * aa, 210 + bb);//设定位置
                Dw.Location = new Point(150 * aa, 180 + bb);//设定位置
                Db.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["SPhotoName"].ToString());
                Df.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["BPhotoName"].ToString());
                Db.SizeMode = PictureBoxSizeMode.StretchImage; //设定图像如何显示
                Df.SizeMode = PictureBoxSizeMode.StretchImage;
                Db.Size = new Size(270, 150);
                Df.Size = new Size(272, 49);
                Db.BorderStyle = BorderStyle.FixedSingle; //设置边框样式
                Db.Cursor = Cursors.Hand;
                //this.Controls.Add(Db);
                panel1.Controls.Add(Db); //绑定到panel1上
                panel1.Controls.Add(Df);
                panel1.Controls.Add(Dw);
                Db.Click += new EventHandler(this.pictureBox_MouseClick); //添加单击事件
                aa++;
                if (aa++ >= 4)
                {
                    aa = 0;
                    bb += 240;
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)//全部
        {
            label2.ForeColor = Color.Red;
            label3.ForeColor = Color.Gray;
            label4.ForeColor = Color.Gray;
            label5.ForeColor = Color.Gray;
            label6.ForeColor = Color.Gray;
            label7.ForeColor = Color.Gray;
            label8.ForeColor = Color.Gray;
            this.panel9.Visible = false;
            panel1.Controls.Clear();
            int aa = 0;
            int bb = 0;
            DataTable objDataTable = DBHelper.GetDataTable("select * from  MerchantsTable");
            for (int i = 0; i < objDataTable.Rows.Count; i++)
            {
                label13.Visible = false;
                pictureBox2.Visible = false;
                Label Dw = new Label();
                PictureBox Df = new PictureBox();
                PictureBox Db = new PictureBox(); //实例化一个控件
                Db.Name = "Db_PictureBox" + i;//设定名称
                Df.Name = "Df_PictureBox" + i;//设定名称
                Dw.Name = "Dw_Label" + i;//设定名称
                Db.Text = objDataTable.Rows[i]["MerchantsID"].ToString();
                Dw.Text = objDataTable.Rows[i]["MerchantsName"].ToString();
                Dw.AutoSize = true;
                Dw.Font = new Font("微软雅黑", 15);
                Dw.Size = new System.Drawing.Size(121, 25);
                Db.Location = new Point(150 * aa, 20 + bb);//设定位置
                Df.Location = new Point(150 * aa, 210 + bb);//设定位置
                Dw.Location = new Point(150 * aa, 180 + bb);//设定位置
                Db.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["SPhotoName"].ToString());
                Df.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["BPhotoName"].ToString());
                Db.SizeMode = PictureBoxSizeMode.StretchImage; //设定图像如何显示
                Df.SizeMode = PictureBoxSizeMode.StretchImage;
                Db.Size = new Size(270, 150);
                Df.Size = new Size(272, 49);
                Db.BorderStyle = BorderStyle.FixedSingle; //设置边框样式
                Db.Cursor = Cursors.Hand;
                //this.Controls.Add(Db);
                panel1.Controls.Add(Db); //绑定到panel1上
                panel1.Controls.Add(Df);
                panel1.Controls.Add(Dw);
                Db.Click += new EventHandler(this.pictureBox_MouseClick); //添加单击事件
                aa++;
                if (aa++ >= 4)
                {
                    aa = 0;
                    bb += 240;
                }
            }

        }

        private void label4_Click(object sender, EventArgs e)//正餐优选
        {
            label2.ForeColor = Color.Gray;
            label3.ForeColor = Color.Gray;
            label4.ForeColor = Color.Red;
            label5.ForeColor = Color.Gray;
            label6.ForeColor = Color.Gray;
            label7.ForeColor = Color.Gray;
            label8.ForeColor = Color.Gray;
            panel1.Controls.Clear();
            int aa = 0;
            int bb = 0;
            DataTable objDataTable = DBHelper.GetDataTable("select * from  MerchantsTable where  MerchantsType='正餐优选'");
            for (int i = 0; i < objDataTable.Rows.Count; i++)
            {
                label13.Visible = false;
                pictureBox2.Visible = false;
                Label Dw = new Label();
                PictureBox Df = new PictureBox();
                PictureBox Db = new PictureBox(); //实例化一个控件
                Db.Name = "Db_PictureBox" + i;//设定名称
                Df.Name = "Df_PictureBox" + i;//设定名称
                Dw.Name = "Dw_Label" + i;//设定名称
                Db.Text = objDataTable.Rows[i]["MerchantsID"].ToString();
                Dw.Text = objDataTable.Rows[i]["MerchantsName"].ToString();
                Dw.AutoSize = true;
                Dw.Font = new Font("微软雅黑", 15);
                Dw.Size = new System.Drawing.Size(121, 25);
                Db.Location = new Point(150 * aa, 20 + bb);//设定位置
                Df.Location = new Point(150 * aa, 210 + bb);//设定位置
                Dw.Location = new Point(150 * aa, 180 + bb);//设定位置
                Db.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["SPhotoName"].ToString());
                Df.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["BPhotoName"].ToString());
                Db.SizeMode = PictureBoxSizeMode.StretchImage; //设定图像如何显示
                Df.SizeMode = PictureBoxSizeMode.StretchImage;
                Db.Size = new Size(270, 150);
                Df.Size = new Size(272, 49);
                Db.BorderStyle = BorderStyle.FixedSingle; //设置边框样式
                Db.Cursor = Cursors.Hand;
                //this.Controls.Add(Db);
                panel1.Controls.Add(Db); //绑定到panel1上
                panel1.Controls.Add(Df);
                panel1.Controls.Add(Dw);
                Db.Click += new EventHandler(this.pictureBox_MouseClick); //添加单击事件
                aa++;
                if (aa++ >= 4)
                {
                    aa = 0;
                    bb += 240;
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)//超市
        {
            label2.ForeColor = Color.Gray;
            label3.ForeColor = Color.Gray;
            label4.ForeColor = Color.Gray;
            label5.ForeColor = Color.Red;
            label6.ForeColor = Color.Gray;
            label7.ForeColor = Color.Gray;
            label8.ForeColor = Color.Gray;
            panel1.Controls.Clear();
            int aa = 0;
            int bb = 0;
            DataTable objDataTable = DBHelper.GetDataTable("select * from  MerchantsTable where  MerchantsType='超市'");
            for (int i = 0; i < objDataTable.Rows.Count; i++)
            {
                label13.Visible = false;
                pictureBox2.Visible = false;
                Label Dw = new Label();
                PictureBox Df = new PictureBox();
                PictureBox Db = new PictureBox(); //实例化一个控件
                Db.Name = "Db_PictureBox" + i;//设定名称
                Df.Name = "Df_PictureBox" + i;//设定名称
                Dw.Name = "Dw_Label" + i;//设定名称
                Db.Text = objDataTable.Rows[i]["MerchantsID"].ToString();
                Dw.Text = objDataTable.Rows[i]["MerchantsName"].ToString();
                Dw.AutoSize = true;
                Dw.Font = new Font("微软雅黑", 15);
                Dw.Size = new System.Drawing.Size(121, 25);
                Db.Location = new Point(150 * aa, 20 + bb);//设定位置
                Df.Location = new Point(150 * aa, 210 + bb);//设定位置
                Dw.Location = new Point(150 * aa, 180 + bb);//设定位置
                Db.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["SPhotoName"].ToString());
                Df.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["BPhotoName"].ToString());
                Db.SizeMode = PictureBoxSizeMode.StretchImage; //设定图像如何显示
                Df.SizeMode = PictureBoxSizeMode.StretchImage;
                Db.Size = new Size(270, 150);
                Df.Size = new Size(272, 49);
                Db.BorderStyle = BorderStyle.FixedSingle; //设置边框样式
                Db.Cursor = Cursors.Hand;
                //this.Controls.Add(Db);
                panel1.Controls.Add(Db); //绑定到panel1上
                panel1.Controls.Add(Df);
                panel1.Controls.Add(Dw);
                Db.Click += new EventHandler(this.pictureBox_MouseClick); //添加单击事件
                aa++;
                if (aa++ >= 4)
                {
                    aa = 0;
                    bb += 240;
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)//精选小吃
        {
            label2.ForeColor = Color.Gray;
            label3.ForeColor = Color.Gray;
            label4.ForeColor = Color.Gray;
            label5.ForeColor = Color.Gray;
            label6.ForeColor = Color.Red;
            label7.ForeColor = Color.Gray;
            label8.ForeColor = Color.Gray;
            panel1.Controls.Clear();
            int aa = 0;
            int bb = 0;
            DataTable objDataTable = DBHelper.GetDataTable("select * from  MerchantsTable where  MerchantsType='精选小吃'");
            for (int i = 0; i < objDataTable.Rows.Count; i++)
            {
                label13.Visible = false;
                pictureBox2.Visible = false;
                Label Dw = new Label();
                PictureBox Df = new PictureBox();
                PictureBox Db = new PictureBox(); //实例化一个控件
                Db.Name = "Db_PictureBox" + i;//设定名称
                Df.Name = "Df_PictureBox" + i;//设定名称
                Dw.Name = "Dw_Label" + i;//设定名称
                Db.Text = objDataTable.Rows[i]["MerchantsID"].ToString();
                Dw.Text = objDataTable.Rows[i]["MerchantsName"].ToString();
                Dw.AutoSize = true;
                Dw.Font = new Font("微软雅黑", 15);
                Dw.Size = new System.Drawing.Size(121, 25);
                Db.Location = new Point(150 * aa, 20 + bb);//设定位置
                Df.Location = new Point(150 * aa, 210 + bb);//设定位置
                Dw.Location = new Point(150 * aa, 180 + bb);//设定位置
                Db.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["SPhotoName"].ToString());
                Df.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["BPhotoName"].ToString());
                Db.SizeMode = PictureBoxSizeMode.StretchImage; //设定图像如何显示
                Df.SizeMode = PictureBoxSizeMode.StretchImage;
                Db.Size = new Size(270, 150);
                Df.Size = new Size(272, 49);
                Db.BorderStyle = BorderStyle.FixedSingle; //设置边框样式
                Db.Cursor = Cursors.Hand;
                //this.Controls.Add(Db);
                panel1.Controls.Add(Db); //绑定到panel1上
                panel1.Controls.Add(Df);
                panel1.Controls.Add(Dw);
                Db.Click += new EventHandler(this.pictureBox_MouseClick); //添加单击事件
                aa++;
                if (aa++ >= 4)
                {
                    aa = 0;
                    bb += 240;
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)//鲜果购
        {
            label2.ForeColor = Color.Gray;
            label3.ForeColor = Color.Gray;
            label4.ForeColor = Color.Gray;
            label5.ForeColor = Color.Gray;
            label6.ForeColor = Color.Gray;
            label7.ForeColor = Color.Red;
            label8.ForeColor = Color.Gray;
            panel1.Controls.Clear();
            int aa = 0;
            int bb = 0;
            DataTable objDataTable = DBHelper.GetDataTable("select * from  MerchantsTable where  MerchantsType='鲜果购'");
            for (int i = 0; i < objDataTable.Rows.Count; i++)
            {
                label13.Visible = false;
                pictureBox2.Visible = false;
                Label Dw = new Label();
                PictureBox Df = new PictureBox();
                PictureBox Db = new PictureBox(); //实例化一个控件
                Db.Name = "Db_PictureBox" + i;//设定名称
                Df.Name = "Df_PictureBox" + i;//设定名称
                Dw.Name = "Dw_Label" + i;//设定名称
                Db.Text = objDataTable.Rows[i]["MerchantsID"].ToString();
                Dw.Text = objDataTable.Rows[i]["MerchantsName"].ToString();
                Dw.AutoSize = true;
                Dw.Font = new Font("微软雅黑", 15);
                Dw.Size = new System.Drawing.Size(121, 25);
                Db.Location = new Point(150 * aa, 20 + bb);//设定位置
                Df.Location = new Point(150 * aa, 210 + bb);//设定位置
                Dw.Location = new Point(150 * aa, 180 + bb);//设定位置
                Db.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["SPhotoName"].ToString());
                Df.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["BPhotoName"].ToString());
                Db.SizeMode = PictureBoxSizeMode.StretchImage; //设定图像如何显示
                Df.SizeMode = PictureBoxSizeMode.StretchImage;
                Db.Size = new Size(270, 150);
                Df.Size = new Size(272, 49);
                Db.BorderStyle = BorderStyle.FixedSingle; //设置边框样式
                Db.Cursor = Cursors.Hand;
                //this.Controls.Add(Db);
                panel1.Controls.Add(Db); //绑定到panel1上
                panel1.Controls.Add(Df);
                panel1.Controls.Add(Dw);
                Db.Click += new EventHandler(this.pictureBox_MouseClick); //添加单击事件
                aa++;
                if (aa++ >= 4)
                {
                    aa = 0;
                    bb += 240;
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)//下午茶
        {
            label2.ForeColor = Color.Gray;
            label3.ForeColor = Color.Gray;
            label4.ForeColor = Color.Gray;
            label5.ForeColor = Color.Gray;
            label6.ForeColor = Color.Gray;
            label7.ForeColor = Color.Gray;
            label8.ForeColor = Color.Red;
            panel1.Controls.Clear();
            int aa = 0;
            int bb = 0;
            DataTable objDataTable = DBHelper.GetDataTable("select * from  MerchantsTable where  MerchantsType='下午茶'");
            for (int i = 0; i < objDataTable.Rows.Count; i++)
            {
                label13.Visible = false;
                pictureBox2.Visible = false;
                Label Dw = new Label();
                PictureBox Df = new PictureBox();
                PictureBox Db = new PictureBox(); //实例化一个控件
                Db.Name = "Db_PictureBox" + i;//设定名称
                Df.Name = "Df_PictureBox" + i;//设定名称
                Dw.Name = "Dw_Label" + i;//设定名称
                Db.Text = objDataTable.Rows[i]["MerchantsID"].ToString();
                Dw.Text = objDataTable.Rows[i]["MerchantsName"].ToString();
                Dw.AutoSize = true;
                Dw.Font = new Font("微软雅黑", 15);
                Dw.Size = new System.Drawing.Size(121, 25);
                Db.Location = new Point(150 * aa, 20 + bb);//设定位置
                Df.Location = new Point(150 * aa, 210 + bb);//设定位置
                Dw.Location = new Point(150 * aa, 180 + bb);//设定位置
                Db.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["SPhotoName"].ToString());
                Df.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["BPhotoName"].ToString());
                Db.SizeMode = PictureBoxSizeMode.StretchImage; //设定图像如何显示
                Df.SizeMode = PictureBoxSizeMode.StretchImage;
                Db.Size = new Size(270, 150);
                Df.Size = new Size(272, 49);
                Db.BorderStyle = BorderStyle.FixedSingle; //设置边框样式
                Db.Cursor = Cursors.Hand;
                //this.Controls.Add(Db);
                panel1.Controls.Add(Db); //绑定到panel1上
                panel1.Controls.Add(Df);
                panel1.Controls.Add(Dw);
                Db.Click += new EventHandler(this.pictureBox_MouseClick); //添加单击事件
                aa++;
                if (aa++ >= 4)
                {
                    aa = 0;
                    bb += 240;
                }
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
           
            didanguanli ss = new didanguanli();
            Haa(ss);
            panel9.Visible = true;
        }
        public void Daa(Form d)
        {
            panel1.Controls.Clear();
            d.TopLevel = false;
            d.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //d.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(d);
            d.Show();
        }
        public void Haa(Form d)
        {
            panel9.Controls.Clear();
            d.TopLevel = false;
            d.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //d.Dock = System.Windows.Forms.DockStyle.Fill;
            panel9.Controls.Add(d);
            d.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {
           
            panel9.Visible = false;

        }

        private void label10_MouseEnter(object sender, EventArgs e)
        {
            label10.ForeColor = Color.Red;
        }

        private void label10_MouseLeave(object sender, EventArgs e)
        {
            label10.ForeColor = Color.White;
        }

        private void pictureBox2_Click(object sender, EventArgs e)//果果乐
        {
            panel9.Visible = true;

        }
        private void label12_Click(object sender, EventArgs e)
        {
            Gerenzhongx kk = new Gerenzhongx();
            Haa(kk);
            panel9.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
             panel9.Visible = true;
        }

        private void label11_MouseEnter(object sender, EventArgs e)
        {
            label11.ForeColor = Color.Red;
        }

        private void label11_MouseLeave(object sender, EventArgs e)
        {
            label11.ForeColor = Color.White;
        }

        private void label12_MouseEnter(object sender, EventArgs e)
        {
            label12.ForeColor = Color.Red;
        }

        private void label12_MouseLeave(object sender, EventArgs e)
        {
            label12.ForeColor = Color.White;
        }

      


        private void yonghu_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnimateWindow(this.Handle, 1000, AW_VER_NEGATIVE | AW_VER_NEGATIVE);
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            int aa = 0;
            int bb = 0;
            DataTable objDataTable = DBHelper.GetDataTable("select * from  MerchantsTable where MerchantsName like '%"+textBox1.Text.ToString().Trim()+"%'");
            for (int i = 0; i < objDataTable.Rows.Count; i++)
            {

                label13.Visible = false;
                pictureBox2.Visible = false;
                Label Dw = new Label();
                PictureBox Df = new PictureBox();
                PictureBox Db = new PictureBox(); //实例化一个控件
                Db.Name = "Db_PictureBox" + i;//设定名称
                Df.Name = "Df_PictureBox" + i;//设定名称
                Dw.Name = "Dw_Label" + i;//设定名称
                Db.Text = objDataTable.Rows[i]["MerchantsID"].ToString();
                Dw.Text = objDataTable.Rows[i]["MerchantsName"].ToString();
                Dw.AutoSize = true;
                Dw.Font = new Font("微软雅黑", 15);
                Dw.Size = new System.Drawing.Size(121, 25);
                Db.Location = new Point(150 * aa, 20 + bb);//设定位置
                Df.Location = new Point(150 * aa, 210 + bb);//设定位置
                Dw.Location = new Point(150 * aa, 180 + bb);//设定位置
                Db.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["SPhotoName"].ToString());
                Df.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["BPhotoName"].ToString());
                Db.SizeMode = PictureBoxSizeMode.StretchImage; //设定图像如何显示
                Df.SizeMode = PictureBoxSizeMode.StretchImage;
                Db.Size = new Size(270, 150);
                Df.Size = new Size(272, 49);
                Db.BorderStyle = BorderStyle.FixedSingle; //设置边框样式
                Db.Cursor = Cursors.Hand;
                //this.Controls.Add(Db);
                panel1.Controls.Add(Db); //绑定到panel1上
                panel1.Controls.Add(Df);
                panel1.Controls.Add(Dw);
                Db.Click += new EventHandler(this.pictureBox_MouseClick); //添加单击事件
                aa++;
                if (aa++ >= 4)
                {
                    aa = 0;
                    bb += 240;
                }
            }
            //if (i==0)
            //{
            //    Label Dw2 = new Label();
            //    Dw2.Name = "Dw_Label_Null";//设定名称
            //    Dw2.Text = string.Format("没有找到你想要的店铺哦");
            //    Dw2.AutoSize = true;
            //    Dw2.Font = new Font("微软雅黑", 15);
            //    Dw2.Size = new System.Drawing.Size(121, 25);
            //    Dw2.Location = new Point(150 * aa, 180 + bb);//设定位置
            //}
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }


       
       

    }
}
