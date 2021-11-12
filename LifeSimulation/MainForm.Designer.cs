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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelInventoryFullness = new System.Windows.Forms.Label();
            this.comboBoxInventory = new System.Windows.Forms.ComboBox();
            this.comboBoxDomestics = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.labelMateTargetCoordinates = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.labelCategory = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.labelHungerLevel = new System.Windows.Forms.Label();
            this.labelHitPoints = new System.Windows.Forms.Label();
            this.labelCoordinates = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonRight = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureMap = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Timers.Timer();
            this.pictureBoxShowingEntity = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.numericAnimalsNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericPlantsPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.pictureMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.timer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBoxShowingEntity)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonFinish
            // 
            this.buttonFinish.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFinish.Location = new System.Drawing.Point(184, 790);
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
            this.labelHeight.Location = new System.Drawing.Point(3, 34);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(169, 20);
            this.labelHeight.TabIndex = 3;
            this.labelHeight.Text = "Высота:";
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStart.Location = new System.Drawing.Point(4, 790);
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
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
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureMap);
            this.splitContainer1.Size = new System.Drawing.Size(1451, 840);
            this.splitContainer1.SplitterDistance = 360;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Bisque;
            this.groupBox2.Controls.Add(this.pictureBoxShowingEntity);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.labelInventoryFullness);
            this.groupBox2.Controls.Add(this.comboBoxInventory);
            this.groupBox2.Controls.Add(this.comboBoxDomestics);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.labelMateTargetCoordinates);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.labelCategory);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.labelHungerLevel);
            this.groupBox2.Controls.Add(this.labelHitPoints);
            this.groupBox2.Controls.Add(this.labelCoordinates);
            this.groupBox2.Controls.Add(this.labelType);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(3, 368);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(344, 299);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Информация об объекте:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 23);
            this.label4.TabIndex = 20;
            this.label4.Text = "Заполненность инвентаря:";
            // 
            // labelInventoryFullness
            // 
            this.labelInventoryFullness.Location = new System.Drawing.Point(182, 214);
            this.labelInventoryFullness.Name = "labelInventoryFullness";
            this.labelInventoryFullness.Size = new System.Drawing.Size(162, 23);
            this.labelInventoryFullness.TabIndex = 19;
            // 
            // comboBoxInventory
            // 
            this.comboBoxInventory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInventory.FormattingEnabled = true;
            this.comboBoxInventory.Location = new System.Drawing.Point(182, 191);
            this.comboBoxInventory.Name = "comboBoxInventory";
            this.comboBoxInventory.Size = new System.Drawing.Size(160, 21);
            this.comboBoxInventory.TabIndex = 17;
            // 
            // comboBoxDomestics
            // 
            this.comboBoxDomestics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDomestics.FormattingEnabled = true;
            this.comboBoxDomestics.Location = new System.Drawing.Point(183, 164);
            this.comboBoxDomestics.Name = "comboBoxDomestics";
            this.comboBoxDomestics.Size = new System.Drawing.Size(160, 21);
            this.comboBoxDomestics.TabIndex = 16;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(7, 191);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(162, 23);
            this.label13.TabIndex = 15;
            this.label13.Text = "Инвентарь:";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(6, 167);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(162, 23);
            this.label15.TabIndex = 13;
            this.label15.Text = "Прирученные животные:";
            // 
            // labelMateTargetCoordinates
            // 
            this.labelMateTargetCoordinates.Location = new System.Drawing.Point(182, 144);
            this.labelMateTargetCoordinates.Name = "labelMateTargetCoordinates";
            this.labelMateTargetCoordinates.Size = new System.Drawing.Size(162, 23);
            this.labelMateTargetCoordinates.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(6, 144);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(162, 23);
            this.label12.TabIndex = 11;
            this.label12.Text = "Координаты патрнёра:";
            // 
            // labelCategory
            // 
            this.labelCategory.Location = new System.Drawing.Point(182, 121);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(162, 23);
            this.labelCategory.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(6, 121);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(162, 23);
            this.label11.TabIndex = 9;
            this.label11.Text = "Категория животного:";
            // 
            // labelHungerLevel
            // 
            this.labelHungerLevel.Location = new System.Drawing.Point(183, 98);
            this.labelHungerLevel.Name = "labelHungerLevel";
            this.labelHungerLevel.Size = new System.Drawing.Size(162, 23);
            this.labelHungerLevel.TabIndex = 8;
            // 
            // labelHitPoints
            // 
            this.labelHitPoints.Location = new System.Drawing.Point(183, 75);
            this.labelHitPoints.Name = "labelHitPoints";
            this.labelHitPoints.Size = new System.Drawing.Size(162, 23);
            this.labelHitPoints.TabIndex = 7;
            // 
            // labelCoordinates
            // 
            this.labelCoordinates.Location = new System.Drawing.Point(183, 52);
            this.labelCoordinates.Name = "labelCoordinates";
            this.labelCoordinates.Size = new System.Drawing.Size(162, 23);
            this.labelCoordinates.TabIndex = 6;
            // 
            // labelType
            // 
            this.labelType.Location = new System.Drawing.Point(182, 29);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(162, 23);
            this.labelType.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(7, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(162, 23);
            this.label9.TabIndex = 4;
            this.label9.Text = "Текущая сытость:";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(7, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(162, 23);
            this.label10.TabIndex = 3;
            this.label10.Text = "Текущее здоровье:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "Текущие координаты:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Тип существа:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Bisque;
            this.groupBox1.Controls.Add(this.buttonRight);
            this.groupBox1.Controls.Add(this.buttonUp);
            this.groupBox1.Controls.Add(this.buttonLeft);
            this.groupBox1.Controls.Add(this.buttonDown);
            this.groupBox1.Location = new System.Drawing.Point(4, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 182);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Кнопки управления:";
            // 
            // buttonRight
            // 
            this.buttonRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.buttonRight.Location = new System.Drawing.Point(231, 63);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(61, 61);
            this.buttonRight.TabIndex = 19;
            this.buttonRight.Text = "Вправо";
            this.buttonRight.UseVisualStyleBackColor = true;
            this.buttonRight.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.buttonUp.Location = new System.Drawing.Point(144, 15);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(61, 61);
            this.buttonUp.TabIndex = 16;
            this.buttonUp.Text = "Вверх";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonLeft
            // 
            this.buttonLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.buttonLeft.Location = new System.Drawing.Point(56, 63);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(61, 61);
            this.buttonLeft.TabIndex = 18;
            this.buttonLeft.Text = "Влево";
            this.buttonLeft.UseVisualStyleBackColor = true;
            this.buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.buttonDown.Location = new System.Drawing.Point(144, 111);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(61, 61);
            this.buttonDown.TabIndex = 17;
            this.buttonDown.Text = "Вниз";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // resolutionSelector
            // 
            this.resolutionSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resolutionSelector.FormattingEnabled = true;
            this.resolutionSelector.Items.AddRange(new object[] {"10", "20", "40", "60"});
            this.resolutionSelector.Location = new System.Drawing.Point(179, 112);
            this.resolutionSelector.Name = "resolutionSelector";
            this.resolutionSelector.Size = new System.Drawing.Size(169, 21);
            this.resolutionSelector.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(4, 113);
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
            this.TickCounter.Location = new System.Drawing.Point(184, 749);
            this.TickCounter.Name = "TickCounter";
            this.TickCounter.Size = new System.Drawing.Size(169, 26);
            this.TickCounter.TabIndex = 13;
            this.TickCounter.Text = "0";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(4, 749);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Тик:";
            // 
            // numericAnimalsNumber
            // 
            this.numericAnimalsNumber.Location = new System.Drawing.Point(179, 86);
            this.numericAnimalsNumber.Maximum = new decimal(new int[] {500, 0, 0, 0});
            this.numericAnimalsNumber.Name = "numericAnimalsNumber";
            this.numericAnimalsNumber.Size = new System.Drawing.Size(169, 20);
            this.numericAnimalsNumber.TabIndex = 11;
            // 
            // numericPlantsPercent
            // 
            this.numericPlantsPercent.Location = new System.Drawing.Point(179, 60);
            this.numericPlantsPercent.Maximum = new decimal(new int[] {500, 0, 0, 0});
            this.numericPlantsPercent.Name = "numericPlantsPercent";
            this.numericPlantsPercent.Size = new System.Drawing.Size(169, 20);
            this.numericPlantsPercent.TabIndex = 10;
            // 
            // labelPlants
            // 
            this.labelPlants.Location = new System.Drawing.Point(4, 60);
            this.labelPlants.Name = "labelPlants";
            this.labelPlants.Size = new System.Drawing.Size(169, 20);
            this.labelPlants.TabIndex = 8;
            this.labelPlants.Text = "Процент растений(10 доли):";
            // 
            // labelAnimals
            // 
            this.labelAnimals.Location = new System.Drawing.Point(4, 86);
            this.labelAnimals.Name = "labelAnimals";
            this.labelAnimals.Size = new System.Drawing.Size(169, 20);
            this.labelAnimals.TabIndex = 9;
            this.labelAnimals.Text = "Количество животных:";
            // 
            // numericHeight
            // 
            this.numericHeight.Location = new System.Drawing.Point(179, 34);
            this.numericHeight.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.numericHeight.Minimum = new decimal(new int[] {84, 0, 0, 0});
            this.numericHeight.Name = "numericHeight";
            this.numericHeight.Size = new System.Drawing.Size(169, 20);
            this.numericHeight.TabIndex = 7;
            this.numericHeight.Value = new decimal(new int[] {84, 0, 0, 0});
            // 
            // numericWidth
            // 
            this.numericWidth.Location = new System.Drawing.Point(179, 8);
            this.numericWidth.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.numericWidth.Minimum = new decimal(new int[] {108, 0, 0, 0});
            this.numericWidth.Name = "numericWidth";
            this.numericWidth.Size = new System.Drawing.Size(169, 20);
            this.numericWidth.TabIndex = 6;
            this.numericWidth.Value = new decimal(new int[] {108, 0, 0, 0});
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 24;
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
            this.pictureMap.Click += new System.EventHandler(this.pictureMap_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 2500D;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // pictureBoxShowingEntity
            // 
            this.pictureBoxShowingEntity.Location = new System.Drawing.Point(145, 230);
            this.pictureBoxShowingEntity.Name = "pictureBoxShowingEntity";
            this.pictureBoxShowingEntity.Size = new System.Drawing.Size(60, 60);
            this.pictureBoxShowingEntity.TabIndex = 21;
            this.pictureBoxShowingEntity.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.ClientSize = new System.Drawing.Size(1469, 858);
            this.Controls.Add(this.splitContainer1);
            this.Location = new System.Drawing.Point(15, 15);
            this.MaximumSize = new System.Drawing.Size(1485, 897);
            this.MinimumSize = new System.Drawing.Size(1485, 897);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.numericAnimalsNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericPlantsPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.pictureMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.timer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBoxShowingEntity)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.PictureBox pictureBoxShowingEntity;

        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Label labelInventoryFullness;

        private System.Windows.Forms.ComboBox comboBoxInventory;

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;

        private System.Windows.Forms.Label labelMateTargetCoordinates;
        private System.Windows.Forms.Label label12;

        private System.Windows.Forms.Label label11;

        private System.Windows.Forms.Label labelHitPoints;

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelHungerLevel;
        private System.Windows.Forms.Label labelCoordinates;
        private System.Windows.Forms.Label labelType;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelCategory;

        private System.Windows.Forms.GroupBox groupBox2;

        private System.Windows.Forms.GroupBox groupBox1;

        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Button buttonDown;

        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Button buttonUp;

        private System.Windows.Forms.ComboBox resolutionSelector;

        private System.Windows.Forms.ComboBox ResolutionSelector;

        private System.Windows.Forms.ComboBox comboBoxDomestics;

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label TickCounter;

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
