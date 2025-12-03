using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommLei.JiChuLei;
using SSheBei.Model;

namespace SSheBei.LianJieQi
{
    /// <summary>
    /// TCP连接器
    /// </summary>
    public class TCPLianJieQi : ABSLianJieQi
    {
        #region 变量区

        /// <summary>
        /// 表示发送报文的客户端
        /// </summary>
        public TcpClient TcpClient;
        /// <summary>
        /// 表示报文链接流
        /// </summary>
        public NetworkStream NetworkStream;
        /// <summary>
        /// 存放rjmodel
        /// </summary>
        private SetLianJieQiModel RJmodel;
        /// <summary>
        /// 描述
        /// </summary>
        private string ComMiaoSu = "";
        private bool  TX = false;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>     
        public TCPLianJieQi()
        {

        }



        /// <summary>
        /// 名称
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
                return TX;

            }
        }

        /// <summary>
        /// 关闭连接器
        /// </summary>
        /// <returns></returns>
        public override ResultModel Close()
        {
            ResultModel rm = new ResultModel();
            TX = false;
            try
            {
                if (NetworkStream != null)
                {
                    NetworkStream.Close();
                    NetworkStream.Dispose();
                    rm.SetValue(true, string.Format("{0}:关闭成功", Name));
                }

            }
            catch (Exception ex)
            {
                rm.SetValue(false, string.Format("{0}:关闭出现异常:{1}", Name, ex.Message));


            }
            try
            {
                if (TcpClient != null)
                {
                    TcpClient.Close();
                    TcpClient.Dispose();
                    TcpClient= null;
                }
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

                ChongLian();

                if (TcpClient.Connected)
                {
                    TX = true;
                    NetworkStream = TcpClient.GetStream();
                    rm.SetValue(true, string.Format("{0}:打开成功", ComMiaoSu));

                }
                else
                {

                    rm.SetValue(false, string.Format("{0}:打开失败", ComMiaoSu));
                }

            }
            catch (Exception ex)
            {
                rm.SetValue(false, string.Format("{0}:打开异常:{1}", ComMiaoSu, ex.Message));


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
                    if (NetworkStream.DataAvailable)
                    {
                      
                        byte[] receiveBuffer = new byte[1024];
                        int bytesRead = NetworkStream.Read(receiveBuffer, 0, receiveBuffer.Length);
                        if (bytesRead > 0)
                        {
                            byte[] gg = receiveBuffer.SubArray(0,bytesRead);
                            rm.SetValue(true, string.Format("{0}:读取数据成功:{1}", ComMiaoSu, ChangYong.ByteOrString(gg, " ")), gg);

                        }
                      

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

                    NetworkStream.Write(bytData, 0, bytData.Length);
                    NetworkStream.Flush();
                    rm.SetValue(true, string.Format("{0}:发送数据成功:{1}", Name, ChangYong.ByteOrString(bytData, " ")));
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
            ComMiaoSu = string.Format("{0}*{1}TCP", RJmodel.IPOrCOMStr, RJmodel.SpeedOrPort);
            this.TcpClient = new TcpClient();

        }
        /// <summary>
        /// 重连
        /// </summary>
        /// <returns></returns>
        public override bool ChongLian()
        {
            try
            {
                Close();
                this.TcpClient = new TcpClient();
                TcpClient.Connect(new IPEndPoint(IPAddress.Parse(RJmodel.IPOrCOMStr), RJmodel.SpeedOrPort));
                if (TcpClient.Connected)
                {
                    NetworkStream = TcpClient.GetStream();
                    
                    return true;
                }
            }
            catch
            {


            }
            return false;
        }
    }
}
