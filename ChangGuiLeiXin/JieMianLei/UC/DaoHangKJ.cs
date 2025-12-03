using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design.WebControls;
using System.Windows.Forms;
using CommLei.JiChuLei;

namespace JieMianLei.UC
{
    public delegate void DaoHangDelegate(object sender, DaoHangModel e);
    public partial class DaoHangKJ : UserControl
    {
        public DaoHangKJ()
        {
            InitializeComponent();
            IniData();
            this.DoubleBuffered = true;
            _LabWigth = this.labYeShu.Width;
            _wigth = this.Width;
        }
        [Category("DaoHangControl"), Description("点击事件")]
        public event DaoHangDelegate UCClilk;
        #region 私有变量
        private int _AllPage = 1;
        private int _Rows = 200;
        private int _Index = 0;
        //  private int _AllRows = 1;
        private int _YuShu = 0;
        private int _LabWigth = 0;
        private int _wigth = 0;
        #endregion
        #region 公开属性
        /// <summary>
        /// 总行数
        /// </summary>
        [Category("DaoHangControl"), Description("总行数")]
        public int AllPage
        {
            get
            {
                return _AllPage;
            }
            set
            {
                int danqianyeshu = DangQianPage;
                _AllPage = value;
                if (_AllPage == 0)
                {
                    _AllPage = 1;
                }
                labYeShu.Text = "共" + _AllPage.ToString() + "页";
                SetWeiZhi(danqianyeshu);
                if (_AllPage>1)
                {
                    if (DangQianPage == 1)
                    {
                        TopBtnEnabled = false;
                        UpBtnEnabled = false;
                        NextBtnEnabled = true;
                        EndBtnEnabled = true;
                        this.btnTurnPage.Enabled = true;
                        this.txbPage.Enabled = true;
                    }
                    else if (DangQianPage == _AllPage)
                    {
                        TopBtnEnabled = true;
                        UpBtnEnabled = true;
                        NextBtnEnabled = false;
                        EndBtnEnabled = false;
                        this.btnTurnPage.Enabled = true;
                        this.txbPage.Enabled = true;
                    }
                    else
                    {
                        TopBtnEnabled = true;
                        UpBtnEnabled = true;
                        NextBtnEnabled = true;
                        EndBtnEnabled = true;
                        this.btnTurnPage.Enabled = true;
                        this.txbPage.Enabled = true;
                    }
                }              
                else
                {
                    IniData();

                }
            }
        }

        /// <summary>
        ///显示行数
        /// </summary>
        [Category("DaoHangControl"), Description("显示行数")]
        public int Rows
        {
            get
            {
                return _Rows;
            }
            set
            {
                _Rows = value;
                if (_Rows <= 0)
                {
                    _Rows = 100;
                }
            }
        }
        /// <summary>
        /// 首页是否能用
        /// </summary>
        [Category("DaoHangControl"), Description("首页是否能用")]
        public bool TopBtnEnabled
        {
            get
            {
                return this.btnTopPage.Enabled;
            }
            set
            {
                this.btnTopPage.Enabled = value;
            }
        }
        /// <summary>
        /// 上一页是否能用
        /// </summary>
        [Category("DaoHangControl"), Description("上一页是否能用")]
        public bool UpBtnEnabled
        {
            get
            {
                return this.btnUpPage.Enabled;
            }
            set
            {
                this.btnUpPage.Enabled = value;
            }
        }
        /// <summary>
        /// 下一页是否能用
        /// </summary>
        [Category("DaoHangControl"), Description("下一页是否能用")]
        public bool NextBtnEnabled
        {
            get
            {
                return this.btnNextPage.Enabled;
            }
            set
            {
                this.btnNextPage.Enabled = value;
            }
        }
        /// <summary>
        /// 尾页是否能用
        /// </summary>
        [Category("DaoHangControl"), Description("尾页是否能用")]
        public bool EndBtnEnabled
        {
            get
            {
                return this.btnEndPage.Enabled;
            }
            set
            {
                this.btnEndPage.Enabled = value;
            }
        }

        /// <summary>
        /// 尾页是否能用
        /// </summary>
        [Category("DaoHangControl"), Description("当前页数")]
        public int DangQianPage
        {
            get
            {
                int shuju = ChangYong.TryInt(this.txbPage.Text, 1);
                if (shuju<=0)
                {
                    shuju = 1;
                }
                if (shuju> _AllPage)
                {
                    shuju = _AllPage;
                }
                return shuju;
            }
           
        }
        #endregion
        #region 私有方法
        private void IniData()
        {
            TopBtnEnabled = false;
            UpBtnEnabled = false;
            NextBtnEnabled = false;
            EndBtnEnabled = false;
            this.btnTurnPage.Enabled = false;
            this.txbPage.Enabled = false;
        }
        #endregion

        public void SetWeiZhi(int yeshu)
        {
            if (yeshu <= 0)
            {
                yeshu = 1;
            }
            if (yeshu > _AllPage)
            {
                yeshu = _AllPage;
            }
            txbPage.Text = yeshu.ToString();
        }
        private void btnTopPage_Click(object sender, EventArgs e)
        {
            _Index = 0;
            this.txbPage.Text = "1";
            TopBtnEnabled = false;
            UpBtnEnabled = false;
            NextBtnEnabled = true;
            EndBtnEnabled = true;
            this.btnTurnPage.Enabled = true;
            this.txbPage.Enabled = true;
            if (UCClilk != null)
            {
                DaoHangModel model = new DaoHangModel();
                model.AnyBtn = DaoHanfMeiJu.TopPage;
                model.Index = _Index;
                model.IsDaoDaEndPage = false;
                model.Row = _Rows;
                model.YuShu = _YuShu;
                model.IsDaoDaTopPage = true;
                UCClilk(this, model);
            }
        }

        private void btnUpPage_Click(object sender, EventArgs e)
        {
            TopBtnEnabled = true;
            UpBtnEnabled = true;
            NextBtnEnabled = true;
            EndBtnEnabled = true;
            this.btnTurnPage.Enabled = true;
            this.txbPage.Enabled = true;
            int Page = 1;
            bool zhen = false;
            if (!int.TryParse(this.txbPage.Text.Trim(), out Page))
            {
                Page = 1;

            }

            this.txbPage.Text = (Page - 1).ToString();
            _Index = Page - 2;
            Page = Page - 1;
            if (Page < 1)
            {
                Page = 1;
            }
            if (Page == 1)
            {
                this.txbPage.Text = Page.ToString();
                TopBtnEnabled = false;
                UpBtnEnabled = false;
                NextBtnEnabled = true;
                EndBtnEnabled = true;
                _Index = 0;
                zhen = true;

            }

            if (UCClilk != null)
            {
                DaoHangModel model = new DaoHangModel();
                model.AnyBtn = DaoHanfMeiJu.UpPage;
                model.Index = _Index;
                model.IsDaoDaEndPage = false;
                model.Row = _Rows;
                model.YuShu = _YuShu;
                model.IsDaoDaTopPage = zhen;
                UCClilk(this, model);
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            TopBtnEnabled = true;
            UpBtnEnabled = true;
            NextBtnEnabled = true;
            EndBtnEnabled = true;
            this.btnTurnPage.Enabled = true;
            this.txbPage.Enabled = true;
            int Page = 1;
            bool zhen = false;
            if (!int.TryParse(this.txbPage.Text.Trim(), out Page))
            {
                Page = 1;

            }
            this.txbPage.Text = (Page + 1).ToString();
            Page = Page + 1;
            if (Page > _AllPage)
            {
                Page = _AllPage;
            }
            if (Page == _AllPage)
            {
                this.txbPage.Text = Page.ToString();
                TopBtnEnabled = true;
                UpBtnEnabled = true;
                NextBtnEnabled = false;
                EndBtnEnabled = false;
                zhen = true;
            }
            _Index = Page - 1;
            if (UCClilk != null)
            {
                DaoHangModel model = new DaoHangModel();
                model.AnyBtn = DaoHanfMeiJu.NextPage;
                model.Index = _Index;
                model.IsDaoDaEndPage = zhen;
                model.IsDaoDaTopPage = false;
                model.Row = _Rows;
                model.YuShu = _YuShu;
                UCClilk(this, model);
            }
        }

        private void btnEndPage_Click(object sender, EventArgs e)
        {
            TopBtnEnabled = true;
            UpBtnEnabled = true;
            NextBtnEnabled = false;
            EndBtnEnabled = false;
            this.btnTurnPage.Enabled = true;
            this.txbPage.Enabled = true;
            this.txbPage.Text = _AllPage.ToString();
            _Index = _AllPage - 1;
            if (UCClilk != null)
            {
                DaoHangModel model = new DaoHangModel();
                model.AnyBtn = DaoHanfMeiJu.EndPage;
                model.Index = _Index;
                model.IsDaoDaEndPage = true;
                model.IsDaoDaTopPage = false;
                model.Row = _Rows;
                model.YuShu = _YuShu;
                UCClilk(this, model);
            }
        }

        private void btnTurnPage_Click(object sender, EventArgs e)
        {
            TopBtnEnabled = true;
            UpBtnEnabled = true;
            NextBtnEnabled = true;
            EndBtnEnabled = true;
            int Page = 1;
            bool zhen = false;
            bool zhen1 = false;
            if (!int.TryParse(this.txbPage.Text.Trim(), out Page))
            {
                Page = 1;
                this.txbPage.Text = Page.ToString();
            }
            if (Page <= 0)
            {
                Page = 1;
                this.txbPage.Text = Page.ToString();
            }
            if (Page > AllPage)
            {
                Page = AllPage;
                this.txbPage.Text = Page.ToString();
            }
            if (Page == 1)
            {
                TopBtnEnabled = false;
                UpBtnEnabled = false;
                NextBtnEnabled = true;
                EndBtnEnabled = true;
                zhen = true;
                zhen1 = false;
            }
            else
            {
                if (Page == AllPage)
                {
                    TopBtnEnabled = true;
                    UpBtnEnabled = true;
                    NextBtnEnabled = false;
                    EndBtnEnabled = false;
                    zhen = false;
                    zhen1 = true;
                }
            }
            _Index = Page - 1;
            if (UCClilk != null)
            {
                DaoHangModel model = new DaoHangModel();
                model.AnyBtn = DaoHanfMeiJu.TurnPage;
                model.Index = _Index;
                model.IsDaoDaEndPage = zhen1;
                model.IsDaoDaTopPage = zhen;
                model.Row = _Rows;
                model.YuShu = _YuShu;
                UCClilk(this, model);
            }
        }
    }

    /// <summary>
    /// 导航控件model
    /// </summary>
    public class DaoHangModel : EventArgs
    {
        /// <summary>
        /// 行数
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// 目前索引的位置
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        ///具体是哪个按键
        /// </summary>
        public DaoHanfMeiJu AnyBtn { get; set; }

        /// <summary>
        /// 余数
        /// </summary>
        public int YuShu { get; set; }

        /// <summary>
        /// 是否到达尾页
        /// </summary>
        public bool IsDaoDaEndPage { get; set; }
        /// <summary>
        /// 是否到达首页
        /// </summary>
        public bool IsDaoDaTopPage { get; set; }
    }
    public enum DaoHanfMeiJu
    {
        /// <summary>
        /// 首页
        /// </summary>
        TopPage = 1,
        /// <summary>
        /// 上一页
        /// </summary>
        UpPage = 2,
        /// <summary>
        /// 下一页
        /// </summary>
        NextPage = 3,

        /// <summary>
        /// 尾页
        /// </summary>
        EndPage = 4,

        /// <summary>
        /// 转页
        /// </summary>
        TurnPage = 5
    }
}
