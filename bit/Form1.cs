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
        private static string bitmexKey = "JWLE9oPI2Tkd8rDVBHTrQq-6";
        private static string bitmexSecret = "YJbAWZqlhqtgY7FWLaCYgJntqRIrQBMvDmKjZmzrcKRawlUq";

        // [2]
        BitMEXApi bitemex = new BitMEXApi(bitmexKey, bitmexSecret);
        bitemex_position bitemex_position = new bitemex_position();
        List<bitmex_order> bitmex_orders = new List<bitmex_order>();
        List<bitmex_bucketed> btmex_Bucketeds = new List<bitmex_bucketed>();
        // [2-1] 
        BitMex_ActionClass.BitMex_ActionClass bitmex_ActionClass = new BitMex_ActionClass.BitMex_ActionClass();

        // [3]
        Timer timer1 = new Timer();

        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        private void Timer1_Tick(object Sender, EventArgs e)
        {
            timer1.Stop();
            Auto_Trad_Play();
            timer1.Start();
        }

        int timeloop = 3;  //고정값
        int pre_currentQty = -1;
        double pre_avgCostPrice = -1;
        private void Auto_Trad_Play()
        {

            //[1] postion변화 감지

            
            //[2]
            if (GetPositions())   //postion불러오기를 성공 할시...
            {
                if (pre_currentQty != bitemex_position.currentQty || pre_avgCostPrice != bitemex_position.avgCostPrice)
                {
                    if (bitmex_Get_bucketed_2() && bitmex_Get_recent_orders())
                    {
                        double close = btmex_Bucketeds[0].close;
                        if (Math.Abs(bitemex_position.currentQty) > 13000)
                        {
                            if (bitemex_position.currentQty > 0)
                            {
                                bitemex.PostOrders("XBTUSD", "Sell", 2500, close - 50, "Limit", "overtradEnd");
                            }
                            else
                            {
                                bitemex.PostOrders("XBTUSD", "Buy", 2500, close + 50, "Limit", "overtradEnd");
                            }
                        }
                        order_System2(close);
                        
                    }
                }
            }
        }

        private bool bitmex_Get_recent_orders()
        {
            try
            {
                //[1] 주문 내역 불러오기
                var json_result = bitemex.GetOrders("XBTUSD", "{\"ordStatus\":\"New\"}", 200, true, "");
                recent_orders = JsonConvert.DeserializeObject<List<bitmex_order>>(json_result);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        List<bitmex_order> pre_recent_orders;
        List<bitmex_order> recent_orders;
        double iniinitial_value = 1000;
        public void order_System2(double now_close)
        {


            // 이걸 왜 했을까??.....
            //if (pre_recent_orders == null && recent_orders.Count() > 0)
            //{
            //    pre_recent_orders = new List<bitmex_order>();
            //    pre_recent_orders = recent_orders;
            //}


            //[2-1] setting
            int step_Qty; double step_spring; double _margin;
            //step_Qty = 60; step_spring = 5.0; 
            //step_Qty = 300; step_spring = 5.0;
            step_Qty = 25; step_spring = 5.0; _margin = 13.0;
            //[2-2] setting2
            iniinitial_value = 1000;
            int step_skip = Math.Abs(Convert.ToInt32(Math.Truncate((now_close - iniinitial_value) / step_spring))); // 7010 - 7000 = 10  => skip:2
            iniinitial_value = iniinitial_value + (step_skip * step_spring);

            //[3] 주문 넣기
            List<bitmex_order> list_bitmex_order = new List<bitmex_order>();
            bitmex_order _bitmex_order;
            //[3-1] 주문 청산 넣기
            if (pre_recent_orders != null)
            {
                foreach (var item in pre_recent_orders.Where(p => p.text == "Trad"))
                {
                    var _item_ = recent_orders.Where(p => p.orderID == item.orderID).FirstOrDefault();
                    if (_item_ == null)
                    {
                        string _side = item.side == "Buy" ? "Sell" : "Buy";
                        double dd_price = _side == "Buy" ? item.price - _margin : item.price + _margin;
                        if (_side == "Buy")
                        {
                            dd_price = dd_price < now_close - _margin ? dd_price : now_close - _margin;
                        }
                        if (_side == "Sell")
                        {
                            dd_price = dd_price > now_close + _margin ? dd_price : now_close + _margin;
                        }

                        _bitmex_order = new bitmex_order();
                        _bitmex_order.symbol = "XBTUSD";
                        _bitmex_order.side = _side;
                        _bitmex_order.orderQty = step_Qty;
                        _bitmex_order.price = dd_price;
                        _bitmex_order.ordType = "Limit";
                        _bitmex_order.text = "Trad_End" + item.price + item.side;
                        list_bitmex_order.Add(_bitmex_order);
                    }
                }
            }



            //[3-2] 새로운 주문 넣기.

            for (int i = 0; i < 20; i++)
            {
                double price = 0;
                //[4-1] Buy    
                price = iniinitial_value - (i + 1) * step_spring;
                // 조건 :  주문 넣은게 없고 / 청산조건에 걸리지 않아야함.  / Trad_End 주문이 없어야함.
                if (recent_orders.Where(p => p.side == "Buy" && p.text == "Trad" && p.price == price).Count() == 0)
                {
                    if (bitemex_position.currentQty <= 0 || price > bitemex_position.marginCallPrice)
                    {
                        if (list_bitmex_order.Where(p => p.text == "Trad_End" + price + "Buy").Count() == 0)
                        {
                            if (recent_orders.Where(p => p.text == "Trad_End" + price + "Buy").Count() == 0)
                            {
                                _bitmex_order = new bitmex_order();
                                _bitmex_order.symbol = "XBTUSD";
                                _bitmex_order.side = "Buy";
                                _bitmex_order.orderQty = step_Qty;
                                _bitmex_order.price = price;
                                _bitmex_order.ordType = "Limit";
                                _bitmex_order.text = "Trad";
                                list_bitmex_order.Add(_bitmex_order);
                            }
                        }

                    }
                }
                //[4-2] Sell 
                price = iniinitial_value + (i + 1) * step_spring;
                // 조건 :  주문 넣은게 없고 /  청산조건에 걸리지 않아야함.
                if (recent_orders.Where(p => p.side == "Sell" && p.text == "Trad" && p.price == price).Count() == 0)
                {
                    if (bitemex_position.currentQty >= 0 || price < bitemex_position.marginCallPrice)
                    {
                        if (list_bitmex_order.Where(p => p.text == "Trad_End" + price + "Sell").Count() == 0)
                        {
                            if (recent_orders.Where(p => p.text == "Trad_End" + price + "Sell").Count() == 0)
                            {
                                _bitmex_order = new bitmex_order();
                                _bitmex_order.symbol = "XBTUSD";
                                _bitmex_order.side = "Sell";
                                _bitmex_order.orderQty = step_Qty;
                                _bitmex_order.price = price;
                                _bitmex_order.ordType = "Limit";
                                _bitmex_order.text = "Trad";
                                list_bitmex_order.Add(_bitmex_order);
                            }
                        }
                    }
                }
            }
            pre_recent_orders = new List<bitmex_order>();
            pre_recent_orders = recent_orders;
            Boolean __result = false;
            if (list_bitmex_order.Count() > 0)
            {
                try
                {
                    string _result = bitemex.PostOrders_bulk(list_bitmex_order);
                    List<bitmex_order> add_orders = JsonConvert.DeserializeObject<List<bitmex_order>>(_result);
                    foreach (var item in add_orders)
                    {
                        bitmex_order add_bitmex_order = new bitmex_order();
                        add_bitmex_order.orderID = item.orderID;
                        add_bitmex_order.price = item.price;
                        add_bitmex_order.side = item.side;
                        add_bitmex_order.orderQty = item.orderQty;
                        add_bitmex_order.text = item.text;
                        pre_recent_orders.Add(add_bitmex_order);
                    }
                    __result = true;
                }
                catch (Exception)
                {

                }
            }
            try
            {
                if (__result)
                {
                    pre_currentQty = bitemex_position.currentQty;
                    pre_avgCostPrice = (double)bitemex_position.avgCostPrice;

                    for (int i = 20; i < pre_recent_orders.Where(p => p.side == "Buy" && p.text == "Trad").Count(); i++)
                    {
                        string deleteid = pre_recent_orders.Where(p => p.side == "Buy" && p.text == "Trad").OrderByDescending(p => p.price).Skip(i).FirstOrDefault().orderID;
                        bitemex.DeleteOrders_ByID(deleteid, "over trad");
                        if (pre_recent_orders.FirstOrDefault(p => p.orderID == deleteid) != null)
                        {
                            pre_recent_orders.Remove(pre_recent_orders.FirstOrDefault(p => p.orderID == deleteid));
                        }
                    }
                    for (int i = 20; i < pre_recent_orders.Where(p => p.side == "Sell" && p.text == "Trad").Count(); i++)
                    {
                        string deleteid = pre_recent_orders.Where(p => p.side == "Sell" && p.text == "Trad").OrderBy(p => p.price).Skip(i).FirstOrDefault().orderID;
                        bitemex.DeleteOrders_ByID(deleteid, "over trad");
                        if (pre_recent_orders.FirstOrDefault(p => p.orderID == deleteid) != null)
                        {
                            pre_recent_orders.Remove(pre_recent_orders.FirstOrDefault(p => p.orderID == deleteid));
                        }

                    }
                }
            }
            catch (Exception)
            {

            }

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
        }

        private Boolean bitmex_Get_bucketed_2()
        {
            try
            {
                /// [1] 봉 얻어오기
                var result_bucketed = bitemex.bitmex_Get_bucketed("5m", true, "XBTUSD", 3, true);
                List<bitmex_bucketed> bucketeds = new List<bitmex_bucketed>();
                btmex_Bucketeds = JsonConvert.DeserializeObject<List<bitmex_bucketed>>(result_bucketed);
                return true;
            }
            catch (Exception ex)
            {
                txt_position.Text = "봉 불러오기 error";
                return false;
            }
        }
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            btn_Start.Enabled = true;
            btn_Stop.Enabled = false;
            timer1.Enabled = false;
            pre_currentQty = -1;
            pre_avgCostPrice = -1;
            txt_position.AppendText("\r\n타이머가 중지 되었습니다.");
        }


        private Boolean GetPositions()
        {
            try
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
                    }
                    else
                    {
                        bitemex_position.account = _bitemex_positions[0].account;
                        bitemex_position.symbol = _bitemex_positions[0].symbol;
                        bitemex_position.currentQty = _bitemex_positions[0].currentQty;
                        bitemex_position.avgCostPrice = _bitemex_positions[0].avgCostPrice;
                        bitemex_position.marginCallPrice = _bitemex_positions[0].marginCallPrice;
                        bitemex_position.liquidationPrice = _bitemex_positions[0].liquidationPrice;
                    }
                    return true;
                }
                else
                {
                    txt_position.AppendText("포지션이 0개이거나 2개이상임. 확인해볼것~.\r\n");
                    // 0개 혹은 2개도 있나 확인해보자.... 아마 없을것임...
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        private void btn_balance_Click(object sender, EventArgs e)
        {
            try
            {
                user_margin _user_margin = new user_margin();
                _user_margin = JsonConvert.DeserializeObject<user_margin>(bitemex.GetUserMargin());
                txt_position.AppendText("○ " + DateTime.Now.ToString("MM월dd일 HH시mm분") +
                     "         " + string.Format("{0:#,###}", _user_margin.walletBalance * 0.1) +
                     "         " + string.Format("{0:#,###}", _user_margin.marginBalance * 0.1) +
                     "         " + string.Format("{0:#,###}", btmex_Bucketeds[0].close) +
                     "         " + string.Format("{0:#,###}", bitemex_position.avgCostPrice) + " * " +
                                   string.Format("{0:#,###}", bitemex_position.currentQty) + "\r\n");
            }
            catch (Exception)
            {
                //
            }
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
