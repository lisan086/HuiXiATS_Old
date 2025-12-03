using SSheBei.Model;
using SSheBei.PeiZhi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSheBei.ABSSheBei
{
    /// <summary>
    /// 一个虚设备
    /// </summary>
    public abstract class ABSNSheBei
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public  int SheBeiID { get; set; }
        /// <summary>
        /// 设备的名称
        /// </summary>
        public string SheBeiName { get; set; } = "";

        /// <summary>
        /// 设备的分组
        /// </summary>
        public string FenZu { get; set; } = "";

        /// <summary>
        /// 配置参数用的
        /// </summary>
        public string PeiZhiObjName { get; set; }

        /// <summary>
        /// true 关闭读和写
        /// </summary>
        public bool GuanBiDuXie { get; set; } = false;

        /// <summary>
        /// 设备类型
        /// </summary>
        public abstract string SheBeiType { get; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public abstract string BanBenHao { get; }

        /// <summary>
        /// 设备通信 true表示通信成功
        /// </summary>
        public abstract bool TongXin { get; }

      

        /// <summary>
        /// 设备的通信
        /// </summary>
        public event ZongXianMsg SheBeiMsgEvnt;

        /// <summary>
        /// 读事件的单独触发
        /// </summary>
        public event Action<int> DuShuJuChuFaEvent;

        /// <summary>
        /// 设备初始化
        /// </summary>
        public abstract void IniData(bool ispeizhi);

        /// <summary>
        /// 打开总线上的设备
        /// </summary>    
        public abstract void Open();
        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public abstract void Close();

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="canshus"></param>
        public abstract void XieShuJu(List<JiCunQiModel> canshus);

       
        /// <summary>
        /// true是否清理全部
        /// </summary>
        /// <param name="isquanbu"></param>
        /// <param name="model"></param>
        public abstract void Clear(bool isquanbu,JiCunQiModel model);

        /// <summary>
        /// 获取每个通信
        /// </summary>
        /// <returns></returns>
        public abstract TxModel GetMeiGeTx();

        /// <summary>
        /// 用这校验数据ID
        /// </summary>
        /// <param name="jicunqiid"></param>
        /// <returns></returns>
        public abstract JiaoYanJieGuoModel JiaoYanChengGong(JiCunQiModel jicunqiid);


        /// <summary>
        /// 获取读参数
        /// </summary>
        /// <returns></returns>
        public abstract List<JiCunQiModel> GetShuJu();


        /// <summary>
        /// 获取配置和调试界面
        /// </summary>
        /// <returns></returns>
        public abstract JieMianFrmModel GetFrm(bool istiaoshi);

        /// <summary>
        /// 用于配置读写 1是读 2是写 3是全部
        /// </summary>
        /// <returns></returns>
        public abstract List<JiCunQiModel> PeiZhiDuXie(int type);

        /// <summary>
        /// 获取连接器
        /// </summary>
        /// <returns></returns>
        public virtual  object GetLianJieQi(int id)
        {
            return null;
        }
        /// <summary>
        /// 获取参数KJ
        /// </summary>
        /// <returns></returns>
        public virtual KJPeiZhiJK GetCanShuKJ(string jicunweiyibiaoshi)
        {
            MoRenKJ moRenKJ = new MoRenKJ();
            return moRenKJ;
        }
        /// <summary>
        /// 触发设备消息
        /// </summary>
        /// <param name="dengJi"></param>
        /// <param name="msg"></param>     
        protected void ChuFaMsg(MsgDengJi dengJi, string msg)
        {

            if (SheBeiMsgEvnt != null)
            {
                MsgModel model = new MsgModel();
                model.Msg = msg;
                model.SheBeiID=SheBeiID;
                
                SheBeiMsgEvnt(dengJi, model);
            }
        }

        /// <summary>
        /// 触发读
        /// </summary>
        protected void ChuFaDu(int id)
        {
            if (DuShuJuChuFaEvent!=null)
            {
                DuShuJuChuFaEvent(id);
            }
        }
    }
}
