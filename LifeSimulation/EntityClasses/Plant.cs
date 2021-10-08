using LifeSimulation.Enumerations;

namespace LifeSimulation.EntityClasses
{
    public abstract class Plant:Entity
    {
        protected int ReadyToSeed;
        protected int SeedCounter;

        public bool Toxicity { get; protected set; }
        public int ToxicityValue{ get; protected set; }

        protected int Age;
        protected int MaxAge;
        protected GrowthStage GrowthStage;
        public bool Eatable{ get; protected set; }
        
        protected abstract void CreateSeeds();
        protected abstract void ChangeGrowthStage(GrowthStage newStage);
    }
}