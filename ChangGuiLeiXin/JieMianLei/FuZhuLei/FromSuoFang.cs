using CommLei.JieMianLei;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseUI.FuZhuLei
{
    public class FromSuoFang
    {
        
        private Dictionary<Control, JiLuKJMolde> JiLu = new Dictionary<Control, JiLuKJMolde>();

        public void Add(Control control,bool iswenzisuofang)
        {
         
            if (JiLu.ContainsKey(control))
            {
                JiLu[control]._IsDuiWenZiSuoFang = iswenzisuofang;
            }
            else
            {
                JiLuKJMolde kJMolde = new JiLuKJMolde();
                kJMolde.AddKJ(control, iswenzisuofang);
                JiLu.Add(control, kJMolde);
            }
        }
        public void YiChu(Control control)
        {

            if (JiLu.ContainsKey(control))
            {
                JiLu.Remove(control);
            }
          
        }
        public void GuaQi()
        {
            List<Control> keys = JiLu.Keys.ToList();
            foreach (var item in keys)
            {
                item.SuspendLayout();
            }
        }

        public void ShuiXing()
        {
            List<Control> keys = JiLu.Keys.ToList();
            foreach (var item in keys)
            {
                item.ResumeLayout(false);
                item.PerformLayout();
            }
        }
        public void GengGaiGao()
        {
            if (JiLu.Count > 0)
            {
                List<Control> keys = JiLu.Keys.ToList();
             
                foreach (var item in keys)
                {
                    float kuang = item.Width;
                    float gao = item.Height;
                    JiLu[item].SuoFangKongJian(kuang, gao);

                }
              

            }
        }
     
    }

    public class JiLuKJMolde
    {
        /// <summary>
        /// 
        /// </summary>
        public List<int> Top { get; set; } = new List<int>();
        /// <summary>
        /// 
        /// </summary>
        public List<int> Left { get; set; } = new List<int>();
        /// <summary>
        /// 
        /// </summary>
        public List<int> Heigth { get; set; } = new List<int>();
        /// <summary>
        /// 
        /// </summary>
        protected List<int> Wigth { get; set; } = new List<int>();
        /// <summary>
        /// 
        /// </summary>
        public List<float> ZiTiDaXiao { get; set; } = new List<float>();
        /// <summary>
        /// 
        /// </summary>
        public List<Control> BianHuaKongJian { get; set; } = new List<Control>();

      

        public List<int> FormKG { get; set; } = new List<int>();

        public bool _IsDuiWenZiSuoFang { get; set; } = true;


        public void AddKJ(Control form,bool iswenzisuofang)
        {
        
            FormKG.Add(form.Width);
            FormKG.Add(form.Height);
            _IsDuiWenZiSuoFang = iswenzisuofang;
            foreach (Control item in form.Controls)
            {
                CunKongJian(item);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kongjian"></param>
        private  void CunKongJian(Control kongjian)
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

        private void RemoveVirtualBorder(object obj)
        {
            //MethodInfo methodinfo = obj.GetType().GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod);
            //methodinfo.Invoke(obj, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, new object[] { ControlStyles.Selectable, false }, Application.CurrentCulture);
        }

        public void SuoFangKongJian(float Kuang, float Gao)
        {
            if (BianHuaKongJian.Count > 0)
            {
                float kuangbilv = (Kuang / (float)FormKG[0]);
                float Gaobilv = (Gao / (float)FormKG[1]);
                float zuixiaozhi = Math.Min(kuangbilv, Gaobilv);
                for (int i = 0; i < BianHuaKongJian.Count; i++)
                {
                 
                   int  Width = (int)(DataSiSheWuRu((double)(Wigth[i] * kuangbilv), WeiShu.Shi, JinWeiType.Wu));
                    int Height = (int)(DataSiSheWuRu((double)(Heigth[i] * Gaobilv), WeiShu.Shi, JinWeiType.Wu));
                    int Left1 = (int)(DataSiSheWuRu((double)(Left[i] * kuangbilv), WeiShu.Shi, JinWeiType.Wu));
                    int Top1 = (int)(DataSiSheWuRu((double)(Top[i] * Gaobilv), WeiShu.Shi, JinWeiType.Wu));
                    BianHuaKongJian[i].SetBounds(Left1, Top1, Width, Height);
                    if (_IsDuiWenZiSuoFang)
                    {
                        BianHuaKongJian[i].Font = new Font(BianHuaKongJian[i].Font.Name, zuixiaozhi * ZiTiDaXiao[i]);
                    }

                }

            }
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
