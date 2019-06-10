using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace 雪莉轻食
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.DisplayMember = "MerchantsName";
            comboBox1.ValueMember = "MerchantsID";
            comboBox1.DataSource = DBHelper.GetDataTable(" select * from MerchantsTable");
            
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button b2 = (Button)sender;//将触发此事件的对象转换为该对象
           int HH = int.Parse(b2.Name.ToString());
           //MessageBox.Show(string.Format("{0}",HH));
           MessageBoxButtons messButton = MessageBoxButtons.YesNo;
           DialogResult dr = MessageBox.Show("确定要删除吗?", "提示", messButton);
           if (dr == DialogResult.Yes)//如果点击“确定”按钮
           {
               if (DBHelper.SaveData("delete from FoodTable where FoodID=" + HH + "") > 0)
               {
                   MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   comboBox1_SelectedIndexChanged(null, null);

               }
               else
               {
                   MessageBox.Show("删除失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
               }
           }
           else//如果点击“取消”按钮
           {

           }
          
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            int aa = 0;
            int bb = 0;
            DataTable objkDataTable = DBHelper.GetDataTable("select * from  FoodTable where MerchantsID=" + int.Parse(comboBox1.SelectedValue.ToString()) + "");
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
                DG.Name = objkDataTable.Rows[i]["FoodID"].ToString();//设定名称
                DY.Name = "Dw_Label" + i;//设定名称
                DD.Name = "Db_PictureBox" + i;//设定名称
                Va.Name = "Db_PictureBox" + i;//设定名称
                Db.Text = objkDataTable.Rows[i]["FoodID"].ToString();
                Dw.Text = "¥" + objkDataTable.Rows[i]["FoodPrice"].ToString() + "/份";
                DY.Text = objkDataTable.Rows[i]["FoodName"].ToString();
                Va.Text = objkDataTable.Rows[i]["FoodID"].ToString();
                DG.Text = "一键删除";
                Dw.AutoSize = true;
                Va.AutoSize = true;
                DY.AutoSize = true;
                Dw.Font = new Font("微软雅黑", 11);
                DG.Font = new Font("微软雅黑", 11);
                DY.Font = new Font("微软雅黑", 15);
                //Dw.Size = new System.Drawing.Size(121, 25);
                DG.Size = new System.Drawing.Size(222, 30);
                Db.Location = new Point(130 * aa, 20 + bb);//设定位置
                Dw.Location = new Point(130 * aa, 200 + bb);//设定位置
                DG.Location = new Point(130 * aa, 228 + bb);//设定位置
                DY.Location = new Point(130 * aa, 170 + bb);//设定位置
                DD.Location = new Point(130 * aa, 240 + bb);//设定位置
                Db.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\StudentPhoto\" + objkDataTable.Rows[i]["PhotoName"].ToString());
                DD.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\StudentPhoto\" + objkDataTable.Rows[i]["HPhotoName"].ToString());
                Db.SizeMode = PictureBoxSizeMode.StretchImage; //设定图像如何显示
                Db.Size = new Size(222, 138);
                Db.BorderStyle = BorderStyle.FixedSingle; //设置边框样式
                //Db.Cursor = Cursors.Hand;
                DG.Cursor = Cursors.Hand;
                DG.Click += new EventHandler(this.Button_Click); //添加单击事件
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
    }
}
