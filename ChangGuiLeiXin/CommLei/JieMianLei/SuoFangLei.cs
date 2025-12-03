using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommLei.JieMianLei
{
    /// <summary>
    /// 窗体缩放类
    /// </summary>
    public class SuoFangLei
    {

     
        /// <summary>
        /// 
        /// </summary>
        protected List<int> Top = new List<int>();
        /// <summary>
        /// 
        /// </summary>
        protected List<int> Left = new List<int>();
        /// <summary>
        /// 
        /// </summary>
        protected List<int> Heigth = new List<int>();
        /// <summary>
        /// 
        /// </summary>
        protected List<int> Wigth = new List<int>();
        /// <summary>
        /// 
        /// </summary>
        protected List<float> ZiTiDaXiao = new List<float>();
        /// <summary>
        /// 
        /// </summary>
        protected List<Control> BianHuaKongJian = new List<Control>();

        private Form _YuanShiForm;

        private List<int> FormKG = new List<int>();

        private bool _IsDuiWenZiSuoFang = true;

        private Control FuKongJian = null;
        /// <summary>
        /// 是否对文字进行缩放
        /// </summary>
        public bool IsDuiWenZiSuoFang
        {
            get { return _IsDuiWenZiSuoFang; }
            set { _IsDuiWenZiSuoFang = value; }
        }

        private enum WeiShu
        {
            /// <summary>
            /// 十分位
            /// </summary>
            Shi = 0,
            /// <summary>
            /// 百分位
            /// </summary>
            Bai = 1,
            /// <summary>
            /// 千分位
            /// </summary>
            Qian = 2

        }

        private enum JinWeiType
        {
            /// <summary>
            /// 五近位
            /// </summary>
            Wu = 0,
            /// <summary>
            ///六近位
            /// </summary>
            Liu = 1,


        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="form"></param>
        public SuoFangLei(Control form)
        {
            Top.Clear();
            Left.Clear();
            Heigth.Clear();
            Wigth.Clear();
            ZiTiDaXiao.Clear();
            BianHuaKongJian.Clear();
            FormKG.Clear();
            FormKG.Add(form.Width);
            FormKG.Add(form.Height);
            FuKongJian = form;
            foreach (Control item in form.Controls)
            {
                CunKongJian(item);
            }
            form.SizeChanged += form_SizeChanged;
        }

        void form_SizeChanged(object sender, EventArgs e)
        {
            if (sender is Control)
            {
                try
                {
                    float kuang = (sender as Control).Width;
                    float gao = (sender as Control).Height;
                    if (this != null)
                    {
                      

                        SuoFangKongJian(kuang, gao);                  
                      
                    }
                }
                catch
                {


                }
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="form"></param>
        public SuoFangLei(Form form)
        {
            Top.Clear();
            Left.Clear();
            Heigth.Clear();
            Wigth.Clear();
            ZiTiDaXiao.Clear();
            BianHuaKongJian.Clear();
            FormKG.Clear();
            _YuanShiForm = form;
            FormKG.Add(form.Width);
            FormKG.Add(form.Height);
            FuKongJian = form;
            foreach (Control item in _YuanShiForm.Controls)
            {
                CunKongJian(item);
            }
            form.SizeChanged += form_ResizeEnd;
        }

        void form_ResizeEnd(object sender, EventArgs e)
        {
            if (sender is Form)
            {
                try
                {
                    float kuang = (sender as Form).Width;
                    float gao = (sender as Form).Height;
                    if (this != null)
                    {
                       // SendMessage(FuKongJian.Handle, WM_SETREDRAW, 0, IntPtr.Zero);//禁止重绘
                        SuoFangKongJian(kuang, gao);
                      //  SendMessage(FuKongJian.Handle, WM_SETREDRAW, 1, IntPtr.Zero);//取消禁止
                       // FuKongJian.Refresh();
                    }
                }
                catch
                {


                }
            }
        }
        /// <summary>
        /// 解决有些不需要进行的缩放的控件
        /// </summary>
        /// <param name="form"></param>
        /// <param name="Liskongjian"></param>
        public SuoFangLei(Form form, List<Control> Liskongjian)
        {
            Top.Clear();
            Left.Clear();
            Heigth.Clear();
            Wigth.Clear();
            ZiTiDaXiao.Clear();
            BianHuaKongJian.Clear();
            FormKG.Clear();
            _YuanShiForm = form;
            FormKG.Add(form.Width);
            FormKG.Add(form.Height);
            CunKongJian1(Liskongjian);
            FuKongJian = form;
            form.SizeChanged += form_ResizeEnd;
        }
        /// <summary>
        /// 解决有些不需要进行的缩放的控件
        /// </summary>
        /// <param name="form"></param>
        /// <param name="Liskongjian"></param>
        public SuoFangLei(Control form, List<Control> Liskongjian)
        {
            Top.Clear();
            Left.Clear();
            Heigth.Clear();
            Wigth.Clear();
            ZiTiDaXiao.Clear();
            BianHuaKongJian.Clear();
            FormKG.Clear();
            FormKG.Add(form.Width);
            FormKG.Add(form.Height);
            CunKongJian1(Liskongjian);
            FuKongJian = form;
            form.SizeChanged += form_SizeChanged;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Liskongjian"></param>
        protected virtual void CunKongJian1(List<Control> Liskongjian)
        {
            if (Liskongjian.Count > 0)
            {
                for (int i = 0; i < Liskongjian.Count; i++)
                {
                    BianHuaKongJian.Add(Liskongjian[i]);
                    Top.Add(Liskongjian[i].Top);
                    Left.Add(Liskongjian[i].Left);
                    Heigth.Add(Liskongjian[i].Height);
                    Wigth.Add(Liskongjian[i].Width);
                    RemoveVirtualBorder(Liskongjian[i]);
                    ZiTiDaXiao.Add(Liskongjian[i].Font.Size);

                }
            }


        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="kongjian"></param>
        protected virtual void CunKongJian(Control kongjian)
        {
            BianHuaKongJian.Add(kongjian);
            Top.Add(kongjian.Top);
            Left.Add(kongjian.Left);
            Heigth.Add(kongjian.Height);
            Wigth.Add(kongjian.Width);
            RemoveVirtualBorder(kongjian);
            ZiTiDaXiao.Add(kongjian.Font.Size);
            foreach (Control item in kongjian.Controls)
            {
                RemoveVirtualBorder(item);
                Top.Add(item.Top);
                Left.Add(item.Left);
                Heigth.Add(item.Height);
                Wigth.Add(item.Width);
                ZiTiDaXiao.Add(item.Font.Size);
                BianHuaKongJian.Add(item);
                if (item.Controls.Count > 0)
                {
                    CunKongJian(item);
                }
            }


        }

        private void SuoFangKongJian(float Kuang, float Gao)
        {
            if (BianHuaKongJian.Count > 0)
            {
                float kuangbilv = (Kuang / (float)FormKG[0]);
                float Gaobilv = (Gao / (float)FormKG[1]);
                float zuixiaozhi = Math.Min(kuangbilv, Gaobilv);
                for (int i = 0; i < BianHuaKongJian.Count; i++)
                {
                  //  BianHuaKongJian[i].SuspendLayout();
                    BianHuaKongJian[i].Width = (int)(DataSiSheWuRu((double)(Wigth[i] * kuangbilv), WeiShu.Shi, JinWeiType.Wu));
                    BianHuaKongJian[i].Height = (int)(DataSiSheWuRu((double)(Heigth[i] * Gaobilv), WeiShu.Shi, JinWeiType.Wu));
                    BianHuaKongJian[i].Left = (int)(DataSiSheWuRu((double)(Left[i] * kuangbilv), WeiShu.Shi, JinWeiType.Wu));
                    BianHuaKongJian[i].Top = (int)(DataSiSheWuRu((double)(Top[i] * Gaobilv), WeiShu.Shi, JinWeiType.Wu));
                    if (_IsDuiWenZiSuoFang)
                    {
                        BianHuaKongJian[i].Font = new Font(BianHuaKongJian[i].Font.Name, zuixiaozhi * ZiTiDaXiao[i]);
                    }
                   // BianHuaKongJian[i].
                }
            }
        }

        private void RemoveVirtualBorder(object obj)
        {
            MethodInfo methodinfo = obj.GetType().GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod);
            methodinfo.Invoke(obj, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, new object[] { ControlStyles.Selectable, false }, Application.CurrentCulture);
        }


        /// <summary>
        ///  五入
        /// </summary>
        /// <param name="CanShu2">传进来的参数</param>
        /// <param name="weishu">在那个位置进位</param>
        /// <param name="type">是否是五开始进位</param>
        /// <returns></returns>
        private double DataSiSheWuRu(double CanShu2, WeiShu weishu, JinWeiType type)
        {
            switch (weishu)
            {
                case WeiShu.Shi:
                    {
                        switch (type)
                        {
                            case JinWeiType.Wu:
                                {
                                    int shuju = (int)CanShu2;
                                    double jishushuju = shuju + 1;
                                    double CanShu3 = CanShu2 + 0.5;
                                    if (CanShu3 >= jishushuju)
                                    {
                                        return jishushuju;
                                    }
                                    else
                                    {
                                        return CanShu2;
                                    }
                                }

                            case JinWeiType.Liu:
                                {
                                    int shuju = (int)CanShu2;
                                    double jishushuju = shuju + 1;
                                    double CanShu3 = CanShu2 + 0.4;
                                    if (CanShu3 >= jishushuju)
                                    {
                                        return jishushuju;
                                    }
                                    else
                                    {
                                        return CanShu2;
                                    }
                                }

                            default:
                                {
                                    return CanShu2;
                                }

                        }
                    }

                case WeiShu.Bai:
                    switch (type)
                    {
                        case JinWeiType.Wu:
                            {
                                int shuju = (int)(CanShu2 * 10);
                                int intcount = (int)CanShu2;
                                int canshu3 = (int)(CanShu2 * 10);

                                if (((CanShu2 - intcount) * 100) % 10 >= 5)
                                {
                                    shuju += 1;
                                    return (double)(shuju / (double)10);
                                }
                                else
                                {
                                    return (double)(canshu3 / (double)10);
                                }

                            }

                        case JinWeiType.Liu:
                            {
                                int shuju = (int)(CanShu2 * 10);
                                int intcount = (int)CanShu2;
                                int canshu3 = (int)(CanShu2 * 10);

                                if (((CanShu2 - intcount) * 100) % 10 >= 6)
                                {
                                    shuju += 1;
                                    return (double)(shuju / (double)10);
                                }
                                else
                                {
                                    return (double)(canshu3 / (double)10);
                                }
                            }

                        default:
                            {
                                return CanShu2;
                            }

                    }


                case WeiShu.Qian:
                    switch (type)
                    {
                        case JinWeiType.Wu:
                            {
                                int shuju = (int)(CanShu2 * 100);
                                int intcount = (int)CanShu2;
                                int canshu3 = (int)(CanShu2 * 100);

                                if (((CanShu2 - intcount) * 1000) % 10 >= 5)
                                {
                                    shuju += 1;
                                    return (double)(shuju / (double)100);
                                }
                                else
                                {
                                    return (double)(canshu3 / (double)100);
                                }

                            }

                        case JinWeiType.Liu:
                            {
                                int shuju = (int)(CanShu2 * 100);
                                int intcount = (int)CanShu2;
                                int canshu3 = (int)(CanShu2 * 100);

                                if (((CanShu2 - intcount) * 1000) % 10 >= 6)
                                {
                                    shuju += 1;
                                    return (double)(shuju / (double)100);
                                }
                                else
                                {
                                    return (double)(canshu3 / (double)100);
                                }
                            }

                        default:
                            {
                                return CanShu2;
                            }

                    }

                default:
                    return CanShu2;

            }
        }

    }
}
