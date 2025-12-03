using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommLei.JieMianLei
{
    /// <summary>
    /// 在指定的容器控件显示窗体
    /// </summary>
    public class JieJueChuangTiPingJie
    {
        /// <summary>
        /// 对传入的窗体对象进行处理
        /// </summary>
        /// <param name="from"></param>
        /// <param name="kongjian"></param>
        public void OpenFrom(Form from, Control kongjian)
        {
            from.TopLevel = false;
            from.FormBorderStyle = FormBorderStyle.None;
            from.Dock = DockStyle.Fill;
            from.Parent = kongjian;
            from.Show();
        }
        /// <summary>
        /// 对传入的窗体进行关闭校验
        /// </summary>
        /// <param name="mingcheng"></param>
        /// <param name="kongjian"></param>
        /// <returns></returns>
        public bool CloseForm(string mingcheng, Control kongjian)
        {
            bool res = false;
            foreach (Control item in kongjian.Controls)
            {
                if (item is Form)
                {
                    Form chuanti = (Form)item;
                    if (chuanti.Name == mingcheng)
                    {
                        res = true;
                        break;
                    }
                    else
                    {
                        chuanti.Close();
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 首选使用
        /// </summary>
        /// <param name="MFdx"></param>
        public void OpenFrom(FromDX MFdx)
        {

            if (CloseForm(MFdx.DX_mingcheng, MFdx.DX_kongjian) == false)
            {
                OpenFrom(MFdx.DX_chuangti, MFdx.DX_kongjian);
            }
        }
    }
    /// <summary>
    /// 用于打开那个窗体
    /// </summary>
    public class FromDX
    {

        /// <summary>
        /// 窗体Name属性
        /// </summary>
        public string DX_mingcheng { get; set; }

        /// <summary>
        /// 父控件是谁
        /// </summary>
        public Control DX_kongjian { get; set; }

        /// <summary>
        /// 显示窗体
        /// </summary>
        public Form DX_chuangti { get; set; }
        /// <summary>
        /// 构造函数，不需要赋值属性了
        /// </summary>
        /// <param name="from"></param>
        /// <param name="rongqikongjian"></param>
        public FromDX(Form from, Control rongqikongjian)
        {
            if (from != null)
            {
                DX_chuangti = from;
                DX_mingcheng = DX_chuangti.Name;
            }
            DX_kongjian = rongqikongjian;
        }
        /// <summary>
        /// 需要赋值属性
        /// </summary>
        public FromDX()
        {

        }

    }
}
