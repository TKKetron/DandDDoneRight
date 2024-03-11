using System;
using System.Collections.Generic;
using static DirtyDandD.Globals.GlobalVariables;

namespace DirtyDandD.Classes
{
    public class Spell
    {

        public String spellName { get; init; } //1
        public String spellDescript { get; init; } //2
        public School school { get; init; } //3
        public bool concentration { get; init; } //4
        public bool ritual { get; init; } //5
        public Time time { get; init; } //6
        public string duration { get; init; } //7
        public int level { get; init; } //8
        public int range { get; init; } //9
        public List<Caster> casterList { get; init; } //10
        public List<Components> componentsList { get; init; } //11
        public string material { get; init; } //12
        public Source source { get; init; } //13
        public string specialRange { get; init; } //14

        public Spell()
        {
            level = 0;
            ritual = false;
            specialRange = " feet";
            componentsList = new List<Components>();
            material = "";
            concentration = false;
            spellDescript = "";
            casterList = new List<Caster>();
        }

        public string GetCastTime()
        {
            switch (time)
            {
                case Time.A:
                    return "Action";

                case Time.Ba:
                    return "Bonus Action";

                case Time.day:
                    return "1 Day";

                case Time.H:
                    return "1 Hour";

                case Time.M:
                    return "1 Minute";

                case Time.Ms:
                    return "10 Minutes";

                case Time.R:
                    return "1 Reaction";
                default:
                    return "ERROR 404: NOT FOUND";

            }
        }

        public string GetRange()
        {
            switch (range)
            {
                case -1:
                    return specialRange;
                case 0:
                    if (specialRange.Equals(" feet"))
                        return "Self";
                    return "Self" + specialRange;
                case -2:
                    return "Touch";
                default:
                    return range.ToString() + specialRange;
            }
        }

        public string GetComponents()
        {
            string value = "";
            foreach (Components c in componentsList)
                value += c.ToString("g") + ", ";
            value = value.Remove(value.LastIndexOf(","), 1);
            if (material != "")
                value += material;
            return value;
        }

        public string GetCasterList()
        {
            string value = "";
            foreach (Caster c in casterList)
            {
                if (c == Caster.Artificer)
                    value += c.ToString("G");
                else if (c == Caster.Bard)
                    value += c.ToString("G");
                else if (c == Caster.Cleric)
                    value += c.ToString("G");
                else if (c == Caster.Druid)
                    value += c.ToString("G");
                else if (c == Caster.Paladin)
                    value += c.ToString("G");
                else if (c == Caster.Ranger)
                    value += c.ToString("G");
                else if (c == Caster.Sorcerer)
                    value += c.ToString("G");
                else if (c == Caster.Warlock)
                    value += c.ToString("G");
                else if (c == Caster.Wizard)
                    value += c.ToString("G");

                if (c != casterList[casterList.Count - 1])
                    value += ", ";

            }

            return value;
        }
    }
}
