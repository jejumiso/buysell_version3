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

        // hyunju3414764
        private static string bitmexKey = "vdWSmeX7xugJPc6B5mq9O2aZ";
        private static string bitmexSecret = "gePBeNVzkb2V7e2hxCjB-K5zBYduwPpKoP8tfQThNLlE89Bq";

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
            Auto_Trad_Play();
        }

        int timeloop = 10;  //고정값
        int pre_currentQty = -1;
        double pre_avgCostPrice = -1;
        //double iniinitial_value = 0.0;
        //int mody_Qty1 = 0;  // 직접 주문 넣은거의 주문 갯수
        //int mody_Qty2 = 0;  // 최초 한번 값 구하고 바뀌지 않음  : 0으로 계속 만들기 귀찮아서 만든거임
        private void Auto_Trad_Play()
        {

            //[1] postion변화 감지
            Boolean return_position_result = GetPositions();
            
            //[2]
            if (return_position_result)   //postion불러오기를 성공 할시...
            {
                if (pre_currentQty != bitemex_position.currentQty || pre_avgCostPrice != bitemex_position.avgCostPrice)
                {
                    if (bitmex_Get_bucketed_2())
                    {
                        //[-]
                        pre_currentQty = bitemex_position.currentQty;
                        pre_avgCostPrice = (double)bitemex_position.avgCostPrice;
                        //if (bitemex_position.currentQty == 0)
                        //{
                        //    iniinitial_value = (btmex_Bucketeds[2].open + btmex_Bucketeds[1].close) / 2;
                        //    iniinitial_value = Math.Ceiling(iniinitial_value * 2) / 2;
                        //}
                        //[1]
                        double now_close = btmex_Bucketeds[0].close;
                        //[2]
                        //bitmex_ActionClass.order_System3(bitemex_position, iniinitial_value, now_close);
                        bitmex_ActionClass.order_System2(bitemex_position, now_close);
                    }
                }
                else
                {
                    //포지션의 변화가 없으므로 empty
                }
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
                throw;
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
            user_margin _user_margin = new user_margin();
            _user_margin = JsonConvert.DeserializeObject<user_margin>(bitemex.GetUserMargin());
            txt_position.AppendText("○ " + DateTime.Now.ToString("MM월dd일 HH시mm분") +
                 "         " + string.Format("{0:#,###}", _user_margin.walletBalance * 0.1) +
                 "         " + string.Format("{0:#,###}", _user_margin.marginBalance * 0.1) + "\r\n");
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
