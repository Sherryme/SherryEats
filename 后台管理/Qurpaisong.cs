using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace 雪莉轻食
{
    public partial class Qurpaisong : Form
    {
        public Qurpaisong()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            int aa = 0;
            int bb = 0;
            int a = 0;
            int b = 0;
            DataTable objDataTable = DBHelper.GetDataTable("select * from  TheOrderTable a inner join OrderStateTable b on a.OrderNumber=b.OrderNumber where b.OrderState='等待派送'");
            for (int i = 0; i < objDataTable.Rows.Count; i++)
            {
                DataTable objDataTable1 = DBHelper.GetDataTable("select * from  TheOrderTable a inner join DetailedTheOrderTable b on a.OrderID=b.OrderID   where a.OrderID=" + int.Parse(objDataTable.Rows[i]["OrderID"].ToString()) + "");
                Panel Ha = new Panel();
                Ha.Name = "Panel" + i;
                Ha.BorderStyle = BorderStyle.FixedSingle;
                Ha.Location = new Point(196 * aa, 0 + bb);//设定位置
                Ha.Size = new Size(390, 493);
                panel1.Controls.Add(Ha);

                Label Hs = new Label();
                Hs.Name = "Label" + i;
                Hs.AutoSize = true;
                Hs.Font = new Font("微软雅黑", 15);
                Hs.Text = "订单编号：" + objDataTable.Rows[i]["OrderNumber"].ToString();
                Hs.Location = new Point(0, 2);//设定位置
                Ha.Controls.Add(Hs);

                Panel Hj = new Panel();
                Hj.Name = "Panel" + i;
                Hj.BorderStyle = BorderStyle.FixedSingle;
                Hj.Location = new Point(0, 35);//设定位置
                Hj.Size = new Size(386, 188);
                Hj.AutoScroll = true;
                Ha.Controls.Add(Hj);

                Label yy = new Label();
                yy.Name = "Label" + i;
                yy.AutoSize = true;
                yy.Font = new Font("微软雅黑", 15);
                yy.Text = "总计：¥" + objDataTable.Rows[i]["ShoppingPrice"].ToString();
                yy.Location = new Point(100, 230);//设定位置
                Ha.Controls.Add(yy);

                GroupBox LM = new GroupBox();
                LM.Name = "GroupBox" + i;
                LM.Location = new Point(0, 250);//设定位置
                LM.Font = new Font("微软雅黑", 15);
                LM.Size = new Size(400, 210);
                LM.Text = "联系地址";
                Ha.Controls.Add(LM);

                Label rr = new Label();
                rr.Name = "Label" + i;
                rr.AutoSize = true;
                rr.Font = new Font("微软雅黑", 12);
                rr.Text = "收货人： " + objDataTable.Rows[i]["Name"].ToString();
                rr.Location = new Point(25, 45);//设定位置
                LM.Controls.Add(rr);

                Label ee = new Label();
                ee.Name = "Label" + i;
                ee.AutoSize = true;
                ee.Font = new Font("微软雅黑", 12);
                ee.Text = "联系电话： " + objDataTable.Rows[i]["XPhone"].ToString();
                ee.Location = new Point(10, 85);//设定位置
                LM.Controls.Add(ee);

                Label pp = new Label();
                pp.Name = "Label" + i;
                pp.AutoSize = true;
                pp.Font = new Font("微软雅黑", 12);
                string nn = "联系地址： " + objDataTable.Rows[i]["Address0"].ToString();
                pp.Text = nn.Length > 23 ? nn.Substring(0, 23) + "..." : nn;
                pp.Location = new Point(10, 130);//设定位置
                LM.Controls.Add(pp);

                Label ww = new Label();
                ww.Name = "Label" + i;
                ww.AutoSize = true;
                ww.Font = new Font("微软雅黑", 12);
                if (objDataTable.Rows[i]["ShoppingNote"].ToString() == "")
                {
                    ww.Text = "用户备注： " + "无";
                }
                else
                {
                    ww.Text = "用户备注： " + objDataTable.Rows[i]["ShoppingNote"].ToString();
                }
                ww.Location = new Point(10, 170);//设定位置
                LM.Controls.Add(ww);


                Button DG = new Button();
                DG.Name = objDataTable.Rows[i]["OrderID"].ToString();//设定名称
                DG.Text = "确认派送";
                DG.Font = new Font("微软雅黑", 11);
                DG.Location = new Point(0, 459);//设定位置
                DG.Cursor = Cursors.Hand;
                DG.Size = new System.Drawing.Size(389, 30);
                DG.BackColor = Color.Red;
                DG.ForeColor = Color.Transparent;
                DG.FlatStyle = FlatStyle.Flat;
                DG.Click += new EventHandler(this.Button_Click); //添加单击事件
                Ha.Controls.Add(DG);


                b = 0;
                for (int j = 0; j < objDataTable1.Rows.Count; j++)
                {

                    Label gg = new Label();
                    gg.Name = "Label" + i;
                    gg.AutoSize = true;
                    gg.Font = new Font("微软雅黑", 15);
                    gg.Text = objDataTable1.Rows[j]["SFoodName"].ToString();
                    gg.Location = new Point(3, 2 + b);//设定位置
                    Hj.Controls.Add(gg);

                    Label tt = new Label();
                    tt.Name = "Label" + i;
                    tt.AutoSize = true;
                    tt.Font = new Font("微软雅黑", 15);
                    tt.Text = "x " + objDataTable1.Rows[j]["OrderCount"].ToString();
                    tt.Location = new Point(185, 2 + b);//设定位置
                    Hj.Controls.Add(tt);

                    Label jj = new Label();
                    jj.Name = "Label" + i;
                    jj.AutoSize = true;
                    jj.Font = new Font("微软雅黑", 15);
                    jj.Text = "¥" + objDataTable1.Rows[j]["FoodPrice"].ToString();
                    jj.Location = new Point(285, 2 + b);//设定位置
                    Hj.Controls.Add(jj);
                    b += 40;

                }
                aa++;
                if (aa++ >= 3)
                {
                    aa = 0;
                    bb += 520;
                }
            }

        }
        public static int LL = 0;
        private void Button_Click(object sender, EventArgs e)
        {
            Button b2 = (Button)sender;//将触发此事件的对象转换为该对象
            LL = int.Parse(b2.Name.ToString());
            DataTable objDataTable2 = DBHelper.GetDataTable("select * from  TheOrderTable where OrderID=" + LL + "");


            if (DBHelper.SaveData("update OrderStateTable set OrderState='派送中'where OrderNumber='" + objDataTable2.Rows[0]["OrderNumber"].ToString() + "'") > 0)
            {
                MessageBox.Show("订单确认成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Main_Form.GTY.button5_Click(null, null);
            }
            else
            {
                MessageBox.Show("订单确认失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }
    }
}
