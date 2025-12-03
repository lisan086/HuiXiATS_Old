using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommLei.JieMianLei
{
    /// <summary>
    /// 跨线程调用控件
    /// </summary>
    public class Controlinkove
    {
        /// <summary>
        /// 那个窗体
        /// </summary>
        protected Form _form;

        /// <summary>
        /// 控制参数，防止窗体关闭时，还在调用控件，默认是true，为false就会跳出
        /// </summary>
        private bool _FangZhiGuanBiBaoCuo = true;
        /// <summary>
        /// 控制参数，防止窗体关闭时，还在调用控件，默认是true，为false就会跳出
        /// </summary>
        public bool FangZhiGuanBiBaoCuo
        {
            get { return _FangZhiGuanBiBaoCuo; }
            set { _FangZhiGuanBiBaoCuo = value; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="form"></param>
        public Controlinkove(Form form)
        {
            _form = form;
        }
        /// <summary>
        /// 用于子窗体的设置
        /// </summary>
        /// <param name="form"></param>
        public void SetFrom(Form form)
        {
            _form = form;
        }

        #region 控件单个文本变化
        /// <summary>
        /// 跨线程给控件文本赋值
        /// </summary>
        /// <param name="Kongjian"></param>
        /// <param name="Text"></param>
        public void ContorlText(Control Kongjian, string Text)
        {
            if (_FangZhiGuanBiBaoCuo == false)
            {
                return;
            }
            try
            {
                _form.Invoke(new Action<string>((x) => { Kongjian.Text = x; }), Text);
            }
            catch
            {

            }
        }

        #endregion

        #region 改变控件的背景色
        /// <summary>
        /// 控件的背景色
        /// </summary>
        /// <param name="Kongjian"></param>
        /// <param name="Text"></param>
        public void ContorlBackColor(Control Kongjian, Color Text)
        {
            if (_FangZhiGuanBiBaoCuo == false)
            {
                return;
            }
            try
            {
                _form.Invoke(new Action<Color>((x) => { Kongjian.BackColor = x; }), Text);
            }
            catch
            {

            }
        }
        #endregion

        #region 清理一些文本文件
        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="Kongjian"></param>
        public void Clear(TextBox Kongjian)
        {
            if (_FangZhiGuanBiBaoCuo == false)
            {
                return;
            }
            try
            {
                _form.Invoke(new Action(() => { Kongjian.Clear(); }));
            }
            catch
            {

            }
        }
        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="Kongjian"></param>
        public void Clear(Label Kongjian)
        {
            if (_FangZhiGuanBiBaoCuo == false)
            {
                return;
            }
            try
            {
                _form.Invoke(new Action(() => { Kongjian.Text = ""; }));
            }
            catch
            {

            }
        }
        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="Kongjian"></param>
        /// <param name="LeiRong"></param>
        public void Clear(Label Kongjian, string LeiRong)
        {
            if (_FangZhiGuanBiBaoCuo == false)
            {
                return;
            }
            try
            {
                _form.Invoke(new Action<string>((x) => { Kongjian.Text = x; }), LeiRong);
            }
            catch
            {

            }
        }
        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="Kongjian"></param>
        public void Clear(ListView Kongjian)
        {
            if (_FangZhiGuanBiBaoCuo == false)
            {
                return;
            }
            try
            {
                _form.Invoke(new Action(() => { Kongjian.Items.Clear(); }));
            }
            catch
            {

            }
        }
        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="Kongjian"></param>
        public void Clear(ListBox Kongjian)
        {
            if (_FangZhiGuanBiBaoCuo == false)
            {
                return;
            }
            try
            {
                _form.Invoke(new Action(() => { Kongjian.Items.Clear(); }));
            }
            catch
            {

            }
        }
        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="Kongjian"></param>
        public void Clear(ComboBox Kongjian)
        {
            if (_FangZhiGuanBiBaoCuo == false)
            {
                return;
            }
            try
            {
                _form.Invoke(new Action(() => { Kongjian.Items.Clear(); }));
            }
            catch
            {

            }
        }
        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="Kongjian"></param>
        public void Clear(RichTextBox Kongjian)
        {
            if (_FangZhiGuanBiBaoCuo == false)
            {
                return;
            }
            try
            {
                _form.Invoke(new Action(() => { Kongjian.Clear(); }));
            }
            catch
            {

            }
        }
        #endregion

        #region 泛型的方法
        /// <summary>
        /// 泛型执行跨线程操作控件
        /// </summary>
        /// <typeparam name="T">执行的文本</typeparam>
        /// <typeparam name="V">控件</typeparam>
        /// <param name="Kongjian"></param>
        /// <param name="Tneirong"></param>
        /// <param name="zuoshenmeshi"></param>
        public void FanXingGaiBing<T, V>(T Kongjian, V Tneirong, Action<T, V> zuoshenmeshi)
        {
            if (_FangZhiGuanBiBaoCuo == false)
            {
                return;
            }
            try
            {
                _form.Invoke(new Action(() => {
                    if (zuoshenmeshi != null)
                    {
                        zuoshenmeshi(Kongjian, Tneirong);
                    }

                }));
            }
            catch
            {

            }
        }
        /// <summary>
        /// 泛型执行跨线程操作控件
        /// </summary>
        ///<param name="zuoshenmeshi"></param>
        public void FanXingGaiBing(Action zuoshenmeshi)
        {
            if (_FangZhiGuanBiBaoCuo == false)
            {
                return;
            }
            try
            {
                _form.Invoke(new Action(() =>
                {
                    if (zuoshenmeshi != null)
                    {
                        zuoshenmeshi();
                    }

                }));
            }
            catch
            {

            }
        }




        /// <summary>
        /// 泛型执行跨线程操作控件
        /// </summary>
        /// <typeparam name="T">执行的文本</typeparam>
        ///  <param name="Kongjian"></param>
        /// <param name="zuoshenmeshi"></param>
        public void FanXingGaiBing<T>(T Kongjian, Action<T> zuoshenmeshi)
        {
            if (_FangZhiGuanBiBaoCuo == false)
            {
                return;
            }
            try
            {
                _form.Invoke(new Action(() =>
                {
                    if (zuoshenmeshi != null)
                    {
                        zuoshenmeshi(Kongjian);
                    }

                }));
            }
            catch
            {

            }
        }

        #endregion

        //以后增加
    }
}
