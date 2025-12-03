using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseUI.UC
{
    /// <summary>
    /// 点击触发了事件
    /// </summary>
    /// <param name="DaIndex">标题索引</param>
    /// <param name="ZiIndex">点击子索引</param>
    /// <param name="IsRoot">是否是标题</param>
    /// <param name="Text">文本资料</param>
    public delegate void ChuFaXinHao(int DaIndex, int ZiIndex, bool IsRoot, string sText,string xwenben);
    public class ChouTiKJ : Panel
    {
        /// <summary>
        /// 记录控件
        /// </summary>
        private Dictionary<int, BandPanel> JiLuKongJian = new Dictionary<int, BandPanel>();
     
        /// <summary>
        /// 标题控件的高度
        /// </summary>
        private int buttonHeight;
        /// <summary>
        /// 第几个选择了
        /// </summary>
        private int selectedBand;

        private int ShangYiCi = 0;
    
        /// <summary>
        /// 点击事件
        /// </summary>
        public event ChuFaXinHao ChuFaXinHaoEvent;
        /// <summary>
        /// 标题控件的高度
        /// </summary>
        public int ButtonHeight
        {
            get
            {
                return buttonHeight;
            }

            set
            {
                buttonHeight = value;
                foreach (var item in JiLuKongJian.Keys)
                {
                    JiLuKongJian[item].BianHuaGao(buttonHeight);
                }
                // do recalc layout for entire bar
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000001;  // Turn on WS_EX_COMPOSITED  
                return cp;
            }
        }
        /// <summary>
        /// 第几个选择了
        /// </summary>
        public int SelectedBand
        {
            get
            {
                return selectedBand;
            }
            set
            {
                SelectBand(value);
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ChouTiKJ()
        {
            buttonHeight = 30;
            selectedBand = 0;
           
            this.DoubleBuffered = true;
            Initialize();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            // parent must exist!
            this.SizeChanged += new EventHandler(SizeChangedEvent);
            //this.BorderStyle = BorderStyle.FixedSingle;
        }
        /// <summary>
        /// 增加控件
        /// </summary>
        /// <param name="caption">文本text</param>
        /// <param name="content">什么容器控件</param>
        /// <param name="image">图片</param>
        public void AddBand(string shiianwenben,string xinshiwenben, ContentPanel content, Image image, Color beijingcolor, Color ziticolor)
        {
            content.outlookBar = this;
            int index = Controls.Count;
            content.DaIndex = index;
            BandTagInfo bti = new BandTagInfo(this, index);
            BandPanel bandPanel = new BandPanel(shiianwenben,xinshiwenben, content, bti, image, beijingcolor, ziticolor, buttonHeight);
            bandPanel.Dock = DockStyle.Fill;
            JiLuKongJian.Add(index, bandPanel);                
            UpdateBarInfo();
            Controls.Add(bandPanel);
        }
        /// <summary>
        /// 清理那个层
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IconPanel Clear(int index)
        {
            if (JiLuKongJian.Keys.Contains(index))
            {
                IconPanel duixiang = null;
                BandPanel pd = JiLuKongJian[index];
                List<Control> shouji = new List<Control>();
                for (int i = 0; i < pd.Controls.Count; i++)
                {
                    if (pd.Controls[i] is ContentPanel)
                    {
                        for (int c = 0; c < pd.Controls[i].Controls.Count; c++)
                        {
                            shouji.Add(pd.Controls[i].Controls[c]);
                        }
                        duixiang = pd.Controls[i] as IconPanel;
                    }
                }
                duixiang.Controls.Clear();
                shouji.ForEach((x) => { x.Dispose(); });

                return duixiang;
            }
            return null;
        }
        /// <summary>
        /// 设置那个层显示
        /// </summary>
        /// <param name="index"></param>
        public void SelectBand(int index)
        {
            ShangYiCi = selectedBand;
            selectedBand = index;
            UpdateBarInfo();
          
        }

     

        /// <summary>
        /// 初始化空件
        /// </summary>
        private void UpdateBarInfo()
        {
            List<int> shujud = JiLuKongJian.Keys.ToList();
            List<int> xuyaozhuan = new List<int>();
            for (int item = 0; item< shujud.Count; item++)
            {
                if (item == selectedBand)
                {
                    JiLuKongJian[item].Dock = DockStyle.Fill;
                   // JiLuKongJian[item].BringToFront();
                }
                else
                {

                    if (item < selectedBand)
                    {
                        JiLuKongJian[item].Dock = DockStyle.Top;
                        JiLuKongJian[item].BringToFront();
                    }
                    else
                    {

                        JiLuKongJian[item].Dock = DockStyle.Bottom;
                        
                        if (item < ShangYiCi)
                        {
                            xuyaozhuan.Insert(0, item);
                        }
                    }
                    JiLuKongJian[item].ZiDongHuiSuo();

                }
            }
            xuyaozhuan.Add(selectedBand);
            for (int i = 0; i < xuyaozhuan.Count; i++)
            {
                if (JiLuKongJian.ContainsKey(xuyaozhuan[i]))
                {
                    JiLuKongJian[xuyaozhuan[i]].BringToFront();
                }
            }
        }

     

        private void SizeChangedEvent(object sender, EventArgs e)
        {
           // Size = new Size(Size.Width, ((Control)sender).ClientRectangle.Size.Height);
            UpdateBarInfo();
           
        }
        /// <summary>
        /// 触发事件，控件内部调用
        /// </summary>
        /// <param name="DaIndex"></param>
        /// <param name="ZiIndex"></param>
        /// <param name="IsRoot"></param>
        /// <param name="Text"></param>
        public void ChuFaXinHao(int DaIndex, int ZiIndex, bool IsRoot, string Text,string swenben)
        {
            if (ChuFaXinHaoEvent != null)
            {
                ChuFaXinHaoEvent(DaIndex, ZiIndex, IsRoot, Text, swenben);
            }
        }
    }

    /// <summary>
    /// 绑定的信息
    /// </summary>
    internal class BandTagInfo
    {
        /// <summary>
        /// 父控件
        /// </summary>
        public ChouTiKJ outlookBar;
        /// <summary>
        /// 子索引
        /// </summary>
        public int index;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ob"></param>
        /// <param name="index"></param>
        public BandTagInfo(ChouTiKJ ob, int index)
        {
            outlookBar = ob;
            this.index = index;
        }
    }
    /// <summary>
    /// 容器panl控件有标题控件一些集合
    /// </summary>
    [DesignTimeVisible(false)]
    internal class BandPanel : Panel
    {
        private int GaoDu = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="content"></param>
        /// <param name="bti"></param>
        /// <param name="mage"></param>
        public BandPanel(string shijianwenben,  string xianshiwenben, ContentPanel content, BandTagInfo bti, Image mage, Color color, Color ziticolor,int gaodu)
        {
            this.DoubleBuffered = true;
            BandButton bandButton = new BandButton(shijianwenben, xianshiwenben, bti, mage);
            bandButton.BackColor = color;
            bandButton.ForeColor = ziticolor;
            bandButton.Height = gaodu;
            GaoDu = gaodu;
            bandButton.Dock = DockStyle.Top;
            content.Dock = DockStyle.Fill;
            Controls.Add(content);
            Controls.Add(bandButton);
           
        }


        public void BianHuaGao(int gaodu)
        {
            Controls[1].Height = gaodu;
            GaoDu = gaodu;
        }

        public void ZiDongHuiSuo()
        {
            this.Height = GaoDu-2;
        }
    }
    /// <summary>
    /// 标题控件
    /// </summary>
    [DesignTimeVisible(false)]
    internal class BandButton : Button
    {
        private BandTagInfo bti;
        private string BaoCunWenBen = "";
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="caption">标题文本</param>
        /// <param name="bti">标题绑定的信息</param>
        /// <param name="mage">标题的图片</param>
        public BandButton(string shijianwenben, string xianshiwenben, BandTagInfo bti, Image mage)
        {
            Text = xianshiwenben;
            BaoCunWenBen = shijianwenben;
            Font = new Font("微软雅黑", 12f);
            BackColor = Color.Transparent;
            FlatStyle = FlatStyle.Flat;
            Visible = true;
            this.bti = bti;
            this.DoubleBuffered = true;
            this.FlatAppearance.BorderColor = Color.DimGray;
            this.FlatAppearance.BorderSize = 1;
            ForeColor = Color.White;
            ImageAlign = ContentAlignment.MiddleLeft;
            Image = mage;
            Click += new EventHandler(SelectBand);
        }

        private void SelectBand(object sender, EventArgs e)
        {
            bti.outlookBar.SelectBand(bti.index);
            bti.outlookBar.ChuFaXinHao(bti.index, 0, true, BaoCunWenBen, Text);
        }
    }

    /// <summary>
    /// 虚容器
    /// </summary>
    [DesignTimeVisible(false)]
    public abstract class ContentPanel : Panel
    {
        public ChouTiKJ outlookBar;
        public int DaIndex = -1;
        public ContentPanel()
        {
            // initial state
            Visible = true;
        }
    }
    /// <summary>
    /// 竖向容器控件装子控件用的
    /// </summary>
    [DesignTimeVisible(false)]
    public class IconPanel : ContentPanel
    {
        private List<Panl> Lis = new List<Panl>();
        /// <summary>
        /// 表示的高边距
        /// </summary>
        protected int _Hightmargin;

        /// <summary>
        /// 表示的宽边距
        /// </summary>
        protected int _Wightmargin;
        /// <summary>
        /// 表示的高边距
        /// </summary>
        public int HightMarGin
        {
            get
            {
                return _Hightmargin;
            }
        }
        /// <summary>
        /// 表示的宽边距
        /// </summary>
        public int WightMargin
        {
            get
            {
                return _Wightmargin;
            }
        }
        /// <summary>
        ///构造函数
        /// </summary>
        public IconPanel(Color beijingse)
        {
            _Wightmargin = 0;
            _Hightmargin = 0;
            Padding = new Padding(1,1,1,1);
            BackColor = beijingse;
            AutoScroll = true;
            this.DoubleBuffered = true;
           
        }
        /// <summary>
        /// 添加子控件
        /// </summary>
        /// <param name="caption">子控件文本</param>
        /// <param name="image">子控件图片</param>
        public void AddIcon(string shijianwenben, string xianshiwenben, Image image, Color beijingcolor, Color ziticolor)
        {
            int index = Controls.Count; // two entries per icon
            Panl panelIcon = new Panl(this, image, index, shijianwenben, xianshiwenben, beijingcolor, ziticolor);
            panelIcon.Dock = DockStyle.Top;
            Lis.Insert(0, panelIcon);
            Controls.Clear();
            
            Controls.AddRange(Lis.ToArray());
        }

    }

    [DesignTimeVisible(false)]
    internal class Panl : Panel
    {
        public Panl(IconPanel parent, Image image, int index, string shijianwenben, string xianshiwenben, Color beijingcolor, Color ziticolor)
        {
            this.DoubleBuffered = true;
            if (image != null)
            {
                this.Height = image.Height + 5 + 30;
            }
            else
            {
                this.Height = 40 + 30;
            }
            PanelIcon panelIcon = new PanelIcon(parent, image, index, shijianwenben, xianshiwenben, beijingcolor, ziticolor);
            panelIcon.Dock = DockStyle.Fill;
            Label wenben = new Label();
            wenben.AutoSize = false;
            wenben.Text = "";
            wenben.BorderStyle = BorderStyle.None;
            wenben.BackColor = Color.Transparent;
            wenben.Dock = DockStyle.Bottom;
            wenben.Height = 2;
            Controls.Add(panelIcon);
            Controls.Add(wenben);

        }
    }
    /// <summary>
    /// 子控件
    /// </summary>
    [DesignTimeVisible(false)]
    public class PanelIcon : Button
    {
        /// <summary>
        /// 子控件的索引
        /// </summary>
        private int _Index;
        /// <summary>
        /// 子控件对应的父控件
        /// </summary>
        public IconPanel iconPanel;
        /// <summary>
        /// 子控件的文本
        /// </summary>
        private string WenBen = "";


        /// <summary>
        /// 子控件的索引
        /// </summary>
        public int Index
        {
            get
            {
                return _Index;
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent">父控件</param>
        /// <param name="image">图片</param>
        /// <param name="index">索引</param>
        /// <param name="wenben">文本</param>
        public PanelIcon(IconPanel parent, Image image, int index,string shijianwenben, string xianshiwenben, Color color, Color ziticolor)
        {
            this._Index = index;
            WenBen = shijianwenben;

            this.iconPanel = parent;
           
            this.Text = xianshiwenben;
            this.Visible = true;
            this.Image = image;
            if (image != null)
            {
                this.Height = image.Height+5 + 30;
            }
            else
            {
                this.Height = 40 + 30;
            }
          
            this.Font = new Font("微软雅黑", 10f);
            this.ForeColor = ziticolor;
            this.BackColor = color;
            this.FlatStyle = FlatStyle.Flat;
            this.ImageAlign = ContentAlignment.TopCenter;
            this.TextAlign = ContentAlignment.BottomCenter;
            this.FlatAppearance.BorderColor = Color.DimGray;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseOverBackColor = Color.LightSlateGray;
            this.FlatAppearance.MouseDownBackColor = Color.Yellow;
            this.DoubleBuffered = true;
            Click += PanelIcon_Click; ;


        }

        private void PanelIcon_Click(object sender, EventArgs e)
        {
            this.iconPanel.outlookBar.ChuFaXinHao(this.iconPanel.DaIndex, Index, false, WenBen, this.Text);
        }




    }
}
