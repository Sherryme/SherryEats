using System;
using System.Data;
using System.Windows.Forms;

namespace 雪莉轻食
{
    public partial class GWE : Form
    {
        public GWE()
        {
            InitializeComponent();
        }
         decimal danja;
         private void GWE_Load(object sender, EventArgs e)
         {
             dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
             dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
             dataGridView2.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;



             dataGridView2.RowHeadersVisible = false; //隐藏前面空白选择部分
             // 禁止用户改变DataGridView1的所有列的列宽
             dataGridView2.AllowUserToResizeColumns = false;
             //禁止用户改变DataGridView1所有行的行高
             dataGridView2.AllowUserToResizeRows = false;
             dataGridView2.Columns[0].ReadOnly = true; //禁止用户编辑第一行
             dataGridView2.Columns[1].ReadOnly = true; //禁止用户编辑第一行
             dataGridView2.Columns[2].ReadOnly = true;//禁止用户编辑第三行
             this.dataGridView2.AllowUserToResizeColumns = false; //禁止用户拖动标题宽度
             DataTable objkDataTable = DBHelper.GetDataTable("select * from  ShoppingTable a inner join FoodTable b on a.FoodID=b.FoodID where a.MerchantsID=" + UserMain.DA + " and  ShoppingCount>0");
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
                 dataGridView2.ClearSelection(); //取消默认选中
             }
             DataTable objkDataTable1 = DBHelper.GetDataTable("select * from  ShoppingTable where MerchantsID =" + UserMain.DA);
             if (objkDataTable1.Rows.Count > 0)
             {
                 label4.Text = "¥" + objkDataTable1.Rows[0]["ShoppingPrice"].ToString() + "元";
             }
         }
    }
}
