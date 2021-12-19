using System;
using System.Collections.Generic;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.MapClasses;

namespace LifeSimulation.VillageClasses
{
    public class VillagesObserver
    {
        private List<Village> Villages = new List<Village>();
        private List<(Village, Village)> CurrentWars = new List<(Village, Village)>();
        private Map Map;
        public Randomizer Randomizer{ get; private set; }
        private List<String> Adjectives = new List<string>();
        private List<String> Nouns = new List<string>();
        private int NumberOfVillages = 0;

        public VillagesObserver(Map map)
        {
            Map = map;
            Randomizer = Map.Randomizer;

            Nouns.Add("смельчаки");
            Nouns.Add("добытчики");
            Nouns.Add("первопроходцы");
            Nouns.Add("умнички");
            Nouns.Add("уничтожители");
            Nouns.Add("земляне");
            Nouns.Add("везунчики");
            Nouns.Add("повесы");
            Nouns.Add("богачи");
            Nouns.Add("трудоголики");
            Nouns.Add("мастера");
            Nouns.Add("победители");
            Nouns.Add("защитники");
            Nouns.Add("студенты");
            Nouns.Add("жители");

            Adjectives.Add("красивые");
            Adjectives.Add("умные");
            Adjectives.Add("голодные");
            Adjectives.Add("ленивые");
            Adjectives.Add("дружные");
            Adjectives.Add("сильные");
            Adjectives.Add("сплоченные");
            Adjectives.Add("смешные");
            Adjectives.Add("ветреные");
            Adjectives.Add("крутые");
            Adjectives.Add("глуповатые");
            Adjectives.Add("агрессивные");
            Adjectives.Add("молодые");
            Adjectives.Add("удачливые");
            Adjectives.Add("быстрые");
        }

        public Village CreateVillage()
        {
            ++NumberOfVillages;

            String adjective = Adjectives[Randomizer.GetRandomInt(0, Adjectives.Count - 1)];
            String noun = Nouns[Randomizer.GetRandomInt(0, Nouns.Count - 1)];

            Village newVillage = new Village(Randomizer.GetRandomInt(1, 100) + " " + adjective + " " + noun, this);

            Villages.Add(newVillage);
            return newVillage;
        }
    }
}