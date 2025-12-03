using CommLei.JiChuLei;
using Common.DataChuLi;
using SSheBei.ABSSheBei;
using SSheBei.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSheBei.PeiZhi
{
    /// <summary>
    /// 界面配置用的
    /// </summary>
    internal class JieMianPeiZhiLei
    {
        /// <summary>
        /// 设备
        /// </summary>
        public List<ABSNSheBei> SheBeis = new List<ABSNSheBei>();

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void IniChuShiHua(JiaZaiSheBeiModel model)
        {
            SheBeis.Clear();
            string wenjian = new PathModel().LuJin;
            List<string> wenjians = new List<string>();
            wenjians.Add(model.JiaZaiWanJianName);
            JieKouJiaZaiLei<ABSNSheBei> wenss = new JieKouJiaZaiLei<ABSNSheBei>();
            //Dictionary<string, List<Type>> shebeis = wenss.JiaZaiXuYaoType(wenjian, wenjians);
            Dictionary<string, List<Type>> shebeis = wenss.LoadNeededTypes<ABSNSheBei>(wenjian, wenjians);

            foreach (var item in shebeis.Keys)
            {
                List<Type> types = shebeis[item];
                foreach (var item1 in types)
                {
                    ABSNSheBei tongjijiekou = (ABSNSheBei)Activator.CreateInstance(item1);
                    ABSNSheBei sshebei = tongjijiekou;
                    sshebei.SheBeiID = model.SheBeiID;
                    sshebei.SheBeiName = model.SheBeiName;
                    sshebei.PeiZhiObjName = model.SheBeiPeiZhi;
                    sshebei.IniData(true);
                    SheBeis.Add(sshebei);               
                }
            }
          
          
        }

        /// <summary>
        /// 获取设备类型
        /// </summary>
        /// <returns></returns>
        public List<string> GetSheBeiType()
        {
            List<string> strings = new List<string>();
            for (int i = 0; i < SheBeis.Count; i++)
            {
                strings.Add(SheBeis[i].SheBeiType);
            }
            return strings;
        }

        /// <summary>
        /// 获取设备的版本号
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetBanBen(string type)
        {
            for (int i = 0; i < SheBeis.Count; i++)
            {
                if (SheBeis[i].SheBeiType== type)
                {
                    return SheBeis[i].BanBenHao;
                }
            }
            return "";
        }

        /// <summary>
        /// 加载文件
        /// </summary>
        /// <returns></returns>
        public string GetSheBeiWenJianName()
        {
            string wenjian = new PathModel().LuJin;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = wenjian;
            if (openFileDialog.ShowDialog()==DialogResult.OK)
            {
                string name = openFileDialog.FileName;
                return ChangYong.GetWenJianQuanName(name);
            }
            return "";
        }

        /// <summary>
        /// 获取配置界面
        /// </summary>
        /// <param name="shebeitype"></param>
        /// <param name="peizhiobjname"></param>
        /// <returns></returns>
        public JieMianFrmModel GetJieMian(string shebeitype,string peizhiobjname)
        {       
            foreach (var item in SheBeis)
            {
                if (item.SheBeiType == shebeitype)
                {
                    item.PeiZhiObjName = peizhiobjname;
                    item.IniData(true);
                    return item.GetFrm(false);
                }
            }
            return null;
        }
    }
}
