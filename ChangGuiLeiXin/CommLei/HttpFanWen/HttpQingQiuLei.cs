using CommLei.JiChuLei;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.HttpFanWen
{
    /// <summary>
    /// 这是客户端调用的类
    /// </summary>
    public class HttpQingQiuLei
    {
       

        /// <summary>
        /// http请求参数
        /// </summary>
        private HttpRequestModel _Model;

        /// <summary>
        /// 请求的Cookie;
        /// </summary>
        private  CookieContainer _Cookie { get; set; }

        /// <summary>
        /// 数据的大小
        /// </summary>
        private int ShuJuDaXiao = 2 * 1024 * 1024;
        private bool ZongKaiShi = true;

        #region 单例
        private readonly static object DanYi = new object();
        private static HttpQingQiuLei jieRuFuWuQi = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        private HttpQingQiuLei()
        {
            _Model = new HttpRequestModel();
            _Cookie = new CookieContainer();
        }

        /// <summary>
        /// 单例类，必须KaiqiRiZhi设置为True才能写日志
        /// </summary>
        /// <returns>返回NewXieRiZhiLog</returns>
        public static HttpQingQiuLei Cerate()
        {
            if (jieRuFuWuQi == null)
            {
                lock (DanYi)
                {
                    if (jieRuFuWuQi == null)
                    {
                        jieRuFuWuQi = new HttpQingQiuLei();
                    }
                }
            }
            return jieRuFuWuQi;
        }
        #endregion
        /// <summary>
        /// 非文件的请求 true 表示服务端传大量数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="model"></param>
        /// <param name="shifouxiazaidawenjian"></param>
        /// <returns></returns>
        public OutBaseModel<V> QiuQiuHttp<T, V>(T model,bool shifouxiazaidawenjian=false) where V : class where T : InBaseModel
        {
            #region 新的         
            OutBaseModel<V> suixiang = new OutBaseModel<V>();
            if (model.IsShiYong == false)
            {
                suixiang.Msg = "接口设置为不使用";
                return suixiang;
            }
            if (string.IsNullOrEmpty(_Model.StrUri))
            {
                suixiang.Msg = "请求的地址不存在";
                return suixiang;
            }
            string sql = PingJieStr<T>(model, "&");
            if (string.IsNullOrEmpty(sql))
            {
                suixiang.Msg = "请求的数据不能存在";
                return suixiang;
            }
            string shuju = string.Format("{0}?{1}", _Model.StrUri, sql);
            if (shifouxiazaidawenjian)
            {
                suixiang= RequestXiaZaiWenJian<V>(shuju);
            }
            else
            {
                int get = 1;
                if (sql.Length > ShuJuDaXiao)
                {
                    get = 2;
                }
              
                if (get == 1)
                {
                    string rec =RequestAndRespone(shuju);
                    suixiang = GetModelXin<V>(rec, shuju);
                }
                else
                {
                    string rec =PostQingQiu(_Model.StrUri, sql);
                    suixiang = GetModelXin<V>(rec, shuju);
                }
            }
            return suixiang;
            #endregion
        }

        private OutBaseModel<V> GetModelXin<V>(string data, string fasongmsg) where V : class
        {
            OutBaseModel<V> outBaseModel = new OutBaseModel<V>();
            if (string.IsNullOrEmpty(data))
            {
                outBaseModel.ChengGong = false;
                outBaseModel.Msg = $"服务器返回的数据为空";
                return outBaseModel;
            }

            try
            {
                outBaseModel = ChangYong.HuoQuJsonToShiTi<OutBaseModel<V>>(data);
                if (outBaseModel == null)
                {
                    outBaseModel = new OutBaseModel<V>();
                    outBaseModel.ChengGong = false;
                    OutBaseModel outBaseModel2 = ChangYong.HuoQuJsonToShiTi<OutBaseModel>(data);
                    if (outBaseModel2 != null)
                    {
                        if (!outBaseModel2.ChengGong)
                        {
                            outBaseModel.Msg = outBaseModel2.ShuJu.ToString();
                        }
                    }
                    else
                    {
                        outBaseModel.Msg = "服务器返回有问题";
                    }
                }
                else if (!outBaseModel.ChengGong)
                {
                    outBaseModel.Msg = outBaseModel.ShuJu.ToString();
                }
            }
            catch (Exception ex)
            {
                outBaseModel = new OutBaseModel<V>();
                outBaseModel.ChengGong = false;
                outBaseModel.Msg = $"{ex.Message},{fasongmsg}";
            }

            return outBaseModel;
        }



        /// <summary>
        /// 文件的传输
        /// </summary>
        /// <param name="model"></param>
        /// <param name="len"></param>
        /// <param name="ChuangShu"></param>
        /// <returns></returns>
        public OutBaseModel<V> ChangChuanWenJin<T, V>(T model, long len, Action<Stream> ChuangShu) where T : InBaseModel where V : class
        {

            OutBaseModel<V> suixiang = new OutBaseModel<V>();
            if (model.IsShiYong == false)
            {
                suixiang.Msg = "接口设置为不使用";
                return suixiang;
            }
            if (string.IsNullOrEmpty(_Model.StrUri))
            {
                suixiang.Msg = "请求的地址不存在";
                return suixiang;
            }           
            string sql = PingJieStr<T>(model, "&");
            if (string.IsNullOrEmpty(sql))
            {
                suixiang.Msg = "请求的数据不能存在";
                return suixiang;
            }
            string rec = string.Format("{0}?{1}", _Model.StrUri, sql);
            string jieshou =ShangChuanShiPing(rec, len, ChuangShu);
            suixiang = GetModelXin<V>(jieshou, rec);
            return suixiang;
        }
        /// <summary>
        /// 测试时候用的用于用的
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string CeShuFanHuiString(string model)
        {
            #region 新的
        
        

            int get = 1;
            if (model.Length > ShuJuDaXiao)
            {
                get = 2;
            }
         
            if (get == 1)
            {
                string rec = string.Format("{0}?{1}", _Model.StrUri, model);
                string shuju = RequestAndRespone(rec);
                return shuju;
            }
            else
            {
                string shuju = PostQingQiu(_Model.StrUri, model);            
                return shuju;
            }
         
            #endregion
        }

        /// <summary>
        /// 参考用的
        /// </summary>
        public void SetWanZhi(string uri,int shujudaxiao=2*1024*1024)
        {
            _Model.StrUri = uri;
            ShuJuDaXiao = shujudaxiao;
        }

      

        private string PingJieStr<T>(T model,string pijiefu)
        {
            List<string> yuju = new List<string>();
            Type t = model.GetType();
            PropertyInfo[] shuxin = t.GetProperties();
            int lenth = shuxin.Length;
            for (int i = 0; i < lenth; i++)
            {
                if (shuxin[i].IsDefined(typeof(TeXingAttribute), true))
                {
                    TeXingAttribute xi = (TeXingAttribute)shuxin[i].GetCustomAttributes(true)[0];
                    if (xi.GetShiYong())
                    {
                        if (string.IsNullOrEmpty(xi.GetName()))
                        {
                            yuju.Add(string.Format("{0}={1}", shuxin[i].Name, shuxin[i].GetValue(model, null)));
                        }
                        else
                        {
                            yuju.Add( string.Format("{0}={1}", xi.GetName(), shuxin[i].GetValue(model, null)));
                        }

                    }
                  
                }              
            }
            if (yuju.Count>0)
            {
                string s = string.Join(pijiefu, yuju.Select((p) => { return p; }));
                return s;
            }
            return "";
        }


        #region 请求方法


        /// <summary>
        /// 请求与相应 这是Get请求  和小数据的post请求 请求地址
        /// </summary>
        /// <param name="srtRequest">网址</param>
        /// <returns>返回相应</returns>
        private  string RequestAndRespone(string srtRequest)
        {
           
            HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(srtRequest);
            http.Method = this._Model.RequestMethod;
            http.ContentType = this._Model.ContentType;
            http.Proxy = null;
            http.KeepAlive = true;
            string res = string.Empty;
            try
            {
                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), _Model.EncodWay);
                res = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                response.Close();
            }
            catch
            {
                res = "无法连接远程服务器，客户端请求失败";
            }
            return res;
        }

        /// <summary>
        /// 这是上传文件的请求 data表示文件的大小
        /// </summary>
        /// <param name="srtRequest"></param>
        /// <param name="data"></param>
        /// <param name="ChuangShu"></param>
        /// <returns></returns>
        private  string ShangChuanShiPing(string srtRequest, long data, Action<Stream> ChuangShu)
        {
            if (data == 0 || ChuangShu == null || string.IsNullOrEmpty(srtRequest))
            {
                return "客户端上传的内容为空";
            }
            HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(srtRequest);

            http.ContentLength = data;

            http.Timeout = 300 * 1000;
            http.CookieContainer = _Cookie;
            http.ReadWriteTimeout = 300 * 1000;
            http.UserAgent = _Model.UserAgent;
            http.Accept = _Model.Accept;

            http.ContentType = _Model.ContentType;
            http.Method = _Model.JieShouMethod;

            Stream dataStream = null;
            try
            {
                dataStream = http.GetRequestStream();

            }
            catch
            {
                if (dataStream != null)
                {
                    dataStream.Close();
                    dataStream.Dispose();
                }

                return "无法连接服务器，客户端请求失败";

            }

            ChuangShu(dataStream);

            dataStream.Close();
            dataStream.Dispose();
            //读取返回消息
            string res = string.Empty;
            try
            {
                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                res = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                response.Close();
            }
            catch (WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                res = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
                response.Close();
            }

            return res;



        }

        /// <summary>
        /// 大数据的post请求 srtRequest地址 后面是数据
        /// </summary>
        /// <param name="srtRequest"></param>
        /// <param name="udr"></param>
        /// <returns></returns>
        private  string PostQingQiu(string srtRequest, string udr)
        {
            CookieContainer cookie = new CookieContainer();
            _Cookie = new CookieContainer();
            string content = "";
            try
            {
                HttpWebRequest httpRequset = (HttpWebRequest)HttpWebRequest.Create(srtRequest);//创建http 请求
                httpRequset.CookieContainer = cookie;//设置cookie
                httpRequset.Method = _Model.RequestMethod;//POST 提交              
                httpRequset.UserAgent = _Model.UserAgent;
                httpRequset.Accept = _Model.Accept;//text/xml;charset=utf-8
                httpRequset.ContentType = _Model.ContentType;//以上信息在监听请求的时候都有的直接复制过来//text/xml;charset=utf-8
                byte[] bytes = _Model.EncodWay.GetBytes(udr); //System.Text.Encoding.UTF8.GetBytes(postString);
                httpRequset.ContentLength = bytes.Length;
                Stream stream = httpRequset.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();//以上是POST数据的写入
                HttpWebResponse httpResponse = (HttpWebResponse)httpRequset.GetResponse();//获得 服务端响应
                using (Stream responsestream = httpResponse.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(responsestream, System.Text.Encoding.UTF8))
                    {
                        content = sr.ReadToEnd();
                    }
                }
                _Cookie = cookie;

            }
            catch
            {


            }
            return content;//拿到cookie
        }

        /// <summary>
        /// 请求与相应
        /// </summary>
        /// <param name="srtRequest">网址</param>     
        /// <returns>返回相应</returns>
        public OutBaseModel<V> RequestXiaZaiWenJian<V>(string srtRequest) where V : class
        {
            OutBaseModel<V> outBaseModel = new OutBaseModel<V>();
           
         

            HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(srtRequest);

            http.Method = this._Model.RequestMethod;

            http.ContentType = this._Model.ContentType;
            http.Proxy = null;
            http.KeepAlive = true;

            //读取返回消息
            string res = string.Empty;
            try
            {

                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), _Model.EncodWay);
                var jr = new JsonTextReader(reader); //Newtonsoft JSON读取器，它解决了JSON数组流式返回需要分析json格式的问题。
                JsonSerializer serializer = new JsonSerializer();
                while (ZongKaiShi)
                {
                    try
                    {
                        if (jr.Read())
                        {                          
                            outBaseModel = serializer.Deserialize<OutBaseModel<V>>(jr);
                        }
                        else
                        {
                            break;
                        }

                    }
                    catch
                    {

                       
                    }
                    Thread.Sleep(1);
                }

                jr.Close();
                reader.Close();
                reader.Dispose();
                response.Close();


                if (outBaseModel == null)
                {
                    outBaseModel = new OutBaseModel<V>();
                    outBaseModel.ChengGong = false;
                    outBaseModel.Msg = "服务器返回的数据有问题";
                }
                else
                {
                    if (outBaseModel.ChengGong == false)
                    {
                        outBaseModel.Msg = outBaseModel.ShuJu.ToString();
                    }
                }
               
            }
            catch
            {

                outBaseModel = new OutBaseModel<V>();
                outBaseModel.ChengGong = false;
                outBaseModel.Msg = "无法连接远程服务器";

            }
            return outBaseModel;
        }

        #endregion

        internal class HttpRequestModel
        {
            /// <summary>
            /// 请求模式，默认为POST
            /// </summary>
            public string RequestMethod { get; set; }


            /// <summary>
            /// 接收模式，默认为GET
            /// </summary>
            public string JieShouMethod { get; set; }

            /// <summary>
            /// 设置http头标，默认为 "text/xml;charset=utf-8 "
            /// </summary>
            public string ContentType { get; set; }

            /// <summary>
            /// 设置http头标，默认为 "text/xml;charset=utf-8 "
            /// </summary>
            public string ContentType1 { get; set; }

            /// <summary>
            /// 获取返回的编码方式,默认为Encoding.Default
            /// </summary>
            public Encoding EncodWay { get; set; }

            /// <summary>
            /// 请求用户的信息
            /// </summary>
            public string UserAgent { get; set; }

            /// <summary>
            /// 接收的格式
            /// </summary>
            public string Accept { get; set; }


            /// <summary>
            /// q请求的地址
            /// </summary>
            public string StrUri { get; set; }

            /// <summary>
            /// 构造函数
            /// </summary>
            public HttpRequestModel()
            {
                RequestMethod = "POST";

                EncodWay = Encoding.UTF8;
                JieShouMethod = "POST";


                //  UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.157 Safari/537.36";
                Accept = "text/html, application/xhtml+xml, */*";//text/xml;charset=utf-8// text/plain, */*; q=0.01
                                                                 //    Accept1 = "text/plain, */*; q=0.01";
                                                                 // Accept = "text/plain, application/xhtml+xml, */*";//text/xml;charset=utf-8
                                                                 //   ContentType = "application/json";
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                // ContentType = "application/json; charset=UTF-8";

                //  ContentType1 = "application/json; charset=UTF-8";

                StrUri = "";
            }
        }
    }
}
