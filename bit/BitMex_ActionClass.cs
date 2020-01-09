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
        //// hyunju3414764
        //private static string bitmexKey = "vdWSmeX7xugJPc6B5mq9O2aZ";
        //private static string bitmexSecret = "gePBeNVzkb2V7e2hxCjB-K5zBYduwPpKoP8tfQThNLlE89Bq";


        //BitMEXApi bitemex = new BitMEXApi(bitmexKey, bitmexSecret);


        ////int no = 11055;

        //List<bitmex_order> pre_recent_orders = new List<bitmex_order> ();
        //public void order_System2(bitemex_position bitemex_position, double now_close)
        //{
        //    //[1] setting
        //    int step_Qty; double step_spring; double _margin;
        //    step_Qty = 25; step_spring = 5.0; _margin = 7.0;

        //    //[2] 주문 내역 불러오기
        //    var json_result = bitemex.GetOrders("XBTUSD", "{\"ordStatus\":\"New\"}", 20, true, "");
        //    List<bitmex_order> recent_orders = JsonConvert.DeserializeObject<List<bitmex_order>>(json_result);

        //    //[3] 기준값 정하기
        //    double iniinitial_value = 0;
        //    if (recent_orders.Where(p => p.side == "Buy" && p.text == "Buy").Count() > 0)
        //    {
        //        iniinitial_value = recent_orders.Where(p => p.side == "Buy" && p.text == "Buy").OrderByDescending(p => p.price).FirstOrDefault().price + step_spring;
        //    }
        //    else if (recent_orders.Where(p => p.side == "Sell" && p.text == "Sell").Count() > 0)
        //    {
        //        iniinitial_value = recent_orders.Where(p => p.side == "Sell" && p.text == "Sell").OrderBy(p => p.price).FirstOrDefault().price - step_spring;
        //    }
        //    else
        //    {
        //        iniinitial_value = now_close;
        //    }


        //    //[4] 주문 넣기
        //    List<bitmex_order> list_bitmex_order = new List<bitmex_order>();
        //    bitmex_order _bitmex_order;
        //    for (int i = 0; i < 3; i++)
        //    {
        //        double price = 0;
        //        int step_skip = 0;
        //        //[4-1]
        //        if (now_close < iniinitial_value - step_spring)
        //        {
        //            step_skip = (Int32)Math.Truncate((iniinitial_value - now_close) / step_spring);
        //        }
        //        else
        //        {
        //            step_skip = 0;
        //        }
        //        price = iniinitial_value - step_spring * (i + step_skip + 1);
        //        if (recent_orders.Where(p => p.side == "Buy" && p.text == "Buy" && p.price == price).Count() == 0)
        //        {
        //            if (recent_orders.Where(p => p.side == "Sell" && p.text == "End" && p.price == price+_margin).Count() == 0)
        //            {
        //                if (bitemex_position.currentQty <= 0 || price > bitemex_position.marginCallPrice)
        //                {
        //                    _bitmex_order = new bitmex_order();
        //                    _bitmex_order.symbol = "XBTUSD";
        //                    _bitmex_order.side = "Buy";
        //                    //_bitmex_order.clOrdID = "Buy_" + no.ToString();
        //                    _bitmex_order.orderQty = step_Qty;
        //                    _bitmex_order.price = price;
        //                    _bitmex_order.ordType = "Limit";
        //                    _bitmex_order.text = "Buy";
        //                    list_bitmex_order.Add(_bitmex_order);
        //                }
        //            }
        //        }

        //        //[4-2]
        //        if (now_close > iniinitial_value+ step_spring)
        //        {
        //            step_skip = (Int32)Math.Truncate((now_close - iniinitial_value) / step_spring);
        //        }
        //        else
        //        {
        //            step_skip = 0;
        //        }
        //        price = iniinitial_value + step_spring * (i + step_skip + 1);
        //        if (recent_orders.Where(p => p.side == "Sell" && p.text == "Sell" && p.price == price).Count() == 0)
        //        {
        //            if (recent_orders.Where(p => p.side == "Buy" && p.text == "End" && p.price == price - _margin).Count() == 0)
        //            {
        //                if (bitemex_position.currentQty >= 0 || price < bitemex_position.marginCallPrice)
        //                {
        //                    _bitmex_order = new bitmex_order();
        //                    _bitmex_order.symbol = "XBTUSD";
        //                    _bitmex_order.side = "Sell";
        //                    //_bitmex_order.clOrdID = "Sell_" + no.ToString();
        //                    _bitmex_order.orderQty = step_Qty;
        //                    _bitmex_order.price = price;
        //                    _bitmex_order.ordType = "Limit";
        //                    _bitmex_order.text = "Sell";
        //                    list_bitmex_order.Add(_bitmex_order);
        //                }
        //            }
        //        }
        //    }


        //    //[5] 청산
        //    if (pre_recent_orders != null && pre_recent_orders.Count() > 0)
        //    {
        //        foreach (var item in pre_recent_orders.Where(p => p.text != "end"))
        //        {
        //            if (recent_orders.Where(p => p.orderID == item.orderID).Count() == 0)
        //            {
        //                _bitmex_order = new bitmex_order();
        //                _bitmex_order.symbol = "XBTUSD";
        //                _bitmex_order.side = item.text == "Buy" ? "Sell" : "Buy";
        //                //_bitmex_order.clOrdID = item.text == "Buy" ? "Buy_" + no.ToString() : "Sell_" + no.ToString();
        //                _bitmex_order.orderQty = step_Qty - 1;
        //                _bitmex_order.price = item.text == "Buy" ? item.price + _margin : item.price - _margin;
        //                _bitmex_order.ordType = "Limit";
        //                _bitmex_order.text = "End";
        //                list_bitmex_order.Add(_bitmex_order);
        //            }

        //        }
        //    }


        //    #region 안쓰는기능
        //    //int _BuyCount = recent_orders.Where(p => p.side == "Buy" && p.text == "Buy").Count();
        //    //int _SellCount = recent_orders.Where(p => p.side == "Sell" && p.text == "Sell").Count();

        //    //if (bitemex_position.currentQty == 0)
        //    //{
        //    //    bitemex.DeleteAllOrders();

        //    //    for (int i = 0; i < 2; i++)
        //    //    {
        //    //        _bitmex_order = new bitmex_order();
        //    //        _bitmex_order.symbol = "XBTUSD";
        //    //        _bitmex_order.side = "Buy";
        //    //        _bitmex_order.clOrdID = "Buy_" + no.ToString();
        //    //        _bitmex_order.orderQty = step_Qty;
        //    //        _bitmex_order.price = now_close - step_spring * (i + 1);
        //    //        _bitmex_order.ordType = "Limit";
        //    //        _bitmex_order.text = "Buy";
        //    //        list_bitmex_order.Add(_bitmex_order);
        //    //    }
        //    //    for (int i = 0; i < 2; i++)
        //    //    {
        //    //        _bitmex_order = new bitmex_order();
        //    //        _bitmex_order.symbol = "XBTUSD";
        //    //        _bitmex_order.side = "Sell";
        //    //        _bitmex_order.clOrdID = "Sell_" + no.ToString();
        //    //        _bitmex_order.orderQty = step_Qty;
        //    //        _bitmex_order.price = now_close + step_spring * (i + 1);
        //    //        _bitmex_order.ordType = "Limit";
        //    //        _bitmex_order.text = "Sell";
        //    //        list_bitmex_order.Add(_bitmex_order);
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    double sell_low_price = 0;
        //    //    double sell_high_price = 0;
        //    //    double buy_high_price = 0;
        //    //    double buy_low_price = 0;
        //    //    if (pre_recent_orders != null && pre_recent_orders.Count() > 0)
        //    //    {
        //    //        //청산하기
        //    //        foreach (var item in pre_recent_orders.Where(p => p.text != "end"))
        //    //        {

        //    //            if (recent_orders.Where(p => p.orderID == item.orderID).Count() == 0)
        //    //            {

        //    //                if (item.text == "Buy")
        //    //                {
        //    //                    _bitmex_order = new bitmex_order();
        //    //                    _bitmex_order.symbol = "XBTUSD";
        //    //                    _bitmex_order.side = "Sell";
        //    //                    _bitmex_order.clOrdID = "Sell_" + no.ToString();
        //    //                    _bitmex_order.orderQty = step_Qty;
        //    //                    _bitmex_order.price = item.price + (_margin * 1);
        //    //                    _bitmex_order.ordType = "Limit";
        //    //                    _bitmex_order.text = "end";
        //    //                    list_bitmex_order.Add(_bitmex_order);
        //    //                }
        //    //                else if (item.text == "Sell")
        //    //                {
        //    //                    _bitmex_order = new bitmex_order();
        //    //                    _bitmex_order.symbol = "XBTUSD";
        //    //                    _bitmex_order.side = "Buy";
        //    //                    _bitmex_order.clOrdID = "Buy_" + no.ToString();
        //    //                    _bitmex_order.orderQty = step_Qty;
        //    //                    _bitmex_order.price = item.price - (_margin * 1);
        //    //                    _bitmex_order.ordType = "Limit";
        //    //                    _bitmex_order.text = "end";
        //    //                    list_bitmex_order.Add(_bitmex_order);
        //    //                }
        //    //            }

        //    //        }
        //    //        //청산하기 성공시 재주문.
        //    //        foreach (var item in pre_recent_orders.Where(p => p.text == "end"))
        //    //        {
        //    //            if (recent_orders.Where(p => p.orderID == item.orderID).Count() == 0)
        //    //            {

        //    //                if (item.text == "Buy")
        //    //                {
        //    //                    _bitmex_order = new bitmex_order();
        //    //                    _bitmex_order.symbol = "XBTUSD";
        //    //                    _bitmex_order.side = "Sell";
        //    //                    _bitmex_order.clOrdID = "Sell_" + no.ToString();
        //    //                    _bitmex_order.orderQty = step_Qty;
        //    //                    _bitmex_order.price = item.price + (_margin * 1);
        //    //                    _bitmex_order.ordType = "Limit";
        //    //                    _bitmex_order.text = "Sell";
        //    //                    list_bitmex_order.Add(_bitmex_order);
        //    //                    buy_high_price = buy_high_price > _bitmex_order.price ? buy_high_price : _bitmex_order.price;
        //    //                    buy_low_price = buy_low_price < _bitmex_order.price ? buy_low_price : _bitmex_order.price;
        //    //                    _BuyCount++;
        //    //                }
        //    //                else if (item.text == "Sell")
        //    //                {
        //    //                    _bitmex_order = new bitmex_order();
        //    //                    _bitmex_order.symbol = "XBTUSD";
        //    //                    _bitmex_order.side = "Buy";
        //    //                    _bitmex_order.clOrdID = "Buy_" + no.ToString();
        //    //                    _bitmex_order.orderQty = step_Qty;
        //    //                    _bitmex_order.price = item.price - (_margin * 1);
        //    //                    _bitmex_order.ordType = "Limit";
        //    //                    _bitmex_order.text = "Buy";
        //    //                    list_bitmex_order.Add(_bitmex_order);
        //    //                    sell_high_price = sell_high_price > _bitmex_order.price ? sell_high_price : _bitmex_order.price;
        //    //                    sell_low_price = sell_low_price < _bitmex_order.price ? sell_low_price : _bitmex_order.price;
        //    //                    _SellCount++;
        //    //                }
        //    //            }
        //    //        }
        //    //    }


        //    //    if (recent_orders.Where(p => p.side == "Sell").Count() > 0)
        //    //    {
        //    //        double __sell_low_price = recent_orders.Where(p => p.side == "Sell").OrderBy(p => p.price).FirstOrDefault().price;
        //    //        sell_low_price = sell_low_price != 0 && sell_low_price < __sell_low_price ? sell_low_price : __sell_low_price;
        //    //        double __sell_high_price = recent_orders.Where(p => p.side == "Sell").OrderByDescending(p => p.price).FirstOrDefault().price;
        //    //        sell_high_price = sell_high_price > __sell_high_price ? sell_high_price : __sell_high_price;
        //    //    }
        //    //    else
        //    //    {
        //    //        sell_low_price = now_close;
        //    //        sell_high_price = now_close;
        //    //    }

        //    //    if (recent_orders.Where(p => p.side == "Buy").Count() > 0)
        //    //    {
        //    //        double __buy_high_price = recent_orders.Where(p => p.side == "Buy").OrderByDescending(p => p.price).FirstOrDefault().price;
        //    //        buy_high_price = buy_high_price > __buy_high_price ? buy_high_price : __buy_high_price;
        //    //        double __buy_low_price = recent_orders.Where(p => p.side == "Buy").OrderBy(p => p.price).FirstOrDefault().price;
        //    //        buy_low_price = __buy_low_price != 0 && buy_low_price < __buy_low_price ? buy_low_price : __buy_low_price;
        //    //    }
        //    //    else
        //    //    {
        //    //        buy_high_price = now_close;
        //    //        buy_low_price = now_close;
        //    //    }


        //    //    if (_BuyCount < 7)
        //    //    {
        //    //        for (int i = _BuyCount; i < 7; i++)
        //    //        {
        //    //            _bitmex_order = new bitmex_order();
        //    //            _bitmex_order.symbol = "XBTUSD";
        //    //            _bitmex_order.side = "Buy";
        //    //            _bitmex_order.clOrdID = "Buy_" + no.ToString();
        //    //            _bitmex_order.orderQty = step_Qty;
        //    //            _bitmex_order.price = buy_low_price - step_spring * (i + 1);
        //    //            _bitmex_order.ordType = "Limit";
        //    //            _bitmex_order.text = "Buy";
        //    //            list_bitmex_order.Add(_bitmex_order);
        //    //        }
        //    //    }
        //    //    else if (_BuyCount == 7)
        //    //    {

        //    //    }
        //    //    else
        //    //    {
        //    //        for (int i = 7; i < _BuyCount; i++)
        //    //        {
        //    //            string _id = recent_orders.Where(p => p.side == "Buy").OrderBy(p => p.price).Skip(i).FirstOrDefault().orderID;
        //    //            bitemex.DeleteOrders_ByID(_id, "cancel order by ID");
        //    //        }
        //    //    }

        //    //    if (_SellCount < 7)
        //    //    {
        //    //        for (int i = _SellCount; i < 7; i++)
        //    //        {
        //    //            _bitmex_order = new bitmex_order();
        //    //            _bitmex_order.symbol = "XBTUSD";
        //    //            _bitmex_order.side = "Sell";
        //    //            _bitmex_order.clOrdID = "Sell_" + no.ToString();
        //    //            _bitmex_order.orderQty = step_Qty;
        //    //            _bitmex_order.price = sell_high_price + step_spring * (i + 1);
        //    //            _bitmex_order.ordType = "Limit";
        //    //            _bitmex_order.text = "Sell";
        //    //            list_bitmex_order.Add(_bitmex_order);
        //    //        }
        //    //    }
        //    //    else if (_SellCount == 7)
        //    //    {

        //    //    }
        //    //    else
        //    //    {
        //    //        for (int i = 7; i < _SellCount; i++)
        //    //        {
        //    //            string _id = recent_orders.Where(p => p.side == "Sell").OrderByDescending(p => p.price).Skip(i).FirstOrDefault().orderID;
        //    //            bitemex.DeleteOrders_ByID(_id, "cancel order by ID");
        //    //        }
        //    //    }





        //    //} 
        //    #endregion


        //    if (list_bitmex_order.Count() > 0)
        //    {
        //        string _result = bitemex.PostOrders_bulk(list_bitmex_order);
        //        string asdf = "";
        //    }
        //    json_result = bitemex.GetOrders("XBTUSD", "{\"ordStatus\":\"New\"}", 20, true, "");
        //    pre_recent_orders = new List<bitmex_order>();
        //    pre_recent_orders = JsonConvert.DeserializeObject<List<bitmex_order>>(json_result);
        //    //no++;
        //}
    }
}
