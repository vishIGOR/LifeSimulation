using System.ComponentModel;

namespace LifeSimulation
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.buttonFinish = new System.Windows.Forms.Button();
            this.labelWidth = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonRight = new System.Windows.Forms.Button();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.resolutionSelector = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TickCounter = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericAnimalsNumber = new System.Windows.Forms.NumericUpDown();
            this.numericPlantsPercent = new System.Windows.Forms.NumericUpDown();
            this.labelPlants = new System.Windows.Forms.Label();
            this.labelAnimals = new System.Windows.Forms.Label();
            this.numericHeight = new System.Windows.Forms.NumericUpDown();
            this.numericWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureMap = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.numericAnimalsNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericPlantsPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.pictureMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonFinish
            // 
            this.buttonFinish.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFinish.Location = new System.Drawing.Point(4, 790);
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.Size = new System.Drawing.Size(169, 40);
            this.buttonFinish.TabIndex = 0;
            this.buttonFinish.Text = "Закончить симуляцию";
            this.buttonFinish.UseVisualStyleBackColor = true;
            this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
            // 
            // labelWidth
            // 
            this.labelWidth.Location = new System.Drawing.Point(4, 10);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(169, 20);
            this.labelWidth.TabIndex = 1;
            this.labelWidth.Text = "Ширина:";
            // 
            // labelHeight
            // 
            this.labelHeight.Location = new System.Drawing.Point(4, 56);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(169, 20);
            this.labelHeight.TabIndex = 3;
            this.labelHeight.Text = "Высота:";
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStart.Location = new System.Drawing.Point(4, 744);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(169, 40);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "Начать симуляцию";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(6, 6);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonRight);
            this.splitContainer1.Panel1.Controls.Add(this.buttonLeft);
            this.splitContainer1.Panel1.Controls.Add(this.buttonDown);
            this.splitContainer1.Panel1.Controls.Add(this.buttonUp);
            this.splitContainer1.Panel1.Controls.Add(this.resolutionSelector);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.TickCounter);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.numericAnimalsNumber);
            this.splitContainer1.Panel1.Controls.Add(this.numericPlantsPercent);
            this.splitContainer1.Panel1.Controls.Add(this.labelPlants);
            this.splitContainer1.Panel1.Controls.Add(this.labelAnimals);
            this.splitContainer1.Panel1.Controls.Add(this.numericHeight);
            this.splitContainer1.Panel1.Controls.Add(this.numericWidth);
            this.splitContainer1.Panel1.Controls.Add(this.labelWidth);
            this.splitContainer1.Panel1.Controls.Add(this.buttonStart);
            this.splitContainer1.Panel1.Controls.Add(this.buttonFinish);
            this.splitContainer1.Panel1.Controls.Add(this.labelHeight);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDown1);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDown2);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDown3);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDown4);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureMap);
            this.splitContainer1.Size = new System.Drawing.Size(1281, 840);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 6;
            // 
            // buttonRight
            // 
            this.buttonRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.buttonRight.Location = new System.Drawing.Point(119, 318);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(51, 51);
            this.buttonRight.TabIndex = 19;
            this.buttonRight.Text = "Вправо";
            this.buttonRight.UseVisualStyleBackColor = true;
            this.buttonRight.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // buttonLeft
            // 
            this.buttonLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.buttonLeft.Location = new System.Drawing.Point(4, 318);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(51, 51);
            this.buttonLeft.TabIndex = 18;
            this.buttonLeft.Text = "Влево";
            this.buttonLeft.UseVisualStyleBackColor = true;
            this.buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.buttonDown.Location = new System.Drawing.Point(62, 376);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(51, 51);
            this.buttonDown.TabIndex = 17;
            this.buttonDown.Text = "Вниз";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.buttonUp.Location = new System.Drawing.Point(62, 260);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(51, 51);
            this.buttonUp.TabIndex = 16;
            this.buttonUp.Text = "Вверх";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // resolutionSelector
            // 
            this.resolutionSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resolutionSelector.FormattingEnabled = true;
            this.resolutionSelector.Items.AddRange(new object[] {"10", "20", "40", "60"});
            this.resolutionSelector.Location = new System.Drawing.Point(4, 217);
            this.resolutionSelector.Name = "resolutionSelector";
            this.resolutionSelector.Size = new System.Drawing.Size(169, 21);
            this.resolutionSelector.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(4, 194);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(169, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "Размер клетки:";
            // 
            // TickCounter
            // 
            this.TickCounter.BackColor = System.Drawing.Color.White;
            this.TickCounter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TickCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.TickCounter.Location = new System.Drawing.Point(4, 692);
            this.TickCounter.Name = "TickCounter";
            this.TickCounter.Size = new System.Drawing.Size(169, 26);
            this.TickCounter.TabIndex = 13;
            this.TickCounter.Text = "0";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 672);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Тик:";
            // 
            // numericAnimalsNumber
            // 
            this.numericAnimalsNumber.Location = new System.Drawing.Point(4, 171);
            this.numericAnimalsNumber.Maximum = new decimal(new int[] {500, 0, 0, 0});
            this.numericAnimalsNumber.Name = "numericAnimalsNumber";
            this.numericAnimalsNumber.Size = new System.Drawing.Size(169, 20);
            this.numericAnimalsNumber.TabIndex = 11;
            // 
            // numericPlantsPercent
            // 
            this.numericPlantsPercent.Location = new System.Drawing.Point(4, 125);
            this.numericPlantsPercent.Maximum = new decimal(new int[] {500, 0, 0, 0});
            this.numericPlantsPercent.Name = "numericPlantsPercent";
            this.numericPlantsPercent.Size = new System.Drawing.Size(169, 20);
            this.numericPlantsPercent.TabIndex = 10;
            // 
            // labelPlants
            // 
            this.labelPlants.Location = new System.Drawing.Point(4, 102);
            this.labelPlants.Name = "labelPlants";
            this.labelPlants.Size = new System.Drawing.Size(169, 20);
            this.labelPlants.TabIndex = 8;
            this.labelPlants.Text = "Процент растений(10 доли):";
            // 
            // labelAnimals
            // 
            this.labelAnimals.Location = new System.Drawing.Point(4, 148);
            this.labelAnimals.Name = "labelAnimals";
            this.labelAnimals.Size = new System.Drawing.Size(169, 20);
            this.labelAnimals.TabIndex = 9;
            this.labelAnimals.Text = "Количество животных:";
            // 
            // numericHeight
            // 
            this.numericHeight.Location = new System.Drawing.Point(4, 79);
            this.numericHeight.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.numericHeight.Minimum = new decimal(new int[] {84, 0, 0, 0});
            this.numericHeight.Name = "numericHeight";
            this.numericHeight.Size = new System.Drawing.Size(169, 20);
            this.numericHeight.TabIndex = 7;
            this.numericHeight.Value = new decimal(new int[] {84, 0, 0, 0});
            // 
            // numericWidth
            // 
            this.numericWidth.Location = new System.Drawing.Point(4, 33);
            this.numericWidth.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.numericWidth.Minimum = new decimal(new int[] {108, 0, 0, 0});
            this.numericWidth.Name = "numericWidth";
            this.numericWidth.Size = new System.Drawing.Size(169, 20);
            this.numericWidth.TabIndex = 6;
            this.numericWidth.Value = new decimal(new int[] {108, 0, 0, 0});
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(4, 171);
            this.numericUpDown1.Maximum = new decimal(new int[] {200, 0, 0, 0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(169, 20);
            this.numericUpDown1.TabIndex = 11;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(4, 125);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(169, 20);
            this.numericUpDown2.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 20);
            this.label5.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 20);
            this.label2.TabIndex = 9;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(4, 79);
            this.numericUpDown3.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.numericUpDown3.Minimum = new decimal(new int[] {10, 0, 0, 0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(169, 20);
            this.numericUpDown3.TabIndex = 7;
            this.numericUpDown3.Value = new decimal(new int[] {10, 0, 0, 0});
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(4, 33);
            this.numericUpDown4.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.numericUpDown4.Minimum = new decimal(new int[] {10, 0, 0, 0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(169, 20);
            this.numericUpDown4.TabIndex = 6;
            this.numericUpDown4.Value = new decimal(new int[] {10, 0, 0, 0});
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(4, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(169, 20);
            this.label6.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(4, 744);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 40);
            this.button1.TabIndex = 5;
            this.button1.Text = "Начать симуляцию";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(4, 790);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(169, 40);
            this.button2.TabIndex = 0;
            this.button2.Text = "Закончить симуляцию";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(4, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(169, 20);
            this.label7.TabIndex = 3;
            // 
            // pictureMap
            // 
            this.pictureMap.Location = new System.Drawing.Point(0, 0);
            this.pictureMap.MaximumSize = new System.Drawing.Size(1080, 840);
            this.pictureMap.MinimumSize = new System.Drawing.Size(1080, 840);
            this.pictureMap.Name = "pictureMap";
            this.pictureMap.Size = new System.Drawing.Size(1080, 840);
            this.pictureMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureMap.TabIndex = 0;
            this.pictureMap.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 2500D;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.ClientSize = new System.Drawing.Size(1299, 858);
            this.Controls.Add(this.splitContainer1);
            this.Location = new System.Drawing.Point(15, 15);
            this.MaximumSize = new System.Drawing.Size(1315, 897);
            this.MinimumSize = new System.Drawing.Size(1315, 897);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.numericAnimalsNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericPlantsPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.pictureMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.timer1)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Button buttonDown;

        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Button buttonUp;

        private System.Windows.Forms.Button button3;

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;

        private System.Windows.Forms.ComboBox resolutionSelector;

        private System.Windows.Forms.ComboBox ResolutionSelector;

        private System.Windows.Forms.ComboBox comboBox1;

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;

        private System.Windows.Forms.Label TickCounter;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;

        private System.Timers.Timer timer1;

        private System.Windows.Forms.Button buttonFinish;

        private System.Windows.Forms.Label labelWidth;

        private System.Windows.Forms.Label labelHeight;
        
        private System.Windows.Forms.Label labelPlants;
        private System.Windows.Forms.Label labelAnimals;

        private System.Windows.Forms.NumericUpDown numericWidth;
        private System.Windows.Forms.NumericUpDown numericHeight;
        private System.Windows.Forms.NumericUpDown numericPlantsPercent;
        private System.Windows.Forms.NumericUpDown numericAnimalsNumber;

        private System.Windows.Forms.PictureBox pictureMap;

        private System.Windows.Forms.SplitContainer splitContainer1;

        private System.Windows.Forms.Button buttonStart;

        #endregion
    }
}
