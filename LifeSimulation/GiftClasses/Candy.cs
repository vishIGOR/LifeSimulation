using LifeSimulation.EntityClasses;
using LifeSimulation.MapClasses;

namespace LifeSimulation.GiftClasses
{
    public class Candy:Gift
    {
        protected override void GiftEffect()
        {
            Owner.RestoreHitPoints();
        }

        public Candy(Map map) : base(map)
        {
        }
    }
}