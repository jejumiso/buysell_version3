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

        // jejuairfarm
        private static string bitmexKey = "I1mAR6Kn0HxzW6uRZS4pSFwq";
        private static string bitmexSecret = "Fmv_Kvyq663upBdsyOwvlt7Mmo1KlvbH7sG5HlV2s9Gv8AMk";
        //////hyunju3414764
        //private static string bitmexKey = "G58vcGUdSYs4Kc1CdaImJHrq";
        //private static string bitmexSecret = "lT25GUXQnn2_30i1mFsyNajQgQ7023p4XZO692YOpu2MDlMY";
        ////hyunjaeyoung3414
        //private static string bitmexKey = "MV60V1JtCsCZPRjQfh5uey8e";
        //private static string bitmexSecret = "meK3UuvpYKejEeluX73SgBvjqwK67tk6qLoVg3GMPRLE678i";

        // [2]
        BitMEXApi bitemex = new BitMEXApi(bitmexKey, bitmexSecret);
        bitemex_position bitemex_position = new bitemex_position();
        bitemex_position pre_bitemex_position = new bitemex_position();
        List<bitmex_order> bitmex_orders = new List<bitmex_order>();
        List<bitmex_bucketed> btmex_Bucketeds = new List<bitmex_bucketed>();
        // [2-1] 
        BitMex_ActionClass.BitMex_ActionClass bitmex_ActionClass = new BitMex_ActionClass.BitMex_ActionClass();

        // [3]
        Timer timer1 = new Timer();
        Timer timer2 = new Timer();
        

        // [4] 
        Boolean ishide = false;

        public Form1()
        {
            InitializeComponent();
        }
       

        /// <summary>
        /// 
        /// </summary>
        private void Timer1_Tick(object Sender, EventArgs e)
        {
            Auto_Trad_Play();
        }

        int second = -1;
        int timeloop = 3;  //고정값
        Boolean readPosition = false;
        string pre_timestamp = "";
        double limit_trad_price = 0.0;
        user_margin _user_margin = new user_margin();
        private void Auto_Trad_Play()
        {
            //[-]
            //txt_log2.AppendText(DateTime.Now.ToString("HH시mm분 ss초\r\n"));


            //[1] 
            if (second < 0 || second % 300 < timeloop)
            {
                //[1-1]   5분에 한번 update
                _user_margin = JsonConvert.DeserializeObject<user_margin>(bitemex.GetUserMargin());
            }

            //[2]
            if (second < 0 || second > 56)  //53초 이후부터는 timeloop초에 한번 주문 
            {
                //[2-1] 56초 이후 1회 실행
                if (readPosition == false)
                {
                    limit_trad_price = 7000 * (_user_margin.walletBalance * 0.00000001) * 8;
                    limit_trad_price = Math.Ceiling(limit_trad_price * 2)/2;
                    //[1] Positions
                    GetPositions();
                    readPosition = true;
                }
                
                //[2-2] 56초 이후 주문 들어갈때까지 계속 실행
                bitmex_Get_bucketed_2();
                if (pre_timestamp != btmex_Bucketeds[0].timestamp)
                {
                    bitmex_ActionClass.order_System2(limit_trad_price, btmex_Bucketeds, bitemex_position);
                    second = 0;
                    readPosition = false;
                    pre_timestamp = btmex_Bucketeds[0].timestamp;
                    
                }         
            }
            //[end]
            second = second + timeloop;
            //[-]
            //txt_log2.AppendText(DateTime.Now.ToString("HH시mm분 ss초\r\n"));

        }

        private void btn_Start_Click_1(object sender, EventArgs e)
        {
            btn_Start.Enabled = false;
            btn_Stop.Enabled = true;

            Auto_Trad_Play();

            // timer1의 속성 정의 – 코드에서 Timer 생성
            timer1.Enabled = true;
            timer1.Interval = timeloop * 1000;
            timer1.Tick += Timer1_Tick;

            // timer1의 속성 정의 – 코드에서 Timer 생성
            // 부하 안주기 위해서 테스트에서만 진행.
            //timer2.Enabled = true;
            //timer2.Interval = 1 * 1000;
            //timer2.Tick += Timer2_Tick;
        }

        private void bitmex_Get_bucketed_2()
        {
            try
            {
                /// [1] 봉 얻어오기
                var result_bucketed = bitemex.bitmex_Get_bucketed("1m", true, "XBTUSD", 3, true);
                List<bitmex_bucketed> bucketeds = new List<bitmex_bucketed>();
                btmex_Bucketeds = JsonConvert.DeserializeObject<List<bitmex_bucketed>>(result_bucketed);
            }
            catch (Exception ex)
            {
                txt_position.Text = "봉 불러오기 error";
                throw;
            }


            //if (!ishide)
            //{
            //    int j = 0;
            //    foreach (var item in btmex_Bucketeds)
            //    {
            //        txt_position.AppendText((j + 1) + " 번째 1분 봉\r\n");
            //        txt_position.AppendText("open : " + item.open + "\r\n");
            //        txt_position.AppendText("high : " + item.high + "\r\n");
            //        txt_position.AppendText("low : " + item.low + "\r\n");
            //        txt_position.AppendText("close : " + item.close + "\r\n");
            //        j++;
            //    }
            //}

        }





        private void Get_Orders()
        {
            try
            {
                var json_result = bitemex.GetOrders("XBTUSD", "{\"ordStatus\":\"New\"}",36, true,"");
                bitmex_orders = JsonConvert.DeserializeObject<List<bitmex_order>>(json_result);

                //if (!ishide)
                //{
                //    if (bitmex_orders.Count() == 0)
                //    {
                //        txt_position.AppendText("주문 내역이 없습니다.");
                //    }
                //    foreach (var item in bitmex_orders)
                //    {
                //        txt_position.AppendText("주문 : " + item.side + " ");
                //        txt_position.AppendText("" + item.price + " * ");
                //        txt_position.AppendText("" + item.orderQty + " ");
                //        txt_position.AppendText("(" + item.ordStatus + ")\r\n");
                //    }
                //}
            }
            catch (Exception ex)
            {
                txt_position.Text = "주문 불러오기 에러.";
                throw;
            }


        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            btn_Start.Enabled = true;
            btn_Stop.Enabled = false;
            timer1.Enabled = false;
            txt_position.AppendText("\r\n타이머가 중지 되었습니다.");
            txt_position.AppendText("\r\n타이머가 중지 되었습니다.");
        }
        private void btn_Stop2_Click(object sender, EventArgs e)
        {
            btn_Start.Enabled = false;
        }

        private void GetPositions()
        {
            var json_result = bitemex.GetPositions("{ \"symbol\" : \"XBTUSD\" }");
            List<bitemex_position> _bitemex_positions = new List<bitemex_position>();
            _bitemex_positions = JsonConvert.DeserializeObject<List<bitemex_position>>(json_result);

            if (_bitemex_positions.Count == 1)
            {
                // 포지션은 무조건 1개 밖에 없을 것임....
                if (_bitemex_positions[0].currentQty == 0)
                {
                    bitemex_position.account = 0;
                    bitemex_position.symbol = "";
                    bitemex_position.currentQty = 0;
                    bitemex_position.avgCostPrice = 0.0;
                    bitemex_position.marginCallPrice = 0.0;
                    bitemex_position.liquidationPrice = 0.0;
                    //if (!ishide)
                    //{
                    //    txt_position.AppendText("포지션 없음.\r\n");
                    //}

                }
                else
                {
                    bitemex_position.account = _bitemex_positions[0].account;
                    bitemex_position.symbol = _bitemex_positions[0].symbol;
                    bitemex_position.currentQty = _bitemex_positions[0].currentQty;
                    bitemex_position.avgCostPrice = _bitemex_positions[0].avgCostPrice;
                    bitemex_position.marginCallPrice = _bitemex_positions[0].marginCallPrice;
                    bitemex_position.liquidationPrice = _bitemex_positions[0].liquidationPrice;
                    //if (!ishide)
                    //{
                    //    txt_position.AppendText("포지션 : " + bitemex_position.avgCostPrice + " * "+ bitemex_position.currentQty + "\r\n");
                    //    txt_position.AppendText("(청산: " + bitemex_position.marginCallPrice + " / "+ bitemex_position.liquidationPrice + ")\r\n");

                    //}
                }
            }
            else
            {
                txt_position.AppendText("포지션이 0개이거나 2개이상임. 확인해볼것~.\r\n");
                // 0개 혹은 2개도 있나 확인해보자.... 아마 없을것임...
            }
        }

        


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        



        private void btn_potionConfirm_Click(object sender, EventArgs e)
        {
            GetPositions();
        }

        private void btn_positionDel_Click(object sender, EventArgs e)
        {
            bitmex_Get_bucketed_2();
            GetPositions();

            if (bitemex_position.currentQty != 0)
            {
                if (bitemex_position.currentQty > 0)
                {
                    string result_order1 = bitemex.PostOrders("XBTUSD", "Sell", bitemex_position.currentQty, btmex_Bucketeds[0].close - 0.5, "Limit");
                }
                else
                {
                    string result_order1 = bitemex.PostOrders("XBTUSD", "Buy", Math.Abs(bitemex_position.currentQty), btmex_Bucketeds[0].close + 0.5, "Limit");
                }
                //chk_OrderChange = true;
            }
            Get_Orders();

        }


        private void btn_test1_Click(object sender, EventArgs e)
        {
            //Stop
            //Limit
            //StopLimit
            var result_bucketed = bitemex.bitmex_Get_bucketed("1m", true, "XBTUSD", 1, true);
            btmex_Bucketeds = JsonConvert.DeserializeObject<List<bitmex_bucketed>>(result_bucketed);
            string result_order1 = bitemex.PostOrders("XBTUSD", "Sell", 100, btmex_Bucketeds[0].close + 50, "Limit");
            //string result_order2 = bitmex.PostOrders("XBTUSD", "Buy", 100, btmex_Bucketeds[0].close - 50, "Limit");
            Get_Orders();
        }

        private void btn_test2_Click(object sender, EventArgs e)
        {
            var result_bucketed = bitemex.bitmex_Get_bucketed("1m", true, "XBTUSD", 1, true);
            btmex_Bucketeds = JsonConvert.DeserializeObject<List<bitmex_bucketed>>(result_bucketed);
            string result_order1 = bitemex.PostOrders("XBTUSD", "Sell", 40, btmex_Bucketeds[0].close + 0.5, "Limit");
            Get_Orders();
        }
        private void btn_test3_Click(object sender, EventArgs e)
        {
            var result_bucketed = bitemex.bitmex_Get_bucketed("1m", true, "XBTUSD", 1, true);
            btmex_Bucketeds = JsonConvert.DeserializeObject<List<bitmex_bucketed>>(result_bucketed);
            string result_order1 = bitemex.PostOrders("XBTUSD", "Sell", 40, btmex_Bucketeds[0].close - 0.5, "Limit");
            Get_Orders();
        }

      

        private void btn_orderConfirm_Click(object sender, EventArgs e)
        {
            //chk_OrderChange = true;
            Get_Orders();
        }

        private void btn_Step_Cal_Click(object sender, EventArgs e)
        {
            int step1_Qty = 5011110; double step1_spring = 4.0; double _margin1 = 5.0;          // 7025.0      ( 35,125,000)
            int step2_Qty = 3000; double step2_spring = 8.0; double _margin2 = 6.0;         // 7037.5    ( 70,375,000)
            int step3_Qty = 8000; double step3_spring = 16.0; double _margin3 = 12.0;       // 7068.75     (141,375,000)
            int step4_Qty = 12000; double step4_spring = 35.0; double _margin4 = 12.0;
            int step5_Qty = 15000; double step5_spring = 60.0; double _margin5 = 15.0;
            int step6_Qty = 18000; double step6_spring = 90.0; double _margin6 = 15.0;
            int step7_Qty = 45000; double step7_spring = 150.0; double _margin7 = 10.0;
            int step8_Qty = 80000; double step8_spring = 200.0; double _margin8 = 10.0;
            int step9_Qty = 80000; double step9_spring = 300.0; double _margin9 = 10.0;
            int step10_Qty = 80000; double step10_spring = 400.0; double _margin10 = 10.0;
            int step11_Qty = 80000; double step11_spring = 500.0; double _margin11 = 10.0;

            txt_position.Text = "";

            double standardXBT = 7000;
            double total_BuyQty = 0.0;
            double income = 0.0;
            double now_XBTUSD = 0.0;
            double pre_XBTUSD = 0.0;
            double buy_Bitcoin = 0.0;
            double average_Bitcoin = 0.0;
            int total_QTY = 0;


            double sell_Bitcoin = 0;


            pre_XBTUSD = 0;
            now_XBTUSD = standardXBT - step1_spring;
            total_QTY = total_QTY + step1_Qty;
            total_BuyQty = total_BuyQty + now_XBTUSD * step1_Qty;
            buy_Bitcoin = Math.Round((0 + (step1_Qty / now_XBTUSD)) * 100000)/100000;
            average_Bitcoin = Math.Round(total_BuyQty / total_QTY * 10) / 10;   //
            sell_Bitcoin = buy_Bitcoin * (average_Bitcoin + _margin1) /(average_Bitcoin);
            income = sell_Bitcoin - buy_Bitcoin;
            txt_position.AppendText("[ 1단계]  " + string.Format("{0,8}",total_QTY) + "XBT(" + buy_Bitcoin + " bitcoin) (평균-" + average_Bitcoin +
                "  진입가격-" + now_XBTUSD + "  " +
                "  판매가격-" + (average_Bitcoin + _margin1) +
                "{" + Math.Round(sell_Bitcoin * 1000) / 1000 + "})      이익:" + Math.Round(income * 10000) / 10000 + " bitcoin\r\n\r\n");



            pre_XBTUSD = now_XBTUSD;
            now_XBTUSD = standardXBT - step2_spring;
            total_QTY = total_QTY + step2_Qty;
            total_BuyQty = total_BuyQty + now_XBTUSD * step2_Qty;
            buy_Bitcoin = Math.Round(((buy_Bitcoin * now_XBTUSD / pre_XBTUSD) + step2_Qty / now_XBTUSD) * 100000) / 100000;
            average_Bitcoin = Math.Round(total_BuyQty / total_QTY * 10) / 10;   //
            sell_Bitcoin = buy_Bitcoin * (average_Bitcoin + _margin2) / (average_Bitcoin);
            income = sell_Bitcoin - buy_Bitcoin;
            txt_position.AppendText("[ 2단계]  " + string.Format("{0,8}", total_QTY) + "XBT(" + buy_Bitcoin + " bitcoin) (평균-" + average_Bitcoin +
                "  진입가격-" + now_XBTUSD + "  " +
                "  판매가격-" + (average_Bitcoin + _margin2) +
                "{" + Math.Round(sell_Bitcoin * 1000) / 1000 + "})      이익:" + Math.Round(income * 10000) / 10000 + " bitcoin\r\n\r\n");


            pre_XBTUSD = now_XBTUSD;
            now_XBTUSD = standardXBT - step3_spring;
            total_QTY = total_QTY + step3_Qty;
            total_BuyQty = total_BuyQty + now_XBTUSD * step3_Qty;

            buy_Bitcoin = Math.Round(((buy_Bitcoin * now_XBTUSD / pre_XBTUSD) + step3_Qty / now_XBTUSD) * 100000) / 100000;
            average_Bitcoin = Math.Round(total_BuyQty / total_QTY * 10) / 10;   //
            sell_Bitcoin = buy_Bitcoin * (average_Bitcoin + _margin3) / (average_Bitcoin);
            income = sell_Bitcoin - buy_Bitcoin;
            txt_position.AppendText("[ 3단계]  " + string.Format("{0,8}", total_QTY) + "XBT(" + buy_Bitcoin + " bitcoin) (평균-" + average_Bitcoin +
                "  진입가격-" + now_XBTUSD + "  " +
                "  판매가격-" + (average_Bitcoin + _margin3) +
                "{" + Math.Round(sell_Bitcoin * 1000) / 1000 + "})      이익:" + Math.Round(income * 10000) / 10000 + " bitcoin\r\n\r\n");



            pre_XBTUSD = now_XBTUSD;
            now_XBTUSD = standardXBT - step4_spring;
            total_QTY = total_QTY + step4_Qty;
            total_BuyQty = total_BuyQty + now_XBTUSD * step4_Qty;

            buy_Bitcoin = Math.Round(((buy_Bitcoin * now_XBTUSD / pre_XBTUSD) + step4_Qty / now_XBTUSD) * 100000) / 100000;
            average_Bitcoin = Math.Round(total_BuyQty / total_QTY * 10) / 10;   //
            sell_Bitcoin = buy_Bitcoin * (average_Bitcoin + _margin4) / (average_Bitcoin);
            income = sell_Bitcoin - buy_Bitcoin;
            txt_position.AppendText("[ 4단계]  " + string.Format("{0,8}", total_QTY) + "XBT(" + buy_Bitcoin + " bitcoin) (평균-" + average_Bitcoin +
                "  진입가격-" + now_XBTUSD + "  " +
                "  판매가격-" + (average_Bitcoin + _margin4) +
                "{" + Math.Round(sell_Bitcoin * 1000) / 1000 + "})      이익:" + Math.Round(income * 10000) / 10000 + " bitcoin\r\n\r\n");

            pre_XBTUSD = now_XBTUSD;
            now_XBTUSD = standardXBT - step5_spring;
            total_QTY = total_QTY + step5_Qty;
            total_BuyQty = total_BuyQty + now_XBTUSD * step5_Qty;

            buy_Bitcoin = Math.Round(((buy_Bitcoin * now_XBTUSD / pre_XBTUSD) + step5_Qty / now_XBTUSD) * 100000) / 100000;
            average_Bitcoin = Math.Round(total_BuyQty / total_QTY * 10) / 10;   //
            sell_Bitcoin = buy_Bitcoin * (average_Bitcoin + _margin5) / (average_Bitcoin);
            income = sell_Bitcoin - buy_Bitcoin;
            txt_position.AppendText("[ 5단계]  " + string.Format("{0,8}", total_QTY) + "XBT(" + buy_Bitcoin + " bitcoin) (평균-" + average_Bitcoin +
                "  진입가격-" + now_XBTUSD + "  " +
                "  판매가격-" + (average_Bitcoin + _margin5) +
                "{" + Math.Round(sell_Bitcoin * 1000) / 1000 + "})      이익:" + Math.Round(income * 10000) / 10000 + " bitcoin\r\n\r\n");




            pre_XBTUSD = now_XBTUSD;
            now_XBTUSD = standardXBT - step6_spring;
            total_QTY = total_QTY + step6_Qty;
            total_BuyQty = total_BuyQty + now_XBTUSD * step6_Qty;

            buy_Bitcoin = Math.Round(((buy_Bitcoin * now_XBTUSD / pre_XBTUSD) + step6_Qty / now_XBTUSD) * 100000) / 100000;
            average_Bitcoin = Math.Round(total_BuyQty / total_QTY * 10) / 10;   //
            sell_Bitcoin = buy_Bitcoin * (average_Bitcoin + _margin6) / (average_Bitcoin);
            income = sell_Bitcoin - buy_Bitcoin;
            txt_position.AppendText("[ 6단계]  " + string.Format("{0,8}", total_QTY) + "XBT(" + buy_Bitcoin + " bitcoin) (평균-" + average_Bitcoin +
                "  진입가격-" + now_XBTUSD + "  " +
                "  판매가격-" + (average_Bitcoin + _margin6) +
                "{" + Math.Round(sell_Bitcoin * 1000) / 1000 + "})      이익:" + Math.Round(income * 10000) / 10000 + " bitcoin\r\n\r\n");

            pre_XBTUSD = now_XBTUSD;
            now_XBTUSD = standardXBT - step7_spring;
            total_QTY = total_QTY + step7_Qty;
            total_BuyQty = total_BuyQty + now_XBTUSD * step7_Qty;

            buy_Bitcoin = Math.Round(((buy_Bitcoin * now_XBTUSD / pre_XBTUSD) + step7_Qty / now_XBTUSD) * 100000) / 100000;
            average_Bitcoin = Math.Round(total_BuyQty / total_QTY * 10) / 10;   //
            sell_Bitcoin = buy_Bitcoin * (average_Bitcoin + _margin7) / (average_Bitcoin);
            income = sell_Bitcoin - buy_Bitcoin;
            txt_position.AppendText("[ 7단계]  " + string.Format("{0,8}", total_QTY) + "XBT(" + buy_Bitcoin + " bitcoin) (평균-" + average_Bitcoin +
                "  진입가격-" + now_XBTUSD + "  " +
                "  판매가격-" + (average_Bitcoin + _margin7) +
                "{" + Math.Round(sell_Bitcoin * 1000) / 1000 + "})      이익:" + Math.Round(income * 10000) / 10000 + " bitcoin\r\n\r\n");

            pre_XBTUSD = now_XBTUSD;
            now_XBTUSD = standardXBT - step8_spring;
            total_QTY = total_QTY + step8_Qty;
            total_BuyQty = total_BuyQty + now_XBTUSD * step8_Qty;

            buy_Bitcoin = Math.Round(((buy_Bitcoin * now_XBTUSD / pre_XBTUSD) + step8_Qty / now_XBTUSD) * 100000) / 100000;
            average_Bitcoin = Math.Round(total_BuyQty / total_QTY * 10) / 10;   //
            sell_Bitcoin = buy_Bitcoin * (average_Bitcoin + _margin8) / (average_Bitcoin);
            income = sell_Bitcoin - buy_Bitcoin;
            txt_position.AppendText("[ 8단계]  " + string.Format("{0,8}", total_QTY) + "XBT(" + buy_Bitcoin + " bitcoin) (평균-" + average_Bitcoin +
                "  진입가격-" + now_XBTUSD + "  " +
                "  판매가격-" + (average_Bitcoin + _margin8) +
                "{" + Math.Round(sell_Bitcoin * 1000) / 1000 + "})      이익:" + Math.Round(income * 10000) / 10000 + " bitcoin\r\n\r\n");


            pre_XBTUSD = now_XBTUSD;
            now_XBTUSD = standardXBT - step9_spring;
            total_QTY = total_QTY + step9_Qty;
            total_BuyQty = total_BuyQty + now_XBTUSD * step9_Qty;

            buy_Bitcoin = Math.Round(((buy_Bitcoin * now_XBTUSD / pre_XBTUSD) + step9_Qty / now_XBTUSD) * 100000) / 100000;
            average_Bitcoin = Math.Round(total_BuyQty / total_QTY * 10) / 10;   //
            sell_Bitcoin = buy_Bitcoin * (average_Bitcoin + _margin9) / (average_Bitcoin);
            income = sell_Bitcoin - buy_Bitcoin;
            txt_position.AppendText("[ 9단계]  " + string.Format("{0,8}", total_QTY) + "XBT(" + buy_Bitcoin + " bitcoin) (평균-" + average_Bitcoin +
                "  진입가격-" + now_XBTUSD + "  " +
                "  판매가격-" + (average_Bitcoin + _margin9) +
                "{" + Math.Round(sell_Bitcoin * 1000) / 1000 + "})      이익:" + Math.Round(income * 10000) / 10000 + " bitcoin\r\n\r\n");


            pre_XBTUSD = now_XBTUSD;
            now_XBTUSD = standardXBT - step10_spring;
            total_QTY = total_QTY + step10_Qty;
            total_BuyQty = total_BuyQty + now_XBTUSD * step10_Qty;

            buy_Bitcoin = Math.Round(((buy_Bitcoin * now_XBTUSD / pre_XBTUSD) + step10_Qty / now_XBTUSD) * 100000) / 100000;
            average_Bitcoin = Math.Round(total_BuyQty / total_QTY * 10) / 10;   //
            sell_Bitcoin = buy_Bitcoin * (average_Bitcoin + _margin10) / (average_Bitcoin);
            income = sell_Bitcoin - buy_Bitcoin;
            txt_position.AppendText("[10단계]  " + string.Format("{0,8}", total_QTY) + "XBT(" + buy_Bitcoin + " bitcoin) (평균-" + average_Bitcoin +
                "  진입가격-" + now_XBTUSD + "  " +
                "  판매가격-" + (average_Bitcoin + _margin10) +
                "{" + Math.Round(sell_Bitcoin * 1000) / 1000 + "})      이익:" + Math.Round(income * 10000) / 10000 + " bitcoin\r\n\r\n");


            pre_XBTUSD = now_XBTUSD;
            now_XBTUSD = standardXBT - step11_spring;
            total_QTY = total_QTY + step11_Qty;
            total_BuyQty = total_BuyQty + now_XBTUSD * step11_Qty;

            buy_Bitcoin = Math.Round(((buy_Bitcoin * now_XBTUSD / pre_XBTUSD) + step11_Qty / now_XBTUSD) * 100000) / 100000;
            average_Bitcoin = Math.Round(total_BuyQty / total_QTY * 10) / 10;   //
            sell_Bitcoin = buy_Bitcoin * (average_Bitcoin + _margin11) / (average_Bitcoin);
            income = sell_Bitcoin - buy_Bitcoin;
            txt_position.AppendText("[11단계]  " + string.Format("{0,8}", total_QTY) + "XBT(" + buy_Bitcoin + " bitcoin) (평균-" + average_Bitcoin +
                "  진입가격-" + now_XBTUSD + "  " +
                "  판매가격-" + (average_Bitcoin + _margin11) +
                "{" + Math.Round(sell_Bitcoin * 1000) / 1000 + "})      이익:" + Math.Round(income * 10000) / 10000 + " bitcoin\r\n\r\n");

        }

        private void btn_balance_Click(object sender, EventArgs e)
        {
            _user_margin = JsonConvert.DeserializeObject<user_margin>(bitemex.GetUserMargin());
            txt_position.AppendText("○ " + DateTime.Now.ToString("MM월dd일 HH시mm분") +
                 "         " + string.Format("{0:#,###}", _user_margin.walletBalance * 0.1) +
                 "         " + string.Format("{0:#,###}", _user_margin.marginBalance * 0.1) + "\r\n");
        }
    }
}
