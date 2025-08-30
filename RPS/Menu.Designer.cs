namespace RPS
{
    partial class Menu
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
            button1 = new Button();
            button2 = new Button();
            pictureBox1 = new PictureBox();
            button3 = new Button();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Cursor = Cursors.Hand;
            button1.Font = new Font("Comic Sans MS", 36F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button1.Location = new Point(633, 373);
            button1.Name = "button1";
            button1.Size = new Size(285, 80);
            button1.TabIndex = 0;
            button1.Text = "1 ИГРОК";
            button1.UseCompatibleTextRendering = true;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Cursor = Cursors.Hand;
            button2.Font = new Font("Comic Sans MS", 36F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button2.Location = new Point(599, 495);
            button2.Name = "button2";
            button2.Size = new Size(342, 80);
            button2.TabIndex = 1;
            button2.Text = "2 ИГРОКА";
            button2.UseCompatibleTextRendering = true;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Location = new Point(645, 38);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(256, 256);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // button3
            // 
            button3.Cursor = Cursors.Hand;
            button3.Font = new Font("Comic Sans MS", 36F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button3.Location = new Point(12, 632);
            button3.Name = "button3";
            button3.Size = new Size(100, 100);
            button3.TabIndex = 3;
            button3.Text = "i";
            button3.UseCompatibleTextRendering = true;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Cursor = Cursors.Hand;
            button4.Font = new Font("Comic Sans MS", 36F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button4.Location = new Point(633, 619);
            button4.Name = "button4";
            button4.Size = new Size(279, 80);
            button4.TabIndex = 4;
            button4.Text = "ВЫХОД";
            button4.UseCompatibleTextRendering = true;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1533, 744);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(pictureBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "Menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu";
            Load += Menu_Load;
            KeyDown += Menu_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private PictureBox pictureBox1;
        private Button button3;
        private Button button4;
    }
}