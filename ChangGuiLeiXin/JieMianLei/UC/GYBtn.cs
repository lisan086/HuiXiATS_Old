using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseUI.UC
{
    public class GYBtn : Button
    {

        public event Action<object, DianJiModel> DianJiEvent;
        /// <summary>
        /// 构造函数
        /// </summary>
        public GYBtn()
            : base()
        {
            this.DoubleBuffered = true;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Click += GYBtn_Click;
            this.MouseDown += GYBtn_MouseDown;
            this.MouseUp += GYBtn_MouseUp;
        }

        private void GYBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (DianJiEvent!=null)
            {
                DianJiModel model = new DianJiModel();
                model.BtnState = BtnState.MouseUp;
                model.IsChangAn = true;
                DianJiEvent(this, model);
            }
        }

        private void GYBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (DianJiEvent != null)
            {
                DianJiModel model = new DianJiModel();
                model.BtnState = BtnState.MouseDown;
                model.IsChangAn = true;
                DianJiEvent(this, model);
            }
        }

        private void GYBtn_Click(object sender, EventArgs e)
        {
            if (DianJiEvent != null)
            {
                DianJiModel model = new DianJiModel();
                model.BtnState = BtnState.Click;
                model.IsChangAn = false;
                DianJiEvent(this, model);
            }
        }
    }

    /// <summary>
    ///按着状态
    /// </summary>
    public enum BtnState
    {
        Click = 0,
        MouseDown=1,
        MouseUp=2,
        NO=3,
    }

    public class DianJiModel: EventArgs
    {
        public BtnState BtnState { get; set; } = BtnState.NO;

        /// <summary>
        /// true  表示长按
        /// </summary>
        public bool IsChangAn { get; set; } = false;
    }
}
