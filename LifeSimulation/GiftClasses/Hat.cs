using LifeSimulation.MapClasses;

namespace LifeSimulation.GiftClasses
{
    public class Hat:Gift
    {
        protected override void GiftEffect()
        {
            Owner.IncreaseMaxHitPoints(20);
        }

        public Hat(Map map) : base(map)
        {
        }
    }
}