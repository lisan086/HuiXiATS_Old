using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommLei.JiChuLei;
using FoZhaoMes.Model.InModel;
using FoZhaoMes.Model.OutMdeol;

namespace FoZhaoMes.Lei
{
    public class XingDeMesQingQiuLei
    {


        private Dictionary<int, HttpQingQiuPostLei> HttpSheBeiLei = new Dictionary<int, HttpQingQiuPostLei>();


        private Encoding Encoding = Encoding.UTF8;

        #region 单例
        private readonly static object DanYi = new object();
        private static XingDeMesQingQiuLei jieRuFuWuQi = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        private XingDeMesQingQiuLei()
        {

        }

        /// <summary>
        /// 单例类，必须KaiqiRiZhi设置为True才能写日志
        /// </summary>
        /// <returns>返回NewXieRiZhiLog</returns>
        public static XingDeMesQingQiuLei Cerate()
        {
            if (jieRuFuWuQi == null)
            {
                lock (DanYi)
                {
                    if (jieRuFuWuQi == null)
                    {
                        jieRuFuWuQi = new XingDeMesQingQiuLei();
                    }
                }
            }
            return jieRuFuWuQi;
        }
        #endregion


        public void SetShuJu(List<int> zhan)
        {
            HttpSheBeiLei.Clear();
            for (int i = 0; i < zhan.Count; i++)
            {
                HttpQingQiuPostLei lianjiemodel = new HttpQingQiuPostLei();

                if (HttpSheBeiLei.ContainsKey(zhan[i]) == false)
                {
                    HttpSheBeiLei.Add(zhan[i], lianjiemodel);
                }
            }
        }
        /// <summary>
        /// 非文件的请求 true 表示服务端传大量数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="model"></param>
        /// <param name="shifouxiazaidawenjian"></param>
        /// <returns></returns>
        public OutFoShanBaseModel<V> QiuQiuHttp<T, V>(T model, int gwid, string wangzhi) where V:new()  where T : InFoShanBaseModel
        {
            if (HttpSheBeiLei.ContainsKey(gwid))
            {
                string shijian = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string rec = ChangYong.HuoQuJsonStr(model);
                HttpQingQiuPostLei shebeis = HttpSheBeiLei[gwid];
                shebeis.SetWanZhi(wangzhi);
                
                string jieguo = shebeis.QiuQiuHttp(rec);
                OutFoShanBaseModel<V> shuju = GetModelXin<V>(jieguo, $"{shijian} {rec}");

                return shuju;
            }
            else
            {
                OutFoShanBaseModel<V> shuju = new OutFoShanBaseModel<V>();
                shuju.Msg = $"没有找到设备名称的连接服务端:{gwid}";
                return shuju;
            }


        }



        private OutFoShanBaseModel<V> GetModelXin<V>(string data, string fasongmsg) where V : new()
        {
            OutFoShanBaseModel<V> outBaseModel = new OutFoShanBaseModel<V>();
            if (string.IsNullOrEmpty(data))
            {
                outBaseModel.ChengGong = false;
                outBaseModel.Msg = $"发送数据:{fasongmsg}   接收数据:{data}";
                return outBaseModel;
            }

            try
            {
                V duixiang = ChangYong.HuoQuJsonToShiTi<V>(data);
                outBaseModel.Msg = $"发送数据:{fasongmsg}  接收数据:{data}";
                if (duixiang == null)
                {

                    outBaseModel.ChengGong = false;
                    outBaseModel.Msg = $"发送数据:{fasongmsg}  接收数据:{data}";
                }
                else
                {
                    outBaseModel.ChengGong = true;
                    outBaseModel.ShuJu = duixiang;
                }
            }
            catch (Exception ex)
            {
                outBaseModel = new OutFoShanBaseModel<V>();
                outBaseModel.ChengGong = false;
                outBaseModel.Msg = $"发送数据:{fasongmsg}  接收数据:{data} 解析josn出错:{ex}";
            }

            return outBaseModel;
        }


        public void Close()
        {



        }





    }
}
