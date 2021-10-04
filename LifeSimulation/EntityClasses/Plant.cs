namespace LifeSimulation.EntityClasses
{
    public abstract class Plant:Entity
    {
        protected int ReadyToCreep;
        protected int CreepCounter;
        
        protected virtual void Creep(){}
    }
}