using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Common.Sql
{
    /// <summary>
    /// sql访问数据库
    /// </summary>
    public class SQLSeverLianJieQi : ABSSqlDBLianJie
    {
        #region 变量区
        /// <summary>
        /// 数据库连接
        /// </summary>
        private  string ConString="";
        private bool IsKeYiShiYong = true;
        private SqlConnection Con = null;
        /// <summary>
        /// 返回的消息
        /// </summary>
        public override event LianJieShuJuXiaoXi _LianJieShuJuXiaoXi;
        #endregion
        /// <summary>
        /// 访问数据库的类型
        /// </summary>
        public override SQLType SQLType
        {
            get
            {
                return SQLType.SQLSever;
            }
        }
        /// <summary>
        /// 实例化
        /// </summary>
        public SQLSeverLianJieQi()
        {
            ReadeXml();
            Open();
        }

        private void ReadeXml()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "PeiZhiDB.xml");
            if (!File.Exists(path))
            {
                IsKeYiShiYong = false;
                FaSongFuWu(string.Format("{0}文件不存在", path), 2);
                return;
            }
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(path);
                string ip = xml.SelectSingleNode("DB/数据库IP").InnerText;
                string mc = xml.SelectSingleNode("DB/数据库MC").InnerText;
                string yonghu = xml.SelectSingleNode("DB/数据库YH").InnerText;
                string mima = xml.SelectSingleNode("DB/数据库MIMA").InnerText;//_ShuJuKuMiMa
                ConString = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};", ip, mc, yonghu, mima);
            }
            catch (Exception ex)
            {
                IsKeYiShiYong = false;
                FaSongFuWu(string.Format("配置文件有问题:{0}", ex.Message), 2);
            }

        }
        private void FaSongFuWu(string msg, int leixing)
        {
            if (_LianJieShuJuXiaoXi != null)
            {
                _LianJieShuJuXiaoXi(msg, leixing);
            }
        }
        private SqlDataReader GetReader(string sqlyuju)
        {
            SqlCommand com = new SqlCommand(sqlyuju, Con);
            SqlDataReader reader = com.ExecuteReader();
            return reader;

        }
        private void Open()
        {
            if (IsKeYiShiYong==false)
            {
                FaSongFuWu(string.Format("配置文件有问题，导致无法连接数据库"), 2);
                return;
            }
            Con = new SqlConnection(ConString);
            Con.Open();
            if (Con.State == ConnectionState.Open)
            {
                IsKeYiShiYong = true;
            }
            else
            {
                IsKeYiShiYong = false;
            }
        }

        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override int UpdateOrInsertdate(string sql)
        {
            if (IsKeYiShiYong == false)
            {
                FaSongFuWu(string.Format("数据库未连接"), 2);

                return -2;
            }
            SqlCommand com = new SqlCommand(sql, Con);
            try
            {
                int zhixing = com.ExecuteNonQuery();
                FaSongFuWu(sql, 1);             
                return zhixing;
            }
            catch (Exception ex)
            {
                FaSongFuWu(string.Format("数据库访问失败:{0}", ex.Message), 2);
                return -3;
            }
            finally
            {           
                com.Dispose();
            }

        }

        /// <summary>
        /// 事务的更新
        /// </summary>
        /// <param name="sqllist"></param>
        /// <returns></returns>
        public override bool ShiWuUpdate(List<string> sqllist)
        {
            if (IsKeYiShiYong == false)
            {
                FaSongFuWu(string.Format("数据库未连接"), 2);
                return false;
            }
            if (sqllist.Count == 0)
            {
                FaSongFuWu(string.Format("没有sql语句"), 2);
                return false;
            }
            SqlCommand com = new SqlCommand();
            com.Connection = Con;
            try
            {
                com.Transaction = Con.BeginTransaction();
                foreach (var item in sqllist)
                {
                    com.CommandText = item;
                    FaSongFuWu(com.CommandText, 1);
                    com.ExecuteNonQuery();
                }
                com.Transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                if (com.Transaction != null)
                {
                    com.Transaction.Rollback();
                }
                FaSongFuWu(string.Format("执行事务失败:{0}", ex.Message), 2);

            }
            finally
            {
               
                com.Dispose();
            }
            return false;
        }
        /// <summary>
        /// 获取数据库表
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override DataTable GetDataTable(string sql)
        {
            if (IsKeYiShiYong == false)
            {
                FaSongFuWu(string.Format("数据库未连接"), 2);
                return null;
            }          
            try
            {              
                FaSongFuWu(sql, 1);
                SqlDataAdapter det = new SqlDataAdapter(sql, Con);
                DataSet set = new DataSet();
                det.Fill(set);
                if (set.Tables.Count > 0)
                {
                    DataTable dt = set.Tables[0];
                    det.Dispose();
                    set.Dispose();
                    return dt;
                }
                else
                {
                   
                    det.Dispose();
                    set.Dispose();
                    return null;
                }
               
            }
            catch (Exception ex)
            {
                FaSongFuWu(string.Format("数据库访问失败:{0}", ex.Message), 2);

                return null;
            }
           
        }

        /// <summary>
        /// 返回的数量
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override int GetCount(string sql)
        {
            if (IsKeYiShiYong == false)
            {
                FaSongFuWu(string.Format("数据库未连接"), 2);
                return -1;
            }
            try
            {
              
                FaSongFuWu(sql, 1);

                SqlDataAdapter det = new SqlDataAdapter(sql, Con);
                DataSet set = new DataSet();
                det.Fill(set);
                if (set.Tables.Count > 0)
                {
                    DataTable dt = set.Tables[0];
                    det.Dispose();
                    set.Dispose();
                    return dt.Rows.Count;
                }
                else
                {
                    det.Dispose();
                    set.Dispose();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                FaSongFuWu(string.Format("数据库访问失败:{0}", ex.Message), 2);

                return -1;
            }
            
        }

        /// <summary>
        /// 更新单个字段
        /// </summary>
        /// <param name="ziduanming"></param>
        /// <param name="biaoming"></param>
        /// <param name="tiaojianziduan"></param>
        /// <param name="ziduanshuju"></param>
        /// <param name="tiaojianshuju"></param>
        /// <returns></returns>
        public override int UpdateZiDuan(string ziduanming, string biaoming, string tiaojianziduan, object ziduanshuju, object tiaojianshuju)
        {
            if (IsKeYiShiYong == false)
            {
                FaSongFuWu(string.Format("数据库未连接"), 2);
                return -1;
            }
            string sql = string.Format(@"update {0} set {1}={2}{3}{4}  where {5}={6}{7}{8} ", biaoming, ziduanming, HuoQuLeiXing(ziduanshuju) == 1 ? "'" : "", ziduanshuju, HuoQuLeiXing(ziduanshuju) == 1 ? "'" : "", tiaojianziduan, HuoQuLeiXing(tiaojianshuju) == 1 ? "'" : "", tiaojianshuju, HuoQuLeiXing(tiaojianshuju) == 1 ? "'" : "");
            int shuliang = UpdateOrInsertdate(sql);
            FaSongFuWu(sql, 1);

            return shuliang;
        }


        /// <summary>
        /// 向数据库写入图片
        /// </summary>
        /// <param name="byteimage"></param>
        /// <param name="biaoming"></param>
        /// <param name="tiaojianziduanming"></param>
        /// <param name="ziduanming"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public override bool XieRuTuPian(byte[] byteimage, string biaoming, string tiaojianziduanming, string ziduanming, object id)
        {
            if (IsKeYiShiYong == false)
            {
                FaSongFuWu(string.Format("数据库未连接"), 2);
                return false;
            }
            if (byteimage == null)
            {
                FaSongFuWu(string.Format("传来的数据为null"), 2);
                return false;
            }
            int biaozhi = HuoQuLeiXing(id);
            string sq = string.Format(@"select {1} from {0} where {1}={3}{2}{3}", biaoming, tiaojianziduanming, id, id, biaozhi == 1 ? "'" : "");
            SqlDataReader reader1 = GetReader(sq);
            try
            {
                if (reader1.Read())
                {
                    reader1.Close();
                   
                    string sql = string.Format(@"update {0} set {1}=@{2}  where   {3}={5}{4}{5}", biaoming, ziduanming, "pic", tiaojianziduanming, id, biaozhi==1?"'":"");
                    SqlCommand com = new SqlCommand();
                    com.CommandType = CommandType.Text;
                    com.CommandText = sql;
                    com.Connection = Con;
                    FaSongFuWu(sql, 1);

                    com.Parameters.Add("pic", SqlDbType.Image);
                    com.Parameters["pic"].Value = byteimage;

                    int shuliang = com.ExecuteNonQuery();
                    if (shuliang == 1)
                    {
                        com.Dispose();
                        return true;
                    }
                    else
                    {
                        com.Dispose();
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                FaSongFuWu(string.Format("数据库访问失败:{0}", ex.Message), 2);


            }
            finally
            {
                reader1.Close();
                reader1.Dispose();
            }

           
            return false;

        }

       
        /// <summary>
        /// 向数据库写入图片
        /// </summary>
        /// <param name="byteimage"></param>
        /// <param name="biaoming"></param>
        /// <param name="tiaojianziduanming"></param>
        /// <param name="ziduanming"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public override bool XieRuTuPian(Image byteimage, string biaoming, string tiaojianziduanming, string ziduanming, object id)
        {
            if (IsKeYiShiYong == false)
            {
                FaSongFuWu(string.Format("数据库未连接"), 2);
                return false;
            }
            if (byteimage == null)
            {
                FaSongFuWu(string.Format("传来的数据为null"), 2);
                return false;
            }
            int biaozhi = HuoQuLeiXing(id);
            byte[] imbyte = ImageToBytes(byteimage);
            string sq = string.Format(@"select * from {0} where {1}={3}{2}{3}", biaoming, tiaojianziduanming, id, biaozhi == 1 ? "'" : "");
            SqlDataReader reader1 = GetReader(sq);
            try
            {
                if (reader1.Read())
                {
                    reader1.Close();
                    string sql = string.Format(@"update {0} set {1}=@{2}  where   {3}={5}{4}{5}", biaoming, ziduanming, "pic", tiaojianziduanming, id, biaozhi == 1 ? "'" : "");
                    SqlCommand com = new SqlCommand();
                    com.CommandType = CommandType.Text;
                    com.CommandText = sql;
                    com.Connection = Con;
                    FaSongFuWu(sql, 1);

                    com.Parameters.Add("pic", SqlDbType.Image);
                    com.Parameters["pic"].Value = imbyte;

                    int shuliang = com.ExecuteNonQuery();
                    if (shuliang == 1)
                    {
                        com.Dispose();
                        return true;
                    }
                    else
                    {
                        com.Dispose();
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                FaSongFuWu(string.Format("数据库访问失败:{0}", ex.Message), 2);


            }
            finally
            {
                reader1.Close();
                reader1.Dispose();
            }

          
            return false;

        }

    

        /// <summary>
        /// 根据字段名查询最后一个数据，没有就返回为空
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ziduan"></param>
        /// <returns></returns>
        public override object GenJuSqlChaXunZhi(string sql, string ziduan)
        {
            FaSongFuWu(sql, 1);
            DataTable dt = GetDataTable(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    int count = dt.Rows.Count - 1;
                    try
                    {
                        return dt.Rows[count][ziduan];
                    }
                    catch
                    {
                        return null;

                    }

                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 更具字段查询所有的值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ziduan"></param>
        /// <returns></returns>
        public override List<object> GenJuSqlLisChaXunZhi(string sql, string ziduan)
        {
            FaSongFuWu(sql, 1);
            DataTable dt = GetDataTable(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    try
                    {
                        List<object> fanhui = new List<object>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            fanhui.Add(dt.Rows[i][ziduan]);
                        }
                        return fanhui;
                    }
                    catch
                    {
                        return new List<object>();

                    }

                }
                else
                {
                    return new List<object>();
                }
            }
            else
            {
                return new List<object>();
            }
        }

        /// <summary>
        /// 根据lis字段查询值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ziduan"></param>
        /// <returns></returns>
        public override Dictionary<string, object> GenJuSqlChaXunZhi(string sql, List<string> ziduan)
        {
            FaSongFuWu(sql, 1);
            DataTable dt = GetDataTable(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    int count = dt.Rows.Count - 1;
                    try
                    {
                        Dictionary<string, object> zidianzhi = new Dictionary<string, object>();
                        for (int i = 0; i < ziduan.Count; i++)
                        {
                            zidianzhi.Add(ziduan[i], dt.Rows[count][ziduan[i]]);

                        }
                        return zidianzhi;
                    }
                    catch
                    {
                        return null;

                    }

                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public override void Close()
        {
            IsKeYiShiYong = false;
            if (Con!=null)
            {
                Con.Close();
                Con.Dispose();
            }
        }

        private byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

   

        private int HuoQuLeiXing(object o)
        {
            int biaozhi = 1;
            if (o is int)
            {
                biaozhi = 2;
            }
            else if (o is float)
            {
                biaozhi = 2;
            }
            else if (o is double)
            {
                biaozhi = 2;
            }
            return biaozhi;
        }

    }
}
