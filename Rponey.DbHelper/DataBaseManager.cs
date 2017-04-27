using System;
using System.Configuration;
using RPoney.Data.Contract;
using RPoney.Data.SqlClient;

namespace RPoney.DbHelper
{
    /// <summary>
    /// 数据库管理
    /// </summary>
    public static class DataBaseManager
    {
        public static string GetCountAsGroup(string tbName, string where) => $"select count(1) from (select count(1) as a1 from {tbName} where 1=1 {where}) as t1";

        public static string GetCountString(string tbName, string where) => $"select count(1) from {tbName} where 1=1 {where}";
        /// <summary>
        /// 获取数据库当前时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDbDate()
        {
            return DateTime.Parse(MainDb().ExecuteScalar("select getdate()").ToString());
        }
        /// <summary>
        /// 获取分页SQL
        /// </summary>
        /// <param name="tbName">表名</param>
        /// <param name="filter">字段</param>
        /// <param name="orderFile">排序</param>
        /// <param name="where">条件</param>
        /// <param name="page">页码</param>
        /// <param name="size">页数</param>
        /// <returns></returns>
        public static string GetPageString(string tbName, string filter, string orderFile, string where, int page, int size, bool selectAll = false)
        {
            if (!selectAll)
            {
                string[] strArray = { (((page - 1) * size) + 1).ToString(), (page * size).ToString(), tbName, where, orderFile, filter };
                var format = "\r\n With tmp_tb as(select ROW_NUMBER() Over(Order By {4} ) as wsj_rb, {5} from {2} where 1=1 {3}  )\r\n                select * from tmp_tb where wsj_rb between {0} and {1}\r\n            ";
                return string.Format(format, strArray);
            }
            else
            {
                string[] strArray = new string[] { tbName, where, orderFile, filter };
                var format = "SELECT {3} FROM {0} WHERE 1=1 {1} ORDER BY {2}  ";
                return string.Format(format, strArray);
            }
        }

        public static IDbHelper MainDb()
        {
            return MainDb(ConfigurationManager.AppSettings["DBCFileName"]);
        }

        public static IDbHelper MainDb(string dBcFileName)
        {
            return SqlHelper.FromFile(dBcFileName, true);
        }

        public static string SqlEscape(this string input, string escapeChar)
        {
            return string.IsNullOrEmpty(input) ? "" : input.Trim().Replace("'", escapeChar + "'").Replace("_", escapeChar + "_").Replace("%", escapeChar + "%").Replace("[", escapeChar + "[").Replace("]", escapeChar + "]");
        }
    }
}
