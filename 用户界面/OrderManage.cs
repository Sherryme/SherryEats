using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace 雪莉轻食
{
    public partial class didanguanli : Form
    {
        public didanguanli()
        {
            InitializeComponent();
        }
       public static int GG;
        private void panel1_MouseClick(object sender, EventArgs e)
        {
            Panel b1 = (Panel)sender;//将触发此事件的对象转换为该对象
            GG= int.Parse(b1.Text);
            XQ kk = new XQ();
            kk.ShowDialog();

        }
        private void didanguanli_Load(object sender, EventArgs e)
        {
            
            int aa = 0;
            int bb = 0;
            DataTable objDataTable = DBHelper.GetDataTable("select * from  TheOrderTable a inner join MerchantsTable b on a.MerchantsID=b.MerchantsID where a.UserID=" + Login.tt + " order by a.SaleDate desc ");
            for (int i = 0; i < objDataTable.Rows.Count; i++)
            {
                DataTable objDataTable1 = DBHelper.GetDataTable("select * from  OrderStateTable where OrderNumber='" + objDataTable.Rows[i]["OrderNumber"].ToString() + "'");
                label5.Visible = false;
                pictureBox2.Visible = false;
                Label DK = new Label();
                Label DY = new Label();
                Label DP = new Label();
                Label DQ = new Label();
                Label Dw = new Label();
                Label DD = new Label();
                Panel HH = new Panel();
                PictureBox Db = new PictureBox(); //实例化一个控件
                Db.Name = "Db_PictureBox" + i;//设定名称
                HH.Name = "Panel" + i;//设定名称
                Dw.Name = "Label" + i;//设定名称
                DD.Name = "Label" + i;//设定名称
                DQ.Name = "Label" + i;//设定名称
                DP.Name = "Label" + i;//设定名称
                DY.Name = "Label" + i;//设定名称
                DK.Name = "Label" + i;//设定名称
                HH.Text = objDataTable.Rows[i]["OrderID"].ToString();
                DD.Text = objDataTable.Rows[i]["SaleDate"].ToString();
                DQ.Text ="订单编号： "+ objDataTable.Rows[i]["OrderNumber"].ToString();
                DP.Text = objDataTable.Rows[i]["SPhone"].ToString();
                DY.Text = " ¥ " + objDataTable.Rows[i]["ShoppingPrice"].ToString();
                DK.Text = objDataTable1.Rows[0]["OrderState"].ToString();
                Dw.Size = new System.Drawing.Size(121, 25);
                Dw.Location = new Point(200, 30);//设定位置
                DP.Location = new Point(500, 55);//设定位置
                DY.Location = new Point(670, 55);//设定位置
                DK.Location = new Point(810, 55);//设定位置
                DD.Location = new Point(200, 90);//设定位置
                DQ.Location = new Point(420, 90);//设定位置
                Db.Location = new Point(15, 18 + bb);//设定位置
                HH.Location = new Point(5, 0 + bb);//设定位置
                Dw.AutoSize = true;
                DQ.AutoSize = true;
                DP.AutoSize = true;
                DD.AutoSize = true;
                DY.AutoSize = true;
                DK.AutoSize = true;
                Db.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\SHPhoto\" + objDataTable.Rows[i]["SPhotoName"].ToString());
                Db.SizeMode = PictureBoxSizeMode.StretchImage; //设定图像如何显示
                Dw.Text = objDataTable.Rows[i]["MerchantsName"].ToString();
                Dw.Font = new Font("微软雅黑", 15);
                DQ.Font = new Font("微软雅黑", 15);
                DD.Font = new Font("微软雅黑", 15);
                DP.Font = new Font("微软雅黑", 15);
                DY.Font = new Font("微软雅黑", 15);
                DK.Font = new Font("微软雅黑", 15);
                Db.Size = new Size(160, 110);
                HH.Size = new Size(908, 144);
                Db.BorderStyle = BorderStyle.FixedSingle; //设置边框样式
                HH.BorderStyle = BorderStyle.FixedSingle; //设置边框样式
                HH.BackColor = Color.White;
                DD.ForeColor = Color.Red;
                DY.ForeColor = Color.Red;
                DQ.ForeColor = Color.Green;
                DK.ForeColor = Color.Green;
                HH.Cursor = Cursors.Hand;
                panel1.Controls.Add(Db);
                panel1.Controls.Add(HH);
                HH.Controls.Add(Dw);
                HH.Controls.Add(DD);
                HH.Controls.Add(DQ);
                HH.Controls.Add(DP);
                HH.Controls.Add(DY);
                HH.Controls.Add(DK);
                HH.Click += new EventHandler(this.panel1_MouseClick); //添加单击事件
                HH.MouseEnter += new EventHandler(this.panel1_MouseEnter); //添加单击事件
                HH.MouseLeave += new EventHandler(this.panel1_MouseLeave); //添加单击事件
                aa++;
                if (aa++ >= 1)
                {
                    bb += 150;
                    //aa += 10;
                }
            }
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            Panel b1 = (Panel)sender;//将触发此事件的对象转换为该对象
            b1.BackColor = Color.WhiteSmoke;
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            Panel b1 = (Panel)sender;//将触发此事件的对象转换为该对象
            b1.BackColor = Color.White;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
             
        }
    }
}
