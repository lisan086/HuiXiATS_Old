using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLei.DataChuLi
{
    /// <summary>
    /// 强加密，目前还有很多没有完善
    /// </summary>
    public class QiangJiaMi
    {
        #region 自定义加密算法
        /*  011,125,132:1  012,124,138:2 013,123,230:3 014,128,129:4 015,112,456:5 016,198,199:6 017,298,268:7 018,368,485:8 019,698,789:9 001,986,894:0
         * 021,323,325:, 022,326,324:A 023,332,333:B 024,327,328:C 025,342,340:D 026,341,343:E 027,344,346:F 028,347,348:G 030,350,351::
         * 031,710,720:a  032,711,721:b 033,722,731:c 034,712,713:d 035,715,716:e 036,718,724:f 037,725,728 g
         * 041,072,071:. 042,073,074:? 043,076,078:q 044,077,075:y 045,079,070:t 046,091,092:u 047,093,094:i 048,095,096:p 049,097,099:s
         * 051,140,141:h   052,142,145:j   053,146,143:l 054,218,218:z 055,226,227:x  056,148,144:v 057,188,185:n 058,183,184:m 059,210,213:r 060,252,215:o 061,228,229:w 062,253,212:k
         * 151,158,467:Q  152,462,461:Y 153,460,463:T 154,464,478:U 155,477,469:I 156,470,471:P 157,476,479:S
         * 161,510,511:H  162,512,513:J 163,514,515:L 164,516,517:Z 165,518,519:X 166,520,521:V 167,522,523:N 168,524,525:M 169,526,527:R 170,528,529:O 171,530,531:W 172,532,533:K
         * 081,540,541:+ 082,542,543:= 083,544,545:- 084,546,547:# 085,548,549:$ 086,550,551:% 087,552,553:^ 088,554,555:& 089,556,557:* 
         * 598,558,559:<   597,570,571:>  596,572,573:" "
        */
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public QiangJiaMi()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();//生成字节数组
            int iRoot = BitConverter.ToInt32(buffer, 0);//利用BitConvert方法把字节数组转换为整数              
            SuiJiShu = new Random(iRoot);
            SuiJiShu.Next(1, 99);
            quzhi();
            quzhifanzhunNew();
        }


        private void quzhifanzhunNew()
        {
            List<string> lis = new List<string>();
            foreach (string item in ZiDian.Keys)
            {
                lis = ZiDian[item];
                for (int i = 0; i < lis.Count; i++)
                {
                    ZiDianFanZhun.Add(lis[i], item);
                }
            }
        }

        private Dictionary<string, List<string>> ZiDian = new Dictionary<string, List<string>>();

        private Dictionary<string, string> ZiDianFanZhun = new Dictionary<string, string>();

        private Random SuiJiShu;



        private string QuSuiJiShu(List<string> lis)
        {
            SuiJiShu.Next(0, lis.Count);
            return lis[SuiJiShu.Next(0, lis.Count)];
        }

        private void quzhi()
        {
            ZiDian.Clear();
            #region 1
            List<string> lis = new List<string>()
            {
                "011","125","132"
            };
            ZiDian.Add("1", lis);
            #endregion
            #region 2
            //011,125,132:1  012,124,138:2 013,123,230:3 014,128,129:4 015,112:5 016,198,199:6 017,298,268:7 018,368,485:8 019,698,789:9 001,986,894:0
            lis = new List<string>();
            lis.Clear();
            lis.Add("012");
            lis.Add("124");
            lis.Add("138");
            ZiDian.Add("2", lis);
            #endregion
            #region 3
            // 013,123,230:3 014,128,129:4 015,112:5 016,198,199:6 017,298,268:7 018,368,485:8 019,698,789:9 001,986,894:0
            lis = new List<string>();
            lis.Clear();
            lis.Add("013");
            lis.Add("123");
            lis.Add("230");
            ZiDian.Add("3", lis);
            #endregion
            #region 4
            //  014,128,129:4 015,112:5 016,198,199:6 017,298,268:7 018,368,485:8 019,698,789:9 001,986,894:0
            lis = new List<string>();
            lis.Clear();
            lis.Add("014");
            lis.Add("128");
            lis.Add("129");
            ZiDian.Add("4", lis);
            #endregion
            #region 5
            //  015,112,456:5 016,198,199:6 017,298,268:7 018,368,485:8 019,698,789:9 001,986,894:0
            lis = new List<string>();
            lis.Clear();
            lis.Add("015");
            lis.Add("112");
            lis.Add("456");
            ZiDian.Add("5", lis);
            #endregion
            #region 6
            //  016,198,199:6 017,298,268:7 018,368,485:8 019,698,789:9 001,986,894:0
            lis = new List<string>();
            lis.Clear();
            lis.Add("016");
            lis.Add("198");
            lis.Add("199");
            ZiDian.Add("6", lis);
            #endregion
            #region 7
            // 017,298,268:7 018,368,485:8 019,698,789:9 001,986,894:0
            lis = new List<string>();
            lis.Clear();
            lis.Add("017");
            lis.Add("298");
            lis.Add("268");
            ZiDian.Add("7", lis);
            #endregion
            #region 8
            // 018,368,485:8 019,698,789:9 001,986,894:0
            lis = new List<string>();
            lis.Clear();
            lis.Add("018");
            lis.Add("368");
            lis.Add("485");
            ZiDian.Add("8", lis);
            #endregion
            #region 9
            // 019,698,789:9 001,986,894:0
            lis = new List<string>();
            lis.Clear();
            lis.Add("019");
            lis.Add("698");
            lis.Add("789");
            ZiDian.Add("9", lis);
            #endregion
            #region 0
            // 001,986,894:0
            lis = new List<string>();
            lis.Clear();
            lis.Add("001");
            lis.Add("986");
            lis.Add("894");
            ZiDian.Add("0", lis);
            #endregion
            #region ,
            //  * 021,323,325:, 022,326,324:A 023,332,333:B 024,327,328:C 025,342,340:D 026,341,343:E 027,344,346:F 028,347,348:G 030,350,351::
            lis = new List<string>();
            lis.Clear();
            lis.Add("021");
            lis.Add("323");
            lis.Add("325");
            ZiDian.Add(",", lis);
            #endregion
            #region A
            //   022,326,324:A 023,332,333:B 024,327,328:C 025,342,340:D 026,341,343:E 027,344,346:F 028,347,348:G 030,350,351::
            lis = new List<string>();
            lis.Clear();
            lis.Add("022");
            lis.Add("326");
            lis.Add("324");
            ZiDian.Add("A", lis);
            #endregion
            #region B
            // 023,332,333:B 024,327,328:C 025,342,340:D 026,341,343:E 027,344,346:F 028,347,348:G 030,350,351::
            lis = new List<string>();
            lis.Clear();
            lis.Add("023");
            lis.Add("332");
            lis.Add("333");
            ZiDian.Add("B", lis);
            #endregion
            #region C
            //  024,327,328:C 025,342,340:D 026,341,343:E 027,344,346:F 028,347,348:G 030,350,351::
            lis = new List<string>();
            lis.Clear();
            lis.Add("024");
            lis.Add("327");
            lis.Add("328");
            ZiDian.Add("C", lis);
            #endregion
            #region D
            //   025,342,340:D 026,341,343:E 027,344,346:F 028,347,348:G 030,350,351::
            lis = new List<string>();
            lis.Clear();
            lis.Add("025");
            lis.Add("342");
            lis.Add("340");
            ZiDian.Add("D", lis);
            #endregion
            #region E
            //  026,341,343:E 027,344,346:F 028,347,348:G 030,350,351::
            lis = new List<string>();
            lis.Clear();
            lis.Add("026");
            lis.Add("341");
            lis.Add("343");
            ZiDian.Add("E", lis);
            #endregion
            #region F
            //  027,344,346:F 028,347,348:G 030,350,351::
            lis = new List<string>();
            lis.Clear();
            lis.Add("027");
            lis.Add("344");
            lis.Add("346");
            ZiDian.Add("F", lis);
            #endregion
            #region G
            //  028,347,348:G 030,350,351::
            lis = new List<string>();
            lis.Clear();
            lis.Add("028");
            lis.Add("347");
            lis.Add("348");
            ZiDian.Add("G", lis);
            #endregion
            #region :
            // 030,350,351::
            lis = new List<string>();
            lis.Clear();
            lis.Add("030");
            lis.Add("350");
            lis.Add("351");
            ZiDian.Add(":", lis);
            #endregion
            #region a
            //   032,711,721:b 033,722,731:c 034,712,713:d 035,715,716:e 036,718,724:f 037,725,728 g
            lis = new List<string>();
            lis.Clear();
            lis.Add("031");
            lis.Add("710");
            lis.Add("720");
            ZiDian.Add("a", lis);
            #endregion
            #region b
            //   032,711,721:b 033,722,731:c 034,712,713:d 035,715,716:e 036,718,724:f 037,725,728 g
            lis = new List<string>();
            lis.Clear();
            lis.Add("032");
            lis.Add("711");
            lis.Add("721");
            ZiDian.Add("b", lis);
            #endregion
            #region c
            //  033,722,731:c 034,712,713:d 035,715,716:e 036,718,724:f 037,725,728 g
            lis = new List<string>();
            lis.Clear();
            lis.Add("033");
            lis.Add("722");
            lis.Add("731");
            ZiDian.Add("c", lis);
            #endregion
            #region d
            //   034,712,713:d 035,715,716:e 036,718,724:f 037,725,728 g
            lis = new List<string>();
            lis.Clear();
            lis.Add("034");
            lis.Add("712");
            lis.Add("713");
            ZiDian.Add("d", lis);
            #endregion
            #region e
            //   035,715,716:e 036,718,724:f 037,725,728 g
            lis = new List<string>();
            lis.Clear();
            lis.Add("035");
            lis.Add("715");
            lis.Add("716");
            ZiDian.Add("e", lis);
            #endregion
            #region f
            //   036,718,724:f 037,725,728 g
            lis = new List<string>();
            lis.Clear();
            lis.Add("036");
            lis.Add("718");
            lis.Add("724");
            ZiDian.Add("f", lis);
            #endregion
            #region g
            //    037,725,728 g
            lis = new List<string>();
            lis.Clear();
            lis.Add("037");
            lis.Add("725");
            lis.Add("728");
            ZiDian.Add("g", lis);
            #endregion
            #region .
            // * 041,072,071:. 042,073,074:? 043,076,078:q 044,077,075:y 045,079,070:t 046,091,092:u 047,093,094:i 048,095,096:p 049,097,099:s
            lis = new List<string>();
            lis.Clear();
            lis.Add("041");
            lis.Add("072");
            lis.Add("071");
            ZiDian.Add(".", lis);
            #endregion
            #region ?
            //  042,073,074:? 043,076,078:q 044,077,075:y 045,079,070:t 046,091,092:u 047,093,094:i 048,095,096:p 049,097,099:s
            lis = new List<string>();
            lis.Clear();
            lis.Add("042");
            lis.Add("073");
            lis.Add("074");
            ZiDian.Add("?", lis);
            #endregion
            #region q
            //  043,076,078:q 044,077,075:y 045,079,070:t 046,091,092:u 047,093,094:i 048,095,096:p 049,097,099:s
            lis = new List<string>();
            lis.Clear();
            lis.Add("043");
            lis.Add("076");
            lis.Add("078");
            ZiDian.Add("q", lis);
            #endregion
            #region y
            //  044,077,075:y 045,079,070:t 046,091,092:u 047,093,094:i 048,095,096:p 049,097,099:s
            lis = new List<string>();
            lis.Clear();
            lis.Add("044");
            lis.Add("075");
            lis.Add("077");
            ZiDian.Add("y", lis);
            #endregion
            #region t
            //   045,079,070:t 046,091,092:u 047,093,094:i 048,095,096:p 049,097,099:s
            lis = new List<string>();
            lis.Clear();
            lis.Add("045");
            lis.Add("070");
            lis.Add("079");
            ZiDian.Add("t", lis);
            #endregion
            #region u
            //   046,091,092:u 047,093,094:i 048,095,096:p 049,097,099:s
            lis = new List<string>();
            lis.Clear();
            lis.Add("046");
            lis.Add("091");
            lis.Add("092");
            ZiDian.Add("u", lis);
            #endregion
            #region i
            //  047,093,094:i 048,095,096:p 049,097,099:s
            lis = new List<string>();
            lis.Clear();
            lis.Add("047");
            lis.Add("093");
            lis.Add("094");
            ZiDian.Add("i", lis);
            #endregion
            #region p
            //   048,095,096:p 049,097,099:s
            lis = new List<string>();
            lis.Clear();
            lis.Add("048");
            lis.Add("095");
            lis.Add("096");
            ZiDian.Add("p", lis);
            #endregion
            #region s
            //  049,097,099:s
            lis = new List<string>();
            lis.Clear();
            lis.Add("049");
            lis.Add("097");
            lis.Add("099");
            ZiDian.Add("s", lis);
            #endregion
            #region h
            //   * 051,140,141:h   052,142,145:j   053,146,143:l 054,146,147:z 055,147,149:x  056,148,187:v 057,188,185:n 058,183,184:m 059,210,213:r 060,213,215:o 061,218,219:w 062,217,212:k
            lis = new List<string>();
            lis.Clear();
            lis.Add("051");
            lis.Add("140");
            lis.Add("141");
            ZiDian.Add("h", lis);
            #endregion
            #region j
            //    052,142,145:j   053,146,143:l 054,146,147:z 055,147,149:x  056,148,187:v 057,188,185:n 058,183,184:m 059,210,213:r 060,213,215:o 061,218,219:w 062,217,212:k
            lis = new List<string>();
            lis.Clear();
            lis.Add("052");
            lis.Add("142");
            lis.Add("145");
            ZiDian.Add("j", lis);
            #endregion
            #region l
            //     053,146,143:l 054,146,147:z 055,147,149:x  056,148,187:v 057,188,185:n 058,183,184:m 059,210,213:r 060,213,215:o 061,218,219:w 062,217,212:k
            lis = new List<string>();
            lis.Clear();
            lis.Add("053");
            lis.Add("146");
            lis.Add("143");
            ZiDian.Add("l", lis);
            #endregion
            #region z
            //  054,218,219:z 055,226,227:x  056,148,187:v 057,188,185:n 058,183,184:m 059,210,213:r 060,213,215:o 061,218,219:w 062,217,212:k
            lis = new List<string>();
            lis.Clear();
            lis.Add("054");
            lis.Add("218");
            lis.Add("219");
            ZiDian.Add("z", lis);
            #endregion
            #region x
            //  055,226,227:x  056,148,187:v 057,188,185:n 058,183,184:m 059,210,213:r 060,213,215:o 061,218,219:w 062,217,212:k
            lis = new List<string>();
            lis.Clear();
            lis.Add("055");
            lis.Add("226");
            lis.Add("227");
            ZiDian.Add("x", lis);
            #endregion
            #region v
            // 056,148,144:v 057,188,185:n 058,183,184:m 059,210,213:r 060,213,215:o 061,218,219:w 062,217,212:k
            lis = new List<string>();
            lis.Clear();
            lis.Add("056");
            lis.Add("148");
            lis.Add("144");
            ZiDian.Add("v", lis);
            #endregion
            #region n
            //  057,188,185:n 058,183,184:m 059,210,213:r 060,213,215:o 061,218,219:w 062,217,212:k
            lis = new List<string>();
            lis.Clear();
            lis.Add("057");
            lis.Add("188");
            lis.Add("185");
            ZiDian.Add("n", lis);
            #endregion
            #region m
            // 058,183,184:m 059,210,213:r 060,213,215:o 061,218,219:w 062,217,212:k
            lis = new List<string>();
            lis.Clear();
            lis.Add("058");
            lis.Add("183");
            lis.Add("184");
            ZiDian.Add("m", lis);
            #endregion
            #region r
            // 059,210,213:r 060,213,215:o 061,218,219:w 062,217,212:k
            lis = new List<string>();
            lis.Clear();
            lis.Add("059");
            lis.Add("210");
            lis.Add("213");
            ZiDian.Add("r", lis);
            #endregion
            #region o
            // 060,252,215:o 061,228,229:w 062,253,212:k
            lis = new List<string>();
            lis.Clear();
            lis.Add("060");
            lis.Add("252");
            lis.Add("215");
            ZiDian.Add("o", lis);
            #endregion
            #region w
            //061,228,229:w 062,217,212:k
            lis = new List<string>();
            lis.Clear();
            lis.Add("061");
            lis.Add("228");
            lis.Add("229");
            ZiDian.Add("w", lis);
            #endregion
            #region k
            // 062,253,212:k
            lis = new List<string>();
            lis.Clear();
            lis.Add("062");
            lis.Add("253");
            lis.Add("212");
            ZiDian.Add("k", lis);
            #endregion
            #region Q
            // * 151,158,467:Q  152,462,461:Y 153,460,463:T 154,464,465:U 155,467,469:I 156,470,471:P 157,476,479:S
            lis = new List<string>();
            lis.Clear();
            lis.Add("151");
            lis.Add("158");
            lis.Add("467");
            ZiDian.Add("Q", lis);
            #endregion
            #region Y
            // 152,462,461:Y 153,460,463:T 154,464,465:U 155,467,469:I 156,470,471:P 157,476,479:S
            lis = new List<string>();
            lis.Clear();
            lis.Add("152");
            lis.Add("462");
            lis.Add("461");
            ZiDian.Add("Y", lis);
            #endregion
            #region T
            // 153,460,463:T 154,464,465:U 155,467,469:I 156,470,471:P 157,476,479:S
            lis = new List<string>();
            lis.Clear();
            lis.Add("153");
            lis.Add("460");
            lis.Add("463");
            ZiDian.Add("T", lis);
            #endregion
            #region U
            //  154,464,478:U 155,467,469:I 156,470,471:P 157,476,479:S
            lis = new List<string>();
            lis.Clear();
            lis.Add("154");
            lis.Add("464");
            lis.Add("478");
            ZiDian.Add("U", lis);
            #endregion
            #region I
            //155,477,469:I 156,470,471:P 157,476,479:S
            lis = new List<string>();
            lis.Clear();
            lis.Add("155");
            lis.Add("477");
            lis.Add("469");
            ZiDian.Add("I", lis);
            #endregion
            #region P
            // 156,470,471:P 157,476,479:S
            lis = new List<string>();
            lis.Clear();
            lis.Add("156");
            lis.Add("470");
            lis.Add("471");
            ZiDian.Add("P", lis);
            #endregion
            #region S
            // 157,476,479:S
            lis = new List<string>();
            lis.Clear();
            lis.Add("476");
            lis.Add("157");
            lis.Add("479");
            ZiDian.Add("S", lis);
            #endregion
            #region H
            // * 161,510,511:H  162,512,513:J 163,514,515:L 164,516,517:Z 165,518,519:X 166,520,521:V 167,522,523:N 168,524,525:M 169,526,527:R 170,528,529:O 171,530,531:W 172,532,533:K
            lis = new List<string>();
            lis.Clear();
            lis.Add("161");
            lis.Add("510");
            lis.Add("511");
            ZiDian.Add("H", lis);
            #endregion
            #region J
            //  162,512,513:J 163,514,515:L 164,516,517:Z 165,518,519:X 166,520,521:V 167,522,523:N 168,524,525:M 169,526,527:R 170,528,529:O 171,530,531:W 172,532,533:K
            lis = new List<string>();
            lis.Clear();
            lis.Add("162");
            lis.Add("512");
            lis.Add("513");
            ZiDian.Add("J", lis);
            #endregion
            #region L
            //  163,514,515:L 164,516,517:Z 165,518,519:X 166,520,521:V 167,522,523:N 168,524,525:M 169,526,527:R 170,528,529:O 171,530,531:W 172,532,533:K
            lis = new List<string>();
            lis.Clear();
            lis.Add("163");
            lis.Add("514");
            lis.Add("515");
            ZiDian.Add("L", lis);
            #endregion
            #region Z
            //  164,516,517:Z 165,518,519:X 166,520,521:V 167,522,523:N 168,524,525:M 169,526,527:R 170,528,529:O 171,530,531:W 172,532,533:K
            lis = new List<string>();
            lis.Clear();
            lis.Add("164");
            lis.Add("516");
            lis.Add("517");
            ZiDian.Add("Z", lis);
            #endregion
            #region X
            // 165,518,519:X 166,520,521:V 167,522,523:N 168,524,525:M 169,526,527:R 170,528,529:O 171,530,531:W 172,532,533:K
            lis = new List<string>();
            lis.Clear();
            lis.Add("165");
            lis.Add("518");
            lis.Add("519");
            ZiDian.Add("X", lis);
            #endregion
            #region V
            // 166,520,521:V 167,522,523:N 168,524,525:M 169,526,527:R 170,528,529:O 171,530,531:W 172,532,533:K
            lis = new List<string>();
            lis.Clear();
            lis.Add("166");
            lis.Add("520");
            lis.Add("521");
            ZiDian.Add("V", lis);
            #endregion
            #region N
            //  167,522,523:N 168,524,525:M 169,526,527:R 170,528,529:O 171,530,531:W 172,532,533:K
            lis = new List<string>();
            lis.Clear();
            lis.Add("167");
            lis.Add("522");
            lis.Add("523");
            ZiDian.Add("N", lis);
            #endregion
            #region M
            //  168,524,525:M 169,526,527:R 170,528,529:O 171,530,531:W 172,532,533:K
            lis = new List<string>();
            lis.Clear();
            lis.Add("168");
            lis.Add("524");
            lis.Add("525");
            ZiDian.Add("M", lis);
            #endregion
            #region R
            // 169,526,527:R 170,528,529:O 171,530,531:W 172,532,533:K
            lis = new List<string>();
            lis.Clear();
            lis.Add("169");
            lis.Add("526");
            lis.Add("527");
            ZiDian.Add("R", lis);
            #endregion
            #region O
            // 170,528,529:O 171,530,531:W 172,532,533:K
            lis = new List<string>();
            lis.Clear();
            lis.Add("170");
            lis.Add("528");
            lis.Add("529");
            ZiDian.Add("O", lis);
            #endregion
            #region W
            // 171,530,531:W 172,532,533:K
            lis = new List<string>();
            lis.Clear();
            lis.Add("171");
            lis.Add("530");
            lis.Add("531");
            ZiDian.Add("W", lis);
            #endregion
            #region K
            //172,532,533:K
            lis = new List<string>();
            lis.Clear();
            lis.Add("172");
            lis.Add("532");
            lis.Add("533");
            ZiDian.Add("K", lis);
            #endregion
            #region +
            // * 081,540,541:+ 082,542,543:= 083,544,545:- 084,546,547:# 085,548,549:$ 086,550,551:% 087,552,553:^ 088,554,555:& 089,556,557:*
            lis = new List<string>();
            lis.Clear();
            lis.Add("081");
            lis.Add("540");
            lis.Add("541");
            ZiDian.Add("+", lis);
            #endregion
            #region =
            // 082,542,543:= 083,544,545:- 084,546,547:# 085,548,549:$ 086,550,551:% 087,552,553:^ 088,554,555:& 089,556,557:*
            lis = new List<string>();
            lis.Clear();
            lis.Add("082");
            lis.Add("542");
            lis.Add("543");
            ZiDian.Add("=", lis);
            #endregion
            #region -
            // 083,544,545:- 084,546,547:# 085,548,549:$ 086,550,551:% 087,552,553:^ 088,554,555:& 089,556,557:*
            lis = new List<string>();
            lis.Clear();
            lis.Add("083");
            lis.Add("544");
            lis.Add("545");
            ZiDian.Add("-", lis);
            #endregion
            #region #
            //  084,546,547:# 085,548,549:$ 086,550,551:% 087,552,553:^ 088,554,555:& 089,556,557:*
            lis = new List<string>();
            lis.Clear();
            lis.Add("084");
            lis.Add("546");
            lis.Add("547");
            ZiDian.Add("#", lis);
            #endregion
            #region $
            // 085,548,549:$ 086,550,551:% 087,552,553:^ 088,554,555:& 089,556,557:*
            lis = new List<string>();
            lis.Clear();
            lis.Add("085");
            lis.Add("548");
            lis.Add("549");
            ZiDian.Add("$", lis);
            #endregion
            #region %
            // 086,550,551:% 087,552,553:^ 088,554,555:& 089,556,557:*
            lis = new List<string>();
            lis.Clear();
            lis.Add("086");
            lis.Add("550");
            lis.Add("551");
            ZiDian.Add("%", lis);
            #endregion
            #region ^
            //  087,552,553:^ 088,554,555:& 089,556,557:*
            lis = new List<string>();
            lis.Clear();
            lis.Add("087");
            lis.Add("552");
            lis.Add("553");
            ZiDian.Add("^", lis);
            #endregion
            #region &
            //   088,554,555:& 089,556,557:*
            lis = new List<string>();
            lis.Clear();
            lis.Add("088");
            lis.Add("554");
            lis.Add("555");
            ZiDian.Add("&", lis);
            #endregion
            #region *
            //   089,556,557:*
            lis = new List<string>();
            lis.Clear();
            lis.Add("089");
            lis.Add("556");
            lis.Add("557");
            ZiDian.Add("*", lis);
            #endregion
            #region <
            // * 598,558,559:<   597,570,571:>
            lis = new List<string>();
            lis.Clear();
            lis.Add("598");
            lis.Add("558");
            lis.Add("559");
            ZiDian.Add("<", lis);
            #endregion
            #region >
            //  597,570,571:>
            lis = new List<string>();
            lis.Clear();
            lis.Add("597");
            lis.Add("570");
            lis.Add("571");
            ZiDian.Add(">", lis);
            #endregion
            #region " "
            //  596,572,573:" "
            lis = new List<string>();
            lis.Clear();
            lis.Add("596");
            lis.Add("572");
            lis.Add("573");
            ZiDian.Add(" ", lis);
            #endregion
        }

        private string FanHui(string canshu)
        {
            // 011,125,132:1  012,124,138:2 013,123,230:3 014,128,129:4 015,112,456:5 016,198,199:6 017,298,268:7 018,368,485:8 019,698,789:9 001,986,894:0
            string s = "";
            List<string> lis = new List<string>();
            lis.Clear();
            #region 加密的取值
            if (canshu.Equals("1"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("2"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("3"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("4"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("5"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("6"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("7"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("8"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("9"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("0"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals(","))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("A"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("B"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("C"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("D"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("E"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);

            }
            else if (canshu.Equals("F"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("G"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals(":"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("a"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("b"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("c"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("d"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("e"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("f"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("g"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("."))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("?"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("q"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("y"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("t"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("u"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("i"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("p"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("s"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("Q"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("Y"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("T"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }

            else if (canshu.Equals("U"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("I"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("P"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("S"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("h"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("j"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("l"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("z"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("v"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("n"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }

            else if (canshu.Equals("n"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("m"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("r"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("o"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("w"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("k"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }

            else if (canshu.Equals("H"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("J"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("L"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("Z"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("X"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("V"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }

            else if (canshu.Equals("N"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("M"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("R"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("O"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("W"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("K"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }

            else if (canshu.Equals("+"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("="))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("-"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }

            else if (canshu.Equals("#"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("$"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("%"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("^"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("&"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("*"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals("<"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals(">"))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            else if (canshu.Equals(" "))
            {
                lis = ZiDian[canshu];
                s = QuSuiJiShu(lis);
            }
            // 081:+ 082:= 083:- 084:# 085:$ 086:% 087:^ 088:& 089:* 
            #endregion

            return s;
        }


        /// <summary>
        /// 加密校验
        /// </summary>
        /// <param name="canshu"></param>
        /// <returns></returns>
        private bool jiaoyan(string canshu)
        {
            bool zhen = false;
            int changdu = canshu.Length;

            for (int i = 0; i < changdu; i++)
            {
                if (string.IsNullOrEmpty(canshu[i].ToString()))
                {
                    zhen = true;
                    break;
                }
            }
            return zhen;
        }

        private bool jiemijiaoyan(string canshu)
        {
            bool zhen = false;
            int changdu = canshu.Length;
            if (changdu % 3 == 0)
            {
                for (int i = 0; i < changdu; i += 3)
                {
                    string s = string.Format(@"{0}{1}{2}", canshu[i], canshu[i + 1], canshu[i + 2]);
                    try
                    {
                        if (string.IsNullOrEmpty(ZiDianFanZhun[s]))
                        {
                            zhen = true;
                            break;
                        }
                    }
                    catch
                    {
                        zhen = true;
                        break;
                    }
                }
            }
            else
            {
                zhen = true;
            }

            return zhen;
        }

        /// <summary>
        /// 加密运算
        /// </summary>
        /// <param name="canshu">传来的字符串</param>
        /// <returns>传出的加密字符串</returns>
        public string JiaMi(string canshu)
        {
            StringBuilder sb = new StringBuilder();

            if (!jiaoyan(canshu))
            {
                for (int i = 0; i < canshu.Length; i++)
                {
                    sb.Append(FanHui(canshu[i].ToString()));

                }
            }

            return sb.ToString();
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="canshu">传来的字符串</param>
        /// <returns>传出的解密字符串</returns>
        public string JieMi(string canshu)
        {
            StringBuilder sb = new StringBuilder();

            if (!jiemijiaoyan(canshu))
            {
                for (int i = 0; i < canshu.Length; i += 3)
                {
                    string jimi = string.Format(@"{0}{1}{2}", canshu[i], canshu[i + 1], canshu[i + 2]);
                    sb.Append(ZiDianFanZhun[jimi]);
                }
            }

            return sb.ToString();
        }
    }
}
