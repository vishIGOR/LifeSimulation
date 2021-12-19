using LifeSimulation.ResourceClasses;

namespace LifeSimulation.ToolClasses.MiningTool
{
    public class PickAxe:MiningTool
    {
        PickAxe()
        {
            ResourceType = new Saltpeter();
            ID = 1;
        }
    }
}