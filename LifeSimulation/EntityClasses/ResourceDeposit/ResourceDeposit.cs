using LifeSimulation.ResourceClasses;

namespace LifeSimulation.EntityClasses.ResourceDeposit
{
    public abstract class ResourceDeposit:Entity
    {
        public int MaxFullness{ get; protected set; }
        public int Fullness{ get; protected set; }
        public ResourceType ResourceType{ get; protected set; }
        public override void ChooseAction()
        {
            throw new System.NotImplementedException();
        }

        protected override void Die()
        {
            throw new System.NotImplementedException();
        }
    }
}