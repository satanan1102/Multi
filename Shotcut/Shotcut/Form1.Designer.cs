namespace Shotcut
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.Choose_botton = new System.Windows.Forms.Button();
            this.Run_botton = new System.Windows.Forms.Button();
            this.Search_button = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Thredshold_botton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // Choose_botton
            // 
            this.Choose_botton.Location = new System.Drawing.Point(436, 28);
            this.Choose_botton.Name = "Choose_botton";
            this.Choose_botton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Choose_botton.Size = new System.Drawing.Size(75, 23);
            this.Choose_botton.TabIndex = 0;
            this.Choose_botton.Text = "Choose";
            this.Choose_botton.UseVisualStyleBackColor = true;
            this.Choose_botton.Click += new System.EventHandler(this.Choose_botton_Click);
            // 
            // Run_botton
            // 
            this.Run_botton.Location = new System.Drawing.Point(527, 28);
            this.Run_botton.Name = "Run_botton";
            this.Run_botton.Size = new System.Drawing.Size(75, 23);
            this.Run_botton.TabIndex = 1;
            this.Run_botton.Text = "Run";
            this.Run_botton.UseVisualStyleBackColor = true;
            this.Run_botton.Click += new System.EventHandler(this.Run_Click);
            // 
            // Search_button
            // 
            this.Search_button.Location = new System.Drawing.Point(698, 383);
            this.Search_button.Name = "Search_button";
            this.Search_button.Size = new System.Drawing.Size(75, 23);
            this.Search_button.TabIndex = 2;
            this.Search_button.Text = "Search";
            this.Search_button.UseVisualStyleBackColor = true;
            this.Search_button.Click += new System.EventHandler(this.Search_button_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(27, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(390, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(27, 77);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(575, 329);
            this.chart1.TabIndex = 4;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(644, 77);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(183, 20);
            this.textBox2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(644, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "ใส่ค่า Threadshold";
            // 
            // Thredshold_botton
            // 
            this.Thredshold_botton.Location = new System.Drawing.Point(698, 114);
            this.Thredshold_botton.Name = "Thredshold_botton";
            this.Thredshold_botton.Size = new System.Drawing.Size(75, 23);
            this.Thredshold_botton.TabIndex = 7;
            this.Thredshold_botton.Text = "Ok";
            this.Thredshold_botton.UseVisualStyleBackColor = true;
            this.Thredshold_botton.Click += new System.EventHandler(this.Thredshold_botton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 448);
            this.Controls.Add(this.Thredshold_botton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Search_button);
            this.Controls.Add(this.Run_botton);
            this.Controls.Add(this.Choose_botton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Choose_botton;
        private System.Windows.Forms.Button Run_botton;
        private System.Windows.Forms.Button Search_button;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Thredshold_botton;
    }
}

