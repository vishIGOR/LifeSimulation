using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Grass:Plant
    {
        public Grass(Tile tile, Map map)
        {
            Tile = tile;
            Map = map;
            Randomizer = Map.Randomizer;
            
            Tile.IsSeeded = true;
            
            HitPoints = 10;
            MaxHitPoints = 10;
            
            ReadyToSeed = 15;
            SeedCounter = 0;
            
            Color = Brushes.LightGreen;
            
            Toxicity = false;
            ToxicityValue = 0;

            Eatable = false;

            GrowthStage = GrowthStage.Seed;

            Age = 0;
            MaxAge = 60;
        }

        public Grass(Tile tile, Map map, GrowthStage growthStage)
        {
            Tile = tile;
            Map = map;
            Randomizer = Map.Randomizer;
            
            Tile.IsSeeded = true;
            
            HitPoints = 10;
            MaxHitPoints = 10;
            
            ReadyToSeed = 18;
            SeedCounter = 0;
            
            Color = Brushes.LightGreen;
            
            Toxicity = false;
            ToxicityValue = 0;

            Eatable = false;

            GrowthStage = GrowthStage.Seed;

            Age = 0;
            MaxAge = 60;
            
            ChangeGrowthStage(growthStage);
            
            switch (growthStage)
            {
                case GrowthStage.Sprout:
                    Age = 5;
                    break;
                case GrowthStage.Grown:
                    Age = 15;
                    break;
                case GrowthStage.Elder:
                    Age = 50;
                    break;
            }
            
            SeedCounter = Randomizer.GetRandomInt(0, ReadyToSeed-5);
        }
        protected override void ChangeGrowthStage(GrowthStage newStage)
        {
            GrowthStage = newStage;
            switch (newStage)
            {
                case GrowthStage.Sprout:
                    Eatable = true;
                    break;
                case GrowthStage.Grown:
                    Eatable = true;
                    break;
                case GrowthStage.Elder:
                    Eatable = true;
                    break;
            }
        }

        protected override void Die()
        {
            Map.Plants.Remove(this);
            Map.DeadEntities.Add(this);
        }

        public override void ChooseAction()
        {
            ++Age;
            if (HitPoints<= 0)
            {
                Die();
                return;
            }

            switch (GrowthStage)
            {
                case GrowthStage.Seed:
                    if (Age == 5)
                    {
                        ChangeGrowthStage(GrowthStage.Sprout);
                    }
                    break;
                case GrowthStage.Sprout:
                    if (Age == 15)
                    {
                        ChangeGrowthStage(GrowthStage.Grown);
                    }
                    break;
                case GrowthStage.Grown:
                    ++SeedCounter;
                    if (Age == 50)
                    {
                        ChangeGrowthStage(GrowthStage.Elder);
                    }
                    if (SeedCounter == ReadyToSeed)
                    {
                        CreateSeeds();
                        SeedCounter = 0;
                    }
                    break;
                case GrowthStage.Elder:
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
                Plant newPlant = new Grass(possibleTiles[Randomizer.GetRandomInt(0, counter - 1)], Map);
                Map.Plants.Add(newPlant);
                Map.NewEntities.Add(newPlant);
            }
            
        }
    }
}