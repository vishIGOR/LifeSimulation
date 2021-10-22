using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class WolfBerry : Plant, IProductingFetuses
    {
        private int ReadyToFetus;
        private int FetusCounter;

        public WolfBerry(Tile tile, Map map, PlantStage growthStage)
        {
            MaxHitPoints = 10;
            ReadyToSeed = 20;
            MaxAge = 80;
            Color = Brushes.Teal;

            Toxicity = true;
            ToxicityValue = 100;
            ToxicityCounter = 5;

            SetStandartValues(tile,map);
            ChangePlantStage(growthStage);

            switch (growthStage)
            {
                case PlantStage.Sprout:
                    Age = 10;
                    break;
                case PlantStage.Grown:
                    Age = 25;
                    break;
                case PlantStage.Elder:
                    Age = 65;
                    break;
            }
            
            ReadyToFetus = 25;
            FetusCounter = Randomizer.GetRandomInt(0, ReadyToFetus - 5);
        }

        public void CreateFetuses()
        {
            Fetus newFetus = new WolfBerryFetus(Tile, Map);
            Map.Fetuses.Add(newFetus);
            Map.NewEntities.Add(newFetus);
        }

        protected override void ChangePlantStage(PlantStage newStage)
        {
            PlantStage = newStage;
            switch (newStage)
            {
                case PlantStage.Sprout:
                    HitPoints = 20;
                    MaxHitPoints = 20;
                    Eatable = true;
                    break;
                case PlantStage.Grown:
                    HitPoints = 80;
                    MaxHitPoints = 80;
                    Eatable = true;
                    break;
                case PlantStage.Elder:
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

            switch (PlantStage)
            {
                case PlantStage.Seed:
                    if (Age == 10)
                    {
                        ChangePlantStage(PlantStage.Sprout);
                    }
                    break;
                case PlantStage.Sprout:
                    if (Age == 25)
                    {
                        ChangePlantStage(PlantStage.Grown);
                    }

                    break;
                case PlantStage.Grown:
                    ++SeedCounter;
                    ++FetusCounter;
                    if (Age == 65)
                    {
                        ChangePlantStage(PlantStage.Elder);
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
                case PlantStage.Elder:
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
            for (int deltaX = -1; deltaX <= 1; ++deltaX)
            {
                for (int deltaY = -1; deltaY <= 1; ++deltaY)
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

            if (counter >= 1)
            {
                Plant newPlant = new WolfBerry(possibleTiles[Randomizer.GetRandomInt(0, counter - 1)], Map,PlantStage.Seed);
                Map.Plants.Add(newPlant);
                Map.NewEntities.Add(newPlant);
            }
        }
    }
}