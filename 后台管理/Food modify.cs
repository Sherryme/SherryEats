using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace 雪莉轻食
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        public static string nj;
        public static string name;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            nj = "";
            name = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
               
                nj = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(nj);
                string FilePath = System.Windows.Forms.Application.StartupPath + @"\StudentPhoto";
                if (Directory.Exists(FilePath) == false)
                {
                    //判断文件夹是否存在如果不存在创建文件夹
                    Directory.CreateDirectory(FilePath);//创建文件夹
                }
                string FileName = DateTime.Now.ToString("yyyyMMddhhmmss") + new Random().Next(1000, 9999);
                //以当前时间和1000到9999的随机数合起来作为文件的名字
                FileName += nj.Substring(nj.LastIndexOf("."));
                File.Copy(nj, FilePath + @"\" + FileName);
                Jaa = FileName;
                Haa = FilePath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Fag() == false)
                return;
            string SQLst = "update FoodTable set FoodName='" + this.comboBox4.Text.ToString().Trim() + "', FoodPrice='" + this.textBox3.Text.ToString().Trim() + "',FoodIn='" + this.textBox2.Text.ToString().Trim() + "',FoodType='" + this.comboBox1.Text.ToString() + "',FoodState='" + this.comboBox2.SelectedItem.ToString() + "',PhotoName='" + Jaa + "',PhotoPath='" + Haa + "',HPhotoName='无标题1.png' where FoodID=" + Dd + " ";
            if ( DBHelper.SaveData(SQLst)>0)
            {
                MessageBox.Show("菜品修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Jaa = "";
                Haa = "";
            }
            else
            {
                MessageBox.Show("保存失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }






        }
        public bool Fag()
        {
            if (this.comboBox3.Text.ToString()== "请选择")
            {
                MessageBox.Show("请选择店铺", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.comboBox4.Text == "" || this.comboBox4.Text.Length > 11)
            {
                MessageBox.Show("菜品名称不能为空且长度不能超过11位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (this.textBox3.Text == "" || decimal.Parse(this.textBox3.Text) > 1000)
            {
                MessageBox.Show("菜品价格不能为空且价格不能超过1000元", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (decimal.Parse(this.textBox3.Text) <= 0)
            {
                MessageBox.Show("菜品价格必须大于0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.comboBox1.Text.ToString() == "请选择")
            {
                MessageBox.Show("请选择菜品分类", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.comboBox2.SelectedItem.ToString() == "请选择")
            {
                MessageBox.Show("请选择菜品状态", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text.ToString() != "请选择")
            {
                DataTable objDataTable = DBHelper.GetDataTable(" select * from  FoodTable where MerchantsID=" + int.Parse(comboBox3.SelectedValue.ToString()) + "");
                comboBox4.DisplayMember = "FoodName";
                comboBox4.ValueMember = "FoodID";
                comboBox4.DataSource = objDataTable;
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            this.comboBox2.SelectedItem = "请选择";
            comboBox3.Items.Clear();
            comboBox3.DisplayMember = "MerchantsName";
            comboBox3.ValueMember = "MerchantsID";
            comboBox3.DataSource = DBHelper.GetDataTable("select '0'as MerchantsID,'请选择'as MerchantsName union select  MerchantsID,MerchantsName from MerchantsTable");
        }
        public static int Dd;
        public static string Haa;
        public static string Jaa;
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text.ToString() != "请选择")
            {
                DataTable objkDataTable = DBHelper.GetDataTable("select * from  FoodTable where FoodID=" + int.Parse(comboBox4.SelectedValue.ToString())+ "");
                string aa = objkDataTable.Rows[0]["FoodType"].ToString();
                comboBox1.SelectedItem = aa;
                Dd = int.Parse(comboBox4.SelectedValue.ToString());
                textBox3.Text = objkDataTable.Rows[0]["FoodPrice"].ToString();
                pictureBox1.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\StudentPhoto\" + objkDataTable.Rows[0]["PhotoName"].ToString());
                Haa = objkDataTable.Rows[0]["PhotoPath"].ToString();
                Jaa = objkDataTable.Rows[0]["PhotoName"].ToString();
                textBox2.Text = objkDataTable.Rows[0]["FoodIn"].ToString();
                string a = objkDataTable.Rows[0]["FoodState"].ToString();
                comboBox2.SelectedItem = a;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main_Form.GTY.button2_Click(null,null);
            
        }
    }
}
