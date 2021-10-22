using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Grass:Plant
    {
        public Grass(Tile tile, Map map, PlantStage growthStage)
        {
            MaxHitPoints = 10;
            ReadyToSeed = 18;
            MaxAge = 60;
            Color = Brushes.LightGreen;
            
            Toxicity = false;
            ToxicityValue = 0;
            
            SetStandartValues(tile,map);
            ChangePlantStage(growthStage);
            
            switch (growthStage)
            {
                case PlantStage.Sprout:
                    Age = 5;
                    break;
                case PlantStage.Grown:
                    Age = 15;
                    break;
                case PlantStage.Elder:
                    Age = 50;
                    break;
            }
            
            SeedCounter = Randomizer.GetRandomInt(0, ReadyToSeed-5);
        }
        protected override void ChangePlantStage(PlantStage newStage)
        {
            PlantStage = newStage;
            switch (newStage)
            {
                case PlantStage.Sprout:
                    Eatable = true;
                    break;
                case PlantStage.Grown:
                    Eatable = true;
                    break;
                case PlantStage.Elder:
                    Eatable = true;
                    break;
            }
        }

        public override void ChooseAction()
        {
            ++Age;
            if (HitPoints<= 0)
            {
                Die();
                return;
            }

            switch (PlantStage)
            {
                case PlantStage.Seed:
                    if (Age == 5)
                    {
                        ChangePlantStage(PlantStage.Sprout);
                    }
                    break;
                case PlantStage.Sprout:
                    if (Age == 15)
                    {
                        ChangePlantStage(PlantStage.Grown);
                    }
                    break;
                case PlantStage.Grown:
                    ++SeedCounter;
                    if (Age == 50)
                    {
                        ChangePlantStage(PlantStage.Elder);
                    }
                    if (SeedCounter == ReadyToSeed)
                    {
                        CreateSeeds();
                        SeedCounter = 0;
                    }
                    break;
                case PlantStage.Elder:
                    if (Age==MaxAge )
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
            for (int deltaX = -1; deltaX <= 1; ++deltaX)
            {
                for (int deltaY = -1; deltaY <= 1; ++deltaY)
                {
                    if (Tile.X + deltaX >= 0 && Tile.Y + deltaY >= 0)
                    {
                        if (Tile.X + deltaX < Map.Width && Tile.Y + deltaY < Map.Height)
                        {
                            if (!Map.Tiles[Tile.X + deltaX, Tile.Y + deltaY].IsSeeded && Map.Tiles[Tile.X + deltaX, Tile.Y + deltaY].PlantPossibility)
                            {
                                ++counter;
                                possibleTiles.Add(Map.Tiles[Tile.X + deltaX, Tile.Y + deltaY]);
                            }
                        }
                    }
                }
            }

            if (counter >= 1)
            {
                Plant newPlant = new Grass(possibleTiles[Randomizer.GetRandomInt(0, counter - 1)], Map,PlantStage.Seed);
                Map.Plants.Add(newPlant);
                Map.NewEntities.Add(newPlant);
            }
            
        }
    }
}