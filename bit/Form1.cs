using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bit.Model;
using BitMEX;
using Newtonsoft.Json;




namespace bit
{
    public partial class Form1 : Form
    {
        
        private static string bitmexKey = "I1mAR6Kn0HxzW6uRZS4pSFwq";
        private static string bitmexSecret = "Fmv_Kvyq663upBdsyOwvlt7Mmo1KlvbH7sG5HlV2s9Gv8AMk";
        BitMEXApi bitmex = new BitMEXApi(bitmexKey, bitmexSecret);

        bitemex_position bitemex_position = new bitemex_position();
        List<bitmex_order> bitmex_orders = new List<bitmex_order>();
        List<bitmex_bucketed> subBucketeds = new List<bitmex_bucketed>();

        Timer timer1 = new Timer();
        int loop_no = 0;

        Boolean ishide = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object Sender, EventArgs e)
        {
            if (!ishide)
            {
                if (loop_no % 10 == 0 || true)
                {
                    txt_position.Text = "";
                    txtBox.Text = "";
                    txt_order.Text = "";
                }
                txt_position.AppendText("------------" + (loop_no + 1) + " 회 포지션 확인 ------------- \r\n");
                txtBox.AppendText("\r\n----------" + (loop_no + 1) + "  회 봉 확인 --------------- \r\n");
                txt_order.AppendText("\r\n----------" + (loop_no + 1) + "  회 주문내역 확인 --------------- \r\n");
            }

            Get_Orders();
            GetPositions();
            bitmex_Get_bucketed_h();
            order_System_h();
            loop_no++;
        }

        private void btn_Start_Click_1(object sender, EventArgs e)
        {
            if (!ishide)
            {
                if (loop_no % 10 == 0 || true)
                {
                    txt_position.Text = "";
                    txtBox.Text = "";
                    txt_order.Text = "";
                }
                txt_position.AppendText("------------" + (loop_no + 1) + " 회 포지션 확인 ------------- \r\n");
                txtBox.AppendText("\r\n----------" + (loop_no + 1) + "  회 봉 확인 --------------- \r\n");
                txt_order.AppendText("\r\n----------" + (loop_no + 1) + "  회 주문내역 확인 --------------- \r\n");
            }

            Get_Orders();
            GetPositions();
            bitmex_Get_bucketed_h();
            order_System_h();
            loop_no++;

            // timer1의 속성 정의 – 코드에서 Timer 생성
            timer1.Enabled = true;
            timer1.Interval = 20 * 1000;
            timer1.Tick += Timer1_Tick;
        }

        private void Get_Orders()
        {
            var json_result = bitmex.GetOrders("XBTUSD", 10, true);
            
            bitmex_orders = JsonConvert.DeserializeObject<List<bitmex_order>>(json_result);

            if (!ishide)
            {
                if (bitmex_orders.Count() == 0)
                {
                    txt_order.AppendText("주문 내역이 없습니다.");
                }
                foreach (var item in bitmex_orders)
                {
                    txt_order.AppendText("주문 : " + item.side + " ");
                    txt_order.AppendText("" + item.price + " * ");
                    txt_order.AppendText("" + item.orderQty + " ");
                    txt_order.AppendText("(" + item.ordStatus + ")\r\n");
                }
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            txtBox.AppendText("\r\n타이머가 중지 되었습니다.");
            txt_position.AppendText("\r\n타이머가 중지 되었습니다.");
            loop_no = 0;
        }

        private void GetPositions()
        {
            var json_result = bitmex.GetPositions();
            List<bitemex_position> _bitemex_positions = new List<bitemex_position>();
            _bitemex_positions = JsonConvert.DeserializeObject<List<bitemex_position>>(json_result);

            if (_bitemex_positions.Count == 1)
            {
                // 포지션은 무조건 1개 밖에 없을 것임....

                if (_bitemex_positions[0].currentQty == 0)
                {
                    if (!ishide)
                    {
                        txt_position.AppendText("포지션 없음.\r\n");
                    }
                    bitemex_position.account = 0;
                    bitemex_position.currentQty = 0;
                    bitemex_position.avgCostPrice = 0;
                }
                else
                {
                    if (!ishide)
                    {
                        txt_position.AppendText("포지션 계정 :" + _bitemex_positions[0].account + "\r\n");
                        txt_position.AppendText("symbol : " + _bitemex_positions[0].symbol + "\r\n");
                        txt_position.AppendText("avgCostPrice : " + _bitemex_positions[0].avgCostPrice + "\r\n");
                        txt_position.AppendText("currentQty : " + _bitemex_positions[0].currentQty + "\r\n");
                    }
                    bitemex_position.account = _bitemex_positions[0].account;
                    bitemex_position.currentQty = _bitemex_positions[0].currentQty;
                    bitemex_position.avgCostPrice = _bitemex_positions[0].avgCostPrice;
                }
            }
            else
            {
                txt_position.AppendText("포지션이 0개이거나 2개이상임. 확인해볼것~.\r\n");
                // 0개 혹은 2개도 있나 확인해보자.... 아마 없을것임...
            }
        }

        /// <summary>
        /// 분봉
        /// </summary>
        private void bitmex_Get_bucketed_Min()
        {
            /// [1] 봉 얻어오기
            var result_bucketed = bitmex.bitmex_Get_bucketed("1m", true, "XBTUSD", 2, true);
            List<bitmex_bucketed> bucketeds = new List<bitmex_bucketed>();
            bucketeds = JsonConvert.DeserializeObject<List<bitmex_bucketed>>(result_bucketed);
            
            subBucketeds = subbucketeds(bucketeds, 2, true);

            int j = 0;
            foreach (var item in subBucketeds)
            {
                txtBox.AppendText((j+1) + " 번째 분 봉\r\n");
                txtBox.AppendText("open : " + item.open + "\r\n");
                txtBox.AppendText("high : " + item.high + "\r\n");
                txtBox.AppendText("low : " + item.low + "\r\n");
                txtBox.AppendText("close : " + item.close + "\r\n");
                //txtBox.AppendText("trades : " + item.trades + "\r\n");
                //txtBox.AppendText("volume(거래량?) : " + item.volume + "\r\n");
                //txtBox.AppendText("vwap : " + item.vwap + "\r\n");
                //txtBox.AppendText("lastSize : " + item.lastSize + "\r\n");
                //txtBox.AppendText("turnover : " + item.turnover + "\r\n");
                //txtBox.AppendText("homeNotional : " + item.homeNotional + "\r\n");
                //txtBox.AppendText("foreignNotional : " + item.foreignNotional + "\r\n");
                j++;
            }

            
        }

        private void order_System_min()
        {


            double high = subBucketeds[0].high;
            double low = subBucketeds[0].low;
            double open = subBucketeds[0].open;
            double close = subBucketeds[0].close;


            int step1_Qty = 200;
            int step2_Qty = 600;
            double step1_spring = 9.0;
            double step2_spring = 42.0;
            double step1_margin = 10.0;
            double step2_margin = 80.0;


            ///
            ///  아래주문에서 30개 주문을 넣었는데 10개만 체결시 오류 생김!!!!!!!!

            if (bitemex_position.currentQty == 0)  // 1단계
            {
                double Sellprice = subBucketeds[0].open + step1_spring;
                double Buyprice = subBucketeds[0].open - step1_spring;

                Boolean ordercheck = false;
                // [1] 조건의 주문이 아니면 주문 취소
                foreach (var item in bitmex_orders)
                {
                    if (item.price != Sellprice && item.price != Buyprice)
                    {
                        ordercheck = true;
                        string deleteAll = bitmex.DeleteAllOrders();
                    }

                }
                if (ordercheck)
                {
                    string deleteAll = bitmex.DeleteAllOrders();
                }

                //[2]
                if (bitmex_orders.Count() == 0 || ordercheck)
                {
                    // [1단계] open에서 +- 18 매수/매매 주문
                    
                    string result_order1 = bitmex.PostOrders("XBTUSD", "Sell", step1_Qty, Sellprice, "Limit");
                    if (true)
                    {
                        //주문 성공 혹은 실패 알려주기
                    }
                    string result_order2 = bitmex.PostOrders("XBTUSD", "Buy", step1_Qty, Buyprice, "Limit");
                    if (true)
                    {
                        //주문 성공 혹은 실패 알려주기
                    }
                }
                
            }
            else if(bitemex_position.currentQty <= step1_Qty) // 2단계
            {

                Boolean ordercheck = false;

                if (Math.Abs(bitemex_position.currentQty) < step1_Qty)
                {
                    // 극단적인 경우로서 미체결 물량이 있을경우임.. 가능성 희박..
                    // [1] 전량 매도 처리 하자..
                    // [2] 주문 전부 취소하자.
                    string deleteall_result = bitmex.DeleteAllOrders();
                    ordercheck = true;
                }

                if (bitmex_orders.Count() != 2)  //1단계 주문이 있는지 확인 : 먼가 깔끔하지 않음..
                {
                    // 1단계 주문 취소
                    string result = bitmex.DeleteAllOrders();
                    // 단계취소하면서 바로 주문을 넣어야하는데 로직 잘못됨..
                    // 2단계는 spring 크게 둘꺼니깐 일단 이렇게 하자.

                    ordercheck = true;
                }



                if (bitmex_orders.Count() == 0 || ordercheck)
                {

                    // [2단계] bitemex_position.currentCost  -50 매도 주문
                    double Sellprice = bitemex_position.currentQty > 0 ? (double)bitemex_position.avgCostPrice + step1_margin : (double)bitemex_position.avgCostPrice + step2_spring;

                    double Buyprice = bitemex_position.currentQty > 0 ? (double)bitemex_position.avgCostPrice - step2_spring : (double)bitemex_position.avgCostPrice - step1_margin;

                    if (bitemex_position.currentQty > 0)
                    {
                        string result_order1 = bitmex.PostOrders("XBTUSD", "Sell", step1_Qty, Sellprice, "Limit");
                        if (true)
                        {
                            //주문 성공 혹은 실패 알려주기
                        }
                        string result_order2 = bitmex.PostOrders("XBTUSD", "Buy", step2_Qty, Buyprice, "Limit");
                        if (true)
                        {
                            //주문 성공 혹은 실패 알려주기
                        }
                    }
                    else
                    {
                        string result_order1 = bitmex.PostOrders("XBTUSD", "Sell", step2_Qty, Sellprice, "Limit");
                        if (true)
                        {
                            //주문 성공 혹은 실패 알려주기
                        }
                        string result_order2 = bitmex.PostOrders("XBTUSD", "Buy", step1_Qty, Buyprice, "Limit");
                        if (true)
                        {
                            //주문 성공 혹은 실패 알려주기
                        }
                    }
                }


            }
            else if (bitemex_position.currentQty == step1_Qty + step2_Qty) // 3단계
            {
                // 2 단계 프로세스와 거의 같을듯.


                if (bitemex_position.currentQty > 0)
                {
                    string result_order1 = bitmex.PostOrders("XBTUSD", "Sell", step1_Qty, (double)bitemex_position.avgCostPrice + step2_margin, "Limit");
                    if (true)
                    {
                        //주문 성공 혹은 실패 알려주기
                    }
                }
                else
                {
                    
                    string result_order2 = bitmex.PostOrders("XBTUSD", "Buy", step1_Qty, (double)bitemex_position.avgCostPrice - step2_margin, "Limit");
                    if (true)
                    {
                        //주문 성공 혹은 실패 알려주기
                    }
                }

            }


        }

        /// <summary>
        /// 시간봉
        /// </summary>
        private void bitmex_Get_bucketed_h()
        {
            /// [1] 봉 얻어오기 : 합치기 할때..
            // var result_bucketed = bitmex.bitmex_Get_bucketed("1h", true, "XBTUSD", 6, true);
            // List<bitmex_bucketed> bucketeds = new List<bitmex_bucketed>();
            // bucketeds = JsonConvert.DeserializeObject<List<bitmex_bucketed>>(result_bucketed);
            //subBucketeds = subbucketeds(bucketeds, 1, true);


            /// [1] 봉 얻어오기 : 합치기 안할때
            var result_bucketed = bitmex.bitmex_Get_bucketed("1h", true, "XBTUSD", 6, true);
            subBucketeds = JsonConvert.DeserializeObject<List<bitmex_bucketed>>(result_bucketed);


            int j = 0;
            foreach (var item in subBucketeds)
            {
                if (!ishide)
                {
                    txtBox.AppendText((j + 1) + " 번째 봉\r\n");
                    txtBox.AppendText("open : " + item.open + "\r\n");
                    txtBox.AppendText("high : " + item.high + "\r\n");
                    txtBox.AppendText("low : " + item.low + "\r\n");
                    txtBox.AppendText("close : " + item.close + "\r\n");
                }
                j++;
            }
        }

        private void order_System_h()
        {
            if (subBucketeds.Count() == 6)
            {
                double gap4 = subBucketeds[4].open - subBucketeds[4].close;
                double gap3 = subBucketeds[3].open - subBucketeds[3].close;
                double gap2 = subBucketeds[2].open - subBucketeds[2].close;
                double gap1 = subBucketeds[1].open - subBucketeds[1].close;  //갭 작으면 실행..
                double gap0 = subBucketeds[0].open - subBucketeds[0].close;  //현재봉 : 쓸일 없음

                if (bitemex_position.currentQty != 0)
                {
                    if (bitmex_orders.Count() == 0)
                    {
                        if (bitemex_position.currentQty > 0)
                        {
                            bitmex.PostOrders("XBTUSD", "Sell", Math.Abs(bitemex_position.currentQty), Math.Round((double)bitemex_position.avgCostPrice, 1) + 120, "Limit");
                        }
                        else
                        {
                            bitmex.PostOrders("XBTUSD", "Buy", Math.Abs(bitemex_position.currentQty), Math.Round((double)bitemex_position.avgCostPrice, 1) - 120, "Limit");
                        }
                    }
                    
                }
                else
                {
                    if ((Math.Abs(gap2) >= 45 && Math.Abs(gap3) >= 45 && gap2 * gap3 > 0 && Math.Abs(gap2 + gap3) > 110) || (Math.Abs(gap2) > 110))
                    {
                        double chkway = gap2 * gap1;

                        if (chkway > 0 && Math.Abs(gap1) < 13.0 || chkway < 0 && Math.Abs(gap1) < 26.0)  // 방향이 같을 경우에는 갭 13미만... 갭 방향이 바뀌었으면 갭 25미만일 경우
                        {
                            if (gap2 > 0)
                            {
                                bitmex.DeleteAllOrders();
                                string result_order1 = bitmex.PostOrders("XBTUSD", "Sell", 1000, subBucketeds[0].close + 0.5, "Limit");
                            }
                            else
                            {
                                bitmex.DeleteAllOrders();
                                string result_order1 = bitmex.PostOrders("XBTUSD", "Buy", 1000, subBucketeds[0].close - 0.5, "Limit");
                            }
                        }
                    }
                }
                
            }
            else
            {
                txt_position.AppendText("주문시스템 확인 할것!!!!!!!!!!!!!");
            }
        }





        private List<bitmex_bucketed> subbucketeds(List<bitmex_bucketed> bucketeds , int size , Boolean partial)
        {
            var i = 0;
            var j = 0;
            List<bitmex_bucketed> subBucketeds = new List<bitmex_bucketed>();

            bitmex_bucketed bitmex_bucketed = new bitmex_bucketed();

            foreach (var item in bucketeds)
            {
                
                if (partial)
                {
                    if (j%size == 0)
                    {
                        bitmex_bucketed= new bitmex_bucketed();
                    }


                    if (i == 0)
                    {
                        bitmex_bucketed.high = item.high;
                        bitmex_bucketed.low = item.low;
                        bitmex_bucketed.open = item.open;
                        bitmex_bucketed.close = item.close;
                    }
                    else
                    {
                        bitmex_bucketed.high = bitmex_bucketed.high > item.high ? bitmex_bucketed.high  : item.high;
                        bitmex_bucketed.low = bitmex_bucketed.low < item.low ? bitmex_bucketed.low : item.low;
                        bitmex_bucketed.open = item.open;
                        //d.close = item.close;
                    }
                    int d = i % size;
                    if (i % size == (size-1) || j == bucketeds.Count() -1)
                    {
                        i = 0;
                        subBucketeds.Add(bitmex_bucketed);
                        subBucketeds.ToList();

                    }
                    else
                    {
                        i++;
                    }
                    j++;
                }

                
            }

            return subBucketeds;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_test1_Click(object sender, EventArgs e)
        {

            // Limit Stop  StopLimit  MarketIfTouched   LimitIfTouched
            string result_order1 = bitmex.PostOrders("XBTUSD", "Sell", 60, 7514.0, "Limit");
            txtBox.AppendText(result_order1 + " \r\n");
        }
    }
}
