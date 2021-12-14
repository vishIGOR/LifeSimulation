using LifeSimulation.ResourceClasses;

namespace LifeSimulation.EntityClasses
{
    public interface IMineable
    {
        (ResourceType,int) BeMined();
        ResourceType ReturnResourceType();
    }
}