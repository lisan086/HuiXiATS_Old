using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JieMianLei.UI.TiShiKuang
{
    public class TiShiKuangLei
    {
        private TiShiKuang _TiShiChuang = null;
        private Dictionary<string, CanShuModel> _BangDingZiDian = new Dictionary<string, CanShuModel>();

        private Control GetFuKongJian(Control fuform)
        {
            if (fuform is Form)
            {
                return fuform;
            }
            else
            {
                return fuform.Parent;
            }
        }

        public void BangDingKongJian(Control kongjian, Func<List<string>> neirong, int gaodu = 400)
        {
            if (kongjian == null)
            {
                return;
            }
            if (neirong == null)
            {
                return;
            }
            if (!_BangDingZiDian.ContainsKey(kongjian.Name))
            {
                CanShuModel model = new CanShuModel();
                model.GetNeiRong = neirong;
                model.GaoDuo = gaodu;
                model.FuKongJian = GetFuKongJian(kongjian);
                _BangDingZiDian.Add(kongjian.Name, model);
            }
            kongjian.MouseHover += kongjian_MouseEnter;
            kongjian.MouseLeave += kongjian_MouseLeave;
        }

        void kongjian_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (_TiShiChuang != null)
                {
                    _TiShiChuang.CloseGuanBi();
                    _TiShiChuang = null;
                }

            }
            catch
            {
                _TiShiChuang = null;
            }

        }

        void kongjian_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                _TiShiChuang = null;
                if (_TiShiChuang == null)
                {
                    if (sender is Control)
                    {
                        Control kongjian = sender as Control;
                        CanShuModel model = _BangDingZiDian[kongjian.Name];
                        if (model != null)
                        {
                            List<string> neirong = new List<string>();
                            _TiShiChuang = new TiShiKuang();
                            if (model.GetNeiRong != null)
                            {
                                neirong = model.GetNeiRong.Invoke();
                            }
                            Point p = model.FuKongJian.PointToScreen(kongjian.Location);

                            _TiShiChuang.ShowTiShiKuang(neirong, model.FuKongJian, p, kongjian.Width, model.GaoDuo, kongjian.Location);
                        }

                    }

                }
            }
            catch
            {
                _TiShiChuang = null;
            }
        }
    }

    internal class CanShuModel
    {
        public Func<List<string>> GetNeiRong { get; set; }
        public int GaoDuo { get; set; }
        public Control FuKongJian { get; set; }
        public CanShuModel()
        {
            GetNeiRong = null;
            GaoDuo = 40;
        }
    }


    public class TiShiRenYiKuang
    {
        private TiShiKuang _TiShiChuang = null;
        private Dictionary<string, RenYiModel> _BangDingZiDian = new Dictionary<string, RenYiModel>();

        private Control GetFuKongJian(Control fuform)
        {
            if (fuform is Form)
            {
                return fuform;
            }
            else
            {
                return fuform.Parent;
            }
        }

        public void BangDingKongJian(Control kongjian, Func<object> setcanshu, int gaodu,int kuangdu, IRenYiKJ xuyaoxianshikj)
        {
            if (kongjian == null)
            {
                return;
            }
            if (setcanshu == null)
            {
                return;
            }
            if (xuyaoxianshikj==null)
            {
                return;
            }
            if (!_BangDingZiDian.ContainsKey(kongjian.Name))
            {
                RenYiModel model = new RenYiModel();
                model.GetNeiRong = setcanshu;
                model.GaoDuo = gaodu;
                model.KuanDu = kuangdu;
                model.FuKongJian = GetFuKongJian(kongjian);
                model.RenYiKJ = xuyaoxianshikj;
                _BangDingZiDian.Add(kongjian.Name, model);
            }
            kongjian.MouseHover += kongjian_MouseEnter;
            kongjian.MouseLeave += kongjian_MouseLeave;
        }

        void kongjian_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (_TiShiChuang != null)
                {
                    _TiShiChuang.CloseGuanBi();
                    _TiShiChuang = null;
                }

            }
            catch
            {
                _TiShiChuang = null;
            }

        }

        void kongjian_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                _TiShiChuang = null;
                if (_TiShiChuang == null)
                {
                    if (sender is Control)
                    {
                        Control kongjian = sender as Control;
                        RenYiModel model = _BangDingZiDian[kongjian.Name];
                        if (model != null)
                        {
                            object  neirong = new object();
                            _TiShiChuang = new TiShiKuang();
                            if (model.GetNeiRong != null)
                            {
                                 neirong = model.GetNeiRong.Invoke();
                            }
                            Point p = model.FuKongJian.PointToScreen(kongjian.Location);
                            model.RenYiKJ.SetCanShu(neirong);
                            _TiShiChuang.ShowTiShiKuang(model.FuKongJian, p, kongjian.Width, model.GaoDuo, kongjian.Location, model.RenYiKJ.GetKJ(), model.KuanDu);
                        }

                    }

                }
            }
            catch
            {
                _TiShiChuang = null;
            }
        }
    }


    internal class RenYiModel
    {
        public Func<object> GetNeiRong { get; set; }
        public int GaoDuo { get; set; }

        public int KuanDu { get; set; }
        public Control FuKongJian { get; set; }

        public IRenYiKJ RenYiKJ { get; set; }
        public RenYiModel()
        {
            GetNeiRong = null;
            GaoDuo = 40;
            KuanDu = 40;
        }
    }


    public interface IRenYiKJ
    {
        void SetCanShu(object canshu);
        Control GetKJ();
    }
}
