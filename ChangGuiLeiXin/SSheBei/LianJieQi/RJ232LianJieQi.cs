using CommLei.JiChuLei;
using SSheBei.Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSheBei.LianJieQi
{
    /// <summary>
    /// 232与485的通信连接器
    /// </summary>
    public class RJ232LianJieQi : ABSLianJieQi
    {
        #region 变量区
        /// <summary>
        /// 串口对象
        /// </summary>
        private SerialPort SerPortCOM;

        /// <summary>
        /// 串口参数对象
        /// </summary>
        private SetLianJieQiModel SerportModel;


        private int ShouCi = 0;

        private bool IsZhenCOM = false;
        /// <summary>
        /// 描述
        /// </summary>
        private string _ComMiaoSu = "";


        /// <summary>
        /// 连接器的名称
        /// </summary>
        public override string Name
        {
            get
            {
                return _ComMiaoSu;
            }
        }

        /// <summary>
        /// true表示通讯上
        /// </summary>
        public override bool TongXinState
        {
            get
            {
                bool tong = false;
                if (SerPortCOM != null)
                {
                    tong = SerPortCOM.IsOpen;
                }
                return tong;
            }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public RJ232LianJieQi()
        {

        }


        /// <summary>
        /// 发送数据的方法,如果ResultModel.IsChengGong==true,
        /// </summary>
        /// <param name="bytData">发送数据</param>
        /// <returns>返回一个 ResultModel对象</returns>
        public override ResultModel Send(byte[] bytData)
        {
            ResultModel rm = new ResultModel();
            try
            {

                if (TongXinState)
                {
                    Clear();                   
                    this.SerPortCOM.Write(bytData, 0, bytData.Length);
                    rm.SetValue(true, string.Format("{0}:数据成功发送:{1}", _ComMiaoSu, ChangYong.ByteOrString(bytData, " ")));
                }
                else
                {
                    rm.SetValue(false, string.Format("{0}:串口未打开,不能发送数据:{1}", _ComMiaoSu, ChangYong.ByteOrString(bytData, " ")));
                }
            }
            catch (Exception ex)
            {

                rm.SetValue(false, string.Format("{0}：发送数据异常信息:{1},{2}", _ComMiaoSu, ex.Message, ChangYong.ByteOrString(bytData, " ")));
            }

            return rm;
        }

        /// <summary>
        /// 读取数据方法,如果ResultModel.IsChengGong==true
        /// </summary>
        /// <returns>返回一个 ResultModel对象</returns>
        public override ResultModel Read()
        {
            ResultModel rm = new ResultModel();
            rm.IsSuccess = false;
            try
            {
                if (TongXinState)
                {
                    if (this.SerPortCOM.BytesToRead > 0)
                    {
                        int intDataConut = this.SerPortCOM.BytesToRead;
                        byte[] bytData = new byte[intDataConut];
                        if (SerPortCOM.Read(bytData, 0, bytData.Length) > 0)
                        {
                            rm.SetValue(true, string.Format("{0}:数据成功接收:{1}", _ComMiaoSu, ChangYong.ByteOrString(bytData, " ")), bytData);

                        }

                    }

                }
                else
                {
                    rm.SetValue(false, string.Format("{0}:接收数据失败,串口未打开 ", _ComMiaoSu));
                }
            }
            catch (Exception ex)
            {
                rm.SetValue(false, string.Format(string.Format("{0}:数据接收异常:{1}", _ComMiaoSu, ex.Message)));

            }
            return rm;
        }

        /// <summary>
        /// 打开串口的方法,如果ResultModel.IsChengGong==true,打开成功
        /// </summary>
        /// <returns>返回一个 ResultModel对象</returns>
        public override ResultModel Open()
        {
            ResultModel rm = new ResultModel();
            try
            {
                if (IsZhenCOM)
                {

                    SerPortCOM.Open();

                    if (SerPortCOM.IsOpen)
                    {
                        rm.SetValue(true, string.Format("{0}:打开成功", _ComMiaoSu));


                    }
                    else
                    {
                        rm.SetValue(false, string.Format("{0}:打开失败", _ComMiaoSu));

                    }
                }
                else
                {
                    rm.SetValue(false, string.Format("{0}:配置的COM有问题", _ComMiaoSu));

                }
            }
            catch (Exception ex)
            {
                rm.SetValue(false, string.Format("{0}:打开出现异常:{1}", _ComMiaoSu, ex.Message));


            }
            return rm;
        }

        /// <summary>
        /// 关闭连接 
        /// </summary>
        /// <returns>返回ResultModel对象</returns>
        public override ResultModel Close()
        {
            ResultModel rm = new ResultModel();
            try
            {
                if (SerPortCOM != null)
                {
                    SerPortCOM.Close();
                    rm.SetValue(true, string.Format("{0}:关闭成功", _ComMiaoSu));
                }

            }
            catch (Exception ex)
            {
                rm.SetValue(true, string.Format("{0}:关闭出现异常,信息:{1}", _ComMiaoSu, ex.Message));



            }
            return rm;
        }

        private void Clear()
        {
            if (TongXinState)
            {
                this.SerPortCOM.DiscardInBuffer();
                this.SerPortCOM.DiscardOutBuffer();
            }
        }

        /// <summary>
        /// 设置连接的参数
        /// </summary>
        /// <param name="model"></param>
        public override void SetCanShu(SetLianJieQiModel model)
        {
            this.SerportModel = model;
            _ComMiaoSu = string.Format("{0}:{1},{2}*{3}*{4}串口", model.IPOrCOMStr, model.SpeedOrPort, 8, 1, "无校验");
           
            if (string.IsNullOrEmpty(SerportModel.IPOrCOMStr))
            {
                IsZhenCOM = false;
            }
            else
            {
               
                IsZhenCOM = true;
            }
            if (IsZhenCOM)
            {
                SerPortCOM = new SerialPort();
                SerPortCOM.PortName = SerportModel.IPOrCOMStr;
                SerPortCOM.BaudRate = SerportModel.SpeedOrPort;
                SerPortCOM.DataBits = 8;
                SerPortCOM.StopBits = StopBits.One;
                SerPortCOM.Parity = Parity.None;
                SerPortCOM.ReadBufferSize = 500;
            }

        }

        /// <summary>
        /// 重连接用的
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
                        if (SerPortCOM != null)
                        {
                            SerPortCOM.Close();
                            SerPortCOM.Dispose();
                            SerPortCOM = null;
                        }
                    }
                    catch
                    {


                    }

                }
                SerPortCOM = new SerialPort();
                if (string.IsNullOrEmpty(SerportModel.IPOrCOMStr))
                {
                    IsZhenCOM = false;
                }
                else
                {
                    IsZhenCOM = true;
                }
                if (IsZhenCOM)
                {
                    SerPortCOM.PortName = SerportModel.IPOrCOMStr;
                    SerPortCOM.BaudRate = SerportModel.SpeedOrPort;
                    SerPortCOM.DataBits = 8;
                    SerPortCOM.StopBits = StopBits.One;
                    SerPortCOM.Parity = Parity.None;
                    SerPortCOM.ReadBufferSize = 500;
                    SerPortCOM.Open();
                    if (SerPortCOM.IsOpen)
                    {
                        ShouCi = 0;
                        return true;
                    }
                }

            }
            catch
            {


            }
            return false;
        }
    }
}
