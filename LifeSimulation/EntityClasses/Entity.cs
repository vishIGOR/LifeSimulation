﻿using System;
using System.Drawing;
using System.IO;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.MapClasses.Enumerators;
using LifeSimulation.TileClasses;
namespace LifeSimulation.EntityClasses
{
    public abstract class Entity
    {
        public Image Image { get; protected set; }
        public Tile Tile { get; protected set; }
        
        public int HitPoints{ get; protected set; }
        public int MaxHitPoints{ get; protected set; }
        
        protected Randomizer Randomizer;
        protected Map Map;

        public bool Eatable{ get; protected set; }
        public bool Toxicity{ get; protected set; }
        public int ToxicityValue{ get; protected set; }
        public int ToxicityCounter{ get; protected set; }
        
        public Brush Color { get; protected set; }
        public abstract void ChooseAction();
        protected abstract void Die();

        public void DamageIt(int damage)
        {
            HitPoints -= damage;
        }

        protected virtual void SetStandartValues(Tile tile, Map map)
        {
            Tile = tile;
            Map = map;
            Randomizer = Map.Randomizer;
            
            
            Image = System.Drawing.Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"../../Images/",GetType()+".png"));
        }

        public virtual void ReactToChangeSeason(SeasonType newSeason)
        {
            if (newSeason == SeasonType.Winter)
            {
                HitPoints -= 5;
            }
        }
    }
}