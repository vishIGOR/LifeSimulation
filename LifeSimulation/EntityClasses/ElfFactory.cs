using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using LifeSimulation.MapClasses;
using LifeSimulation.MapClasses.Enumerators;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class ElfFactory:Entity, IChristmas
    {
        private bool IsActive = false;

        public ElfFactory(Map map, Tile tile)
        {
            MaxHitPoints = 100;
            SetStandartValues(tile,map);

            Tile.SpecialObject = this;
        }
        public void BeNotified(bool newState)
        {
            IsActive = newState;
            if (IsActive == false)
            {
                Image = System.Drawing.Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"../../Images/","Nothing.png"));
            }
            else
            {
                Image = System.Drawing.Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"../../Images/","LifeSimulation.EntityClasses.ElfFactory.png"));
            }
        }

        public override void ChooseAction()
        {
            if (HitPoints <= 0)
            {
                Die();
            }
        }

        public override void ReactToChangeSeason(SeasonType newSeason)
        {
            if(newSeason == SeasonType.Winter)
                HitPoints = MaxHitPoints;
        }

        protected override void Die()
        {
            
            Map.Entities.Remove(this);
            Map.ElfFactories.Remove(this);
            Map.ChristmasObjects.Remove(this);
            Tile.SpecialObject = null;
        }

        protected override void SetStandartValues(Tile tile, Map map)
        {
            Eatable = false;

            HitPoints = MaxHitPoints;
            base.SetStandartValues(tile, map);
            Image = System.Drawing.Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"../../Images/","Nothing.png"));

        }
    }
}