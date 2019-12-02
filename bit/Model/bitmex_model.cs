using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bit.Model
{
    public class bitmex_bucketed
    {
        public string timestamp { get; set; }
        public string symbol { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public string trades { get; set; }
        public string volume { get; set; }
        public string vwap { get; set; }
        public string lastSize { get; set; }
        public string turnover { get; set; }
        public string homeNotional { get; set; }
        public string foreignNotional { get; set; }
    }
    public class bitemex_position
    {
        public int account { get; set; }
        public string symbol { get; set; }
        public string currency { get; set; }
        public string underlying { get; set; }
        public string quoteCurrency { get; set; }
        public string commission { get; set; }
        public string initMarginReq { get; set; }
        public string maintMarginReq { get; set; }
        public string riskLimit { get; set; }
        public string leverage { get; set; }
        public string prevClosePrice { get; set; }
        public string openingTimestamp { get; set; }
        public string openingQty { get; set; }
        public string openingCost { get; set; }
        public string openingComm { get; set; }
        public string openOrderBuyQty { get; set; }
        public string openOrderBuyCost { get; set; }
        public string currentTimestamp { get; set; }
        public int currentQty { get; set; }    // 규모
        //public double currentCost { get; set; }
        public string isOpen { get; set; }
        public string markPrice { get; set; }
        public string foreignNotional { get; set; }
        public double? avgCostPrice { get; set; }    // 매매 가격 : 
        public string avgEntryPrice { get; set; }   // 매매 가격 :
        public string breakEvenPrice { get; set; }
        public string marginCallPrice { get; set; }  // 청산 가격 
        public string liquidationPrice { get; set; } // 청산 가격
        public string bankruptPrice { get; set; }
        public string timestamp { get; set; }
        public string lastPrice { get; set; }
    }

    public class bitmex_order
    {
        public string orderID { get; set; }
        public string account { get; set; }
        public string symbol { get; set; }
        public string side { get; set; }       //Buy Sell
        public int orderQty { get; set; }    // 주문 수량
        public double price { get; set; }       // 주문 가격?
        public string ordType { get; set; }
        public string ordStatus { get; set; }
        public int leavesQty { get; set; }   // 미체결량?
        public int cumQty { get; set; }      // 체결량?
        public string transactTime { get; set; }
        public string timestamp { get; set; }
    }


}
