
namespace Fnbr_Testing
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.SidTXTbox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.AuthCode_textBox = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.ExchangeTextBox = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(44, 257);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(413, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "Device Code Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.DeviceCode_Login);
            // 
            // SidTXTbox
            // 
            this.SidTXTbox.Location = new System.Drawing.Point(44, 50);
            this.SidTXTbox.Name = "SidTXTbox";
            this.SidTXTbox.Size = new System.Drawing.Size(320, 20);
            this.SidTXTbox.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(370, 45);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 28);
            this.button2.TabIndex = 2;
            this.button2.Text = "Sid Loggin";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SidLogin_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(370, 94);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 39);
            this.button3.TabIndex = 4;
            this.button3.Text = "Authorization Code Login";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.AuthCode_Click);
            // 
            // AuthCode_textBox
            // 
            this.AuthCode_textBox.Location = new System.Drawing.Point(44, 104);
            this.AuthCode_textBox.Name = "AuthCode_textBox";
            this.AuthCode_textBox.Size = new System.Drawing.Size(320, 20);
            this.AuthCode_textBox.TabIndex = 3;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(371, 154);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(87, 39);
            this.button4.TabIndex = 6;
            this.button4.Text = "Exchange Code Login";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Exchange_Click);
            // 
            // ExchangeTextBox
            // 
            this.ExchangeTextBox.Location = new System.Drawing.Point(45, 164);
            this.ExchangeTextBox.Name = "ExchangeTextBox";
            this.ExchangeTextBox.Size = new System.Drawing.Size(320, 20);
            this.ExchangeTextBox.TabIndex = 5;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(370, 214);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(87, 27);
            this.button5.TabIndex = 8;
            this.button5.Text = "Refresh Login";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(45, 218);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(320, 20);
            this.textBox4.TabIndex = 7;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(43, 300);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(414, 35);
            this.button6.TabIndex = 9;
            this.button6.Text = "Device Auth Login";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 347);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.ExchangeTextBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.AuthCode_textBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.SidTXTbox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox SidTXTbox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox AuthCode_textBox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox ExchangeTextBox;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button6;
    }
}

