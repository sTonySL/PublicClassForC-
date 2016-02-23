using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace jwc
{
    class DataBase
    {
        //公共变量
        private static String _sql_con_str = "Data Source=.;Initial Catalog=jwc;Integrated Security=True";
        private SqlConnection Sql_Con;

        public DataBase() { }
        /// <summary>
        /// 该函数用来传入数据库连接字符串
        /// </summary>
        /// <param name="Sql_con_str">数据库连接字符串</param>
        public DataBase(String Sql_con_str)
        {
            _sql_con_str = Sql_con_str;
        }

        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <returns>返回SqlConnection对象</returns>
        public SqlConnection Con_Open()
        {
            Sql_Con = new SqlConnection(_sql_con_str);
            try
            {
                Sql_Con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Sql_Con;
        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        public void Con_Close()
        {
            if (Sql_Con.State == ConnectionState.Open)
            {
                Sql_Con.Close();
                Sql_Con.Dispose();
            }
        }
        #region 批量插入
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="tb">Datatable数据表</param>
        /// <param name="DistinationTable_Name">要插入的表的名称</param>
        public void BatchData_Insert(DataTable tb, String DistinationTable_Name)
        {
            SqlBulkCopy Batch_Insert = new SqlBulkCopy(Sql_Con);
            Batch_Insert.DestinationTableName = DistinationTable_Name;
            Batch_Insert.BatchSize = tb.Rows.Count;

            try
            {
                if (tb != null && tb.Rows.Count != 0)
                {
                    Batch_Insert.WriteToServer(tb);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Batch_Insert.Close();
            //System.GC.Collect();
        }
        #endregion

        #region 单条插入数据
        /// <summary>
        /// 执行一次插入语句，插入成功则返回true，失败则返回false
        /// </summary>
        /// <param name="notice">通知信息的实体类</param>
        /// /// <param name="DataTableName">数据插入的表的名称</param>
        /// <returns></returns>
        public bool InsertData(String Sql_str)
        {
            //String Insert_str = "insert into "+DataTableName+" (Title,Date,Context)values('" + notice.Title + "','" + notice.Date + "','" + notice.Content + "')";

            SqlCommand Sql_com = new SqlCommand(Sql_str, Sql_Con);
            //ExecuteNoneQuery()方法表示执行SQL语句并返回受影响的行数
            try
            {
                if (Sql_com.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion
    }
}
