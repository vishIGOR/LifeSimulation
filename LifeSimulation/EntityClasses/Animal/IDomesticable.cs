using LifeSimulation.EntityClasses.Omnivore;
using LifeSimulation.Enumerations;

namespace LifeSimulation.EntityClasses
{
    public interface IDomesticable
    {
        bool TryToBeTamed(Human master);
        void DoSpecialAbility();
        void BeUntamed();
        void EatMasterFood(FoodType foodType);
    }
}