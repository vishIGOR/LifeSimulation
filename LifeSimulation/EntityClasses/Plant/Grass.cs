using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.MapClasses.Enumerators;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Grass : Plant
    {
        public Grass(Tile tile, Map map, PlantStage growthStage)
        {
            MaxHitPoints = 10;
            ReadyToSeed = 18;
            MaxAge = 60;
            Color = Brushes.LightGreen;

            Toxicity = false;
            ToxicityValue = 0;
            SeedRadius = 1;
            
            SetStandartValues(tile, map);
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

            SeedCounter = Randomizer.GetRandomInt(0, ReadyToSeed - 5);
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
            if (Map.Season == SeasonType.Summer)
            {
                ++Age;
            }

            if (HitPoints <= 0)
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
                    if (Map.Season != SeasonType.Winter)
                    {
                        ++SeedCounter;
                        if (Age == 50)
                        {
                            ChangePlantStage(PlantStage.Elder);
                        }

                        if (SeedCounter == ReadyToSeed)
                        {
                            FindPlaceToSeed();
                            SeedCounter = 0;
                        }
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

        protected override void CreateNewSeeds(List<Tile> possibleTiles, int numberOfPossibleTiles)
        {
            if (numberOfPossibleTiles >= 1)
            {
                Plant newPlant = new Grass(possibleTiles[Randomizer.GetRandomInt(0, numberOfPossibleTiles - 1)], Map,
                    PlantStage.Seed);
                Map.Plants.Add(newPlant);
                Map.NewEntities.Add(newPlant);
            }
        }

        public override void ReactToChangeSeason(SeasonType newSeason)
        {
            if (newSeason == SeasonType.Winter)
            {
                if (PlantStage != PlantStage.Seed)
                {
                    if (Randomizer.GetRandomInt(1, 10) < 8)
                    {
                        Die();
                    }
                }
            }
        }
    }
}