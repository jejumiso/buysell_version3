//using ServiceStack.Text;
using bit.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace BitMEX
{
    public class OrderBookItem
    {
        public string Symbol { get; set; }
        public int Level { get; set; }
        public int BidSize { get; set; }
        public decimal BidPrice { get; set; }
        public int AskSize { get; set; }
        public decimal AskPrice { get; set; }
        public DateTime Timestamp { get; set; }
    }

    


    public class BitMEXApi
    {
        private const string domain = "https://www.bitmex.com";
        private string apiKey;
        private string apiSecret;
        private int rateLimit;

        public BitMEXApi(string bitmexKey = "", string bitmexSecret = "", int rateLimit = 5000)
        {
            this.apiKey = bitmexKey;
            this.apiSecret = bitmexSecret;
            this.rateLimit = rateLimit;
        }

        private string BuildQueryData(Dictionary<string, string> param)
        {
            if (param == null)
                return "";

            StringBuilder b = new StringBuilder();
            foreach (var item in param)
                b.Append(string.Format("&{0}={1}", item.Key, WebUtility.UrlEncode(item.Value)));

            try { return b.ToString().Substring(1); }
            catch (Exception) { return ""; }
        }

        private string BuildJSON(Dictionary<string, string> param)
        {
            if (param == null)
                return "";

            var entries = new List<string>();
            foreach (var item in param)
                entries.Add(string.Format("\"{0}\":\"{1}\"", item.Key, item.Value));

            return "{" + string.Join(",", entries) + "}";
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private long GetExpires()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 3600; // set expires one hour in the future
        }

        private string Query(string method, string function, Dictionary<string, string> param = null, bool auth = false, bool json = false)
        {
            string paramData = json ? BuildJSON(param) : BuildQueryData(param);
            string url = "/api/v1" + function + ((method == "GET" && paramData != "") ? "?" + paramData : "");
            string postData = (method != "GET") ? paramData : "";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(domain + url);
            webRequest.Method = method;

            if (auth)
            {
                string expires = GetExpires().ToString();
                string message = method + url + expires + postData;
                byte[] signatureBytes = hmacsha256(Encoding.UTF8.GetBytes(apiSecret), Encoding.UTF8.GetBytes(message));
                string signatureString = ByteArrayToString(signatureBytes);

                webRequest.Headers.Add("api-expires", expires);
                webRequest.Headers.Add("api-key", apiKey);
                webRequest.Headers.Add("api-signature", signatureString);
            }

            try
            {
                if (postData != "")
                {
                    webRequest.ContentType = json ? "application/json" : "application/x-www-form-urlencoded";
                    var data = Encoding.UTF8.GetBytes(postData);
                    using (var stream = webRequest.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }

                using (WebResponse webResponse = webRequest.GetResponse())
                using (Stream str = webResponse.GetResponseStream())
                using (StreamReader sr = new StreamReader(str))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (WebException wex)
            {
                using (HttpWebResponse response = (HttpWebResponse)wex.Response)
                {
                    if (response == null)
                        throw;

                    using (Stream str = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(str))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        //public List<OrderBookItem> GetOrderBook(string symbol, int depth)
        //{
        //    var param = new Dictionary<string, string>();
        //    param["symbol"] = symbol;
        //    param["depth"] = depth.ToString();
        //    string res = Query("GET", "/orderBook", param);
        //    return JsonSerializer.DeserializeFromString<List<OrderBookItem>>(res);
        //}



        public string GetOrders(string symbol, string filter ,int count, Boolean reverse,string text)
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = symbol;
            param["filter"] = filter;
            //param["columns"] = "";
            param["count"] = count.ToString();
            //param["start"] = 0.ToString();
            param["reverse"] = reverse.ToString();
            //param["startTime"] = "";
            param["text"] = text;
            string result = Query("GET", "/order", param, true);
            return result;
        }

        public string bitmex_Get_bucketed(string binSize, Boolean partial, string symbol, int count, Boolean reverse)
        {
            var param = new Dictionary<string, string>();
            param["binSize"] = binSize;
            param["partial"] = partial.ToString();
            param["symbol"] = symbol;
            param["count"] = count.ToString();
            param["reverse"] = reverse.ToString();
            string result = Query("GET", "/trade/bucketed", param, true);
            return result;
        }




        public string GetPositions(string filter)
        {
            var param = new Dictionary<string, string>();
            param["filter"] = filter;

            string result = Query("GET", "/position", param, true);
            return result;
        }

        public string GetUserMargin()
        {
            var param = new Dictionary<string, string>();
            param["currency"] = "XBt";
            string result = Query("GET", "/user/margin", param, true);
            return result;
        }


        public string PostOrders(string symbol,string side,int orderQty,double price, string ordType)
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = symbol;
            param["side"] = side;  //Buy Sell
            param["orderQty"] = orderQty.ToString();
            param["price"] = price.ToString();
            param["ordType"] = ordType; //Market
            return Query("POST", "/order", param, true);
        }

        public string PostOrders(string symbol, string side, int orderQty, double price, string ordType, string text)
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = symbol;
            param["side"] = side;  //Buy Sell
            param["orderQty"] = orderQty.ToString();
            param["price"] = price.ToString();
            param["ordType"] = ordType; //Market
            param["text"] = text; //Market
            return Query("POST", "/order", param, true);
        }

        public string PostOrders_bulk(List<bitmex_order> list_bitmex_order)
        {
            var param = new Dictionary<string, string>();
            param["orders"] = JsonConvert.SerializeObject(list_bitmex_order);
            return Query("POST", "/order/bulk", param, true);
        }

        public string PostOrders_PUT(string orderID, double price, int orderQty)
        {
            var param = new Dictionary<string, string>();
            param["orderID"] = orderID;
            param["price"] = price.ToString();
            param["orderQty"] = orderQty.ToString();  //Buy Sell
            
            return Query("PUT", "/order", param, true);
        }

        public string DeleteOrders()
        {
            var param = new Dictionary<string, string>();
            param["orderID"] = "de709f12-2f24-9a36-b047-ab0ff090f0bb";
            param["text"] = "cancel order by ID";
            return Query("DELETE", "/order", param, true, true);
        }
        public string DeleteAllOrders()
        {
            var param = new Dictionary<string, string>();
            //param["orderID"] = "de709f12-2f24-9a36-b047-ab0ff090f0bb";
            //param["text"] = "cancel order by ID";
            return Query("DELETE", "/order/all", param, true, true);
        }
        public string DeleteAllOrders(string filter)
        {            
            var param = new Dictionary<string, string>();
            param["symbol"] = "XBTUSD";          
            param["filter"] = filter;
            string resut = Query("DELETE", "/order/all", param, true);
            return resut;
        }

        public string DeleteAllOrders(string filter,string text)
        {
            var param = new Dictionary<string, string>();
            param["text"] = text;
            param["filter"] = filter;
            string resut = Query("DELETE", "/order/all", param, true);
            return resut;
        }


        private byte[] hmacsha256(byte[] keyByte, byte[] messageBytes)
        {
            using (var hash = new HMACSHA256(keyByte))
            {
                return hash.ComputeHash(messageBytes);
            }
        }

        #region RateLimiter

        private long lastTicks = 0;
        private object thisLock = new object();

        private void RateLimit()
        {
            lock (thisLock)
            {
                long elapsedTicks = DateTime.Now.Ticks - lastTicks;
                var timespan = new TimeSpan(elapsedTicks);
                if (timespan.TotalMilliseconds < rateLimit)
                    Thread.Sleep(rateLimit - (int)timespan.TotalMilliseconds);
                lastTicks = DateTime.Now.Ticks;
            }
        }

        #endregion RateLimiter
    }
}
