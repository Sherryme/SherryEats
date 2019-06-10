using System;
using System.Data;
using System.Windows.Forms;

namespace 雪莉轻食
{
    public partial class quereng : Form
    {
        public static quereng jj;
        public quereng()
        {
            InitializeComponent();
            jj = this;
        }
        public void Haa(Form d)
        {
            panel1.Controls.Clear();
            d.TopLevel = false;
            d.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //d.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(d);
            d.Show();
        }
        private void quereng_Load(object sender, EventArgs e)
        {
            timer1_Tick(null,null);
            DataTable objkDataTable7 = DBHelper.GetDataTable("select * from  ShoppingTable where MerchantsID =" + UserMain.DA);
            label7.Text = "¥" + objkDataTable7.Rows[0]["ShoppingPrice"].ToString();
            DataTable objDataTable = DBHelper.GetDataTable("select * from  MerchantsTable where MerchantsID=" + UserMain.DA);
            label1.Text = objDataTable.Rows[0]["MerchantsName"].ToString() + " >确认购买";
            GWE aa = new GWE();
            Haa(aa);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Xiugaics kkk = new Xiugaics();
            kkk.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DataTable objDataTable5 = DBHelper.GetDataTable("select * from  UserTable where UserID=" + Login.tt);
            label4.Text = objDataTable5.Rows[0]["UserName"].ToString() + " : " + objDataTable5.Rows[0]["Phone"].ToString();
            label5.Text = objDataTable5.Rows[0]["Address0"].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable objDataTable91 = DBHelper.GetDataTable("select * from  UserTable where UserID=" + Login.tt);
            if (objDataTable91.Rows[0]["UserName"].ToString()=="" || objDataTable91.Rows[0]["Phone"].ToString() == "" || objDataTable91.Rows[0]["Address0"].ToString() == "")
            {
                MessageBox.Show("地址信息不完整，请填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.textBox1.Text.Length > 25)
            {
                MessageBox.Show("用户备注不能超过25个字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string RQ = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); //下单日期
            string Bianhao = DateTime.Now.ToString("yyyyMMddhhmmss") + new Random().Next(1000, 9999);  //订单编号
            DataTable objkDataTable7 = DBHelper.GetDataTable("select * from  ShoppingTable where MerchantsID =" + UserMain.DA + "and ShoppingCount>0 ");
            if (objkDataTable7.Rows.Count > 0)
            {
                DataTable objDataTable9 = DBHelper.GetDataTable("select * from  UserTable where UserID=" + Login.tt);
                if (DBHelper.SaveData("insert into TheOrderTable(OrderNumber,UserID,ShoppingPrice,ShoppingNote,SaleDate,Address0,MerchantsID,Name,XPhone)values('" + Bianhao + "'," + Login.tt + ",'" + decimal.Parse(objkDataTable7.Rows[0]["ShoppingPrice"].ToString()) + "','" + textBox1.Text.ToString().Trim() + "','" + RQ + "','" + objDataTable9.Rows[0]["Address0"].ToString() + "'," + UserMain.DA + ",'" + objDataTable9.Rows[0]["UserName"].ToString() + "','" + objDataTable9.Rows[0]["Phone"].ToString() + "')") > 0)
                {

                    DataTable objDataTable5 = DBHelper.GetDataTable("select * from  TheOrderTable where OrderNumber=" + Bianhao);
                    DBHelper.SaveData("insert into OrderStateTable(OrderNumber,OrderState)values('" + Bianhao + "','等待确认')");
                    for (int i = 0; i < objkDataTable7.Rows.Count; i++)
                    {
                        DataTable objDataTable88 = DBHelper.GetDataTable("select * from FoodTable where FoodID=" + objkDataTable7.Rows[i]["FoodID"].ToString() + "");
                        DBHelper.SaveData("insert into DetailedTheOrderTable(OrderID,OrderCount,SFoodName,FoodPrice)values('" + objDataTable5.Rows[0]["OrderID"].ToString() + "','" + objkDataTable7.Rows[i]["ShoppingCount"].ToString() + "','" + objDataTable88.Rows[0]["FoodName"].ToString() + "','" + objDataTable88.Rows[0]["FoodPrice"].ToString() + "')");
                    }
                    DBHelper.SaveData("delete from ShoppingTable where MerchantsID=" + UserMain.DA);
                    MessageBox.Show("支付成功","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    didanguanli gg = new didanguanli();
                    UserMain.form1.Haa(gg);
                    return;
                }
                else
                {
                    MessageBox.Show("购买失败","错误",MessageBoxButtons.AbortRetryIgnore,MessageBoxIcon.Stop);
                }
            }
           



        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    GWC.gg.button1.Visible = false;
        //    GWC.gg.label1.Visible = true;
        //    GWC.gg.dataGridView2.Columns[0].ReadOnly = true; //禁止用户编辑第一行
        //    GWC.gg.dataGridView2.Columns[1].ReadOnly = true; //禁止用户编辑第二行
        //    GWC.gg.dataGridView2.Columns[2].ReadOnly = true;//禁止用户编辑第三行
          
        //}
    }
}
