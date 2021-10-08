namespace LifeSimulation.EntityClasses
{
    public abstract class Fetus:Entity
    {
        
        public bool Toxicity { get; protected set; }
        public int ToxicityValue{ get; protected set; }

        protected int Age;
        protected int MaxAge;
        protected int ReadyToFall;
        protected int ReadyToSeed;
        
        public bool Eatable{ get; protected set; }

        protected abstract void FallAway();
        protected abstract void CreateSeed();
    }
}