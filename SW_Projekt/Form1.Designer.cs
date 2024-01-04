namespace SW_Projekt
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
            this.pB1 = new System.Windows.Forms.PictureBox();
            this.bFromCamera = new System.Windows.Forms.Button();
            this.bFromFile = new System.Windows.Forms.Button();
            this.bBrowseFilesPB1 = new System.Windows.Forms.Button();
            this.tBImagePathPB1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bCalculate = new System.Windows.Forms.Button();
            this.tBResult = new System.Windows.Forms.TextBox();
            this.pB2 = new System.Windows.Forms.PictureBox();
            this.bRight = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tBResult2_Decimal = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.bErode = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pB1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // pB1
            // 
            this.pB1.BackColor = System.Drawing.Color.Black;
            this.pB1.Location = new System.Drawing.Point(12, 95);
            this.pB1.Name = "pB1";
            this.pB1.Size = new System.Drawing.Size(640, 480);
            this.pB1.TabIndex = 0;
            this.pB1.TabStop = false;
            // 
            // bFromCamera
            // 
            this.bFromCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bFromCamera.Location = new System.Drawing.Point(510, 65);
            this.bFromCamera.Name = "bFromCamera";
            this.bFromCamera.Size = new System.Drawing.Size(142, 26);
            this.bFromCamera.TabIndex = 1;
            this.bFromCamera.Text = "From Camera";
            this.bFromCamera.UseVisualStyleBackColor = true;
            this.bFromCamera.Click += new System.EventHandler(this.bFromCamera_Click);
            // 
            // bFromFile
            // 
            this.bFromFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bFromFile.Location = new System.Drawing.Point(510, 24);
            this.bFromFile.Name = "bFromFile";
            this.bFromFile.Size = new System.Drawing.Size(142, 25);
            this.bFromFile.TabIndex = 2;
            this.bFromFile.Text = "From File";
            this.bFromFile.UseVisualStyleBackColor = true;
            this.bFromFile.Click += new System.EventHandler(this.bFromFile_click);
            // 
            // bBrowseFilesPB1
            // 
            this.bBrowseFilesPB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bBrowseFilesPB1.Location = new System.Drawing.Point(399, 24);
            this.bBrowseFilesPB1.Margin = new System.Windows.Forms.Padding(4);
            this.bBrowseFilesPB1.Name = "bBrowseFilesPB1";
            this.bBrowseFilesPB1.Size = new System.Drawing.Size(37, 25);
            this.bBrowseFilesPB1.TabIndex = 26;
            this.bBrowseFilesPB1.Text = "...";
            this.bBrowseFilesPB1.UseVisualStyleBackColor = true;
            this.bBrowseFilesPB1.Click += new System.EventHandler(this.bBrowseFilesPB1_click);
            // 
            // tBImagePathPB1
            // 
            this.tBImagePathPB1.Location = new System.Drawing.Point(69, 24);
            this.tBImagePathPB1.Margin = new System.Windows.Forms.Padding(4);
            this.tBImagePathPB1.Name = "tBImagePathPB1";
            this.tBImagePathPB1.Size = new System.Drawing.Size(328, 22);
            this.tBImagePathPB1.TabIndex = 25;
            this.tBImagePathPB1.Text = "img/Rotated/prostokat.png";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Path:";
            // 
            // bCalculate
            // 
            this.bCalculate.Location = new System.Drawing.Point(658, 581);
            this.bCalculate.Name = "bCalculate";
            this.bCalculate.Size = new System.Drawing.Size(85, 23);
            this.bCalculate.TabIndex = 27;
            this.bCalculate.Text = "Calculate";
            this.bCalculate.UseVisualStyleBackColor = true;
            this.bCalculate.Click += new System.EventHandler(this.bCalculate_Click);
            // 
            // tBResult
            // 
            this.tBResult.Location = new System.Drawing.Point(530, 610);
            this.tBResult.Name = "tBResult";
            this.tBResult.Size = new System.Drawing.Size(338, 22);
            this.tBResult.TabIndex = 28;
            // 
            // pB2
            // 
            this.pB2.BackColor = System.Drawing.Color.Black;
            this.pB2.Location = new System.Drawing.Point(739, 95);
            this.pB2.Name = "pB2";
            this.pB2.Size = new System.Drawing.Size(640, 480);
            this.pB2.TabIndex = 29;
            this.pB2.TabStop = false;
            // 
            // bRight
            // 
            this.bRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bRight.Location = new System.Drawing.Point(658, 292);
            this.bRight.Name = "bRight";
            this.bRight.Size = new System.Drawing.Size(75, 52);
            this.bRight.TabIndex = 30;
            this.bRight.Text = "→";
            this.bRight.UseVisualStyleBackColor = true;
            this.bRight.Click += new System.EventHandler(this.bRight_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(739, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(288, 24);
            this.button1.TabIndex = 31;
            this.button1.Text = "Check Before Calculating";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.bCheck_Click);
            // 
            // tBResult2_Decimal
            // 
            this.tBResult2_Decimal.Location = new System.Drawing.Point(947, 610);
            this.tBResult2_Decimal.Name = "tBResult2_Decimal";
            this.tBResult2_Decimal.Size = new System.Drawing.Size(338, 22);
            this.tBResult2_Decimal.TabIndex = 32;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(739, 25);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown1.TabIndex = 33;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(890, 24);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown2.TabIndex = 34;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(739, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 35;
            this.label2.Text = "Contrast";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(887, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 36;
            this.label3.Text = "Brightness";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1044, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 38;
            this.label4.Text = "Threshold";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(1047, 23);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown3.TabIndex = 37;
            this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // bErode
            // 
            this.bErode.Location = new System.Drawing.Point(1198, 23);
            this.bErode.Name = "bErode";
            this.bErode.Size = new System.Drawing.Size(87, 26);
            this.bErode.TabIndex = 39;
            this.bErode.Text = "Erode";
            this.bErode.UseVisualStyleBackColor = true;
            this.bErode.Click += new System.EventHandler(this.bErode_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(676, 272);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 17);
            this.label6.TabIndex = 41;
            this.label6.Text = "Auto";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1198, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 27);
            this.button2.TabIndex = 42;
            this.button2.Text = "Rotate";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.bRotate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 650);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bErode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.tBResult2_Decimal);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bRight);
            this.Controls.Add(this.pB2);
            this.Controls.Add(this.tBResult);
            this.Controls.Add(this.bCalculate);
            this.Controls.Add(this.bBrowseFilesPB1);
            this.Controls.Add(this.tBImagePathPB1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bFromFile);
            this.Controls.Add(this.bFromCamera);
            this.Controls.Add(this.pB1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pB1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pB1;
        private System.Windows.Forms.Button bFromCamera;
        private System.Windows.Forms.Button bFromFile;
        private System.Windows.Forms.Button bBrowseFilesPB1;
        private System.Windows.Forms.TextBox tBImagePathPB1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bCalculate;
        private System.Windows.Forms.TextBox tBResult;
        private System.Windows.Forms.PictureBox pB2;
        private System.Windows.Forms.Button bRight;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tBResult2_Decimal;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Button bErode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
    }
}

