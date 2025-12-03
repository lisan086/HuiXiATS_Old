using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Sql
{
    /// <summary>
    /// 拼接sql语句
    /// </summary>
    public class SqlYuJuPingJie
    {

       
        /// <summary>
        /// 特性拼接的sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public  string InsertSql<T>(T model)
        {
            string BiaoMing = GetBiaoMing<T>();
            if (string.IsNullOrEmpty(BiaoMing))
            {
                return "";
            }
            if (model == null)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("insert into {0} ", BiaoMing));

            List<string> xinzeng= GetXinZengOrUpdateZiDuan<T>(1, model);
            if (xinzeng.Count<2)
            {
                return "";
            }
            sb.Append("(  ");
            sb.Append(xinzeng[0]);
            sb.Append("  )");
            sb.Append(" values ");
            sb.Append("(  ");
            sb.Append(xinzeng[1] );
            sb.Append(" )");
            return sb.ToString();

        }

        /// <summary>
        /// 不是特性的新增
        /// </summary>
        /// <param name="canshu"></param>
        /// <param name="biaoming"></param>
        /// <returns></returns>
        public string InsertSql(Dictionary<string,object> canshu,string biaoming)
        {
            if (string.IsNullOrEmpty(biaoming))
            {
                return "";
            }
            if (canshu == null||canshu.Count==0)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("insert into {0} ", biaoming));
            List<string> zhi1 = new List<string>();
            List<string> zhi2 = new List<string>();
            foreach (var item in canshu.Keys)
            {
                if (canshu[item]!=null)
                {
                    int leixing = HuoQuLeiXing(canshu[item]);
                    zhi1.Add(item);
                    zhi2.Add(string.Format("{0}{1}{0}", leixing==1?"'":"", canshu[item]));
                }
            }
             
           
            sb.Append("(  ");
            sb.Append(string.Join(",", zhi1.Select((x)=> { return x; })));
            sb.Append("  )");
            sb.Append(" values ");
            sb.Append("(  ");
            sb.Append(string.Join(",", zhi2.Select((x) => { return x; })));
            sb.Append(" )");
            return sb.ToString();
        }

        /// <summary>
        /// 特性更新语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public  string UpateSql<T>(T model)
        {
            // string UpdateSql = string.Format("update XueShengBiao set   XS_KaoHao=@XS_KaoHao, XS_LianXiFangShi=@XS_LianXiFangShi, XS_XingMing=@XS_XingMing   where XS_Id=@XS_Id "); 
            string BiaoMing = GetBiaoMing<T>();        
            if (string.IsNullOrEmpty(BiaoMing))
            {
                return "";
            }
            if (model==null)
            {
                return "";
            }
            List<string> ssd = GetXinZengOrUpdateZiDuan(2, model);
            if (ssd.Count<2)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("update {0}", BiaoMing));
            sb.Append(" set ");
            sb.Append(ssd[0]);
            sb.Append(" where  ");

            sb.Append(ssd[1]);

            return sb.ToString();

        }

        /// <summary>
        /// 不是特性更新语句
        /// </summary>
        /// <param name="genxincanshu"></param>
        /// <param name="tiaojian"></param>
        /// <param name="biaoming"></param>
        /// <returns></returns>
        public string UpateSql(Dictionary<string, object> genxincanshu, List<string> tiaojian, string biaoming)
        {
            // string UpdateSql = string.Format("update XueShengBiao set   XS_KaoHao=@XS_KaoHao, XS_LianXiFangShi=@XS_LianXiFangShi, XS_XingMing=@XS_XingMing   where XS_Id=@XS_Id "); 
           
            if (string.IsNullOrEmpty(biaoming))
            {
                return "";
            }
            if (genxincanshu == null|| genxincanshu.Count==0)
            {
                return "";
            }
           
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("update {0}", biaoming));
            List<string> zhi1 = new List<string>();
         
            foreach (var item in genxincanshu.Keys)
            {
                if (genxincanshu[item] != null)
                {
                    int leixing = HuoQuLeiXing(genxincanshu[item]);
                    
                    zhi1.Add(string.Format("{0}={1}{2}{1}", leixing == 1 ? "'" : "", genxincanshu[item]));
                }
            }
            sb.Append(" set ");
            sb.Append(string.Join(",", zhi1.Select((x) => { return x; })));
            if (tiaojian != null && tiaojian.Count > 0)
            {
                sb.Append(" where  ");
               
                sb.Append(string.Join(" ", tiaojian.Select((x) => { return x; })));
            }

            return sb.ToString();

        }

        /// <summary>
        ///  删除语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public  string DeleteSql<T>(T model)
        {
            // string UpdateSql = string.Format("update XueShengBiao set   XS_KaoHao=@XS_KaoHao, XS_LianXiFangShi=@XS_LianXiFangShi, XS_XingMing=@XS_XingMing   where XS_Id=@XS_Id ");
            string BiaoMing = GetBiaoMing<T>();        
            if (string.IsNullOrEmpty(BiaoMing))
            {
                return "";
            }
            List<string> ssd = GetXinZengOrUpdateZiDuan(3, model);
            if (ssd.Count < 1)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("delete from {0}", BiaoMing));
            sb.Append(" where  ");
            sb.Append(ssd[0]);
            return sb.ToString();

        }

        /// <summary>
        /// 删除语句
        /// </summary>
        /// <param name="BiaoMing">表名</param>
        /// <param name="TiaoJian">条件</param>
        /// <returns></returns>
        public  string DeleteSql(string BiaoMing, List<string> TiaoJian)
        {
            if (string.IsNullOrEmpty(BiaoMing))
            {
                return "";
            }
            if (TiaoJian.Count<=0)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("delete from {0}", BiaoMing));
            if (TiaoJian.Count > 0)
            {
                sb.Append(" where  ");
                for (int i = 0; i < TiaoJian.Count; i++)
                {
                    if (i == TiaoJian.Count - 1)
                    {
                        sb.Append(string.Format(" {0}", TiaoJian[i]));
                    }
                    else
                    {
                        sb.Append(string.Format("{0} and  ", TiaoJian[i]));
                    }
                }
            }


            return sb.ToString();

        }

        /// <summary>
        /// 删除整个表，并且重塑住建
        /// </summary>
        /// <param name="BiaoMing"></param>
        /// <returns></returns>
        public string ChongJianDelete(string BiaoMing)
        {
            return string.Format("truncate table {0}", BiaoMing);
        }

      
        /// <summary>
        /// 查询单表语句
        /// </summary>
        /// <param name="ZiDuanMing">查询字段，没有就查询所有</param>
        /// <param name="BiaoMing">表名</param>
        /// <param name="TiaoJian">条件</param>
        /// <returns></returns>
        public  string SelectSql(List<string> ZiDuanMing, string BiaoMing, List<string> TiaoJian)
        {
            if (string.IsNullOrEmpty(BiaoMing))
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("select "));
            if (ZiDuanMing.Count > 0)
            {
                for (int i = 0; i < ZiDuanMing.Count; i++)
                {
                    if (i == ZiDuanMing.Count - 1)
                    {
                        sb.Append(string.Format(" {0} ", ZiDuanMing[i]));
                    }
                    else
                    {
                        sb.Append(string.Format("{0}, ", ZiDuanMing[i]));
                    }
                }
            }
            else
            {
                sb.Append(string.Format(" * "));
            }
            sb.Append(string.Format(" from {0} ", BiaoMing));
            if (TiaoJian.Count > 0)
            {
                sb.Append(" where  ");
                for (int i = 0; i < TiaoJian.Count; i++)
                {
                    if (i == TiaoJian.Count - 1)
                    {
                        sb.Append(string.Format(" {0} ", TiaoJian[i]));
                    }
                    else
                    {
                        sb.Append(string.Format("{0} and  ", TiaoJian[i]));
                    }
                }
            }
            return sb.ToString();
        }


        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="BiaoMing">表面</param>
        /// <param name="TiaoJian">条件</param>
        /// <returns></returns>
        public  string SelectCount(string BiaoMing, List<string> TiaoJian)
        {
            // SELECT count(*) from XueShengBiao;          
            if (string.IsNullOrEmpty(BiaoMing))
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("select count(*) as MingCheng "));

            sb.Append(string.Format(" from {0} ", BiaoMing));

            if (TiaoJian.Count > 0)
            {
                sb.Append(" where  ");
                for (int i = 0; i < TiaoJian.Count; i++)
                {
                    if (i == TiaoJian.Count - 1)
                    {
                        sb.Append(string.Format(" {0}", TiaoJian[i]));
                    }
                    else
                    {
                        sb.Append(string.Format("{0} and ", TiaoJian[i]));
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 查询表的多少记录带参数的
        /// </summary>
        /// <param name="julushu"></param>
        /// <param name="BiaoMing"></param>
        /// <param name="TiaoJianZiDuan"></param>
        /// <returns></returns>
        public  string SelectSqlTop(int julushu, string BiaoMing, List<string> TiaoJianZiDuan)
        {
            // SELECT avg(second) from test;          
            if (string.IsNullOrEmpty(BiaoMing))
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("select top {0}  *", julushu));

            sb.Append(string.Format(" from {0} ", BiaoMing));

            if (TiaoJianZiDuan.Count > 0)
            {
                sb.Append(" where  ");
                for (int i = 0; i < TiaoJianZiDuan.Count; i++)
                {
                    if (i == TiaoJianZiDuan.Count - 1)
                    {
                        sb.Append(string.Format(" {0} ", TiaoJianZiDuan[i]));
                    }
                    else
                    {
                        sb.Append(string.Format("{0} and ", TiaoJianZiDuan[i]));
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 多表查询
        /// </summary>
        /// <param name="ZiDuanMing">字段</param>
        /// <param name="BiaoMing">多表</param>
        /// <param name="TiaoJianZiDuan">条件</param>
        /// <returns></returns>
        public  string SelectSqlDuoBiaoChaXun(List<string> ZiDuanMing, List<string> BiaoMing, List<string> TiaoJianZiDuan)
        {
            // SELECT avg(second) from test;          
            if (BiaoMing.Count == 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("select "));
            if (ZiDuanMing.Count > 0)
            {
                for (int i = 0; i < ZiDuanMing.Count; i++)
                {
                    if (i == ZiDuanMing.Count - 1)
                    {
                        sb.Append(string.Format(" {0} ", ZiDuanMing[i]));
                    }
                    else
                    {
                        sb.Append(string.Format("{0}, ", ZiDuanMing[i]));
                    }
                }
            }
            else
            {
                sb.Append(string.Format(" * "));
            }
            sb.Append(string.Format(" from  "));
            for (int i = 0; i < BiaoMing.Count; i++)
            {
                if (i == BiaoMing.Count - 1)
                {
                    sb.Append(string.Format(" {0} ", BiaoMing[i]));
                }
                else
                {
                    sb.Append(string.Format("{0}, ", BiaoMing[i]));
                }
            }
            if (TiaoJianZiDuan.Count > 0)
            {
                sb.Append(" where  ");
                for (int i = 0; i < TiaoJianZiDuan.Count; i++)
                {
                    if (i == TiaoJianZiDuan.Count - 1)
                    {
                        sb.Append(string.Format(" {0}", TiaoJianZiDuan[i]));
                    }
                    else
                    {
                        sb.Append(string.Format("{0} and ", TiaoJianZiDuan[i]));
                    }
                }
            }
            return sb.ToString();
        }

     


      
     
        /// <summary>
        /// 获取表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string GetBiaoMing<T>()
        {
            Type t = typeof(T);
            string biaoming = t.Name;
            if (t.IsDefined(typeof(BiaoMingAttribute), true))
            {
                BiaoMingAttribute xi = (BiaoMingAttribute)t.GetCustomAttributes(true)[0];
                if (xi.IsShiYong)
                {
                    return xi.BiaoName;

                }
                return "";
            }
            return biaoming;
        }
        /// <summary>
        /// 获取查询字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<string> GetChaXunZiDuan<T>()
        {
            Type t = typeof(T);
            PropertyInfo[] shuxin = t.GetProperties();
            List<string> ziduan = new List<string>();
            foreach (PropertyInfo item in shuxin)
            {
                if (item.IsDefined(typeof(ZiDuanAttribute), true))
                {
                    BiaoMingAttribute xi = (BiaoMingAttribute)item.GetCustomAttributes(true)[0];
                    if (xi.IsShiYong)
                    {
                        if (string.IsNullOrEmpty(xi.BiaoName) == false)
                        {
                            ziduan.Add(xi.BiaoName);
                        }
                    }
                }            
            }
            return ziduan;
        }

        /// <summary>
        /// 获取查询字段 1表示新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public  List<string> GetXinZengOrUpdateZiDuan<T>(int type,T model)
        {
            List<string> ziduan = new List<string>() ;
            Type t = typeof(T);
            PropertyInfo[] shuxin = t.GetProperties();
            if (type == 1)
            {
                List<string> ab1 = new List<string>();
                List<string> ab2 = new List<string>();
                foreach (PropertyInfo item in shuxin)
                {
                    if (item.IsDefined(typeof(ZiDuanAttribute), true))
                    {
                        ZiDuanAttribute xi = (ZiDuanAttribute)item.GetCustomAttributes(true)[0];
                        if (xi.IsShiYong)
                        {
                            if (string.IsNullOrEmpty(xi.ZiDuanName) == false)
                            {
                                if (xi.IsZiZeng == false)
                                {

                                    object zhi = item.GetValue(model, null);
                                    if (zhi != null)
                                    {
                                        int leix = HuoQuLeiXing(zhi);
                                        ab1.Add(xi.ZiDuanName);
                                        ab2.Add(string.Format("{0}{1}{0}", leix == 1 ? "'" : "", zhi));
                                    }

                                }

                            }
                        }
                    }
                }
                if (ab1.Count > 0 && ab2.Count > 0)
                {
                    ziduan.Add(string.Join(",", ab1.Select((x) => { return x; })));
                    ziduan.Add(string.Join(",", ab2.Select((x) => { return x; })));
                }
            }
            else if(type==2)
            {
                List<string> ab1 = new List<string>();
                List<string> ab2 = new List<string>();
                foreach (PropertyInfo item in shuxin)
                {
                    if (item.IsDefined(typeof(ZiDuanAttribute), true))
                    {
                        ZiDuanAttribute xi = (ZiDuanAttribute)item.GetCustomAttributes(true)[0];
                        if (xi.IsShiYong)
                        {
                            if (string.IsNullOrEmpty(xi.ZiDuanName) == false)
                            {
                                if (xi.IsWhereGengXinJian)
                                {

                                    object zhi = item.GetValue(model, null);
                                    if (zhi != null)
                                    {
                                        int leix = HuoQuLeiXing(zhi);                                    
                                        ab2.Add(string.Format("{0}={1}{2}{1}", xi.ZiDuanName, leix == 1 ? "'" : "", zhi));
                                    }

                                }
                                else
                                {
                                    object zhi = item.GetValue(model, null);
                                    if (zhi != null)
                                    {
                                        int leix = HuoQuLeiXing(zhi);
                                        ab1.Add(string.Format("{0}={1}{2}{1}", xi.ZiDuanName, leix == 1 ? "'" : "", zhi));                                   
                                    }

                                }
                            }
                        }
                    }
                }
                if (ab1.Count > 0 && ab2.Count > 0)
                {
                    ziduan.Add(string.Join(",", ab1.Select((x) => { return x; })));
                    ziduan.Add(string.Join(" and ", ab2.Select((x) => { return x; })));
                }
            }
            else if (type == 3)
            {
                List<string> ab1 = new List<string>();
              
                foreach (PropertyInfo item in shuxin)
                {
                    if (item.IsDefined(typeof(ZiDuanAttribute), true))
                    {
                        ZiDuanAttribute xi = (ZiDuanAttribute)item.GetCustomAttributes(true)[0];
                        if (xi.IsShiYong)
                        {
                            if (string.IsNullOrEmpty(xi.ZiDuanName) == false)
                            {
                                if (xi.IsShanChu)
                                {

                                    object zhi = item.GetValue(model, null);
                                    if (zhi != null)
                                    {
                                        int leix = HuoQuLeiXing(zhi);
                                        ab1.Add(string.Format("{0}={1}{2}{1}", xi.ZiDuanName, leix == 1 ? "'" : "", zhi));
                                    }

                                }
                               
                            }
                        }
                    }
                }
                if (ab1.Count > 0)
                {
                    ziduan.Add(string.Join(" and ", ab1.Select((x) => { return x; })));                 
                }
            }
            return ziduan;
        }


        /// <summary>
        /// 1表示需要单引号
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
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
            else if (o is short)
            {
                biaozhi = 2;
            }
            else if (o is long)
            {
                biaozhi = 2;
            }
            return biaozhi;
        }

      
    }
}
