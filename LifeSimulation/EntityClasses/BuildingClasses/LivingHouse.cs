using System.Collections.Generic;
using LifeSimulation.MapClasses;
using LifeSimulation.ResourceClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses.BuildingClasses
{
    public class LivingHouse : Building
    {
        public List<Human> Owners { get; protected set; }

        public LivingHouse(Tile tile, Map map)
        {
            MaxHitPoints = 120;
            SetStandartValues(tile, map);
            Owners = new List<Human>();
            ResourceCost = (new Wood(), 40);
            
        }

        public override void ChooseAction()
        {
            base.ChooseAction();

            if (Village == null)
            {
                int housesCounter = 0;
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (i + Tile.X > 0 && i + Tile.X < Map.Width &&
                            j + Tile.Y > 0 && j + Tile.Y < Map.Height)
                        {
                            if (Map.Tiles[Tile.X + i, Tile.Y + j].SpecialObject is Building)
                            {
                                ++housesCounter;
                            }
                        }
                    }
                }

                if (housesCounter >= 4)
                {
                    if (Owners.Count > 0)
                    {
                        Owners[0].ChangeProfession(1);
                        ChangeVillage(Owners[0].Village);
                        Village.AddVillageHead(Owners[0]);
                    }
                }
            }
        }


        public void Assign(Human newOwner)
        {
            Owners.Add(newOwner);
        }

        public void UnAssign(Human ownerNoMore)
        {
            Owners.Remove(ownerNoMore);
        }

        protected override void Die()
        {
            base.Die();
            foreach (var owner in Owners)
            {
                owner.House = null;
            }
        }
    }
}