using LifeSimulation.ResourceClasses;

namespace LifeSimulation.EntityClasses
{
    public interface IMeneable
    {
        (ResourceType,int) BeMined();
    }
}