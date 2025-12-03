using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSJianMianJK.Log;
using ATSJianMianJK;
using ZuZhuangUI.Model;
using System.Windows.Forms;

namespace ATSZuZhuangUI.Lei.GongNengLei
{
    public interface IFKJ
    {
        void SetLog(int tdtd, RiJiModel riji);
        void SetCanShu(ZiYuanModel ziYuanModel);
        void SetModel(SheBeiZhanModel ziYuanModel);
        void SetTanChuan(int tdtd, string msg);
        void SetCanShu(EventType arg1, JieMianShiJianModel model);
        Control GetKJ();
        void Close();
    }
}
