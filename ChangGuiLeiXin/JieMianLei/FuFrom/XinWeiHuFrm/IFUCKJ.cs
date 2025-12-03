using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseUI.FuFrom.XinWeiHuFrm
{
    public interface IFUCKJ<T> where T:new()
    {     
        void Clear();
        void SetCanShu(T canshu);

        T GetModel();

        int GetSunXu();

        void SetSunXu(int sunxu);
    }
}
