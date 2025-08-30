namespace RPS
{
    partial class Form2
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            button1 = new Button();
            button2 = new Button();
            counter = new System.Windows.Forms.Timer(components);
            stage_timer = new System.Windows.Forms.Timer(components);
            spining_timer = new System.Windows.Forms.Timer(components);
            pictureBox1 = new PictureBox();
            preparing_timer = new System.Windows.Forms.Timer(components);
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(499, 682);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 5;
            button1.Text = "CARDS";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(730, 682);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 6;
            button2.Text = "SKIP";
            button2.UseVisualStyleBackColor = true;
            button2.Visible = false;
            button2.Click += button2_Click;
            // 
            // counter
            // 
            counter.Interval = 1000;
            counter.Tick += counter_Tick;
            // 
            // stage_timer
            // 
            stage_timer.Interval = 1500;
            stage_timer.Tick += stage_timer_Tick;
            // 
            // spining_timer
            // 
            spining_timer.Interval = 14;
            spining_timer.Tick += spining_timer_Tick;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Location = new Point(674, 240);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 200);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            // 
            // preparing_timer
            // 
            preparing_timer.Interval = 3000;
            preparing_timer.Tick += preparing_timer_Tick;
            // 
            // button3
            // 
            button3.Location = new Point(964, 682);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 8;
            button3.Text = "CARDS";
            button3.UseVisualStyleBackColor = true;
            button3.Visible = false;
            button3.Click += button3_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1533, 744);
            Controls.Add(button3);
            Controls.Add(pictureBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form2";
            FormClosing += Form2_FormClosing;
            FormClosed += Form2_FormClosed;
            Load += Form2_Load;
            Paint += Form2_Paint_1;
            KeyDown += Form2_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private Button button2;
        private System.Windows.Forms.Timer counter;
        private System.Windows.Forms.Timer stage_timer;
        private System.Windows.Forms.Timer spining_timer;
        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer preparing_timer;
        private Button button3;
    }
}
