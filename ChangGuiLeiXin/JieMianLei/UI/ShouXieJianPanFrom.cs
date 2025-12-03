using JieMianLei.FuZhuLei;
using JieMianLei.UI;
using Microsoft.Ink;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseUI.UI
{
    public partial class ShouXieJianPanFrom : BaseFrom
    {
        InkCollector ic;
        RecognizerContext rct;
        private int jilvGuangBiaoWeiZhi = 0;
        public string FullCACText = "";

        private DateTime DateTime = DateTime.Now;
        private int BiaoZhi = 2;
        public int TimeHaoMiao { get; set; } = 1000;

        public bool IsQuXiaoZiDongShua { get; set; } = false;

        private List<Label> shuju = new List<Label>();
        public ShouXieJianPanFrom()
        {
            InitializeComponent();
            MoveShiJianEvent _MoveShiJianEvent = new MoveShiJianEvent();
            _MoveShiJianEvent.BangDingMove(label9, this);
            this.DoubleBuffered = true;
            shuju.Add(label2);
            shuju.Add(label3);
            shuju.Add(label4);
            shuju.Add(label5);
            shuju.Add(label6);
            shuju.Add(label7);
            shuju.Add(label8);
            for (int i = 0; i < shuju.Count; i++)
            {
                shuju[i].Click += ShouDongJianPanFrom_Click;
            }
        }
        private void ShouDongJianPanFrom_Click(object sender, EventArgs e)
        {
            FhuZhi((sender as Label).Text);
        }
        private void JiaZai()
        {

            ic = new InkCollector(ink_here.Handle);
            this.ic.Stroke += new InkCollectorStrokeEventHandler(ic_Stroke);
            ic.Enabled = true;
            ink_();
        }
        void rct_RecognitionWithAlternates(object sender, RecognizerContextRecognitionWithAlternatesEventArgs e)
        {
        
            string result = "";
            RecognitionAlternates alts;
            if (e.RecognitionStatus == RecognitionStatus.NoError)
            {
                alts = e.Result.GetAlternatesFromSelection();
                foreach (RecognitionAlternate alt in alts)
                {
                    result += alt.ToString() + " ";
                }
            }
            result = result.Trim();


            string[] arr = result.Split(' ');
            this.Controlinkove.FanXingGaiBing(() => {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (shuju.Count >= i - 1)
                    {
                        shuju[i].Text = arr[i];
                    }
                }
            });


        }


        public void SetCanShu(string tsex, bool isyouxihao = false)
        {
            this.textBox1.Text = tsex;
            if (isyouxihao)
            {
                this.textBox1.PasswordChar = '*';
            }
        }
        void ic_Stroke(object sender, InkCollectorStrokeEventArgs e)
        {
            DateTime = DateTime.Now;
            BiaoZhi = 1;

            rct.StopBackgroundRecognition();
            rct.Strokes.Add(e.Stroke);

            rct.BackgroundRecognizeWithAlternates(0);
        }

        private void ink_()
        {
            ic.DefaultDrawingAttributes.Color = Color.Red;
            Recognizers recos = new Recognizers();
            Recognizer chineseReco = recos.GetDefaultRecognizer();

            rct = chineseReco.CreateRecognizerContext();


            rct.RecognitionFlags = RecognitionModes.WordMode;
            rct.Strokes = ic.Ink.Strokes;

            this.rct.RecognitionWithAlternates += new RecognizerContextRecognitionWithAlternatesEventHandler(rct_RecognitionWithAlternates);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsQuXiaoZiDongShua)
            {
                if (this.button2.Enabled != true)
                {
                    this.button2.Enabled = true;
                }
                return;
            }
            if (this.button2.Enabled != false)
            {
                this.button2.Enabled = false;
            }
            if (BiaoZhi == 1)
            {
                if ((DateTime.Now - DateTime).TotalMilliseconds >= TimeHaoMiao)
                {
                  
                    XieRu();
                    if (!ic.CollectingInk)
                    {
                        Strokes strokesToDelete = ic.Ink.Strokes;
                        rct.StopBackgroundRecognition();
                        ic.Ink.DeleteStrokes(strokesToDelete);
                        rct.Strokes = ic.Ink.Strokes;
                        ic.Ink.DeleteStrokes();//清除手写区域笔画;
                        ink_here.Refresh();//刷新手写区域
                    }
                    BiaoZhi = 2;
                    DateTime = DateTime.Now;
                }

            }
        }

        private void ShouXieJianPanFrom_Load(object sender, EventArgs e)
        {
            JiaZai();
            this.timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            Thread.Sleep(100);
            FullCACText = "";
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            Thread.Sleep(100);
            FullCACText = this.textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int GuanBiaoWeiZhi = this.textBox1.SelectionStart;
            int XuanZhongItem = this.textBox1.SelectionLength;
            if (XuanZhongItem == 0)
            {

                if (GuanBiaoWeiZhi > 0)
                {
                    this.textBox1.Text = this.textBox1.Text.Remove(GuanBiaoWeiZhi - 1, 1);
                    GuanBiaoWeiZhi--;
                }
                this.textBox1.Select(GuanBiaoWeiZhi, 0);
                jilvGuangBiaoWeiZhi = GuanBiaoWeiZhi;
                this.textBox1.Focus();
            }
            else if (XuanZhongItem > 0)
            {
                if (GuanBiaoWeiZhi >= 0)
                {
                    this.textBox1.Text = this.textBox1.Text.Remove(GuanBiaoWeiZhi, XuanZhongItem);
                }
                this.textBox1.Select(GuanBiaoWeiZhi, 0);
                jilvGuangBiaoWeiZhi = GuanBiaoWeiZhi;
                this.textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BiaoZhi = 2;
            FhuZhi(ic.Ink.Strokes.ToString());
            if (!ic.CollectingInk)
            {
                Strokes strokesToDelete = ic.Ink.Strokes;
                rct.StopBackgroundRecognition();
                ic.Ink.DeleteStrokes(strokesToDelete);
                rct.Strokes = ic.Ink.Strokes;
                ic.Ink.DeleteStrokes();//清除手写区域笔画;
                ink_here.Refresh();//刷新手写区域
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            jilvGuangBiaoWeiZhi = this.textBox1.SelectionStart;
        }

        private void ink_here_Click(object sender, EventArgs e)
        {
            DateTime = DateTime.Now;
            BiaoZhi = 1;
        }
        private void FhuZhi(string zhi)
        {
            if (jilvGuangBiaoWeiZhi == this.textBox1.Text.Length)
            {
                this.textBox1.Text += zhi;
                jilvGuangBiaoWeiZhi = this.textBox1.Text.Length - 1;
            }
            else
            {

                string msg1 = zhi;
                string msg = this.textBox1.Text;

                this.textBox1.Text = msg.Insert(jilvGuangBiaoWeiZhi, msg1);

            }
            this.textBox1.Select(jilvGuangBiaoWeiZhi + 1, 0);
            jilvGuangBiaoWeiZhi = this.textBox1.SelectionStart;
            this.textBox1.Focus();
        }
        private void XieRu()
        {
            List<Point> lstPoints = new List<Point>();
            foreach (var stroke in ic.Ink.Strokes)
            {
                lstPoints.AddRange(stroke.GetPoints());
            }
            ic.Ink.DeleteStrokes();
            Stroke sk = ic.Ink.CreateStroke(lstPoints.ToArray());
            rct.Strokes.Add(sk);
            // textBox1.SelectedText = ic.Ink.Strokes.ToString();


        }

       
    }
}
