namespace RPS
{
    partial class Info
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
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Cursor = Cursors.IBeam;
            label1.Font = new Font("Arial", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(39, 125);
            label1.Name = "label1";
            label1.Size = new Size(147, 53);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Comic Sans MS", 48F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(539, 9);
            label2.Name = "label2";
            label2.Size = new Size(429, 112);
            label2.TabIndex = 1;
            label2.Text = "СПРАВКА";
            // 
            // button1
            // 
            button1.Font = new Font("Comic Sans MS", 36F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button1.Location = new Point(895, 621);
            button1.Name = "button1";
            button1.Size = new Size(64, 64);
            button1.TabIndex = 2;
            button1.Text = ">";
            button1.UseCompatibleTextRendering = true;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Comic Sans MS", 36F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button2.Location = new Point(640, 621);
            button2.Name = "button2";
            button2.Size = new Size(64, 64);
            button2.TabIndex = 3;
            button2.Text = "<";
            button2.UseCompatibleTextRendering = true;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(777, 636);
            label3.Name = "label3";
            label3.Size = new Size(57, 35);
            label3.TabIndex = 4;
            label3.Text = "1/2";
            // 
            // Info
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1515, 697);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "Info";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Info";
            Load += Info_Load;
            KeyDown += Info_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
        private Label label3;
    }
}