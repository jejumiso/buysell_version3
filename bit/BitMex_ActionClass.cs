using bit.Model;
using BitMEX;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bit.BitMex_ActionClass
{
    public class BitMex_ActionClass
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

        BitMEXApi bitemex = new BitMEXApi(bitmexKey, bitmexSecret);


        double iniinitial_value = 0.0;
        string pre_timestamp = "";
        int no = 199;
        /// <summary>
        /// order_System2   version 2.0
        /// spring 주문을 굳이 나눠서 하지 않고 한번에 넣어야 좋을듯함...
        /// 이것도 나쁘지 않음...
        /// 초 급등에 대한 대응이 없음... 초 급등 할때도 소폭의 이익만 챙기게됨...
        /// spring의 적게주고 margin을 적게줌 : 안전하게 자주 먹는 시스템임...   아무리 그래도 step가 올라가면 어쩔수 없음..
        /// 급등을 먹을려고 하면   spring의 값을 넓게 주고 margin 값을 높이 줌   => 이것도 괜츦을듯   만들어보자..
        /// </summary>
        public void order_System2(double limit_trad_price , List<bitmex_bucketed> bitemex_Bucketeds, bitemex_position bitemex_position)
        {

            //[1-1]  setting
            #region setting
            int step1_Qty; double step1_spring; double _margin1;          // 7025.0      ( 35,125,000)
            int step2_Qty; double step2_spring; double _margin2;         // 7037.5    ( 70,375,000)
            int step3_Qty; double step3_spring; double _margin3;       // 7068.75     (141,375,000)
            int step4_Qty; double step4_spring; double _margin4;
            int step5_Qty; double step5_spring; double _margin5;
            int step6_Qty; double step6_spring; double _margin6;
            int step7_Qty; double step7_spring; double _margin7;
            int step8_Qty; double step8_spring; double _margin8;
            int step9_Qty; double step9_spring; double _margin9;

            if (true)//limit_trad_price < 1000
            {
                step1_Qty = 2500; step1_spring = 5.0; _margin1 = 15.0;       
                step2_Qty = 3500; step2_spring = 8.0; _margin2 = 15.0;       
                step3_Qty = 3500; step3_spring = 15.0; _margin3 = 23.0;      
                step4_Qty = 3500; step4_spring = 30.0; _margin4 = 40.0;
                step5_Qty = 5500; step5_spring = 55.0; _margin5 = 60.0;
                step6_Qty = 400; step6_spring = 80.0; _margin6 = 100.0;
                step7_Qty = 50; step7_spring = 140.0; _margin7 = 160.0;
                step8_Qty = 50; step8_spring = 180.0; _margin8 = 210.0;
                step9_Qty = 50; step9_spring = 250.0; _margin9 = 250.0;
            }
            //else if (limit_trad_price < 2000)
            //{
            //    step1_Qty = 110; step1_spring = 5.0; _margin1 = 15.0;     
            //    step2_Qty = 110; step2_spring = 8.0; _margin2 = 15.0;     
            //    step3_Qty = 110; step3_spring = 15.0; _margin3 = 23.0;    
            //    step4_Qty = 220; step4_spring = 30.0; _margin4 = 40.0;
            //    step5_Qty = 220; step5_spring = 55.0; _margin5 = 60.0;
            //    step6_Qty = 220; step6_spring = 80.0; _margin6 = 100.0;
            //    step7_Qty = 220; step7_spring = 140.0; _margin7 = 160.0;
            //    step8_Qty = 220; step8_spring = 180.0; _margin8 = 210.0;
            //    step9_Qty = 220; step9_spring = 250.0; _margin9 = 250.0;
            //}
            //else if (limit_trad_price < 4000)
            //{
            //    step1_Qty = 120; step1_spring = 5.0; _margin1 = 15.0;   
            //    step2_Qty = 120; step2_spring = 8.0; _margin2 = 15.0;   
            //    step3_Qty = 120; step3_spring = 15.0; _margin3 = 23.0;  
            //    step4_Qty = 240; step4_spring = 30.0; _margin4 = 40.0;
            //    step5_Qty = 240; step5_spring = 55.0; _margin5 = 60.0;
            //    step6_Qty = 240; step6_spring = 80.0; _margin6 = 100.0;
            //    step7_Qty = 240; step7_spring = 140.0; _margin7 = 160.0;
            //    step8_Qty = 240; step8_spring = 180.0; _margin8 = 210.0;
            //    step9_Qty = 240; step9_spring = 250.0; _margin9 = 250.0;
            //}
            //else if (limit_trad_price < 8000)
            //{
            //    step1_Qty = 150; step1_spring = 5.0; _margin1 = 15.0;    
            //    step2_Qty = 150; step2_spring = 8.0; _margin2 = 15.0;    
            //    step3_Qty = 150; step3_spring = 15.0; _margin3 = 23.0;   
            //    step4_Qty = 300; step4_spring = 30.0; _margin4 = 40.0;
            //    step5_Qty = 300; step5_spring = 55.0; _margin5 = 60.0;
            //    step6_Qty = 300; step6_spring = 80.0; _margin6 = 100.0;
            //    step7_Qty = 300; step7_spring = 140.0; _margin7 = 160.0;
            //    step8_Qty = 300; step8_spring = 180.0; _margin8 = 210.0;
            //    step9_Qty = 300; step9_spring = 250.0; _margin9 = 250.0;
            //}
            //else if (limit_trad_price < 16000)       
            //{
            //    step1_Qty = 220; step1_spring = 5.0; _margin1 = 15.0;   
            //    step2_Qty = 220; step2_spring = 8.0; _margin2 = 15.0;   
            //    step3_Qty = 220; step3_spring = 15.0; _margin3 = 23.0;  
            //    step4_Qty = 440; step4_spring = 30.0; _margin4 = 40.0;
            //    step5_Qty = 440; step5_spring = 55.0; _margin5 = 60.0;
            //    step6_Qty = 440; step6_spring = 80.0; _margin6 = 100.0;
            //    step7_Qty = 440; step7_spring = 140.0; _margin7 = 160.0;
            //    step8_Qty = 440; step8_spring = 180.0; _margin8 = 210.0;
            //    step9_Qty = 440; step9_spring = 250.0; _margin9 = 250.0;
            //}
            //else if (limit_trad_price < 32000)     // 이정도 안정권에서는 운영해야할듯함...
            //{
            //    step1_Qty = 230; step1_spring = 5.0; _margin1 = 15.0;   
            //    step2_Qty = 230; step2_spring = 8.0; _margin2 = 15.0;   
            //    step3_Qty = 230; step3_spring = 15.0; _margin3 = 23.0;  
            //    step4_Qty = 460; step4_spring = 30.0; _margin4 = 40.0;
            //    step5_Qty = 460; step5_spring = 55.0; _margin5 = 60.0;
            //    step6_Qty = 460; step6_spring = 80.0; _margin6 = 100.0;
            //    step7_Qty = 460; step7_spring = 140.0; _margin7 = 160.0;
            //    step8_Qty = 460; step8_spring = 180.0; _margin8 = 210.0;
            //    step9_Qty = 460; step9_spring = 250.0; _margin9 = 250.0;
            //}
            //else if (limit_trad_price < 64000)
            //{
            //    step1_Qty = 600; step1_spring = 5.0; _margin1 = 15.0;   
            //    step2_Qty = 600; step2_spring = 8.0; _margin2 = 15.0;   
            //    step3_Qty = 600; step3_spring = 15.0; _margin3 = 23.0;  
            //    step4_Qty = 1200; step4_spring = 30.0; _margin4 = 40.0;
            //    step5_Qty = 1200; step5_spring = 55.0; _margin5 = 60.0;
            //    step6_Qty = 1200; step6_spring = 80.0; _margin6 = 100.0;
            //    step7_Qty = 1200; step7_spring = 140.0; _margin7 = 160.0;
            //    step8_Qty = 1200; step8_spring = 180.0; _margin8 = 210.0;
            //    step9_Qty = 1200; step9_spring = 250.0; _margin9 = 250.0;
            //}
            //else if (limit_trad_price < 128000)
            //{
            //    step1_Qty = 800; step1_spring = 5.0; _margin1 = 15.0;   
            //    step2_Qty = 800; step2_spring = 8.0; _margin2 = 15.0;   
            //    step3_Qty = 800; step3_spring = 15.0; _margin3 = 23.0;  
            //    step4_Qty = 1600; step4_spring = 30.0; _margin4 = 40.0;
            //    step5_Qty = 1600; step5_spring = 55.0; _margin5 = 60.0;
            //    step6_Qty = 1600; step6_spring = 80.0; _margin6 = 100.0;
            //    step7_Qty = 1600; step7_spring = 140.0; _margin7 = 160.0;
            //    step8_Qty = 1600; step8_spring = 180.0; _margin8 = 210.0;
            //    step9_Qty = 1600; step9_spring = 250.0; _margin9 = 250.0;
            //}
            //else if (limit_trad_price < 256000)
            //{
            //    step1_Qty = 1500; step1_spring = 5.0; _margin1 = 15.0;   
            //    step2_Qty = 1500; step2_spring = 8.0; _margin2 = 15.0;   
            //    step3_Qty = 1500; step3_spring = 15.0; _margin3 = 23.0;  
            //    step4_Qty = 3000; step4_spring = 30.0; _margin4 = 40.0;
            //    step5_Qty = 3000; step5_spring = 55.0; _margin5 = 60.0;
            //    step6_Qty = 3000; step6_spring = 80.0; _margin6 = 100.0;
            //    step7_Qty = 3000; step7_spring = 140.0; _margin7 = 160.0;
            //    step8_Qty = 3000; step8_spring = 180.0; _margin8 = 210.0;
            //    step9_Qty = 3000; step9_spring = 250.0; _margin9 = 250.0;
            //}
            //else
            //{
            //    //limit_trad_price < 512000
            //    step1_Qty = 3000; step1_spring = 5.0; _margin1 = 15.0;   
            //    step2_Qty = 3000; step2_spring = 8.0; _margin2 = 15.0;   
            //    step3_Qty = 3000; step3_spring = 15.0; _margin3 = 23.0;  
            //    step4_Qty = 6000; step4_spring = 30.0; _margin4 = 40.0;
            //    step5_Qty = 6000; step5_spring = 55.0; _margin5 = 60.0;
            //    step6_Qty = 6000; step6_spring = 80.0; _margin6 = 100.0;
            //    step7_Qty = 6000; step7_spring = 140.0; _margin7 = 160.0;
            //    step8_Qty = 6000; step8_spring = 180.0; _margin8 = 210.0;
            //    step9_Qty = 6000; step9_spring = 250.0; _margin9 = 250.0;
            //} 
            #endregion

            //[1-2] setting
            iniinitial_value = bitemex_Bucketeds[0].open;

            //[2]
            #region 주문 넣기
            //[2-1] 기존 주문 삭제
            bitemex.DeleteAllOrders("{\"clOrdID\":\"" + (no - 1) + "\"}");
            //[2-2] 주문 넣기
            List<bitmex_order> list_bitmex_order = new List<bitmex_order>();
            bitmex_order _bitmex_order;
            double price = 0.0;
            #region step1
            price = bitemex_Bucketeds[0].close <= iniinitial_value - step1_spring ? bitemex_Bucketeds[0].close - 1.5 : iniinitial_value - step1_spring;
            if (bitemex_position.currentQty <= 0 || (bitemex_position.currentQty > 0 && bitemex_position.marginCallPrice < price && bitemex_position.avgCostPrice > price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step1_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
            }
            price = bitemex_Bucketeds[0].close >= iniinitial_value + step1_spring ? bitemex_Bucketeds[0].close + 1.5 : iniinitial_value + step1_spring;
            if (bitemex_position.currentQty >= 0 || (bitemex_position.currentQty < 0 && bitemex_position.marginCallPrice > price && bitemex_position.avgCostPrice < price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step1_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step2
            price = bitemex_Bucketeds[0].close <= iniinitial_value - step2_spring ? bitemex_Bucketeds[0].close - 1.5 : iniinitial_value - step2_spring;
            if (bitemex_position.currentQty <= 0 || (bitemex_position.currentQty > 0 && bitemex_position.marginCallPrice < price && bitemex_position.avgCostPrice > price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step2_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            price = bitemex_Bucketeds[0].close >= iniinitial_value + step2_spring ? bitemex_Bucketeds[0].close + 1.5 : iniinitial_value + step2_spring;
            if (bitemex_position.currentQty >= 0 || (bitemex_position.currentQty < 0 && bitemex_position.marginCallPrice > price && bitemex_position.avgCostPrice < price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step2_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step3
            price = bitemex_Bucketeds[0].close <= iniinitial_value - step3_spring ? bitemex_Bucketeds[0].close - 1.5 : iniinitial_value - step3_spring;
            if (bitemex_position.currentQty <= 0 || (bitemex_position.currentQty > 0 && bitemex_position.marginCallPrice < price && bitemex_position.avgCostPrice > price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step3_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            price = bitemex_Bucketeds[0].close >= iniinitial_value + step3_spring ? bitemex_Bucketeds[0].close + 1.5 : iniinitial_value + step3_spring;
            if (bitemex_position.currentQty >= 0 || (bitemex_position.currentQty < 0 && bitemex_position.marginCallPrice > price && bitemex_position.avgCostPrice < price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step3_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step4
            price = bitemex_Bucketeds[0].close <= iniinitial_value - step4_spring ? bitemex_Bucketeds[0].close - 1.5 : iniinitial_value - step4_spring;
            if (bitemex_position.currentQty <= 0 || (bitemex_position.currentQty > 0 && bitemex_position.marginCallPrice < price && bitemex_position.avgCostPrice > price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step4_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            price = bitemex_Bucketeds[0].close >= iniinitial_value + step4_spring ? bitemex_Bucketeds[0].close + 1.5 : iniinitial_value + step4_spring;
            if (bitemex_position.currentQty >= 0 || (bitemex_position.currentQty < 0 && bitemex_position.marginCallPrice > price && bitemex_position.avgCostPrice < price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step4_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step5
            price = bitemex_Bucketeds[0].close <= iniinitial_value - step5_spring ? bitemex_Bucketeds[0].close - 1.5 : iniinitial_value - step5_spring;
            if (bitemex_position.currentQty <= 0 || (bitemex_position.currentQty > 0 && bitemex_position.marginCallPrice < price && bitemex_position.avgCostPrice > price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step5_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            price = bitemex_Bucketeds[0].close >= iniinitial_value + step5_spring ? bitemex_Bucketeds[0].close + 1.5 : iniinitial_value + step5_spring;
            if (bitemex_position.currentQty >= 0 || (bitemex_position.currentQty < 0 && bitemex_position.marginCallPrice > price && bitemex_position.avgCostPrice < price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step5_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step6
            price = bitemex_Bucketeds[0].close <= iniinitial_value - step6_spring ? bitemex_Bucketeds[0].close - 1.5 : iniinitial_value - step6_spring;
            if (bitemex_position.currentQty <= 0 || (bitemex_position.currentQty > 0 && bitemex_position.marginCallPrice < price && bitemex_position.avgCostPrice > price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step6_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            price = bitemex_Bucketeds[0].close >= iniinitial_value + step6_spring ? bitemex_Bucketeds[0].close + 1.5 : iniinitial_value + step6_spring;
            if (bitemex_position.currentQty >= 0 || (bitemex_position.currentQty < 0 && bitemex_position.marginCallPrice > price && bitemex_position.avgCostPrice < price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step6_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step7
            price = bitemex_Bucketeds[0].close <= iniinitial_value - step7_spring ? bitemex_Bucketeds[0].close - 1.5 : iniinitial_value - step7_spring;
            if (bitemex_position.currentQty <= 0 || (bitemex_position.currentQty > 0 && bitemex_position.marginCallPrice < price && bitemex_position.avgCostPrice > price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step7_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            price = bitemex_Bucketeds[0].close >= iniinitial_value + step7_spring ? bitemex_Bucketeds[0].close + 1.5 : iniinitial_value + step7_spring;
            if (bitemex_position.currentQty >= 0 || (bitemex_position.currentQty < 0 && bitemex_position.marginCallPrice > price && bitemex_position.avgCostPrice < price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step7_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step8
            price = bitemex_Bucketeds[0].close <= iniinitial_value - step8_spring ? bitemex_Bucketeds[0].close - 1.5 : iniinitial_value - step8_spring;
            if (bitemex_position.currentQty <= 0 || (bitemex_position.currentQty > 0 && bitemex_position.marginCallPrice < price && bitemex_position.avgCostPrice > price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step8_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            price = bitemex_Bucketeds[0].close >= iniinitial_value + step8_spring ? bitemex_Bucketeds[0].close + 1.5 : iniinitial_value + step8_spring;
            if (bitemex_position.currentQty >= 0 || (bitemex_position.currentQty < 0 && bitemex_position.marginCallPrice > price && bitemex_position.avgCostPrice < price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step8_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step9
            price = bitemex_Bucketeds[0].close <= iniinitial_value - step9_spring ? bitemex_Bucketeds[0].close - 1.5 : iniinitial_value - step9_spring;
            if (bitemex_position.currentQty <= 0 || (bitemex_position.currentQty > 0 && bitemex_position.marginCallPrice < price && bitemex_position.avgCostPrice > price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step9_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            price = bitemex_Bucketeds[0].close >= iniinitial_value + step9_spring ? bitemex_Bucketeds[0].close + 1.5 : iniinitial_value + step9_spring;
            if (bitemex_position.currentQty >= 0 || (bitemex_position.currentQty < 0 && bitemex_position.marginCallPrice > price && bitemex_position.avgCostPrice < price))
            {
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = no.ToString();
                _bitmex_order.orderQty = step9_Qty;
                _bitmex_order.price = price;
                _bitmex_order.ordType = "Limit";
                _bitmex_order.text = no.ToString();
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            string _result = bitemex.PostOrders_bulk(list_bitmex_order);
            #endregion

            //[3]  청산
            if (no > 1)
            {
                List<bitmex_order> recent_orders = new List<bitmex_order>();

                string clOrdID = (no - 1).ToString();
                var json_result = bitemex.GetOrders("XBTUSD", "", 100, true, "");
                recent_orders = JsonConvert.DeserializeObject<List<bitmex_order>>(json_result);
                list_bitmex_order = new List<bitmex_order>();
                foreach (var item in recent_orders.Where(p => p.clOrdID == (clOrdID) && p.cumQty > 0))
                {
                    _bitmex_order = new bitmex_order();
                    string side = "";
                    if (item.side == "Buy")
                    {
                        side = "Sell";
                        if (item.cumQty <= step1_Qty)
                        {
                            price = item.price + _margin1;
                        }
                        else if (item.cumQty <= step2_Qty)
                        {
                            price = item.price + _margin2;
                        }
                        else if (item.cumQty <= step3_Qty)
                        {
                            price = item.price + _margin3;
                        }
                        else if (item.cumQty <= step4_Qty)
                        {
                            price = item.price + _margin4;
                        }
                        else if (item.cumQty <= step5_Qty)
                        {
                            price = item.price + _margin5;
                        }
                        else if (item.cumQty <= step6_Qty)
                        {
                            price = item.price + _margin6;
                        }
                        else if (item.cumQty <= step7_Qty)
                        {
                            price = item.price + _margin7;
                        }
                        else if (item.cumQty <= step8_Qty)
                        {
                            price = item.price + _margin8;
                        }
                        else
                        {
                            price = item.price + _margin9;
                        }
                    }
                    else
                    {
                        side = "Buy";
                        if (item.cumQty <= step1_Qty)
                        {
                            price = item.price - _margin1;
                        }
                        else if (item.cumQty <= step2_Qty)
                        {
                            price = item.price - _margin2;
                        }
                        else if (item.cumQty <= step3_Qty)
                        {
                            price = item.price - _margin3;
                        }
                        else if (item.cumQty <= step4_Qty)
                        {
                            price = item.price - _margin4;
                        }
                        else if (item.cumQty <= step5_Qty)
                        {
                            price = item.price - _margin5;
                        }
                        else if (item.cumQty <= step6_Qty)
                        {
                            price = item.price - _margin6;
                        }
                        else if (item.cumQty <= step7_Qty)
                        {
                            price = item.price - _margin7;
                        }
                        else if (item.cumQty <= step8_Qty)
                        {
                            price = item.price - _margin8;
                        }
                        else
                        {
                            price = item.price - _margin9;
                        }

                    }


                    if (recent_orders.Where(p => p.side == side && p.price == price && p.ordStatus == "new" && p.text == "end").Count() != 0)
                    {
                        string orderID = recent_orders.Where(p => p.side == side && p.price == price && p.ordStatus == "new" && p.text == "end").FirstOrDefault().orderID;
                        int orderQty = recent_orders.Where(p => p.side == side && p.price == price && p.ordStatus == "new" && p.text == "end").FirstOrDefault().orderQty + item.cumQty;
                        bitemex.PostOrders_PUT(orderID, price, orderQty);
                    }
                    else
                    {
                        _bitmex_order = new bitmex_order();
                        _bitmex_order.symbol = "XBTUSD";
                        _bitmex_order.side = side;
                        _bitmex_order.orderQty = item.cumQty;
                        _bitmex_order.price = price;
                        _bitmex_order.ordType = "Limit";
                        _bitmex_order.text = "end";
                        list_bitmex_order.Add(_bitmex_order);
                    }

                    
                }
                bitemex.PostOrders_bulk(list_bitmex_order);
            }


            // [ end ]
            pre_timestamp = bitemex_Bucketeds[0].timestamp;
            no++;

        }
    }
}
