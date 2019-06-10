using System;
using System.Data;
using System.Windows.Forms;
namespace 雪莉轻食
{
    public partial class GWC : Form
    {
        public static GWC gg;
        public GWC( )
        {
            InitializeComponent();
            gg = this;
        }
        decimal danja;
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable objkDataTable12 = DBHelper.GetDataTable("select * from  ShoppingTable where MerchantsID =" + UserMain.DA);
            if (decimal.Parse(objkDataTable12.Rows[0]["ShoppingPrice"].ToString())== 0)
             {
                 MessageBox.Show("请输入菜品数量","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                 return;
             }
            quereng dd = new quereng();
            UserMain.form1.Haa(dd);
        }
      
        private void GWC_Load(object sender, EventArgs e)
        {
            dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView2.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;

            DataTable objkDataTable1 = DBHelper.GetDataTable("select * from  ShoppingTable a inner join FoodTable b on a.FoodID=b.FoodID where a.MerchantsID=" + UserMain.DA);
            for (int i = 0; i < objkDataTable1.Rows.Count; i++)
            {
                danja += int.Parse(objkDataTable1.Rows[i]["ShoppingCount"].ToString()) * decimal.Parse(objkDataTable1.Rows[i]["FoodPrice"].ToString());
            }
            DBHelper.SaveData("update ShoppingTable set ShoppingPrice='" + danja + "' where MerchantsID =" + UserMain.DA);
            danja = 0;
            DataTable objkDataTable12 = DBHelper.GetDataTable("select * from  ShoppingTable where MerchantsID =" + UserMain.DA);
            if (objkDataTable12.Rows.Count > 0)
            {
                label4.Text = objkDataTable12.Rows[0]["ShoppingPrice"].ToString() + "¥";
            }
            this.button1.Text = "结算";
            dataGridView2.RowHeadersVisible = false; //隐藏前面空白选择部分
            // 禁止用户改变DataGridView1的所有列的列宽
            dataGridView2.AllowUserToResizeColumns = false;
            //禁止用户改变DataGridView1所有行的行高
            dataGridView2.AllowUserToResizeRows = false;
            dataGridView2.Columns[0].ReadOnly = true; //禁止用户编辑第一行
            dataGridView2.Columns[2].ReadOnly = true;//禁止用户编辑第三行
            this.dataGridView2.AllowUserToResizeColumns = false; //禁止用户拖动标题宽度
            DataTable objkDataTable = DBHelper.GetDataTable("select * from  ShoppingTable a inner join FoodTable b on a.FoodID=b.FoodID where a.MerchantsID=" + UserMain.DA);

            if (objkDataTable.Rows.Count > 0)
            {
                 dataGridView2.Rows.Clear();
                for (int i = 0; i < objkDataTable.Rows.Count; i++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = objkDataTable.Rows[i]["FoodName"].ToString();
                    dataGridView2.Rows[i].Cells[1].Value = objkDataTable.Rows[i]["ShoppingCount"].ToString();
                    dataGridView2.Rows[i].Cells[2].Value = objkDataTable.Rows[i]["FoodPrice"].ToString();
                    dataGridView2.Rows[i].Cells[3].Value = objkDataTable.Rows[i]["FoodID"].ToString();
                    danja += int.Parse(objkDataTable.Rows[i]["ShoppingCount"].ToString()) * decimal.Parse(objkDataTable.Rows[i]["FoodPrice"].ToString());
                }
                DBHelper.SaveData("update ShoppingTable set ShoppingPrice='" + danja + "' where MerchantsID =" + UserMain.DA);
            }
            if (dataGridView2.Rows.Count == 0)
            {
                button1.Visible = false;
            }
            else
            {
                button1.Visible = true;
            }
            dataGridView2.ClearSelection(); //取消默认选中
            
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 1)
                {
                    this.dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;//将当前单元格设为可读
                    this.dataGridView2.CurrentCell = this.dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex];//获取当前单元格
                    this.dataGridView2.BeginEdit(true);//将单元格设为编辑状态
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }
        int aa;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //for (int i = 0; i < dataGridView2.Rows.Count; i++)
            //{

            //    if (dataGridView2.Rows[i].Cells[1].Value == null)
            //    {
            //        aa = 0;
            //    }
            //    else
            //    {
            //        aa = int.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString());
            //    }
            //    DBHelper.SaveData("update ShoppingTable set ShoppingCount='" + aa + "' where FoodID =" + int.Parse(dataGridView2.Rows[i].Cells[3].Value.ToString()));
            //}
            //DataTable objkDataTable1 = DBHelper.GetDataTable("select * from  ShoppingTable a inner join FoodTable b on a.FoodID=b.FoodID where a.MerchantsID=" + yonghu.DA);
            //for (int i = 0; i < objkDataTable1.Rows.Count; i++)
            //{
            //    danja += int.Parse(objkDataTable1.Rows[i]["ShoppingCount"].ToString()) * decimal.Parse(objkDataTable1.Rows[i]["FoodPrice"].ToString());
            //}
            //DBHelper.SaveData("update ShoppingTable set ShoppingPrice='" + danja + "' where MerchantsID =" + yonghu.DA);
            //DataTable objkDataTable = DBHelper.GetDataTable("select * from  ShoppingTable where MerchantsID =" + yonghu.DA);
            //if (objkDataTable.Rows.Count > 0)
            //{
            //    label4.Text = "¥" + objkDataTable.Rows[1]["ShoppingPrice"].ToString() + "元";
            //}
        }

        public DataGridViewTextBoxEditingControl dgvTxt = null; // 声明 一个 CellEdit  

        // 自定义事件KeyPress事件
        private void Cells_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressXS(e, dgvTxt);
        }
        public static void keyPressXS(KeyPressEventArgs e, DataGridViewTextBoxEditingControl dgvTxt)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;       //让操作生效
                int j = 0;
                int k = 0;
                bool flag = false;
                if (dgvTxt.Text.Length == 0)
                {
                    if (e.KeyChar == '.')
                    {
                        e.Handled = true;             //让操作失效
                    }
                }
                for (int i = 0; i < dgvTxt.Text.Length; i++)
                {
                    if (dgvTxt.Text[i] == '.')
                    {
                        j++;
                        flag = true;
                    }
                    if (flag)
                    {
                        if (char.IsNumber(dgvTxt.Text[i]) && e.KeyChar != (char)Keys.Back)
                        {
                            k++;
                        }
                    }
                    if (j >= 1)
                    {
                        if (e.KeyChar == '.')
                        {
                            e.Handled = true;             //让操作失效
                        }
                    }
                    if (k == 2)
                    {
                        if (char.IsNumber(dgvTxt.Text[i]) && e.KeyChar != (char)Keys.Back)
                        {
                            if (dgvTxt.Text.Length - dgvTxt.SelectionStart < 3)
                            {
                                if (dgvTxt.SelectedText != dgvTxt.Text)
                                {
                                    e.Handled = true;             ////让操作失效
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            dgvTxt = (DataGridViewTextBoxEditingControl)e.Control; // 赋值  
            dgvTxt.SelectAll();
            dgvTxt.KeyPress += Cells_KeyPress; // 绑定到事件   
        }

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView2.Rows[e.RowIndex].IsNewRow) return;
            decimal dci;
            if (e.ColumnIndex == 4)
            {
                if (e.FormattedValue != null && e.FormattedValue.ToString().Length > 0)
                {
                    if (!decimal.TryParse(e.FormattedValue.ToString(), out dci) || dci < 0)
                    {
                        e.Cancel = true;
                        MessageBox.Show("请输入数字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void dataGridView2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {


            string value =  dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value.ToString();

          
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value);//如果成功就是小数
                DBHelper.SaveData("update ShoppingTable set ShoppingCount='" + int.Parse(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value.ToString()) + "' where FoodID =" + int.Parse(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[3].Value.ToString()));

            }
            catch
            {
                MessageBox.Show("不能为小数", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value = "0";
                aa = 0;
                DBHelper.SaveData("update ShoppingTable set ShoppingCount='" + aa + "' where FoodID =" + int.Parse(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[3].Value.ToString()));
                DataTable objkDataTable11 = DBHelper.GetDataTable("select * from  ShoppingTable a inner join FoodTable b on a.FoodID=b.FoodID where a.MerchantsID=" + UserMain.DA);
                for (int i = 0; i < objkDataTable11.Rows.Count; i++)
                {
                    danja += int.Parse(objkDataTable11.Rows[i]["ShoppingCount"].ToString()) * decimal.Parse(objkDataTable11.Rows[i]["FoodPrice"].ToString());
                }
                DBHelper.SaveData("update ShoppingTable set ShoppingPrice='" + danja + "' where MerchantsID =" + UserMain.DA);
                danja = 0;
                DataTable objkDataTable8 = DBHelper.GetDataTable("select * from  ShoppingTable where MerchantsID =" + UserMain.DA);
                if (objkDataTable8.Rows.Count > 0)
                {
                    label4.Text = "¥" + objkDataTable8.Rows[0]["ShoppingPrice"].ToString() + "元";
                }
                return;
            }
            if (int.Parse(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value.ToString()) > 100)
            {
                MessageBox.Show("数量不能超过100份", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value = 0;
                aa = 0;
                DBHelper.SaveData("update ShoppingTable set ShoppingCount='" + aa + "' where FoodID =" + int.Parse(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[3].Value.ToString()));
                DataTable objkDataTable11 = DBHelper.GetDataTable("select * from  ShoppingTable a inner join FoodTable b on a.FoodID=b.FoodID where a.MerchantsID=" + UserMain.DA);
                for (int i = 0; i < objkDataTable11.Rows.Count; i++)
                {
                    danja += int.Parse(objkDataTable11.Rows[i]["ShoppingCount"].ToString()) * decimal.Parse(objkDataTable11.Rows[i]["FoodPrice"].ToString());
                }
                DBHelper.SaveData("update ShoppingTable set ShoppingPrice='" + danja + "' where MerchantsID =" + UserMain.DA);
                danja = 0;
                DataTable objkDataTable8 = DBHelper.GetDataTable("select * from  ShoppingTable where MerchantsID =" + UserMain.DA);
                if (objkDataTable8.Rows.Count > 0)
                {
                    label4.Text = "¥" + objkDataTable8.Rows[0]["ShoppingPrice"].ToString() + "元";
                }
                return;
            }
            DataTable objkDataTable1 = DBHelper.GetDataTable("select * from  ShoppingTable a inner join FoodTable b on a.FoodID=b.FoodID where a.MerchantsID=" + UserMain.DA);
                for (int i = 0; i < objkDataTable1.Rows.Count; i++)
                {
                    danja += int.Parse(objkDataTable1.Rows[i]["ShoppingCount"].ToString()) * decimal.Parse(objkDataTable1.Rows[i]["FoodPrice"].ToString());
                }
                DBHelper.SaveData("update ShoppingTable set ShoppingPrice='" + danja + "' where MerchantsID =" + UserMain.DA);
                danja = 0;
                DataTable objkDataTable = DBHelper.GetDataTable("select * from  ShoppingTable where MerchantsID =" + UserMain.DA);
                if (objkDataTable.Rows.Count > 0)
                {
                    label4.Text = "¥" + objkDataTable.Rows[0]["ShoppingPrice"].ToString() + "元";
                }
            return;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (DBHelper.SaveData("delete from ShoppingTable where MerchantsID=" + UserMain.DA) > 0)
            {
                DataTable objkDataTable7 = DBHelper.GetDataTable("select FoodName,ShoppingCount,FoodPrice from  ShoppingTable where MerchantsID =" + UserMain.DA);
                dataGridView2.DataSource = objkDataTable7;
                label4.Text = "¥" + "0" + "元";
                button1.Visible = false;
                MessageBox.Show("清空成功","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            //else
            //{
            //    MessageBox.Show("清空失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

    }
}
