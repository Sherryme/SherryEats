using System;
using System.Windows.Forms;

namespace 雪莉轻食
{
    public partial class Xiugaics : Form
    {
        public Xiugaics()
        {
            InitializeComponent();
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (gg() == false)
                return;
           int a = DBHelper.SaveData("update UserTable set UserName='" + textBox1.Text.ToString() + "',Phone='" + textBox2.Text.ToString() + "',Address0='" + textBox3.Text.ToString() + "' where UserID =" + Login.tt);
           if (a > 0)
           {
               MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
               Close();
           }
           else
           {
               MessageBox.Show("保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
           }
        }
        private bool gg()
        {
            if (this.textBox1.ToString() == "" || this.textBox2.ToString() == "" || this.textBox3.Text.ToString() == "")
            {
                MessageBox.Show("姓名，手机号码，送餐地址不能为空","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }
            if(this.textBox1.Text.ToString().Length>7)
            {
                MessageBox.Show("姓名不能大于七位","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }
            if (this.textBox2.Text.ToString().Length != 11)
            {
                MessageBox.Show("手机号码必须为11位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
    }
}
