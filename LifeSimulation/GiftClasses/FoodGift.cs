using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;

namespace LifeSimulation.GiftClasses
{
    public class FoodGift:Gift
    {
        public FoodGift(Map map) : base(map)
        {
        }

        protected override void GiftEffect()
        {
            FoodType randomFoodType = FoodType.Fetus;
            
            switch (Randomizer.GetRandomInt(1,4))
            {
                case 1:
                    randomFoodType = FoodType.Fetus;
                    break;
                case 2:
                    randomFoodType = FoodType.Meat;
                    break;
                case 3:
                    randomFoodType = FoodType.Plant;
                    break;
                case 4:
                    randomFoodType = FoodType.Honey;
                    break;
            }
            Owner.GetFood(randomFoodType);
        }
    }
}