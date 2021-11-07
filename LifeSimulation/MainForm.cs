using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Timers;
using System.Windows.Forms;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation
{
    public partial class MainForm : Form
    {
        private Graphics MapView;
        private int ViewHeight;
        private int ViewWidth;
        private int ViewNumberOfAnimals;
        private int ViewPercentOfPlants;

        private Brush[,] ColorsOfTiles;
        
        private Map CurrentMap;
        private int Resolution;
        public MainForm()
        {
            InitializeComponent();
            timer1.Stop();
            ScrollBarInit();
        }

        private void ScrollBarInit()
        {
            // pictureMap.Controls.Add(vScrollBar1);
            // pictureMap.Controls.Add(hScrollBar1);
            splitContainer1.Panel2.AutoScroll = true;
            pictureMap.SizeMode = PictureBoxSizeMode.AutoSize;
        }
        private void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            CurrentMap.UpdateMap();
            NextView();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            StartSimulation();
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            StopSimulation();
        }

        void StartSimulation()
        {
            if (timer1.Enabled)
            {
                return;
            }
            
            numericHeight.Enabled = false;
            numericWidth.Enabled = false;
            numericPlantsPercent.Enabled = false;
            numericAnimalsNumber.Enabled = false;
            
            Resolution = 4;
            ViewHeight = (int)numericHeight.Value;
            ViewWidth = (int)numericWidth.Value;
            ViewPercentOfPlants = (int)numericPlantsPercent.Value;
            ViewNumberOfAnimals = (int)numericAnimalsNumber.Value;
            CurrentMap = new Map(ViewHeight,ViewWidth,ViewNumberOfAnimals,ViewPercentOfPlants);
            ColorsOfTiles = CurrentMap.ColorsOfTiles;
            
            pictureMap.Image = new Bitmap(ViewWidth*Resolution, ViewHeight*Resolution);
            MapView = Graphics.FromImage(pictureMap.Image);
            
            timer1.Start();
            ScrollBarInit();
        }

        void StopSimulation()
        {
            if (!timer1.Enabled)
            {
                return;
            }
            
            
            timer1.Stop();
            TickCounter.Text = "0";
            numericHeight.Enabled = true;
            numericWidth.Enabled = true;
            numericPlantsPercent.Enabled = true;
            numericAnimalsNumber.Enabled = true;
        }

        void NextView()
        {
            DrawLandscape();
            DrawPlants();
            DrawDeadBodies();
            DrawFetuses();
            DrawAnimals();

            pictureMap.Refresh();
            TickCounter.Text = Convert.ToString(Convert.ToInt32(TickCounter.Text)+1);
        }

        void DrawDeadBodies()
        {
            foreach (var dead in CurrentMap.DeadBodies)
            {
                MapView.FillRectangle(dead.Color, dead.Tile.X * Resolution, dead.Tile.Y * Resolution, Resolution, Resolution);
            }
        }
        
        void DrawLandscape()
        {
            for (int i = 0; i < ViewWidth; ++i)
            {
                for (int j = 0; j < ViewHeight; ++j)
                {
                    MapView.FillRectangle(ColorsOfTiles[i,j],i*Resolution,j*Resolution,Resolution,Resolution);
                }
            }
        }

        void DrawFetuses()
        {
            foreach (var fetus in CurrentMap.Fetuses)
            {
                MapView.FillEllipse(fetus.Color, fetus.Tile.X * Resolution+Resolution/4, fetus.Tile.Y * Resolution+Resolution/4, Resolution/2, Resolution/2);
            }
        }
        
        void DrawPlants()
        {
            foreach (var plant in CurrentMap.Plants)
            {
                MapView.FillRectangle(plant.Color, plant.Tile.X * Resolution, plant.Tile.Y * Resolution, Resolution, Resolution);
            }
        }
        
        void DrawAnimals()
        {
            foreach (var animal in CurrentMap.Animals)
            {
                MapView.FillEllipse(animal.Color,animal.Tile.X*Resolution,animal.Tile.Y*Resolution,Resolution,Resolution);
            }
        }
    }
}