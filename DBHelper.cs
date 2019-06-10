using System;
using System.Data;
using System.Data.SqlClient;
namespace 雪莉轻食
{
    class DBHelper
    {
        private const string strMyConText = @"Data Source=(local);database=SherryEatsDB;Integrated Security=true;";
        #region 保存数据
        /// <summary>
        /// 执行查询语句并且返回受影响的行数
        /// </summary>
        /// <param name="strSQL">执行的查询语句</param>
        /// <returns>返回受影响的行数</returns>
        public static int SaveData(string strSQL)
        {
            SqlConnection objMyCon = new SqlConnection(strMyConText);
            try
            {
                if (objMyCon.State == ConnectionState.Closed)
                    objMyCon.Open();
                SqlCommand objMyCom = new SqlCommand(strSQL, objMyCon);
                return objMyCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                if (objMyCon.State == ConnectionState.Open)
                    objMyCon.Close();
            }
        }
        #endregion

        #region 获得数据表集
        /// <summary>
        /// 执行查询语句返回数据表集
        /// </summary>
        /// <param name="strSQL">执行的查询语句</param>
        /// <returns>返回数据表集</returns>
        public static DataSet GetDataSet(string strSQL)
        {

            SqlConnection objMyCon = new SqlConnection(strMyConText);
            try
            {
                if (objMyCon.State == ConnectionState.Closed)
                    objMyCon.Open();
                SqlCommand objMyCom = new SqlCommand(strSQL, objMyCon);

                SqlDataAdapter da = new SqlDataAdapter(objMyCom);
                DataSet dt = new DataSet();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return new DataSet();
            }
            finally
            {
                if (objMyCon.State == ConnectionState.Open)
                    objMyCon.Close();
            }


        }

        #endregion

        #region 获得数据表
        /// <summary>
        /// 执行查询语句返回数据表
        /// </summary>
        /// <param name="strSQL">执行的查询语句</param>
        /// <returns>返回数据表</returns>
        public static DataTable GetDataTable(string strSQL)
        {

            SqlConnection objMyCon = new SqlConnection(strMyConText);
            try
            {
                if (objMyCon.State == ConnectionState.Closed)
                    objMyCon.Open();
                SqlCommand objMyCom = new SqlCommand(strSQL, objMyCon);

                SqlDataAdapter da = new SqlDataAdapter(objMyCom);
                DataSet dt = new DataSet();
                da.Fill(dt);
                return dt.Tables[0];
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally
            {
                if (objMyCon.State == ConnectionState.Open)
                    objMyCon.Close();
            }


        }

        #endregion

        #region 获得数据
        /// <summary>
        /// 执行查询语句返回第一列第一行的数据
        /// </summary>
        /// <param name="strSQL">执行的查询语句</param>
        /// <returns>返回数据</returns>
        public static string GetString(string strSQL)
        {

            SqlConnection objMyCon = new SqlConnection(strMyConText);
            try
            {
                if (objMyCon.State == ConnectionState.Closed)
                    objMyCon.Open();
                SqlCommand objMyCom = new SqlCommand(strSQL, objMyCon);

                return objMyCom.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                if (objMyCon.State == ConnectionState.Open)
                    objMyCon.Close();
            }


        }

        #endregion

    }
}
