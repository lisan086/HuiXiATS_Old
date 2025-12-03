
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
using System.Windows.Forms;


namespace CommLei.DataChuLi
{
    /// <summary>
    /// 导出Excl文档，读取Excl文档用了EPPLus
    /// </summary>
    public class DuRuExclWenDan
    {
        /// <summary>
        /// 把excl文件转换为datatable,
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="lieindex">第几列开始</param>
        /// <param name="rowindex">第几行开始</param>
        /// <returns></returns>
        public DataTable DuQuWenJian(string filePath, int lieindex = 0, int rowindex = 0)
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
                        int iskeyijia = 0;
                        //循环填充数据
                        for (int j = minColNum; j <= maxColNum; j++)
                        {
                            object obj = ws.Cells[i, j].Value;
                            dr[j - 1] = obj;
                            if (obj==null||string.IsNullOrEmpty(obj.ToString()))
                            {
                                iskeyijia++;
                            }
                        }
                        dt.Rows.Add(dr); //把每行追加到DataTable
                    }
                }
            }
            return dt;


        }

        /// <summary>
        ///  把excl文件转换为lis对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="headDict"></param>
        /// <param name="lieindex"></param>
        /// <param name="rowindex"></param>
        /// <returns></returns>
        public List<T> ShuChuExcelOrLis<T>(string filePath, Dictionary<string, string> headDict, int lieindex = 0, int rowindex = 0) where T : class
        {
            List<T> list = new List<T>();
            Type t = typeof(T);
            PropertyInfo[] shuxin = t.GetProperties();
            Dictionary<string, string> headDict1 = new Dictionary<string, string>();
            bool zhen = CunZai(headDict, shuxin, out headDict1);
            if (zhen)
            {
                try
                {
                    #region MyRegion
                    DataTable dt = DuQuWenJian(filePath, lieindex, rowindex);
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

                }
                catch
                {

                }

            }
            else
            {
                try
                {
                    #region MyRegion
                    DataTable dt = DuQuWenJian(filePath, lieindex, rowindex);
                    for (int i = 0; i < shuxin.Length; i++)
                    {
                        try
                        {
                            dt.Columns[i].ColumnName = shuxin[i].Name;
                        }
                        catch
                        {


                        }

                    }
                    string jieguo = JsonConvert.SerializeObject(dt);
                    list = JsonConvert.DeserializeObject<List<T>>(jieguo);
                    #endregion

                }
                catch
                {

                }
            }

            return list;
        }

        /// <summary>
        ///  把excl文件转换为lis对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="headDict"></param>
        /// <param name="lieindex"></param>
        /// <param name="rowindex"></param>
        /// <returns></returns>
        public List<T> ShuChuExcelOrLisN<T>(string filePath, Dictionary<string, string> headDict, int lieindex = 0, int rowindex = 0) where T : new()
        {
            List<T> list = new List<T>();
            Type t = typeof(T);
            PropertyInfo[] shuxin = t.GetProperties();
            Dictionary<string, string> headDict1 = new Dictionary<string, string>();
            bool zhen = CunZai(headDict, shuxin, out headDict1);
            if (zhen)
            {
                try
                {
                    #region MyRegion
                    DataTable dt = DuQuWenJian(filePath, lieindex, rowindex);
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

                }
                catch
                {

                }

            }
            else
            {
                try
                {
                    #region MyRegion
                    DataTable dt = DuQuWenJian(filePath, lieindex, rowindex);
                    for (int i = 0; i < shuxin.Length; i++)
                    {
                        try
                        {
                            dt.Columns[i].ColumnName = shuxin[i].Name;
                        }
                        catch
                        {


                        }

                    }
                    string jieguo = JsonConvert.SerializeObject(dt);
                    list = JsonConvert.DeserializeObject<List<T>>(jieguo);
                    #endregion

                }
                catch
                {

                }
            }

            return list;
        }


        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="headDict"></param>
        /// <param name="shuxin"></param>
        /// <param name="headDict1"></param>
        /// <returns></returns>
        private bool CunZai(Dictionary<string, string> headDict, PropertyInfo[] shuxin, out Dictionary<string, string> headDict1)
        {
            headDict1 = new Dictionary<string, string>();
            if (headDict == null || headDict.Count == 0)
            {
                return false;
            }
            for (int i = 0; i < shuxin.Length; i++)
            {

                foreach (string item in headDict.Keys)
                {
                    if (headDict[item] == shuxin[i].Name)
                    {

                        headDict1.Add(item, shuxin[i].Name);
                        break;
                    }
                }

            }
            return true;
        }



        /// <summary>
        /// 导出Exc数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="ShuJu"></param>
        /// <param name="LieMing"></param>
        /// <param name="addzishuju">增加子数据用的</param>
        public void DaoChuExc<T>(string filePath, List<T> ShuJu, List<string> LieMing, Action<List<T>, int, ExcelWorksheet> addzishuju) where T : new()
        {
            //核心代码
            FileInfo newFile = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet");
                for (int i = 0; i < LieMing.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = LieMing[i];
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                }
                if (addzishuju != null)
                {
                    addzishuju(ShuJu, LieMing.Count, worksheet);
                }
                #region 以后借鉴
                //for (int i = 0; i < LieMing.Count; i++)
                //{
                //    for (int j = 0; j < ShuJu.Count; j++)
                //    {
                //        worksheet.Cells[i + 2, j + 1].Value = ShuJu[j];

                //    }
                //}
                #endregion

                package.Save();

            }
        }


        /// <summary>
        /// 导出Exc数据
        /// </summary>    
        /// <param name="filePath"></param>
        /// <param name="meilieshuju"></param>   
        public void DaoChuExc(string filePath, Dictionary<string, List<object>> meilieshuju)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (ExcelPackage package = new ExcelPackage(fs))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet");
                    int k = 0;
                    foreach (var item in meilieshuju.Keys)
                    {
                        worksheet.Cells[1, k + 1].Value = item;
                        worksheet.Cells[1, k + 1].Style.Font.Bold = true;
                        k++;
                    }


                    #region 以后借鉴
                    int j = 0;
                    foreach (var item in meilieshuju.Keys)
                    {
                        List<object> zhi = meilieshuju[item];
                        for (int i = 0; i < zhi.Count; i++)
                        {
                            worksheet.Cells[i + 2, j + 1].Value = zhi[i];

                        }
                        j++;
                    }

                    #endregion

                    package.Save();

                }
            }

        }

        /// <summary>
        ///  导出Exc数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dataGridView1"></param>
        /// <param name="buyaolie"></param>
        public void DaoChuDataG(string filePath, DataGridView dataGridView1, List<string> buyaolie)
        {
            if (dataGridView1 == null)
            {
                return;
            }
            if (buyaolie == null)
            {
                buyaolie = new List<string>();
            }
            string wenjinnane = filePath;

            if (string.IsNullOrEmpty(wenjinnane) == false)
            {
                DuRuExclWenDan daoChuXlxsLei = new DuRuExclWenDan();
                Dictionary<string, List<object>> BiaoGe = new Dictionary<string, List<object>>();
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    string neirong = dataGridView1.Columns[i].HeaderText;
                    if (buyaolie.IndexOf(neirong) < 0)
                    {
                        BiaoGe.Add(neirong, new List<object>());
                    }
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewCellCollection cell = dataGridView1.Rows[i].Cells;
                    int shuju = 0;
                    foreach (DataGridViewCell item in cell)
                    {
                        string tou = dataGridView1.Columns[shuju].HeaderText;
                        if (BiaoGe.ContainsKey(tou))
                        {
                            BiaoGe[tou].Add(item.Value);
                        }
                        shuju++;
                    }

                }


                daoChuXlxsLei.DaoChuExc(wenjinnane, BiaoGe);
            }
        }

        /// <summary>
        /// 导出追加的Exc数据
        /// </summary>    
        /// <param name="filePath"></param>
        /// <param name="meilieshuju"></param>   
        public void DaoChuZhuiJiaExe(string filePath, Dictionary<string, object> meilieshuju)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets["Sheet"];
                int row = excelWorksheet.Dimension.End.Row;
                int num = 0;
                foreach (string current in meilieshuju.Keys)
                {
                    object value = meilieshuju[current];
                    excelWorksheet.Cells[row + 1, num + 1].Value = value;
                    num++;
                }
                excelPackage.Save();
            }
        }

        /// <summary>
        /// 导出追加多条的Exc数据
        /// </summary>    
        /// <param name="filePath"></param>
        /// <param name="meilieshuju"></param>   
        public void DaoChuZhuiJiaExe(string filePath, Dictionary<string, List<object>> meilieshuju)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets["Sheet"];
                int row = excelWorksheet.Dimension.End.Row;
                int num = 0;
                foreach (string current in meilieshuju.Keys)
                {
                    List<object> list = meilieshuju[current];
                    for (int i = 0; i < list.Count; i++)
                    {
                        excelWorksheet.Cells[row + 1 + i, num + 1].Value = list[i];
                    }
                    num++;
                }
                excelPackage.Save();
            }
        }
    }
}
