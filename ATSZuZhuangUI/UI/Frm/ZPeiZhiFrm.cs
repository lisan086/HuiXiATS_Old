using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATSFoZhaoZuZhuangUI.PeiZhi.Frm;
using ATSJianMianJK;
using JieMianLei.FuFrom;
using ZuZhuangUI.Lei;
using ZuZhuangUI.Model;
using ZuZhuangUI.PeiZhi.Frm;

namespace ATSFoZhaoZuZhuangUI.UI.Frm
{
    public partial class ZPeiZhiFrm : BaseFuFrom
    {
        
        private ZiYuanModel ZiYuanModel;
        public ZPeiZhiFrm()
        {
            InitializeComponent();
            this.IsZhiXianShiX = true;
        }
        public void SetZhiYuan(ZiYuanModel ziYuanModel)
        {
            ZiYuanModel = ziYuanModel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string msg = "";
            bool zhen = ZiYuanModel.QuanXian.IsYouQuanXian("配置通道", out msg);
            if (zhen)
            {
                ZhanDianFrm zhanDianFrm = new ZhanDianFrm();
                zhanDianFrm.ShowDialog(this);
                if (zhanDianFrm.IsBaoCun)
                {
                    JieMianCaoZuoModel ssssd = new JieMianCaoZuoModel();
                    ssssd.CanShu = zhanDianFrm.QieHuanPeiFangName;
                    JieMianCaoZuoLei.CerateDanLi().JieMianCuoZuo(DoType.HuanPeiFang, ssssd);
                }
            }
            else
            {
                ZiYuanModel.TiShiKuang(msg);
            }
        }

      

        private void button3_Click(object sender, EventArgs e)
        {
            string msg = "";
            bool zhen = ZiYuanModel.QuanXian.IsYouQuanXian("码管理", out msg);
            if (zhen)
            {
                MaGuanLiFrm maGuanLiFrm = new MaGuanLiFrm();

                maGuanLiFrm.ShowDialog(this);
            }
            else
            {
                ZiYuanModel.TiShiKuang(msg);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string msg = "";
            bool zhen = ZiYuanModel.QuanXian.IsYouQuanXian("系统配置", out msg);
            if (zhen)
            {
                XiTongPeiZhiFrm maGuanLiFrm = new XiTongPeiZhiFrm();

                maGuanLiFrm.ShowDialog(this);
            }
            else
            {
                ZiYuanModel.TiShiKuang(msg);
            }
        }
    }
}
