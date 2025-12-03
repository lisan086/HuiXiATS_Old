using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Sql
{
    /// <summary>
    /// 数据库的连接
    /// </summary>
    public abstract class ABSSqlDBLianJie
    {

        /// <summary>
        /// 数据库类型
        /// </summary>
        public abstract SQLType SQLType { get; }

        /// <summary>
        /// 更新或者增加语句的执行，返回大于0说明更新成功，等于0 表示更新失败 小于0表示服务器没有连接上
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public abstract int UpdateOrInsertdate(string sql);

    
        /// <summary>
        /// 用于事务更新，返回true表示更新成功，否则失败
        /// </summary>
        /// <param name="sqllist"></param>
        /// <returns></returns>
        public abstract bool ShiWuUpdate(List<string> sqllist);

    
        /// <summary>
        /// 获取Datable,失败则是null
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public abstract DataTable GetDataTable(string sql);
        
        /// <summary>
        /// 获取一条语句的数量
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public abstract int GetCount(string sql);
        /// <summary>
        /// 单独更新  返回大于0说明更新成功，等于0 表示更新失败 小于0表示服务器没有连接上
        /// </summary>
        /// <param name="ziduanming"></param>
        /// <param name="biaoming"></param>
        /// <param name="tiaojianziduan"></param>
        /// <param name="ziduanshuju"></param>
        /// <param name="tiaojianshuju"></param>
        /// <returns></returns>
        public abstract int UpdateZiDuan(string ziduanming, string biaoming, string tiaojianziduan, object ziduanshuju, object tiaojianshuju);
        /// <summary>
        /// 写入图片 true表示写入成功
        /// </summary>
        /// <param name="byteimage"></param>
        /// <param name="biaoming"></param>
        /// <param name="tiaojianziduanming"></param>
        /// <param name="ziduanming"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract bool XieRuTuPian(byte[] byteimage, string biaoming, string tiaojianziduanming, string ziduanming, object id);


        /// <summary>
        /// 写入图片 true表示写入成功
        /// </summary>
        /// <param name="byteimage"></param>
        /// <param name="biaoming"></param>
        /// <param name="tiaojianziduanming"></param>
        /// <param name="ziduanming"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract bool XieRuTuPian(Image byteimage, string biaoming, string tiaojianziduanming, string ziduanming, object id);
     
        /// <summary>
        ///  1表示sql语句，2表示数据库的连接错误消息
        /// </summary>
        public abstract event LianJieShuJuXiaoXi _LianJieShuJuXiaoXi;

        /// <summary>
        /// 根据sql语句查询字段的值，没有返回null
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ziduan"></param>
        /// <returns></returns>
        public abstract object GenJuSqlChaXunZhi(string sql, string ziduan);
        /// <summary>
        /// 根据sql查询一系列的字段的值key为字段，values为值，没有返回null
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ziduan"></param>
        /// <returns></returns>
        public abstract Dictionary<string, object> GenJuSqlChaXunZhi(string sql, List<string> ziduan);
        /// <summary>
        /// 根据sql查询一系列的字段的值，没有返回null
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ziduan"></param>
        /// <returns></returns>
        public abstract List<object> GenJuSqlLisChaXunZhi(string sql, string ziduan);

        /// <summary>
        /// 关闭用的
        /// </summary>
        public abstract void Close();
    }
    /// <summary>
    /// 1表示sql语句，2表示数据库的连接错误消息
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="LeiXing"></param>
    public delegate void LianJieShuJuXiaoXi(string msg, int LeiXing);

    /// <summary>
    /// 数据库的类型
    /// </summary>
    public enum SQLType
    {
        /// <summary>
        /// mysql
        /// </summary>
        MySql=1,

        /// <summary>
        /// 微软自带的sql数据库
        /// </summary>
        SQLSever=2,
    }
}
