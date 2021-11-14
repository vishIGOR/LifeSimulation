using System;
using System.Diagnostics;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using LifeSimulation.EntityClasses;
using LifeSimulation.EntityClasses.DeadBodyClasses;
using LifeSimulation.EntityClasses.Omnivore;
using LifeSimulation.EntityClasses.Scavenger;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;
using Nito.AsyncEx;

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

        private Entity ShowingEntity;

        private int MetaStartY;
        private int MetaStartX;
        private int MetaWidth = 108;
        private int MetaHeight = 84;
        private AsyncLock Mutex;

        private Map CurrentMap;
        private int Resolution = 10;

        public MainForm()
        {
            InitializeComponent();
            timer1.Stop();
            resolutionSelector.SelectedIndexChanged += SelectedResolutionChanged;
            Mutex = new AsyncLock();
        }

        private async void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            // await Task.Run(()=>CurrentMap.UpdateMap());
            using (await Mutex.LockAsync())
            {
                CurrentMap.UpdateMap();
                CurrentMap.ReloadEntities();
            }
            NextView();
            ShowInfo();
            TickCounter.Text = Convert.ToString(Convert.ToInt32(TickCounter.Text) + 1);
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

            MetaStartX = 0;
            MetaStartY = 0;
            ViewHeight = (int) numericHeight.Value;
            ViewWidth = (int) numericWidth.Value;
            ViewPercentOfPlants = (int) numericPlantsPercent.Value;
            ViewNumberOfAnimals = (int) numericAnimalsNumber.Value;
            CurrentMap = new Map(ViewHeight, ViewWidth, ViewNumberOfAnimals, ViewPercentOfPlants);
            ColorsOfTiles = CurrentMap.ColorsOfTiles;

            pictureMap.Image = new Bitmap(1080, 840);
            MapView = Graphics.FromImage(pictureMap.Image);

            timer1.Start();
        }

        private async void SelectedResolutionChanged(object sender, EventArgs e)
        {
            Resolution = int.Parse(resolutionSelector.SelectedItem.ToString());
            if (timer1.Enabled)
            {
                switch (Resolution)
                {
                    case 10:
                        MetaWidth = 108;
                        MetaHeight = 84;
                        break;
                    case 20:
                        MetaWidth = 54;
                        MetaHeight = 42;
                        break;
                    case 40:
                        MetaWidth = 27;
                        MetaHeight = 21;
                        break;
                    case 60:
                        MetaWidth = 18;
                        MetaHeight = 14;
                        break;
                }

                if (MetaStartY + MetaHeight > ViewHeight)
                {
                    MetaStartY = ViewHeight - MetaHeight;
                }

                if (MetaStartX + MetaWidth > ViewWidth)
                {
                    MetaStartX = ViewWidth - MetaWidth;
                }

                using (await Mutex.LockAsync())
                {
                    NextView();
                }
                // await Task.Run(()=>NextView());
            }
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
            DrawEntities();

            pictureMap.Refresh();
        }

        void DrawLandscape()
        {
            for (int i = 0; i < MetaWidth; ++i)
            {
                for (int j = 0; j < MetaHeight; ++j)
                {
                    // Debugger.Launch();
                    // Debugger.Log(1,"1","landscape test");
                    //MetaLandscape[i, j] = ColorsOfTiles[i + MetaStartX, j + MetaStartY];
                    MapView.FillRectangle(ColorsOfTiles[i + MetaStartX, j + MetaStartY], i * Resolution, j * Resolution,
                        Resolution, Resolution);
                }
            }
        }

        void DrawPlants()
        {
            Tile currentTile;
            for (int i = MetaStartX; i < MetaStartX + MetaWidth; ++i)
            {
                for (int j = MetaStartY; j < MetaStartY + MetaHeight; ++j)
                {
                    currentTile = CurrentMap.Tiles[i, j];
                    if (currentTile.Plant != null)
                    {
                        MapView.DrawImage(currentTile.Plant.Image, (i - MetaStartX) * Resolution,
                            (j - MetaStartY) * Resolution, Resolution, Resolution);
                    }
                }
            }
        }

        void DrawEntities()
        {
            Tile currentTile;
            for (int i = MetaStartX; i < MetaStartX + MetaWidth; ++i)
            {
                for (int j = MetaStartY; j < MetaStartY + MetaHeight; ++j)
                {
                    currentTile = CurrentMap.Tiles[i, j];
                    if (currentTile.Entities.Count > 0)
                    {
                        foreach (var entity in currentTile.Entities)
                        {
                            MapView.DrawImage(entity.Image, (i - MetaStartX) * Resolution,
                                (j - MetaStartY) * Resolution, Resolution, Resolution);
                        }
                    }
                }
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                if (MetaStartY > 0)
                {
                    --MetaStartY;
                    NextView();
                }
            }
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                if (MetaStartX > 0)
                {
                    --MetaStartX;
                    NextView();
                }
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                if (MetaStartY < ViewHeight - MetaHeight)
                {
                    ++MetaStartY;
                    NextView();
                }
            }
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                if (MetaStartX < ViewWidth - MetaWidth)
                {
                    ++MetaStartX;
                    NextView();
                }
            }
        }

        private void pictureMap_Click(object sender, EventArgs e)
        {
            var mouseEvent = e as MouseEventArgs;
            if (mouseEvent == null || timer1.Enabled == false)
            {
                return;
            }

            int x = mouseEvent.X, y = mouseEvent.Y;


            // Debug.WriteLine($"X={mouseEvent.X}, Y={mouseEvent.Y}");
            // Debug.WriteLine($"{x / Resolution - MetaStartX},{y / Resolution - MetaStartY}");

            Tile targetTile = CurrentMap.Tiles[x / Resolution + MetaStartX, y / Resolution + MetaStartY];

            if (targetTile.Entities.Count > 0)
            {
                ShowingEntity =
                    targetTile.Entities[CurrentMap.Randomizer.GetRandomInt(0, targetTile.Entities.Count - 1)];
                ShowInfo();
            }
            else
            {
                if (targetTile.Plant != null)
                {
                    ShowingEntity = targetTile.Plant;
                    ShowInfo();
                }
            }
            if (ShowingEntity == null)
            {
                Debug.WriteLine($"nothing");
            }
            else
            {
                Debug.WriteLine(ShowingEntity.GetType().ToString());
            }
        }

        private void ShowInfo()
        {
            labelType.Text = "";
            labelCoordinates.Text = "";
            labelHitPoints.Text = "";
            labelType.Text = "";
            labelCategory.Text = "";
            labelHungerLevel.Text = "";
            labelInventoryFullness.Text = "";
            labelMateTargetCoordinates.Text = "";
            pictureBoxShowingEntity.Image = null;
            comboBoxDomestics.Items.Clear();
            comboBoxInventory.Items.Clear();
            
            if (!CurrentMap.Entities.Contains(ShowingEntity) || ShowingEntity== null)
            {
                return;
            }

            pictureBoxShowingEntity.Image = ShowingEntity.Image;
            labelCoordinates.Text = $"({ShowingEntity.Tile.X}, {ShowingEntity.Tile.Y})";
            labelHitPoints.Text = $"{ShowingEntity.HitPoints}/{ShowingEntity.MaxHitPoints}";

            if (ShowingEntity is Animal)
            {
                Animal ShowingAnimal = (Animal) ShowingEntity;
                labelType.Text = "Животное";
                labelHungerLevel.Text = $"{ShowingAnimal.HungerPoints}/{ShowingAnimal.MaxHungerPoints}";
                if (ShowingAnimal.MatingTarget != null)
                {
                    labelMateTargetCoordinates.Text = $"({ShowingAnimal.MatingTarget.Tile.X}, {ShowingAnimal.MatingTarget.Tile.Y})";
                }
                
                if (ShowingEntity is Omnivore)
                {
                    labelCategory.Text = "Всеядное животное";
                }
                if (ShowingEntity is Human)
                {
                    Human ShowingHuman = (Human) ShowingEntity;
                    labelCategory.Text = "Человек";
                    foreach (var domestic in ShowingHuman.DomesticAnimals)
                    {
                        if (domestic is Sheep)
                        {
                            comboBoxDomestics.Items.Add($"Овца: ({domestic.Tile.X}, {domestic.Tile.Y})");
                        }
                        if (domestic is Bear)
                        {
                            comboBoxDomestics.Items.Add($"Мишка: ({domestic.Tile.X}, {domestic.Tile.Y})");
                        }
                        if (domestic is Wolf)
                        {
                            comboBoxDomestics.Items.Add($"волк: ({domestic.Tile.X}, {domestic.Tile.Y})");
                        }
                        
                    }
                    comboBoxInventory.Items.Add($"Мясо :{ShowingHuman.Inventory[(int) FoodType.Meat]}");
                    comboBoxInventory.Items.Add($"Растения :{ShowingHuman.Inventory[(int) FoodType.Plant]}");
                    comboBoxInventory.Items.Add($"Плоды :{ShowingHuman.Inventory[(int) FoodType.Fetus]}");
                    comboBoxInventory.Items.Add($"Мёд :{ShowingHuman.Inventory[(int) FoodType.Honey]}");
                    
                    labelInventoryFullness.Text = $"{ShowingHuman.InventoryFullness}/{ShowingHuman.InventorySize}";
                }
                if (ShowingEntity is Herbivore)
                {
                    labelCategory.Text = "Травоядное животное";
                }
                if (ShowingEntity is Carnivore)
                {
                    labelCategory.Text = "Хищник";
                }
                if (ShowingEntity is Scavenger)
                {
                    labelCategory.Text = "Падальщик";
                }
            }

            if (ShowingEntity is Plant)
            {
                labelType.Text = "Растение";
            }

            if (ShowingEntity is DeadBody)
            {
                labelType.Text = "Труп(мясо)";
            }

            if (ShowingEntity is Fetus)
            {
                labelType.Text = "Плод";
            }
        }
    }
}