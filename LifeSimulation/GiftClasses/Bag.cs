using LifeSimulation.MapClasses;

namespace LifeSimulation.GiftClasses
{
    public class Bag:Gift
    {
        protected override void GiftEffect()
        {
            Owner.FoodInventorySize += 5;
            Owner.ResourcesInventorySize += 20;
        }


        public Bag(Map map) : base(map)
        {
        }
    }
}