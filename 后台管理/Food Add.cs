using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace 雪莉轻食
{
    public partial class Form5 : Form
    {
        public static Form5 FA;
        public Form5()
        {
            InitializeComponent();
            FA = this;
        }
        public static string nj;
        public static string name ;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            nj = "";
            name = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                nj = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(nj);
            }
        }
        public bool Fag()
        {
            if (this.comboBox1.SelectedItem.ToString() == "")
            {
                MessageBox.Show("请选择店铺", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.textBox1.Text == "" || this.textBox1.Text.Length >11)
            {
                MessageBox.Show("菜品名称不能为空且长度不能超过11位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                string SQLstr = "select * from FoodTable where FoodName='"+this.textBox1.Text.ToString().Trim()+"'";
                DataSet dt = GetDataSet(SQLstr);
                if (dt.Tables.Count == 0 || dt.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("该菜品已经存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if(this.textBox3.Text==""||decimal.Parse( this.textBox3.Text)>1000)
            {
                MessageBox.Show("菜品价格不能为空且价格不能超过1000元", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (decimal.Parse( this.textBox3.Text)<=0)
            {
                MessageBox.Show("菜品价格必须大于0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if(this.comboBox1.SelectedItem.ToString()=="请选择")
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
        public bool StrSQl(string SQL)
        {
            SqlConnection objCon = new SqlConnection(@"Data Source=(local);database=SherryEatsDB;Integrated Security=true;");
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

        private void Form5_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedItem = "请选择";
            this.comboBox2.SelectedItem = "请选择";
            comboBox3.Items.Clear();
            comboBox3.DisplayMember = "MerchantsName";
            comboBox3.ValueMember = "MerchantsID";
            comboBox3.DataSource = DBHelper.GetDataTable("select * from MerchantsTable");
        }
        public DataSet GetDataSet(string strSQL)
        {
            SqlConnection objMyCon = new SqlConnection(@"Data Source=(local);database=SherryEatsDB;Integrated Security=true;");
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


        public SqlDataAdapter GetSqlDataAdapter(string strSQL)
        {
            SqlConnection objMyCon = new SqlConnection(@"Data Source=(local);database=SherryEatsDB;Integrated Security=true;");
            try
            {
                //if (objMyCon.State == ConnectionState.Closed)
                    objMyCon.Open();
                SqlCommand objMyCom = new SqlCommand(strSQL, objMyCon);
                SqlDataAdapter da = new SqlDataAdapter(objMyCom);
               
                return da;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return new SqlDataAdapter();
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Fag() == false)
                return;
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
            string SQLst = "insert into FoodTable(FoodName,FoodPrice,FoodIn,FoodType,FoodState,MerchantsID,PhotoName,PhotoPath,HPhotoName)values('" + this.textBox1.Text.ToString().Trim() + "','" + this.textBox3.Text.ToString().Trim() + "','" + this.textBox2.Text.ToString().Trim() + "','" + this.comboBox1.SelectedItem.ToString() + "','" + this.comboBox2.SelectedItem.ToString() + "'," + int.Parse(comboBox3.SelectedValue.ToString()) + ",'" + FileName + "','" + FilePath + "','无标题1.png')";
            if (StrSQl(SQLst) == true)
            {
                MessageBox.Show("菜品添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main_Form.GTY.button1_Click(null,null);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
