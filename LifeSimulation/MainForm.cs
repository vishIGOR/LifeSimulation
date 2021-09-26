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

        private Brush[,] ColorOfTiles;
        
        private Map CurrentMap;
        private int Resolution;
        public MainForm()
        {
            InitializeComponent();
            timer1.Stop();
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
            
            Resolution = 20;
            ViewHeight = 50;
            ViewWidth = 50;
            ViewPercentOfPlants = 70;
            ViewNumberOfAnimals = 10;
            CurrentMap = new Map(ViewHeight,ViewWidth,ViewNumberOfAnimals,ViewPercentOfPlants);
            ColorOfTiles = CurrentMap.ReturnColorsOfTiles();
            
            pictureMap.Image = new Bitmap(ViewWidth*Resolution, ViewHeight*Resolution);
            MapView = Graphics.FromImage(pictureMap.Image);
            
            timer1.Start();
        }

        void StopSimulation()
        {
            if (!timer1.Enabled)
            {
                return;
            }
            
            timer1.Stop();
            
            numericHeight.Enabled = true;
            numericWidth.Enabled = true;
            numericPlantsPercent.Enabled = true;
            numericAnimalsNumber.Enabled = true;
        }

        void NextView()
        {
            DrawLandscape();
            DrawPlants();
            DrawAnimals();

            pictureMap.Refresh();
        }

        void DrawLandscape()
        {
            for (int i = 0; i < ViewWidth; ++i)
            {
                for (int j = 0; j < ViewHeight; ++j)
                {
                    MapView.FillRectangle(ColorOfTiles[i,j],i*Resolution,j*Resolution,Resolution,Resolution);
                }
            }
        }

        void DrawPlants()
        {
            foreach (var plant in CurrentMap.ReturnAllPlants())
            {
                MapView.FillRectangle(plant.EntityColor, plant.CurrentTile.X * Resolution, plant.CurrentTile.Y * Resolution, Resolution, Resolution);
            }
        }
        
        void DrawAnimals()
        {
            foreach (var animal in CurrentMap.ReturnAllAnimals())
            {
                MapView.FillEllipse(animal.EntityColor,animal.CurrentTile.X*Resolution,animal.CurrentTile.Y*Resolution,Resolution,Resolution);
            }
        }
    }
}