using LifeSimulation.AdditionalClasses;
using LifeSimulation.EntityClasses;
using LifeSimulation.MapClasses;

namespace LifeSimulation.GiftClasses
{
    public abstract class Gift
    {
        protected Human Owner;
        protected Randomizer Randomizer;

        public Gift(Map map)
        {
            Randomizer = map.Randomizer;
        }
        public Gift InsertedGift { get; protected set; }
        public  void Unpack()
        {
            GiftEffect();
            if (InsertedGift != null)
            {
                InsertedGift.Unpack();
            }
        }

        protected abstract void GiftEffect();
        
        public void SetOwner(Human human)
        {
            Owner = human;
        }
        
        public void InsertGift(Gift gift)
        {
            if (InsertedGift == null)
            {
                InsertedGift = gift;
            }
            else
            {
                InsertedGift.InsertGift(gift);
            }
        }
    }
}