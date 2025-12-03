using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseUI.DaYIngMoBan.Model;

namespace BaseUI.DaYIngMoBan.TuXing
{
    internal abstract class FuTuLei
    {
     
        #region 公开的属性
        /// <summary>
        /// 第一点
        /// </summary>
        [Browsable(true)]
        public Point D1point { get; set; } = new Point(0, 0);

        /// <summary>
        /// 第二点
        /// </summary>
        [Browsable(true)]
        public Point D2point { get; set; } = new Point(0, 0);

        [Browsable(true)]
        public int TongShuID { get; set; } = -1;
        /// <summary>
        /// 图类型
        /// </summary>
        [Browsable(true)]
        public virtual TuLeiType Type
        {
            get { return TuLeiType.Wu; }
        }
        /// <summary>
        /// 文本内容
        /// </summary>
        [Browsable(true)]
        public virtual string ConntText { get; set; } = "";

        /// <summary>
        /// 绑定的类型
        /// </summary>
        [Browsable(true)]
        public virtual string BanDingText { get; set; } = "";
     
        /// <summary>
        /// 是否静态文本
        /// </summary>
        [Browsable(true)]
        public virtual bool IsJingTaiBianLiang { get; set; } = false;


        /// <summary>
        /// 表示是否选中
        /// </summary>
        public bool IsXuanZhong { get; set; } = false;
        #endregion

        [Browsable(false)]
        public virtual JianTouNS JianTouNS { get { return _JianTouNS; } }
        /// <summary>
        /// 鼠标的方向
        /// </summary>
        protected JianTouNS _JianTouNS = JianTouNS.Wu;
     
        /// <summary>
        /// 画自己
        /// </summary>
        /// <param name="e"></param>
        public abstract void HuaZiJi(Graphics e);

        public abstract bool IsShuBiaoShiFouZai(Point point);

        public abstract FuTuLei FuZhi(FuTuLei fu);

        /// <summary>
        /// true表示明细
        /// </summary>
        /// <param name="ismingxi"></param>
        /// <returns></returns>
        public abstract BiaoQian GetModel(out bool ismingxi);

        public abstract void SetModel(BiaoQian biaoQian);

        public static FuTuLei FanHuiTuXin(BiaoQian biaoQian)
        {
            if (biaoQian==null)
            {
                return null;
            }
            if (biaoQian.Type == 1)
            {
                FuTuLei fu = new DrawText();
                fu.SetModel(biaoQian);
                return fu;
            }
            else if (biaoQian.Type == 2)
            {
                FuTuLei fu = new DrawLine();
                fu.SetModel(biaoQian);
                return fu;
            }
            else if (biaoQian.Type == 3)
            {
                FuTuLei fu = new DrawRect();
                fu.SetModel(biaoQian);
                return fu;
            }
            else if (biaoQian.Type == 4)
            {
                FuTuLei fu = new DrawEWM();
                fu.SetModel(biaoQian);
                return fu;
            }
            else if (biaoQian.Type == 5)
            {
                DrawTuPian fu = new DrawTuPian();
                fu.SetModel(biaoQian);
                return fu;
            }
            return null;
        }
        
        public static FuTuLei JianLiMoXing(TuLeiType tuLeiType)
        {
           
            if (tuLeiType==TuLeiType.WenZi)
            {
                FuTuLei fu = new DrawText();
               
                return fu;
            }
            else if (tuLeiType == TuLeiType.ZhiXian)
            {
                FuTuLei fu = new DrawLine();
              
                return fu;
            }
            else if (tuLeiType == TuLeiType.JuXing)
            {
                FuTuLei fu = new DrawRect();
            
                return fu;
            }
            else if (tuLeiType==TuLeiType.ErWeiMa)
            {
                FuTuLei fu = new DrawEWM();
               
                return fu;
            }
            else if (tuLeiType == TuLeiType.TuPian)
            {
                FuTuLei fu = new DrawTuPian();

                return fu;
            }
            return null;
        }

        public  void SetWeiZhi(Point point, int diji)
        {
            switch (diji)
            {
                case 1:
                    {
                        D1point = new Point(point.X, point.Y);
                    }
                    break;
                case 2:
                    {
                        D2point = new Point(point.X, point.Y);
                    }
                    break;
                default:
                    break;
            }
        }

        public JianTouNS GetShuBiaoNS(Point point)
        {

            if (IsXuanZhong == false)
            {
                _JianTouNS = JianTouNS.Wu;
                return JianTouNS.Wu;
            }
            JianTouNS jianTouNS = JianTouNS.Wu;
            switch (Type)
            {
                case TuLeiType.JuXing:
                    {
                        for (int i = 1; i <= 8; i++)
                        {
                            if (IsHanYou(i, point))
                            {
                                jianTouNS = ChuXian(i);
                                break;
                            }
                        }
                    }
                    break;
                case TuLeiType.ErWeiMa:
                    {
                        for (int i = 1; i <= 8; i++)
                        {
                            if (IsHanYou(i, point))
                            {
                                jianTouNS = ChuXian(i);
                                break;
                            }
                        }
                    }
                    break;
                case TuLeiType.TuPian:
                    {
                        for (int i = 1; i <= 8; i++)
                        {
                            if (IsHanYou(i, point))
                            {
                                jianTouNS = ChuXian(i);
                                break;
                            }
                        }
                    }
                    break;
                case TuLeiType.WenZi:
                    {
                        for (int i = 1; i <= 8; i++)
                        {
                            if (IsHanYou(i, point))
                            {
                                jianTouNS = ChuXian(i);
                                break;
                            }
                        }
                    }
                    break;
                case TuLeiType.ZhiXian:
                    {
                        for (int i = 1; i <= 3; i += 2)
                        {
                            if (IsHanYou(i, point))
                            {
                                jianTouNS = ChuXian(i);
                                break;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            if (jianTouNS == JianTouNS.Wu)
            {
                bool zhen = IsShuBiaoShiFouZai(point);
                if (zhen)
                {
                    jianTouNS = JianTouNS.QuBu;
                }
            }
            _JianTouNS = jianTouNS;
            return jianTouNS;
        }

        private JianTouNS ChuXian(int zhuangtai)
        {
            JianTouNS jianTouNS = JianTouNS.Wu;
            switch (zhuangtai)
            {
                case 1:
                    {
                        jianTouNS = JianTouNS.Zuo1ShangXia;
                    }
                    break;
                case 2:
                    {
                        jianTouNS = JianTouNS.Shang;
                    }
                    break;
                case 3:
                    {
                        jianTouNS = JianTouNS.You1ShangXia;
                    }
                    break;
                case 4:
                    {
                        jianTouNS = JianTouNS.You;
                    }
                    break;
                case 5:
                    {
                        jianTouNS = JianTouNS.You2ShangXia;
                    }
                    break;
                case 6:
                    {
                        jianTouNS = JianTouNS.Xia;
                    }
                    break;
                case 7:
                    {
                        jianTouNS = JianTouNS.Zuo2ShangXia;
                    }
                    break;
                case 8:
                    {
                        jianTouNS = JianTouNS.Zuo;
                    }
                    break;
                default:
                    break;
            }
            return jianTouNS;
        }

        protected bool IsHanYou(int fangxiang, Point point)
        {
            Rectangle rectangle = GetRectangle(fangxiang);
            if (rectangle.Contains(point))
            {
                return true;
            }
            return false;
        }

        protected abstract Rectangle GetRectangle(int fangxiang);
       

    }

    /// <summary>
    /// 图类型
    /// </summary>
    public enum TuLeiType
    {
        /// <summary>
        /// 无图
        /// </summary>
        Wu = 0,
        /// <summary>
        /// 直线
        /// </summary>
        ZhiXian = 1,
        /// <summary>
        /// 矩形
        /// </summary>
        JuXing = 2,
        /// <summary>
        /// 文字
        /// </summary>
        WenZi = 3,
        /// <summary>
        /// 图片
        /// </summary>
        TuPian = 4,
        /// <summary>
        /// 二维码
        /// </summary>
        ErWeiMa=5,
    }

    /// <summary>
    /// 鼠标的方向
    /// </summary>
    public enum JianTouNS
    {
        /// <summary>
        /// 上
        /// </summary>
        Shang = 0,
        /// <summary>
        /// 下
        /// </summary>
        Xia = 1,
        /// <summary>
        /// 左
        /// </summary>
        Zuo = 2,
        /// <summary>
        /// 右
        /// </summary>
        You = 3,
        /// <summary>
        /// 左1上下
        /// </summary>
        Zuo1ShangXia = 4,
        /// <summary>
        /// 左2上下
        /// </summary>
        Zuo2ShangXia = 5,
        /// <summary>
        /// 右2上下
        /// </summary>
        You1ShangXia = 6,
        /// <summary>
        /// 右2上下
        /// </summary>
        You2ShangXia = 7,
        /// <summary>
        /// 表示没有
        /// </summary>
        Wu = 8,
        /// <summary>
        /// 在里面全部
        /// </summary>
        QuBu = 9
    }
}
