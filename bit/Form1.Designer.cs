namespace bit
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Stop = new System.Windows.Forms.Button();
            this.txtBox = new System.Windows.Forms.TextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.txt_position = new System.Windows.Forms.TextBox();
            this.txt_count = new System.Windows.Forms.Label();
            this.txt_order = new System.Windows.Forms.TextBox();
            this.btn_test1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(165, 465);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(75, 30);
            this.btn_Stop.TabIndex = 1;
            this.btn_Stop.Text = "중지";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // txtBox
            // 
            this.txtBox.Location = new System.Drawing.Point(12, 12);
            this.txtBox.Multiline = true;
            this.txtBox.Name = "txtBox";
            this.txtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBox.Size = new System.Drawing.Size(282, 432);
            this.txtBox.TabIndex = 2;
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(56, 465);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 30);
            this.btn_Start.TabIndex = 3;
            this.btn_Start.Text = "시작";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click_1);
            // 
            // txt_position
            // 
            this.txt_position.Location = new System.Drawing.Point(331, 12);
            this.txt_position.Multiline = true;
            this.txt_position.Name = "txt_position";
            this.txt_position.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_position.Size = new System.Drawing.Size(392, 206);
            this.txt_position.TabIndex = 4;
            // 
            // txt_count
            // 
            this.txt_count.AutoSize = true;
            this.txt_count.Location = new System.Drawing.Point(615, 474);
            this.txt_count.Name = "txt_count";
            this.txt_count.Size = new System.Drawing.Size(38, 12);
            this.txt_count.TabIndex = 5;
            this.txt_count.Text = "label1";
            // 
            // txt_order
            // 
            this.txt_order.Location = new System.Drawing.Point(331, 224);
            this.txt_order.Multiline = true;
            this.txt_order.Name = "txt_order";
            this.txt_order.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_order.Size = new System.Drawing.Size(392, 220);
            this.txt_order.TabIndex = 6;
            // 
            // btn_test1
            // 
            this.btn_test1.Location = new System.Drawing.Point(372, 465);
            this.btn_test1.Name = "btn_test1";
            this.btn_test1.Size = new System.Drawing.Size(140, 30);
            this.btn_test1.TabIndex = 7;
            this.btn_test1.Text = "테스트";
            this.btn_test1.UseVisualStyleBackColor = true;
            this.btn_test1.Click += new System.EventHandler(this.btn_test1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 510);
            this.Controls.Add(this.btn_test1);
            this.Controls.Add(this.txt_order);
            this.Controls.Add(this.txt_count);
            this.Controls.Add(this.txt_position);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.txtBox);
            this.Controls.Add(this.btn_Stop);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.TextBox txtBox;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.TextBox txt_position;
        private System.Windows.Forms.Label txt_count;
        private System.Windows.Forms.TextBox txt_order;
        private System.Windows.Forms.Button btn_test1;
    }
}

