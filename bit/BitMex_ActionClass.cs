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
        private static string bitmexKey = "1jLqib5tAmDF29DBb8M9292R";
        private static string bitmexSecret = "aI2c215XvsqSa-XkAyP83CmJnPM07Cuw-ysbRYG7pn8BFEHi";


        BitMEXApi bitemex = new BitMEXApi(bitmexKey, bitmexSecret);


        int no = 3000;
        public void order_System3(bitemex_position bitemex_position ,double iniinitial_value,  int manual_Qty)
        {

            //[1-1]  setting
            #region setting
            int step1_Qty; double step1_spring; double _margin1;        // 7025.0      ( 35,125,000)
            int step2_Qty; double step2_spring; double _margin2;        // 7037.5    ( 70,375,000)
            int step3_Qty; double step3_spring; double _margin3;        // 7068.75     (141,375,000)
            int step4_Qty; double step4_spring; double _margin4;
            int step5_Qty; double step5_spring; double _margin5;
            int step6_Qty; double step6_spring; double _margin6;
            int step7_Qty; double step7_spring; double _margin7;
            int step8_Qty; double step8_spring; double _margin8;
            int step9_Qty; double step9_spring; double _margin9;
            int step10_Qty; double step10_spring; double _margin10;


            step1_Qty = 20; step1_spring = 3; _margin1 = 15.0;
            step2_Qty = 22; step2_spring = 5; _margin2 = 22.0;
            step3_Qty = 24; step3_spring = 8; _margin3 = 25.0;
            step4_Qty = 26; step4_spring = 30.0; _margin4 = 40.0;
            step5_Qty = 104; step5_spring = 35.0; _margin5 = 40.0;
            step6_Qty = 105; step6_spring = 55.0; _margin6 = 60.0;
            step7_Qty = 106; step7_spring = 80.0; _margin7 = 100.0;
            step8_Qty = 107; step8_spring = 140.0; _margin8 = 160.0;
            step9_Qty = 108; step9_spring = 180.0; _margin9 = 210.0;
            step10_Qty = 109; step10_spring = 250.0; _margin10 = 250.0;
            #endregion
            //[1-2] setting



            //[2]
            if (bitemex_position.currentQty == manual_Qty)
            {
                //[2-1] 주문 삭제.
                bitemex.DeleteAllOrders("{\"clOrdID\":\"Buy_" + (no - 1) + "\"}");
                bitemex.DeleteAllOrders("{\"clOrdID\":\"Sell_" + (no - 1) + "\"}");


                //[2-2] 주문 넣기
                List<bitmex_order> list_bitmex_order = new List<bitmex_order>();
                bitmex_order _bitmex_order;
                #region step1
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = "Buy_" + no.ToString();
                _bitmex_order.orderQty = step1_Qty;
                _bitmex_order.price = iniinitial_value - step1_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = "Sell_" + no.ToString();
                _bitmex_order.orderQty = step1_Qty;
                _bitmex_order.price = iniinitial_value + step1_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                #endregion
                #region step2
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = "Buy_" + no.ToString();
                _bitmex_order.orderQty = step2_Qty;
                _bitmex_order.price = iniinitial_value - step2_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = "Sell_" + no.ToString();
                _bitmex_order.orderQty = step2_Qty;
                _bitmex_order.price = iniinitial_value + step2_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                #endregion
                #region step3
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = "Buy_" + no.ToString();
                _bitmex_order.orderQty = step3_Qty;
                _bitmex_order.price = iniinitial_value - step3_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = "Sell_" + no.ToString();
                _bitmex_order.orderQty = step3_Qty;
                _bitmex_order.price = iniinitial_value + step3_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                #endregion
                #region step4
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = "Buy_" + no.ToString();
                _bitmex_order.orderQty = step4_Qty;
                _bitmex_order.price = iniinitial_value - step4_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = "Sell_" + no.ToString();
                _bitmex_order.orderQty = step4_Qty;
                _bitmex_order.price = iniinitial_value + step4_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                #endregion
                #region step5
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = "Buy_" + no.ToString();
                _bitmex_order.orderQty = step5_Qty;
                _bitmex_order.price = iniinitial_value - step5_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = "Sell_" + no.ToString();
                _bitmex_order.orderQty = step5_Qty;
                _bitmex_order.price = iniinitial_value + step5_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                #endregion
                #region step6
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = "Buy_" + no.ToString();
                _bitmex_order.orderQty = step6_Qty;
                _bitmex_order.price = iniinitial_value - step6_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = "Sell_" + no.ToString();
                _bitmex_order.orderQty = step6_Qty;
                _bitmex_order.price = iniinitial_value + step6_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                #endregion
                #region step7
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = "Buy_" + no.ToString();
                _bitmex_order.orderQty = step7_Qty;
                _bitmex_order.price = iniinitial_value - step7_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = "Sell_" + no.ToString();
                _bitmex_order.orderQty = step7_Qty;
                _bitmex_order.price = iniinitial_value + step7_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                #endregion
                #region step8
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = "Buy_" + no.ToString();
                _bitmex_order.orderQty = step8_Qty;
                _bitmex_order.price = iniinitial_value - step8_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = "Sell_" + no.ToString();
                _bitmex_order.orderQty = step8_Qty;
                _bitmex_order.price = iniinitial_value + step8_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                #endregion
                #region step9
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = "Buy_" + no.ToString();
                _bitmex_order.orderQty = step9_Qty;
                _bitmex_order.price = iniinitial_value - step9_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = "Sell_" + no.ToString();
                _bitmex_order.orderQty = step9_Qty;
                _bitmex_order.price = iniinitial_value + step9_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                #endregion
                #region step10
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Buy";
                _bitmex_order.clOrdID = "Buy_" + no.ToString();
                _bitmex_order.orderQty = step10_Qty;
                _bitmex_order.price = iniinitial_value - step10_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                _bitmex_order = new bitmex_order();
                _bitmex_order.symbol = "XBTUSD";
                _bitmex_order.side = "Sell";
                _bitmex_order.clOrdID = "Sell_" + no.ToString();
                _bitmex_order.orderQty = step10_Qty;
                _bitmex_order.price = iniinitial_value + step10_spring;
                _bitmex_order.ordType = "Limit";
                list_bitmex_order.Add(_bitmex_order);
                #endregion
                string _result = bitemex.PostOrders_bulk(list_bitmex_order);
                no++;
            }
            else
            {
                string search_recent_clOrdID = "";
                //string search_recent_side = "";  //이거는 활용 안했음=> 초급등후 초급락하면 position이 buy여도 sell도 같이 체결이 된 상태 일수도 있어서...
                if ((bitemex_position.currentQty- manual_Qty) > 0)
                {
                    bitemex.DeleteAllOrders("{\"clOrdID\":\"Sell_" + (no - 1) + "\"}");
                    search_recent_clOrdID = "Buy_" + (no - 1);
                }
                else
                {
                    //chk_currentQty < 0
                    bitemex.DeleteAllOrders("{\"clOrdID\":\"Buy_" + (no - 1) + "\"}");
                    search_recent_clOrdID = "Sell_" + (no - 1);
                }


                var json_result = bitemex.GetOrders("XBTUSD", "", 60, true, "");
                List<bitmex_order>  recent_orders = JsonConvert.DeserializeObject<List<bitmex_order>>(json_result);

                List<bitmex_order> list_bitmex_order = new List<bitmex_order>();
                foreach (var item in recent_orders)
                {
                    if (item.cumQty > 0 && item.clOrdID == search_recent_clOrdID)
                    {
                        double price = 0;
                        if (item.cumQty == step1_Qty)
                        {
                            price = item.side == "Buy" ? item.price + _margin1 : item.price - _margin1;
                        }
                        else if (item.cumQty == step2_Qty)
                        {
                            price = item.side == "Buy" ? item.price + _margin2 : item.price - _margin2;
                        }
                        else if (item.cumQty == step3_Qty)
                        {
                            price = item.side == "Buy" ? item.price + _margin3 : item.price - _margin3;
                        }
                        else if (item.cumQty == step4_Qty)
                        {
                            price = item.side == "Buy" ? item.price + _margin4 : item.price - _margin4;
                        }
                        else if (item.cumQty == step5_Qty)
                        {
                            price = item.side == "Buy" ? item.price + _margin5 : item.price - _margin5;
                        }
                        else if (item.cumQty == step6_Qty)
                        {
                            price = item.side == "Buy" ? item.price + _margin6 : item.price - _margin6;
                        }
                        else if (item.cumQty == step7_Qty)
                        {
                            price = item.side == "Buy" ? item.price + _margin7 : item.price - _margin7;
                        }
                        else if (item.cumQty == step8_Qty)
                        {
                            price = item.side == "Buy" ? item.price + _margin8 : item.price - _margin8;
                        }
                        else if (item.cumQty == step9_Qty)
                        {
                            price = item.side == "Buy" ? item.price + _margin9 : item.price - _margin9;
                        }
                        else if (item.cumQty == step10_Qty)
                        {
                            price = item.side == "Buy" ? item.price + _margin10 : item.price - _margin10;
                        }
                        else
                        {
                            price = item.side == "Buy" ? item.price + _margin3 : item.price - _margin3;
                        }

                        string newOrder_Side = item.side == "Buy" ? "Sell" : "Buy";
                        if (recent_orders.Where(p => p.ordStatus == "New" && p.side == newOrder_Side && p.orderQty == item.cumQty && p.price == price).Count() == 0)
                        {
                            bitmex_order _bitmex_order = new bitmex_order();
                            _bitmex_order.symbol = "XBTUSD";
                            _bitmex_order.side = newOrder_Side;
                            //_bitmex_order.clOrdID = item.clOrdID + "end";
                            _bitmex_order.orderQty = item.cumQty;
                            _bitmex_order.price = price;
                            _bitmex_order.ordType = "Limit";
                            _bitmex_order.text = "end";
                            list_bitmex_order.Add(_bitmex_order);
                        }
                    }
                }
                if (list_bitmex_order.Count() > 0)
                {
                    string _result = bitemex.PostOrders_bulk(list_bitmex_order);
                }
                
            }

        }
    }
}
