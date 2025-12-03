using CommLei.JiChuLei;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SSheBei.Model;

namespace SSheBei.LianJieQi
{
    /// <summary>
    /// UDP连接器
    /// </summary>
    public class RJ45UdpLianJieQi : ABSLianJieQi
    {
        #region 变量区

        /// <summary>
        /// 网络连接口
        /// </summary>
        private UdpClient udpSend;
        /// <summary>
        /// 存放rjmodel
        /// </summary>
        private SetLianJieQiModel RJmodel;

        /// <summary>
        /// 服务器的IP终结点
        /// </summary>
        private IPEndPoint RemoteIPEP = null;
        /// <summary>
        /// 描述
        /// </summary>
        private string ComMiaoSu = "";

        /// <summary>
        /// 状态
        /// </summary>
        private bool State = false;
        private int ShouCi = 0;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>

        public RJ45UdpLianJieQi()
        {


        }


        /// <summary>
        /// 连接器的名称
        /// </summary>
        public override string Name
        {
            get
            {
                return ComMiaoSu;
            }
        }

        /// <summary>
        /// true表示通信成功
        /// </summary>
        public override bool TongXinState
        {
            get
            {
                return State;
            }
        }
        /// <summary>
        /// 关闭连接器
        /// </summary>
        /// <returns></returns>
        public override ResultModel Close()
        {

            ResultModel rm = new ResultModel();
            try
            {
                udpSend.Close();
                rm.SetValue(true, string.Format("{0}:关闭成功", Name));

            }
            catch (Exception ex)
            {
                rm.SetValue(false, string.Format("{0}:关闭出现异常:{1}", Name, ex.Message));


            }
            return rm;
        }
        /// <summary>
        /// 打开连接器
        /// </summary>
        /// <returns></returns>
        public override ResultModel Open()
        {
            ResultModel rm = new ResultModel();
            try
            {
                udpSend.Connect(RemoteIPEP);
                rm.SetValue(true, string.Format("{0}:打开成功", ComMiaoSu));
                State = true;
            }
            catch (Exception ex)
            {
                rm.SetValue(false, string.Format("{0}:打开异常:{1}", ComMiaoSu, ex.Message));
                State = false;

            }
            return rm;
        }

        /// <summary>
        /// 连接器读数
        /// </summary>
        /// <returns></returns>
        public override ResultModel Read()
        {
            ResultModel rm = new ResultModel();
            try
            {
                if (TongXinState)
                {
                    byte[] buffer = udpSend.Receive(ref RemoteIPEP);
                    if (buffer != null && buffer.Length > 0)
                    {
                        rm.SetValue(true, string.Format("{0}:读取数据成功:{1}", ComMiaoSu, ChangYong.ByteOrString(buffer, " ")), buffer);
                    }

                }
                else
                {
                    rm.SetValue(false, string.Format("{0}:连接口是关闭状态，不能读取数据", ComMiaoSu));
                }
            }
            catch (Exception ex)
            {

                rm.SetValue(false, string.Format("{0}:读取数据异常;信息:{1}", ComMiaoSu, ex.Message));


            }


            return rm;
        }
        /// <summary>
        /// 连接器发送数据
        /// </summary>
        /// <param name="bytData"></param>
        /// <returns></returns>
        public override ResultModel Send(byte[] bytData)
        {
            ResultModel rm = new ResultModel();
            try
            {
                if (TongXinState)
                {

                    if (udpSend.Send(bytData, bytData.Length) > 0)
                    {
                        rm.SetValue(true, string.Format("{0}:发送数据成功:{1}", Name, ChangYong.ByteOrString(bytData, " ")));
                    }
                    else
                    {
                        rm.SetValue(false, string.Format("{0}:发送数据失败:{1}", Name, ChangYong.ByteOrString(bytData, " ")));
                    }
                }
                else
                {
                    rm.SetValue(false, string.Format("{0}:连接口是关闭状态，不能发送数据:{1}", Name, ChangYong.ByteOrString(bytData, " ")));
                }
            }
            catch (Exception ex)
            {
                rm.SetValue(false, string.Format("{0}:发送数据异常:{1};信息:{2}", Name, ChangYong.ByteOrString(bytData, " "), ex.Message));


            }
            return rm;
        }

        /// <summary>
        /// 连接器设置参数
        /// </summary>
        /// <param name="model"></param>
        public override void SetCanShu(SetLianJieQiModel model)
        {
            this.RJmodel = model;
            ComMiaoSu = string.Format("{0}*{1}UDP", RJmodel.IPOrCOMStr, RJmodel.SpeedOrPort);
            udpSend = new UdpClient();
            try
            {
                RemoteIPEP = new IPEndPoint(IPAddress.Parse(RJmodel.IPOrCOMStr), RJmodel.SpeedOrPort);
            }
            catch
            {

                RemoteIPEP = null;
            }
        }

        /// <summary>
        /// 连接器重连
        /// </summary>
        /// <returns></returns>
        public override bool ChongLian()
        {
            try
            {
                if (ShouCi == 0)
                {
                    State = false;
                    ShouCi++;
                    try
                    {
                        udpSend.Close();
                        udpSend.Dispose();
                        udpSend = null;
                    }
                    catch
                    {

                    }

                }
                udpSend = new UdpClient();
                udpSend.Connect(RemoteIPEP);
                State = true;
                ShouCi = 0;
                return true;
            }
            catch
            {


            }
            return false;
        }
    }
}
