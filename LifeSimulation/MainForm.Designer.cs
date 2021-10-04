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
            this.TickCounter = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericAnimalsNumber = new System.Windows.Forms.NumericUpDown();
            this.numericPlantsPercent = new System.Windows.Forms.NumericUpDown();
            this.labelPlants = new System.Windows.Forms.Label();
            this.labelAnimals = new System.Windows.Forms.Label();
            this.numericHeight = new System.Windows.Forms.NumericUpDown();
            this.numericWidth = new System.Windows.Forms.NumericUpDown();
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
            ((System.ComponentModel.ISupportInitialize) (this.pictureMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonFinish
            // 
            this.buttonFinish.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFinish.Location = new System.Drawing.Point(4, 693);
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
            this.buttonStart.Location = new System.Drawing.Point(4, 647);
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
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureMap);
            this.splitContainer1.Size = new System.Drawing.Size(1166, 743);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 6;
            // 
            // TickCounter
            // 
            this.TickCounter.BackColor = System.Drawing.Color.White;
            this.TickCounter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TickCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.TickCounter.Location = new System.Drawing.Point(4, 251);
            this.TickCounter.Name = "TickCounter";
            this.TickCounter.Size = new System.Drawing.Size(169, 26);
            this.TickCounter.TabIndex = 13;
            this.TickCounter.Text = "0";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Тик:";
            // 
            // numericAnimalsNumber
            // 
            this.numericAnimalsNumber.Location = new System.Drawing.Point(4, 171);
            this.numericAnimalsNumber.Maximum = new decimal(new int[] {200, 0, 0, 0});
            this.numericAnimalsNumber.Name = "numericAnimalsNumber";
            this.numericAnimalsNumber.Size = new System.Drawing.Size(169, 20);
            this.numericAnimalsNumber.TabIndex = 11;
            // 
            // numericPlantsPercent
            // 
            this.numericPlantsPercent.Location = new System.Drawing.Point(4, 125);
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
            this.labelPlants.Text = "Процент засеянности:";
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
            this.numericHeight.Minimum = new decimal(new int[] {10, 0, 0, 0});
            this.numericHeight.Name = "numericHeight";
            this.numericHeight.Size = new System.Drawing.Size(169, 20);
            this.numericHeight.TabIndex = 7;
            this.numericHeight.Value = new decimal(new int[] {10, 0, 0, 0});
            // 
            // numericWidth
            // 
            this.numericWidth.Location = new System.Drawing.Point(4, 33);
            this.numericWidth.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.numericWidth.Minimum = new decimal(new int[] {10, 0, 0, 0});
            this.numericWidth.Name = "numericWidth";
            this.numericWidth.Size = new System.Drawing.Size(169, 20);
            this.numericWidth.TabIndex = 6;
            this.numericWidth.Value = new decimal(new int[] {10, 0, 0, 0});
            // 
            // pictureMap
            // 
            this.pictureMap.Location = new System.Drawing.Point(0, 0);
            this.pictureMap.Name = "pictureMap";
            this.pictureMap.Size = new System.Drawing.Size(965, 734);
            this.pictureMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureMap.TabIndex = 0;
            this.pictureMap.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 2400D;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.splitContainer1);
            this.Location = new System.Drawing.Point(15, 15);
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
            ((System.ComponentModel.ISupportInitialize) (this.pictureMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.timer1)).EndInit();
            this.ResumeLayout(false);
        }

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