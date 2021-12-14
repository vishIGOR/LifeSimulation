using System.Collections.Generic;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.MapClasses.Enumerators;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public abstract class Plant : Entity
    {
        protected int ReadyToSeed;
        protected int SeedCounter;

        protected int Age;
        protected int MaxAge;
        protected PlantStage PlantStage;

        // protected abstract void CreateSeeds();
        protected abstract void ChangePlantStage(PlantStage newStage);
        protected int SeedRadius;

        protected override void Die()
        {
            Map.Plants.Remove(this);
            Map.DeadEntities.Add(this);
            Tile.SpecialObject = null;
        }

        protected override void SetStandartValues(Tile tile, Map map)
        {
            base.SetStandartValues(tile, map);

            Age = 0;
            Eatable = false;
            PlantStage = PlantStage.Seed;

            Tile.SpecialObject = this;

            HitPoints = MaxHitPoints;

            SeedCounter = Randomizer.GetRandomInt(0, ReadyToSeed - 5);
        }

        protected virtual void FindPlaceToSeed()
        {
            List<Tile> possibleTiles = new List<Tile>();
            int counter = 0;
            for (int deltaX = -SeedRadius; deltaX <= SeedRadius; ++deltaX)
            {
                for (int deltaY = -SeedRadius; deltaY <= SeedRadius; ++deltaY)
                {
                    if (Tile.X + deltaX >= 0 && Tile.Y + deltaY >= 0)
                    {
                        if (Tile.X + deltaX < Map.Width && Tile.Y + deltaY < Map.Height)
                        {
                            if (Map.Tiles[Tile.X + deltaX, Tile.Y + deltaY].SpecialObject == null &&
                                Map.Tiles[Tile.X + deltaX, Tile.Y + deltaY].PlantPossibility)
                            {
                                ++counter;
                                possibleTiles.Add(Map.Tiles[Tile.X + deltaX, Tile.Y + deltaY]);
                            }
                        }
                    }
                }
            }

            CreateNewSeeds(possibleTiles, counter);
            // if (counter >= 1)
            // {
            // Plant newPlant = new Grass(possibleTiles[Randomizer.GetRandomInt(0, counter - 1)], Map,PlantStage.Seed);
            // Map.Plants.Add(newPlant);
            // Map.NewEntities.Add(newPlant);

            // }
        }

        protected abstract void CreateNewSeeds(List<Tile> possibleTiles, int numberOfPossibleTiles);
    }
}