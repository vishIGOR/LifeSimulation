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
        protected PlantStage PlantStage;
        
        protected abstract void CreateSeeds();
        protected abstract void ChangePlantStage(PlantStage newStage);
        
        protected override void Die()
        {
            Map.Plants.Remove(this);
            Map.DeadEntities.Add(this);
        }

        protected override void SetStandartValues(Tile tile, Map map)
        {
            base.SetStandartValues(tile, map);

            Age = 0;
            Eatable = false;
            PlantStage = PlantStage.Seed;
            
            Tile.IsSeeded = true;

            HitPoints = MaxHitPoints;
            
            SeedCounter = Randomizer.GetRandomInt(0, ReadyToSeed-5);
            
        }
    }
}