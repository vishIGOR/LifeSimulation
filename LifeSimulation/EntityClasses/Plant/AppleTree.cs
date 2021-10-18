using System;
using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class AppleTree : Plant, IProductingFetuses
    {
        private int ReadyToFetus;
        private int FetusCounter;

        public AppleTree(Tile tile, Map map, GrowthStage growthStage)
        {
            MaxHitPoints = 10;
            ReadyToSeed = 25;
            MaxAge = 120;
            Color = Brushes.DarkGreen;

            Toxicity = false;
            ToxicityValue = 0;
            
            SetStandartValues(tile,map);
            ChangeGrowthStage(growthStage);
            
            switch (growthStage)
            {
                case GrowthStage.Sprout:
                    Age = 10;
                    break;
                case GrowthStage.Grown:
                    Age = 25;
                    break;
                case GrowthStage.Elder:
                    Age = 105;
                    break;
            }
            ReadyToFetus = 35;
            FetusCounter = Randomizer.GetRandomInt(0, ReadyToFetus-5);
        }
        public void CreateFetuses()
        {
            Fetus newFetus = new AppleTreeFetus(Tile, Map);
            Map.Fetuses.Add(newFetus);
            Map.NewEntities.Add(newFetus);
        }

        protected override void ChangeGrowthStage(GrowthStage newStage)
        {
            GrowthStage = newStage;
            switch (newStage)
            {
                case GrowthStage.Sprout:
                    HitPoints = 20;
                    MaxHitPoints = 20;
                    Eatable = true;
                    break;
                case GrowthStage.Grown:
                    HitPoints = 80;
                    MaxHitPoints = 80;
                    Eatable = true;
                    break;
                case GrowthStage.Elder:
                    HitPoints = 20;
                    MaxHitPoints = 20;
                    Eatable = true;
                    break;
            }
        }

        public override void ChooseAction()
        {
            ++Age;
            if (HitPoints <= 0)
            {
                Die();
                return;
            }

            switch (GrowthStage)
            {
                case GrowthStage.Seed:
                    if (Age == 10)
                    {
                        ChangeGrowthStage(GrowthStage.Sprout);
                    }

                    break;
                case GrowthStage.Sprout:
                    if (Age == 25)
                    {
                        ChangeGrowthStage(GrowthStage.Grown);
                    }

                    break;
                case GrowthStage.Grown:
                    ++SeedCounter;
                    ++FetusCounter;
                    if (Age == 105)
                    {
                        ChangeGrowthStage(GrowthStage.Elder);
                    }

                    if (SeedCounter == ReadyToSeed)
                    {
                        CreateSeeds();
                        SeedCounter = 0;
                    }

                    if (FetusCounter == ReadyToFetus)
                    {
                        CreateFetuses();
                        FetusCounter = 0;
                    }

                    break;
                case GrowthStage.Elder:
                    if (Age == MaxAge)
                    {
                        Die();
                    }

                    break;
            }
        }

        protected override void CreateSeeds()
        {
            List<Tile> possibleTiles = new List<Tile>();
            int counter = 0;
            int randomInt;

            for (int deltaX = -2; deltaX <= 2; ++deltaX)
            {
                for (int deltaY = -2; deltaY <= 2; ++deltaY)
                {
                    if (Tile.X + deltaX >= 0 && Tile.Y + deltaY >= 0)
                    {
                        if (Tile.X + deltaX < Map.Width && Tile.Y + deltaY < Map.Height)
                        {
                            if (!Map.Tiles[Tile.X + deltaX, Tile.Y + deltaY].IsSeeded &&
                                Map.Tiles[Tile.X + deltaX, Tile.Y + deltaY].PlantPossibility)
                            {
                                ++counter;
                                possibleTiles.Add(Map.Tiles[Tile.X + deltaX, Tile.Y + deltaY]);
                            }
                        }
                    }
                }
            }
            
            if (counter >= 2)
            {
                for (int i = 0; i < 2; ++i)
                {
                    randomInt = Randomizer.GetRandomInt(0, counter - 1);
                    Plant newPlant = new AppleTree(possibleTiles[randomInt], Map,GrowthStage.Seed);
                    Map.Plants.Add(newPlant);
                    Map.NewEntities.Add(newPlant);

                    possibleTiles.RemoveAt(randomInt);
                    --counter;
                }
            }
            else
            {
                if (counter == 1)
                {
                    randomInt = Randomizer.GetRandomInt(0, counter - 1);
                    Plant newPlant = new AppleTree(possibleTiles[randomInt], Map,GrowthStage.Seed);
                    Map.Plants.Add(newPlant);
                    Map.NewEntities.Add(newPlant);
                }
            }
        }
    }
}