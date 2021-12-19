using System.Collections.Generic;
using LifeSimulation.ToolClasses.MiningTool;

namespace LifeSimulation.EntityClasses.BuildingClasses
{
    public class Armory:Building
    {
        public Dictionary<int,int> MiningTools{ get; protected set; }
        public Dictionary<int,int> Weapons{ get; protected set; }
    }
}