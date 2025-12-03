using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace BaseUI.UC
{
    public delegate void IndexBianHua(OutLockPage Page, int index);
    public class OutLockBar : Panel
    {
        /// <summary>
        /// 事件
        /// </summary>
        public event IndexBianHua IndexBianHuaEvent;
        /// <summary>
        /// 顶部高度
        /// </summary>
        private int buttonHeight = 25;

        /// <summary>
        /// 控件的集合
        /// </summary>
        private OutLockPageCollection m_xpanderPanels;

        /// <summary>
        /// 显示第几个页
        /// </summary>
        private int _XianShiIndex = -1;
        public int XianShiIndex
        {
            get { return _XianShiIndex; }

        }
        public int ButtonHeight
        {
            get
            {
                return buttonHeight;
            }

            set
            {
                buttonHeight = value;
                // do recalc layout for entire bar
                if (m_xpanderPanels.Count > 0)
                {
                    for (int i = 0; i < m_xpanderPanels.Count; i++)
                    {
                        m_xpanderPanels[i].SetBtnGaoDu(buttonHeight);
                    }
                    this.Invalidate();
                }
            }
        }

        [RefreshProperties(RefreshProperties.Repaint),
        Category("Collections"),
        Browsable(true),
        Description("Collection containing all the XPanderPanels for the xpanderpanellist.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Editor(typeof(OutLockPageCollectionEditor), typeof(UITypeEditor))]
        public OutLockPageCollection PanlPagePanels
        {
            get { return this.m_xpanderPanels; }
        }

        public OutLockBar()
            : base()
        {
            buttonHeight = 25;
            this.DoubleBuffered = true;

            this.m_xpanderPanels = new OutLockPageCollection(this);
            _XianShiIndex = -1;

        }


        private void HuiZhi()
        {
            if (m_xpanderPanels.Count > 0)
            {
                if (_XianShiIndex >= 0 && _XianShiIndex < m_xpanderPanels.Count)
                {
                    SetNaGePageXianShi(m_xpanderPanels[_XianShiIndex]);
                }
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            HuiZhi();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // HuiZhi();
        }



        public void Add(string text)
        {
            OutLockPage node = new OutLockPage(text, buttonHeight, Color.Magenta, Color.Transparent, this);
            node.Width = this.Width;
            // this.Controls.Add(node);
            m_xpanderPanels.Add(node);
            HuiZhi();
        }
        public void Add(string text, Color btncolor, Color pancolor)
        {
            OutLockPage node = new OutLockPage(text, buttonHeight, btncolor, pancolor, this);
            node.Width = this.Width;
            // this.Controls.Add(node);
            m_xpanderPanels.Add(node);
            HuiZhi();
        }

        public void ChuShiHuaKongJian()
        {
            if (this.Controls.Count > 0)
            {
                if (this.Controls.Count > 0)
                {

                    List<OutLockPage> lispan = new List<OutLockPage>();
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (this.Controls[i] is OutLockPage)
                        {
                            #region MyRegion
                            OutLockPage pan = this.Controls[i] as OutLockPage;

                            pan.Location = new Point(0, i * this.ButtonHeight);
                            pan.Height = this.ButtonHeight;
                            pan.Width = this.Width;



                            #endregion

                        }
                    }



                }
                SetXianShiN(0);
                // this.Refresh();
            }

        }
        public void SetNaGePageXianShi(OutLockPage pan1)
        {
            if (this.Controls.Count > 0)
            {
                int jisushuci = 0;
                int dibucishu = 0;
                List<OutLockPage> lispan = new List<OutLockPage>();
                int zhaochu = -1;
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (this.Controls[i] is OutLockPage)
                    {
                        OutLockPage pan = this.Controls[i] as OutLockPage;

                        if (pan.Equals(pan1))
                        {

                            zhaochu = i;
                            break;
                        }
                    }
                }
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (this.Controls[i] is OutLockPage)
                    {
                        #region MyRegion
                        OutLockPage pan = this.Controls[i] as OutLockPage;
                        if (i < zhaochu)
                        {
                            pan.Location = new Point(0, i * this.ButtonHeight);
                            pan.Width = this.Width;
                            pan.Height = this.ButtonHeight;
                            pan.SetBtnGaoDu(buttonHeight);
                            pan.ShuangShu = 0;
                            jisushuci++;
                            dibucishu++;
                        }
                        else
                        {
                            if (i > zhaochu)
                            {
                                lispan.Add(pan);
                            }
                        }
                        #endregion

                    }
                }
                if (lispan.Count > 0)
                {
                    int j = 1;
                    for (int i = lispan.Count - 1; i >= 0; i--)
                    {
                        OutLockPage pan = lispan[i];
                        pan.Location = new Point(0, this.Height - (j) * this.ButtonHeight);
                        pan.Height = this.ButtonHeight;
                        pan.Width = this.Width;
                        pan.SetBtnGaoDu(buttonHeight);
                        pan.ShuangShu = 0;
                        j++;
                        jisushuci++;
                    }
                }
                int gaodu = this.Height - jisushuci * this.ButtonHeight;
                if (gaodu > this.ButtonHeight)
                {
                    pan1.Location = new Point(0, dibucishu * this.ButtonHeight);
                    pan1.Height = gaodu;
                    pan1.Width = this.Width;
                    pan1.SetBtnGaoDu(buttonHeight);
                }

                pan1.Focus();
                pan1.Invalidate();
            }

        }

        public void SetXianShi(int index)
        {
            bool isyougaibian = true;
            if (_XianShiIndex == index)
            {
                isyougaibian = false;
            }
            _XianShiIndex = index;

            if (isyougaibian)
            {
                if (m_xpanderPanels.Count > 0)
                {
                    if (_XianShiIndex >= 0 && _XianShiIndex < m_xpanderPanels.Count)
                    {
                        SetNaGePageXianShi(m_xpanderPanels[_XianShiIndex]);
                        if (IndexBianHuaEvent != null)
                        {
                            IndexBianHuaEvent(m_xpanderPanels[_XianShiIndex], _XianShiIndex);
                        }
                    }
                }

            }

        }

        private void SetXianShiN(int index)
        {
            bool isyougaibian = true;
            if (isyougaibian)
            {
                if (m_xpanderPanels.Count > 0)
                {
                    if (index >= 0 && index < m_xpanderPanels.Count)
                    {
                        SetNaGePageXianShi(m_xpanderPanels[index]);
                        if (IndexBianHuaEvent != null)
                        {
                            IndexBianHuaEvent(m_xpanderPanels[index], index);
                        }
                    }
                }

            }
        }
    }
    [Designer(typeof(OutLockPageDesigner))]
    [DesignTimeVisible(false)]
    public class OutLockPage : Panel
    {

        #region 属性
        public int ShuangShu
        {
            get { return _ShuangShu; }
            set { _ShuangShu = value; }
        }
        public Color LColor
        {
            get
            {
                return _LColor;
            }

            set
            {
                _LColor = value;
                this.Invalidate(BtnRec);
            }
        }
        public float ZiTiDaoXiao
        {
            get
            {
                return _ZiTiDaoXiao;
            }

            set
            {
                _ZiTiDaoXiao = value;
                this.Invalidate(BtnRec);
            }
        }
        public Color GColor
        {
            get
            {
                return _GColor;
            }

            set
            {
                _GColor = value;
                this.Invalidate(BtnRec);

            }
        }
        public string StrText
        {
            get { return _StrText; }
            set { _StrText = value; this.Invalidate(BtnRec); }
        }
        public int GaoDu
        {
            get
            {
                return buttonHeight;
            }

        }

        public Rectangle BtnRec1
        {
            get
            {
                return BtnRec;
            }
        }

        public TouWeiZhi WenZi
        {
            get
            {
                return _WenZi;
            }

            set
            {
                _WenZi = value;
                this.Invalidate(BtnRec);
            }
        }

        #endregion

        #region 私有变量
        private Color _LColor = Color.Magenta;
        private Color _GColor = Color.Transparent;
        private string _StrText = "第一页";
        private int buttonHeight = 25;
        private Rectangle BtnRec;

        private float _ZiTiDaoXiao = 12;
        private bool Down = false;
        private bool Hover = false;
        private int _ShuangShu = 0;
        #endregion

        #region 枚举
        public enum TouWeiZhi
        {
            Zuo = 0,
            You = 1,
            Zhong = 2,
        }
        private TouWeiZhi _WenZi = TouWeiZhi.Zuo;
        #endregion






        private OutLockBar _OutlookBar;

        public OutLockPage() : this("第一页", 25)
        {

        }
        public OutLockPage(string txt, int chushibtngaodu)
         : base()
        {
            //  components = new Container();
            this.DoubleBuffered = true;
            buttonHeight = chushibtngaodu;
            this.Padding = new Padding(0, 25, 0, 0);
            BtnRec = new Rectangle();
            BtnRec.X = 0;
            BtnRec.Y = 0;
            BtnRec.Width = this.Width;
            BtnRec.Height = Padding.Top;

            _OutlookBar = (OutLockBar)this.Parent;
        }

        public OutLockPage(string txt, int chushibtngaodu, Color btnbeijingse, Color panbejingse, OutLockBar outlookBar)
            : base()
        {
            //  components = new Container();
            this.DoubleBuffered = true;
            buttonHeight = chushibtngaodu;
            this.Padding = new Padding(0, buttonHeight, 0, 0);
            BtnRec = new Rectangle();
            BtnRec.X = 0;
            BtnRec.Y = 0;
            BtnRec.Width = this.Width;
            BtnRec.Height = Padding.Top;
            _OutlookBar = outlookBar;
            _LColor = btnbeijingse;
            _GColor = panbejingse;

        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            using (SolidBrush huashua = new SolidBrush(LColor))
            {
                if (Hover)
                {
                    using (Pen pen = new Pen(Color.Black))
                    {
                        G.DrawRectangle(pen, BtnRec);
                    }
                }
                G.FillRectangle(huashua, BtnRec);
                Font ziti = new Font("微软雅黑", _ZiTiDaoXiao);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                switch (_WenZi)
                {
                    case TouWeiZhi.Zuo:
                        {
                            stringFormat.Alignment = StringAlignment.Near;
                        }
                        break;
                    case TouWeiZhi.You:
                        {
                            stringFormat.Alignment = StringAlignment.Far;
                        }
                        break;
                    case TouWeiZhi.Zhong:
                        {
                            stringFormat.Alignment = StringAlignment.Center;
                        }
                        break;
                    default:
                        break;
                }

                G.DrawString(_StrText, ziti, Brushes.Black, BtnRec, stringFormat);
                ziti.Dispose();
            }
            using (Pen pen = new Pen(Color.Blue))
            {
                pen.DashStyle = DashStyle.Custom;
                pen.DashPattern = new float[] { 5, 5 };
                Rectangle rectangle = new Rectangle(0, buttonHeight, Width - 1, Height - buttonHeight - 1);
                G.DrawRectangle(pen, rectangle);
                using (SolidBrush huashua = new SolidBrush(GColor))
                {
                    G.FillRectangle(huashua, rectangle);
                }
            }


        }


        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            if (_OutlookBar == null)
            {
                _OutlookBar = (OutLockBar)this.Parent;

            }
            if (_OutlookBar != null)
            {
                this.Width = _OutlookBar.Width;

            }
            if (BtnRec != null)
            {
                BtnRec.Width = this.Width;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.X > BtnRec.X && e.X < BtnRec.X + BtnRec.Width && e.Y > BtnRec.Y && e.Y < BtnRec.Y + BtnRec.Height)
            {
                if (!Down)
                    Down = true;
            }
            else { if (Down) Down = false; }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Hover = false;
            this.Cursor = Cursors.Default;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Down = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.X > BtnRec.X && e.X < BtnRec.X + BtnRec.Width && e.Y > BtnRec.Y && e.Y < BtnRec.Y + BtnRec.Height)
            {
                Hover = true;
                this.Cursor = Cursors.Hand;

            }
            else
            {
                Hover = false;
                this.Cursor = Cursors.Default;
            }
            //  this.Invalidate(BtnRec);
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (Hover && Down)
            {
                if (_OutlookBar == null)
                {
                    _OutlookBar = (OutLockBar)this.Parent;

                }
                if (_OutlookBar != null)
                {
                    if (_ShuangShu == 0)
                    {
                        _ShuangShu++;
                        RecalcLayout();
                    }
                }
            }

        }
        public void SetBtnGaoDu(int chushibtngaodu)
        {
            buttonHeight = chushibtngaodu;
            this.Padding = new Padding(0, buttonHeight, 0, 0);
            BtnRec.X = 0;
            BtnRec.Y = 0;
            BtnRec.Width = this.Width;
            BtnRec.Height = Padding.Top;
            this.Invalidate(BtnRec);
        }


        public void RecalcLayout()
        {
            if (_OutlookBar == null)
            {
                _OutlookBar = (OutLockBar)this.Parent;

            }
            if (_OutlookBar != null)
            {
                if (_OutlookBar.Controls.Count > 0)
                {
                    int zhaochu = -1;
                    for (int i = 0; i < _OutlookBar.Controls.Count; i++)
                    {
                        if (_OutlookBar.Controls[i] is OutLockPage)
                        {
                            OutLockPage pan = _OutlookBar.Controls[i] as OutLockPage;
                            if (pan.Equals(this))
                            {
                                zhaochu = i;
                                break;
                            }
                        }
                    }
                    _OutlookBar.SetXianShi(zhaochu);

                }
            }


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (components != null)
                //    components.Dispose();
            }
            base.Dispose(disposing);
        }

    }
    /// <summary>
    /// Contains a collection of XPanderPanel objects.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
    public sealed class OutLockPageCollection : IList, ICollection, IEnumerable
    {

        private OutLockBar m_xpanderPanelList;
        private Control.ControlCollection m_controlCollection;
        #region Constructor

        public OutLockPageCollection(OutLockBar xpanderPanelList)
        {
            this.m_xpanderPanelList = xpanderPanelList;
            this.m_controlCollection = this.m_xpanderPanelList.Controls;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a XPanderPanel in the collection. 
        /// </summary>
        /// <param name="index">The zero-based index of the XPanderPanel to get or set.</param>
        /// <returns>The xPanderPanel at the specified index.</returns>
        public OutLockPage this[int index]
        {
            get { return (OutLockPage)this.m_controlCollection[index] as OutLockPage; }
        }

        #endregion

        #region MethodsPublic
        /// <summary>
        /// Determines whether the XPanderPanelCollection contains a specific XPanderPanel
        /// </summary>
        /// <param name="xpanderPanel">The XPanderPanel to locate in the XPanderPanelCollection</param>
        /// <returns>true if the XPanderPanelCollection contains the specified value; otherwise, false.</returns>
        public bool Contains(OutLockPage xpanderPanel)
        {
            return this.m_controlCollection.Contains(xpanderPanel);
        }
        /// <summary>
        /// Adds a XPanderPanel to the collection.  
        /// </summary>
        /// <param name="xpanderPanel">The XPanderPanel to add.</param>
        public void Add(OutLockPage xpanderPanel)
        {
            this.m_controlCollection.Add(xpanderPanel);
            this.m_xpanderPanelList.Invalidate();

        }
        /// <summary>
        /// Removes the first occurrence of a specific XPanderPanel from the XPanderPanelCollection
        /// </summary>
        /// <param name="xpanderPanel">The XPanderPanel to remove from the XPanderPanelCollection</param>
        public void Remove(OutLockPage xpanderPanel)
        {
            this.m_controlCollection.Remove(xpanderPanel);
        }
        /// <summary>
        /// Removes all the XPanderPanels from the collection. 
        /// </summary>
        public void Clear()
        {
            this.m_controlCollection.Clear();
        }
        /// <summary>
        /// Gets the number of XPanderPanels in the collection. 
        /// </summary>
        public int Count
        {
            get { return this.m_controlCollection.Count; }
        }
        /// <summary>
        /// Gets a value indicating whether the collection is read-only. 
        /// </summary>
        public bool IsReadOnly
        {
            get { return this.m_controlCollection.IsReadOnly; }
        }
        /// <summary>
        /// Returns an enumeration of all the XPanderPanels in the collection.  
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return this.m_controlCollection.GetEnumerator();
        }
        /// <summary>
        /// Returns the index of the specified XPanderPanel in the collection. 
        /// </summary>
        /// <param name="xpanderPanel">The xpanderPanel to find the index of.</param>
        /// <returns>The index of the xpanderPanel, or -1 if the xpanderPanel is not in the <see ref="ControlCollection">ControlCollection</see> instance.</returns>
        public int IndexOf(OutLockPage xpanderPanel)
        {
            return this.m_controlCollection.IndexOf(xpanderPanel);
        }
        /// <summary>
        /// Removes the XPanderPanel at the specified index from the collection. 
        /// </summary>
        /// <param name="index">The zero-based index of the xpanderPanel to remove from the ControlCollection instance.</param>
        public void RemoveAt(int index)
        {
            this.m_controlCollection.RemoveAt(index);
        }
        /// <summary>
        /// Inserts an XPanderPanel to the collection at the specified index. 
        /// </summary>
        /// <param name="index">The zero-based index at which value should be inserted. </param>
        /// <param name="xpanderPanel">The XPanderPanel to insert into the Collection.</param>
        public void Insert(int index, OutLockPage xpanderPanel)
        {
            ((IList)this).Insert(index, (object)xpanderPanel);
        }
        /// <summary>
        /// Copies the elements of the collection to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="xpanderPanels">The one-dimensional Array that is the destination of the elements copied from ICollection.
        /// The Array must have zero-based indexing.
        ///</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        public void CopyTo(OutLockPage[] xpanderPanels, int index)
        {
            this.m_controlCollection.CopyTo(xpanderPanels, index);
        }

        #endregion

        #region Interface ICollection
        /// <summary>
        /// Gets the number of elements contained in the ICollection.
        /// </summary>
        int ICollection.Count
        {
            get { return this.Count; }
        }
        /// <summary>
        /// Gets a value indicating whether access to the ICollection is synchronized
        /// </summary>
        bool ICollection.IsSynchronized
        {
            get { return ((ICollection)this.m_controlCollection).IsSynchronized; }
        }
        /// <summary>
        /// Gets an object that can be used to synchronize access to the ICollection.
        /// </summary>
        object ICollection.SyncRoot
        {
            get { return ((ICollection)this.m_controlCollection).SyncRoot; }
        }
        /// <summary>
        /// Copies the elements of the ICollection to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection. The Array must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)this.m_controlCollection).CopyTo(array, index);
        }

        #endregion

        #region Interface IList
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns> The element at the specified index.</returns>
        object IList.this[int index]
        {
            get { return this.m_controlCollection[index]; }
            set { }
        }
        /// <summary>
        /// Adds an item to the IList.
        /// </summary>
        /// <param name="value">The Object to add to the IList.</param>
        /// <returns>The position into which the new element was inserted.</returns>
        int IList.Add(object value)
        {
            OutLockPage xpanderPanel = value as OutLockPage;
            if (xpanderPanel == null)
            {
                throw new ArgumentException(
                 "有问题");
            }

            this.Add(xpanderPanel);
            m_xpanderPanelList.ChuShiHuaKongJian();
            return this.IndexOf(xpanderPanel);
        }
        /// <summary>
        /// Determines whether the IList contains a specific value.
        /// </summary>
        /// <param name="value">The Object to locate in the IList.</param>
        /// <returns>true if the Object is found in the IList; otherwise, false.</returns>
        bool IList.Contains(object value)
        {
            return this.Contains(value as OutLockPage);
        }
        /// <summary>
        /// Determines the index of a specific item in the IList.
        /// </summary>
        /// <param name="value">The Object to locate in the IList.</param>
        /// <returns>The index of value if found in the list; otherwise, -1.</returns>
        int IList.IndexOf(object value)
        {
            return this.IndexOf(value as OutLockPage);
        }
        /// <summary>
        /// Inserts an item to the IList at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to insert.</param>
        /// <param name="value">The Object to insert into the IList.</param>
        void IList.Insert(int index, object value)
        {
            if ((value is OutLockPage) == false)
            {
                throw new ArgumentException(
                   "有问题");
            }
        }
        /// <summary>
        /// Removes the first occurrence of a specific object from the IList.
        /// </summary>
        /// <param name="value">The Object to remove from the IList.</param>
        void IList.Remove(object value)
        {
            this.Remove(value as OutLockPage);

        }
        /// <summary>
        /// Removes the IList item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        void IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
            m_xpanderPanelList.ChuShiHuaKongJian();
        }
        /// <summary>
        /// Gets a value indicating whether the IList is read-only.
        /// </summary>
        bool IList.IsReadOnly
        {
            get { return this.IsReadOnly; }
        }
        /// <summary>
        /// Gets a value indicating whether the IList has a fixed size.
        /// </summary>
        bool IList.IsFixedSize
        {
            get { return ((IList)this.m_controlCollection).IsFixedSize; }
        }

        #endregion
    }

    public class OutLockPageDesigner : ParentControlDesigner
    {
        public override SelectionRules SelectionRules
        {
            get
            {

                return SelectionRules.Locked;
            }
        }
        protected override bool GetHitTest(Point point)
        {
            OutLockPage lay = base.Control as OutLockPage;//得到宿主控件
            Point e = lay.PointToClient(point);//得到鼠标的坐标
            Rectangle buttonRect = lay.BtnRec1;//定义可以点击的按钮区域
            if (buttonRect != null)
            {
                if (e.X > buttonRect.X && e.X < buttonRect.X + buttonRect.Width && e.Y > buttonRect.Y && e.Y < buttonRect.Y + buttonRect.Height)
                {
                    if (isMouseDown)
                    {

                        lay.RecalcLayout();

                        isMouseDown = false;

                    }
                }
                else
                {
                    isMouseDown = false;
                }
            }
            return false;

        }
        bool isMouseDown = false;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0202)
            {
                isMouseDown = true;
            }
            base.WndProc(ref m);
        }
    }

    /// <summary>
    /// Provides a user interface that can edit most types of collections at design time.
    /// </summary>
    public class OutLockPageCollectionEditor : CollectionEditor
    {
        #region FieldsPrivate

        private CollectionForm m_collectionForm;

        #endregion

        #region MethodsPublic
        /// <summary>
        /// Initializes a new instance of the XPanderPanelCollectionEditor class
        /// using the specified collection type.
        /// </summary>
        /// <param name="type">The type of the collection for this editor to edit.</param>
        public OutLockPageCollectionEditor(Type type)
            : base(type)
        {
        }

        #endregion

        #region MethodsProtected
        /// <summary>
        /// Creates a new form to display and edit the current collection.
        /// </summary>
        /// <returns> A CollectionEditor.CollectionForm to provide as the user interface for editing the collection.</returns>
        protected override CollectionForm CreateCollectionForm()
        {
            this.m_collectionForm = base.CreateCollectionForm();
            return this.m_collectionForm;
        }

        #region MethodsProtected

        /// <summary>
        /// Creates a new instance of the specified collection item type.
        /// </summary>
        /// <param name="ItemType">The type of item to create.</param>
        /// <returns> A new instance of the specified object.</returns>
        protected override Object CreateInstance(Type ItemType)
        {
            /* you can create the new instance yourself 
                 * ComplexItem ci=new ComplexItem(2,"ComplexItem",null);
                 * we know for sure that the itemType it will always be ComplexItem
                 *but this time let it to do the job... 
                 */

            OutLockPage xpanderPanel =
                (OutLockPage)base.CreateInstance(ItemType);

            if (this.Context.Instance != null)
            {

            }
            return xpanderPanel;
        }

        #endregion
        #endregion
    }
}
