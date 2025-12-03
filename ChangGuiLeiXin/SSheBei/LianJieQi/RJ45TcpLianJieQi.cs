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
    /// 网口tcp通信
    /// </summary>
    public class RJ45TcpLianJieQi : ABSLianJieQi
    {
        #region 变量区

        /// <summary>
        /// 网络连接口
        /// </summary>
        private Socket _sSocket;
        /// <summary>
        /// 存放rjmodel
        /// </summary>
        private SetLianJieQiModel RJmodel;
        /// <summary>
        /// 描述
        /// </summary>
        private string ComMiaoSu = "";
        private int ShouCi = 0;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>     
        public RJ45TcpLianJieQi()
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
                if (_sSocket != null && _sSocket.Connected)
                {

                    return true;
                }
                else
                {

                    return false;
                }

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
                _sSocket.Close();
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

                _sSocket.Connect(new IPEndPoint(IPAddress.Parse(RJmodel.IPOrCOMStr), RJmodel.SpeedOrPort));

                if (_sSocket.Connected)
                {

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

                    if (_sSocket.Available > 0)
                    {
                        byte[] bytData = new byte[_sSocket.Available];
                        _sSocket.Receive(bytData, bytData.Length, SocketFlags.Partial);
                        rm.SetValue(true, string.Format("{0}:读取数据成功:{1}", ComMiaoSu, ChangYong.ByteOrString(bytData, " ")), bytData);
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
                    if (this._sSocket.Send(bytData, bytData.Length, SocketFlags.Partial) > 0)
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
            ComMiaoSu = string.Format("{0}*{1}TCP", RJmodel.IPOrCOMStr, RJmodel.SpeedOrPort);
            this._sSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _sSocket.ReceiveTimeout = 500;
        }
        /// <summary>
        /// 重连
        /// </summary>
        /// <returns></returns>
        public override bool ChongLian()
        {
            try
            {
                if (ShouCi == 0)
                {
                    ShouCi++;
                    try
                    {
                        _sSocket.Close();
                        _sSocket.Dispose();
                        _sSocket = null;
                    }
                    catch
                    {

                    }

                }
                this._sSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _sSocket.ReceiveTimeout = 500;
                _sSocket.Connect(new IPEndPoint(IPAddress.Parse(RJmodel.IPOrCOMStr), RJmodel.SpeedOrPort));
                if (_sSocket.Connected)
                {
                    ShouCi = 0;
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
