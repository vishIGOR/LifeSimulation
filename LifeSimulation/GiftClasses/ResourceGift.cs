using System.Windows.Forms;
using LifeSimulation.MapClasses;
using LifeSimulation.ResourceClasses;

namespace LifeSimulation.GiftClasses
{
    public class ResourceGift:Gift
    {
        protected override void GiftEffect()
        {
            ResourceType randomResource = null;
            switch (Randomizer.GetRandomInt(1, 2))
            {
                case 1:
                    randomResource = new Saltpeter();
                    break;
                case 2:
                    randomResource = new Wood();
                    break;
            }
            Owner.GetResource(randomResource,20);
        }

        public ResourceGift(Map map) : base(map)
        {
        }
    }
}