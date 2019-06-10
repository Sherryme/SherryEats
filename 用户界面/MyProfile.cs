using System;
using System.Data;
using System.Windows.Forms;

namespace 雪莉轻食
{
    public partial class Gerenzhongx : Form
    {
        public Gerenzhongx()
        {
            InitializeComponent();
        }

        private void Gerenzhongx_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Daa() == false)
                return;
            int a = DBHelper.SaveData("update UserTable set UserPassWord='" + this.textBox3.Text.ToString().Trim() + "' where UserID="+Login.tt);
            if (a > 0)
            {
                MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("修改失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }
        private bool Daa()
        {
           DataTable objDataTable1 = DBHelper.GetDataTable("select * from  UserTable where UserID="+Login.tt);
            if (textBox1.Text.ToString().Trim() == "" || textBox2.Text.ToString().Trim() == "" || textBox3.Text.ToString().Trim() == "")
            {
                MessageBox.Show("原密码.新密码.确认密码,不能为空","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }
            if (this.textBox1.Text.Length != 6 || this.textBox2.Text.Length != 6 || this.textBox3.Text.Length != 6)
            {
                MessageBox.Show("密码长度必须为6位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.textBox1.Text.ToString().Trim() != objDataTable1.Rows[0]["UserPassWord"].ToString())
            {
                MessageBox.Show("原密码输入错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (this.textBox2.Text.ToString().Trim() != this.textBox3.Text.ToString().Trim())
            {
                MessageBox.Show("前后输入密码不一致", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Taa() == false)
                return;
            int jj = DBHelper.SaveData("update UserTable set UserName='" + this.textBox4.Text.ToString().Trim() + "',Phone='" + this.textBox5.Text.ToString().Trim() + "',Address0='" + this.textBox6.Text.ToString().Trim() + "'where UserID=" + Login.tt);
            if (jj > 0)
            {
                MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("修改失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }
        private bool Taa()
        {
            if (this.textBox4.Text.ToString().Trim() == "" || this.textBox5.Text.ToString().Trim() == "" || this.textBox6.Text.ToString().Trim() == "")
            {
                MessageBox.Show("姓名.电话.收货地址，不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.textBox4.Text.Length > 7)
            {
                MessageBox.Show("姓名长度不能超过7位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.textBox5.Text.Length !=11)
            {
                MessageBox.Show("电话号码必须为11位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.textBox6.Text.Length > 30)
            {
                MessageBox.Show("用户备注不能超过30个字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DataTable objDataTable1 = DBHelper.GetDataTable("select * from  UserTable where UserID=" + Login.tt);
            label1.Text = objDataTable1.Rows[0]["UserName"].ToString();
            label11.Text = objDataTable1.Rows[0]["Phone"].ToString();
            label12.Text = objDataTable1.Rows[0]["Address0"].ToString();
                     
                     
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

    }
}
