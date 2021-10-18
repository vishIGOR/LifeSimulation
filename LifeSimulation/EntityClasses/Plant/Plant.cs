using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public abstract class Plant:Entity
    {
        protected int ReadyToSeed;
        protected int SeedCounter;

        protected int Age;
        protected int MaxAge;
        protected GrowthStage GrowthStage;
        
        protected abstract void CreateSeeds();
        protected abstract void ChangeGrowthStage(GrowthStage newStage);
        
        protected override void Die()
        {
            Map.Plants.Remove(this);
            Map.DeadEntities.Add(this);
        }

        protected override void SetStandartValues(Tile tile, Map map)
        {
            Tile = tile;
            Map = map;
            Randomizer = Map.Randomizer;

            Age = 0;
            Eatable = false;
            GrowthStage = GrowthStage.Seed;
            
            Tile.IsSeeded = true;

            HitPoints = MaxHitPoints;
            
            SeedCounter = Randomizer.GetRandomInt(0, ReadyToSeed-5);
            
        }
    }
}