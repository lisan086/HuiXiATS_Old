using CommLei.GongYeJieHe;
using DotNetSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.YuYin
{
    /// <summary>
    /// 也需要安装语音包
    /// </summary>
    public class DotNetYuYin : ABSYuYin
    {
        /// <summary>
        /// 总开关
        /// </summary>
        private bool KaiGuan = false;

        /// <summary>
        /// 状态开关
        /// </summary>
        private bool State = false;
        /// <summary>
        /// 音量
        /// </summary>
        private int yingliang = 100;
        /// <summary>
        /// 语速
        /// </summary>
        private int yusu = 0;

        private SpVoice sp;
        private FanXingJiHeLei<string> JieYuYan = new FanXingJiHeLei<string>();

        /// <summary>
        /// 实例化
        /// </summary>
        public DotNetYuYin()
        {
            try
            {
                sp = new SpVoice();
                SpeechVoiceSpeakFlags sFlags = SpeechVoiceSpeakFlags.SVSFDefault;
                Thread task = new Thread(() => {
                    while (KaiGuan)
                    {
                        if (State == false)
                        {
                            Thread.Sleep(30);
                            continue;
                        }
                        try
                        {
                            if (State)
                            {
                                if (sp != null)
                                {
                                    int count = JieYuYan.GetCount();
                                    if (count > 0)
                                    {
                                        string quchu = JieYuYan.GetModel_Head_RomeHead();
                                        sp.Speak(quchu, sFlags);
                                    }
                                }
                            }
                        }
                        catch
                        {


                        }

                        Thread.Sleep(50);
                    }

                });
                task.IsBackground = true;
                task.Start();

            }
            catch
            {

            }
        }
        /// <summary>
        /// 实现父类的语音操作
        /// </summary>
        /// <param name="KaiQi"></param>
        public override void YuYinCaoZuo(bool KaiQi)
        {
            if (KaiQi == false)
            {
                State = KaiQi;
                Thread.Sleep(50);
                JieYuYan.Romve_All();
            }
            else
            {
                JieYuYan.Romve_All();
                State = KaiQi;
            }

        }



        /// <summary>
        /// 增加语音
        /// </summary>
        /// <param name="neirong"></param>
        public override void AddHua(string neirong)
        {
            if (State)
            {
                JieYuYan.Add(neirong);
            }
        }

        /// <summary>
        /// 软件关闭调用
        /// </summary>
        public override void Close()
        {
            KaiGuan = false;
            JieYuYan.Romve_All();
           
        }
        /// <summary>
        /// 设置音量
        /// </summary>
        public override int SetYingLiang
        {
            get
            {
                return yingliang;
            }
            set
            {
                yingliang = value;
                if (sp != null)
                {
                    sp.Volume = yingliang;
                }
            }
        }
        /// <summary>
        /// 设置语速
        /// </summary>
        public override int SetYuShu
        {
            get
            {
                return yusu;
            }

            set
            {
                yusu = value;
                if (sp != null)
                {
                    sp.Rate = yusu;
                }
            }
        }
    }
}
