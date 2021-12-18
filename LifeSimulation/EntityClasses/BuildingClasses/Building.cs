using LifeSimulation.MapClasses;
using LifeSimulation.MapClasses.Enumerators;
using LifeSimulation.ResourceClasses;
using LifeSimulation.TileClasses;
using LifeSimulation.VillageClasses;

namespace LifeSimulation.EntityClasses.BuildingClasses
{
    public abstract class Building : Entity
    {
        public static (ResourceType, int) ResourceCost { get; protected set; }
        public Village Village { get; protected set; }

        public override void ChooseAction()
        {
            if (HitPoints <= 0)
            {
                Die();
            }
        }

        public override void ReactToChangeSeason(SeasonType newSeason)
        {
            if (newSeason == SeasonType.Winter)
            {
                HitPoints -= 10;
            }
        }

        protected override void Die()
        {
            Tile.SpecialObject = null;
            Map.Buildings.Remove(this);
            Map.DeadEntities.Add(this);
        }

        protected override void SetStandartValues(Tile tile, Map map)
        {
            base.SetStandartValues(tile, map);

            Eatable = false;
            HitPoints = MaxHitPoints;


            Tile.SpecialObject = this;
            Map.Buildings.Add(this);
            Map.NewEntities.Add(this);
        }

        public void ChangeVillage(Village newVillage)
        {
           

            Village = newVillage;
            Village.AddBuilding(this);
            
            
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i + Tile.X > 0 && i + Tile.X < Map.Width &&
                        j + Tile.Y > 0 && j + Tile.Y < Map.Height)
                    {
                        if (Map.Tiles[Tile.X + i, Tile.Y + j].SpecialObject is Building)
                        {
                            if ((Map.Tiles[Tile.X + i, Tile.Y + j].SpecialObject as Building).Village != newVillage)
                            {
                                (Map.Tiles[Tile.X + i, Tile.Y + j].SpecialObject as Building).ChangeVillage(newVillage);
                            }
                        }
                    }
                }
            }
            
            if (this is LivingHouse)
            {
                foreach (var owner in (this as LivingHouse).Owners)
                {
                    owner.ChangeVillage(newVillage);
                    Village.AddHuman(owner);
                }
            }
        }

        protected void JoinTheVillage()
        {
            Village newVillage = null;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i + Tile.X > 0 && i + Tile.X < Map.Width &&
                        j + Tile.Y > 0 && j + Tile.Y < Map.Height)
                    {
                        if (Map.Tiles[Tile.X + i, Tile.Y + j].SpecialObject is Building)
                        {
                            if ((Map.Tiles[Tile.X + i, Tile.Y + j].SpecialObject as Building).Village != null)
                            {
                                newVillage = (Map.Tiles[Tile.X + i, Tile.Y + j].SpecialObject as Building).Village;
                                break;
                            }
                        }
                    }
                }
            }

            
            if (newVillage != null)
            {
                
                Village = newVillage;
                Village.AddBuilding(this);
                
                if (this is LivingHouse)
                {
                    foreach (var owner in (this as LivingHouse).Owners)
                    {
                        owner.ChangeVillage(newVillage);
                    }
                }
            }

        }
    }
}