using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JieMianLei.FuFrom.KJ
{
    public class MoveShiJianKJ
    {
        // private Control Control;
        /// <summary>
        /// 绑定移动事件
        /// </summary>
        public void BangDingMove(Control control)
        {

            PointsModel model = new PointsModel();

            control.Tag = model;
            control.MouseDown += new MouseEventHandler(control_MouseDown);

            control.MouseUp += new MouseEventHandler(control_MouseUp);

            control.MouseMove += new MouseEventHandler(control_MouseMove);
        }

        void control_MouseMove(object sender, MouseEventArgs e)
        {

            if ((sender as Control).Tag != null)
            {
                Control control = sender as Control;
                PointsModel model = control.Tag as PointsModel;
                if (model != null)
                {
                    if (model.IsAnXia)
                    {
                        int x = e.X - model.ChuShiDian.X;
                        int y = e.Y - model.ChuShiDian.Y;
                        control.Parent.Location = new Point(control.Parent.Location.X + x, control.Parent.Location.Y + y);
                    }
                }
            }
        }

        void control_MouseUp(object sender, MouseEventArgs e)
        {

            try
            {
                PointsModel model = (sender as Control).Tag as PointsModel;
                model.ChuShiDian = new Point(e.X, e.Y);
                model.IsAnXia = false;
            }
            catch
            {


            }

            //  (sender as Control).Tag = model;
        }

        void control_MouseDown(object sender, MouseEventArgs e)
        {

            try
            {
                PointsModel model = (sender as Control).Tag as PointsModel;
                model.ChuShiDian = new Point(e.X, e.Y);
                model.IsAnXia = true;
            }
            catch
            {


            }


        }
    }
    internal class PointsModel
    {
        public Point ChuShiDian { get; set; }

        public bool IsAnXia { get; set; }

        public PointsModel()
        {
            ChuShiDian = new Point(0, 0);
            IsAnXia = false;
        }
    }
}
