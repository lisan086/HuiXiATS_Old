using Common.JieMianLei;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataChuLi
{
    /// <summary>
    /// 全新的导出XLXS
    /// </summary>
    public class DaoChuTxlxs
    {
        /// <summary>
        /// 保存成功返回true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <param name="lisdata"></param>
        /// <returns></returns>
        public bool BaoCunxlxs<T>(string filename,List<T> lisdata)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return false;
            }
            if (lisdata==null)
            {
                lisdata = new List<T>();
            }
            List<PaiXuModel> lis = new List<PaiXuModel>();
            Type t = typeof(T);
            PropertyInfo[] shuxin = t.GetProperties();
            foreach (PropertyInfo item in shuxin)
            {
                if (item.IsDefined(typeof(LieTeXing), true))
                {
                    LieTeXing xi = (LieTeXing)item.GetCustomAttributes(true)[0];
                    bool iskejian = xi.GetKeJian();
                    if (iskejian)
                    {
                        PaiXuModel model = new PaiXuModel();
                        model.LieName = xi.GetLieName();
                        model.Lie = xi.GetDiJiLie();
                        model.Kuan = xi.GetManKuan();
                        model.ModelName = item;
                        lis.Add(model);
                      
                    }

                }
            }
            lis.Sort((x,y)=> {
                if (x.Lie > y.Lie)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            });
            
     

            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (ExcelPackage package = new ExcelPackage(fs))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet");
                    int k = 0;

                 

                    foreach (var item1 in lis)
                    { 
                        worksheet.Cells[1, k + 1].Value = item1.LieName;
                        worksheet.Cells[1, k + 1].Style.Font.Bold = true;
                        worksheet.Column(k + 1).Width = item1.Kuan;
                      
                        if (lisdata.Count > 0)
                        {
                            int i = 0;
                            foreach (var item in lisdata)
                            {
                                worksheet.Cells[i + 2, k + 1].Value = item1.ModelName.GetValue(item);
                                i++;
                            }
                        }
                        k++;
                    }
        
                    package.Save();

                }
            }
            return true;
        }


        /// <summary>
        /// 保存追加成功返回true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool BaoCunZhuiJiaxlxs<T>(string filename, T model)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return false;
            }
            if (model == null)
            {
                return false;
            }
            List<PaiXuModel> lis = new List<PaiXuModel>();
            Type t = typeof(T);
            PropertyInfo[] shuxin = t.GetProperties();
            foreach (PropertyInfo item in shuxin)
            {
                if (item.IsDefined(typeof(LieTeXing), true))
                {
                    LieTeXing xi = (LieTeXing)item.GetCustomAttributes(true)[0];
                    bool iskejian = xi.GetKeJian();
                    if (iskejian)
                    {
                        PaiXuModel model1 = new PaiXuModel();
                        model1.LieName = xi.GetLieName();
                        model1.Lie = xi.GetDiJiLie();
                        model1.Kuan = xi.GetManKuan();
                        model1.ModelName = item;
                        lis.Add(model1);

                    }

                }
            }
            lis.Sort((x, y) => {
                if (x.Lie > y.Lie)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            });
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filename)))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets["Sheet"];
                int row = excelWorksheet.Dimension.End.Row;
                int num = 0;

                foreach (var item1 in lis)
                {
                    excelWorksheet.Cells[row + 1, num + 1].Value = item1.ModelName.GetValue(model);
                    num++;
                }         
                excelPackage.Save();
            }
        
            return true;
        }


        /// <summary>
        /// 导出对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
        public List<T> DaoChuModel<T>(string filename)
        {
            List<T> list = new List<T>();
            Type t = typeof(T);
            PropertyInfo[] shuxin = t.GetProperties();
            Dictionary<string, string> headDict1 = new Dictionary<string, string>();


            foreach (PropertyInfo item in shuxin)
            {
                if (item.IsDefined(typeof(LieTeXing), true))
                {
                    LieTeXing xi = (LieTeXing)item.GetCustomAttributes(true)[0];
                    bool iskejian = xi.GetKeJian();
                    if (iskejian)
                    {
                  
                        headDict1.Add(xi.GetLieName(), item.Name);
                    }

                }
            }
            try
            {
                #region MyRegion
                DataTable dt = DuQuWenJian(filename);
                //根据Dict,修改dt字段名
                foreach (KeyValuePair<string, string> pair in headDict1)
                {
                    try
                    {
                        dt.Columns[pair.Key].ColumnName = pair.Value;
                    }
                    catch
                    {


                    }
                }
                string jieguo = JsonConvert.SerializeObject(dt);
                list = JsonConvert.DeserializeObject<List<T>>(jieguo);
                #endregion
                return list;
            }
            catch
            {

            }

            return new List<T>();

        }


     
        private  DataTable DuQuWenJian(string filePath, int lieindex = 0, int rowindex = 0)
        {
            //创建ExcelPackage对象，这个对象是面对工作簿的，就是里面的所有
            DataTable dt = new DataTable();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (ExcelPackage pck = new ExcelPackage(fs))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                    int minRowNum = ws.Dimension.Start.Row + rowindex; //工作区开始行号
                    int minColNum = ws.Dimension.Start.Column + lieindex;
                    int maxRowNum = ws.Dimension.End.Row; //工作区结束行号
                    int maxColNum = ws.Dimension.End.Column;
                    //创建DataTable 列
                    for (int i = minColNum; i <= maxColNum; i++)
                    {
                        //表头
                        object obj = ws.Cells[minRowNum, i].Value;
                        string colName = obj.ToString();
                        DataColumn datacolum = new DataColumn(colName);
                        dt.Columns.Add(datacolum);
                    }
                    //数据
                    for (int i = minRowNum + 1; i <= maxRowNum; i++)
                    {
                        //创建行
                        DataRow dr = dt.NewRow();
                        //循环填充数据
                        for (int j = minColNum; j <= maxColNum; j++)
                        {
                            object obj = ws.Cells[i, j].Value;
                            dr[j - 1] = obj;
                        }
                        dt.Rows.Add(dr); //把每行追加到DataTable
                    }
                }
            }
            return dt;


        }
        private class PaiXuModel
        {
            public string LieName { get; set; } = "";

            public int Lie { get; set; } = 0;

            public int Kuan { get; set; }

            public PropertyInfo ModelName { get; set; } 
        }
    }

    /// <summary>
    /// 支持脚本的类
    /// </summary>
    public abstract class JiaoBenLei
    {
        /// <summary>
        /// 输出结果的
        /// </summary>
        /// <param name="duixiangmodel"></param>
        /// <returns></returns>
        public abstract object OutPut(string duixiangmodel);

    }
}
