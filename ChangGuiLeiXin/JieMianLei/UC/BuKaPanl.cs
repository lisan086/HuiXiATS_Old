using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseUI.UC
{
    public class BuKaPanl : Panel
    {
        public BuKaPanl() : base()
        {
            this.DoubleBuffered = true;
        }
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000001;  // Turn on WS_EX_COMPOSITED  
        //        return cp;
        //    }
        //}
    }
}
