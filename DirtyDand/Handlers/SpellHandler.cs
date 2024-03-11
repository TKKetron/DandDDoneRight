using DirtyDandD.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using static DirtyDandD.Globals.GlobalVariables;

namespace DirtyDandD.Handlers
{
    public static class SpellHandler
    {
        private static List<Spell> spellRegistry = new List<Spell>();

        public static void InitSpellHandler()
        {
            string result;
            result = File.ReadAllText("./Resources\\Spells5e.txt");
            string[] spells = result.Split('~');

            foreach (string e in spells)
            {
                (string, string, School, bool, bool, Time, string, int, int, List<Caster>, List<Components>, string, Source, string) current =
                    ("", "", School.Abjuration, false, false, Time.A, "", 0, 0, new List<Caster>(), new List<Components>(), "", Source.PHB, "");
                //Splits spells into lines
                List<string> lines = e.Split('\r').ToList();
                if (lines[0] == "")
                    lines.RemoveAt(0);

                for (int i = 0; i < 6; i++)
                    if (lines[i].StartsWith("\n"))
                        lines[i] = lines[i].Remove(0, 1) + " ";
                //Gets the spell name

                current.Item1 = lines[0];

                //Gets the spell level
                if (Int32.TryParse(lines[1].Substring(0, 1), out int strLevel))
                    current.Item8 = strLevel;

                //Gets the spell school
                if (lines[1].IndexOf("Abjuration") >= 0 || lines[1].IndexOf("abjuration") >= 0)
                    current.Item3 = School.Abjuration;
                else if (lines[1].IndexOf("Conjuration") >= 0 || lines[1].IndexOf("conjuration") >= 0)
                    current.Item3 = School.Conjuration;
                else if (lines[1].IndexOf("Divination") >= 0 || lines[1].IndexOf("divination") >= 0)
                    current.Item3 = School.Divination;
                else if (lines[1].IndexOf("Enchantment") >= 0 || lines[1].IndexOf("enchantment") >= 0)
                    current.Item3 = School.Enchantment;
                else if (lines[1].IndexOf("Evocation") >= 0 || lines[1].IndexOf("evocation") >= 0)
                    current.Item3 = School.Evocation;
                else if (lines[1].IndexOf("Illusion") >= 0 || lines[1].IndexOf("illusion") >= 0)
                    current.Item3 = School.Illusion;
                else if (lines[1].IndexOf("Necromancy") >= 0 || lines[1].IndexOf("necromancy") >= 0)
                    current.Item3 = School.Necromancy;
                else if (lines[1].IndexOf("Transmutation") >= 0 || lines[1].IndexOf("transmutation") >= 0)
                    current.Item3 = School.Transmutation;

                //Determines if the spell can be ritual cast
                if (lines[1].Contains("(ritual)"))
                    current.Item5 = true;

                //Gets the spell casting time
                if (lines[2].Contains("1 action"))
                    current.Item6 = Time.A;
                else if (lines[2].Contains("1 bonus action"))
                    current.Item6 = Time.Ba;
                else if (lines[2].Contains("1 reaction"))
                    current.Item6 = Time.R;
                else if (lines[2].Contains("1 minute"))
                    current.Item6 = Time.M;
                else if (lines[2].Contains("10 minutes"))
                    current.Item6 = Time.Ms;
                else if (lines[2].Contains("1 hour"))
                    current.Item6 = Time.H;
                // Gets the range of the spell
                int range = -1;
                if (!Int32.TryParse(lines[3].Substring(7, 3), out range))
                    if (!Int32.TryParse(lines[3].Substring(7, 2), out range))
                        if (!Int32.TryParse(lines[3].Substring(7, 1), out range))
                            if (lines[3].Substring(7, 1).Equals("T"))
                                current.Item9 = -1;//Touch Range
                            else if (lines[3].Substring(7, 2).Equals("Sp"))
                            {
                                current.Item9 = -1;
                                current.Item14 = "Special";
                            }
                            else if (lines[3].Substring(7, 2).Equals("Se") && lines[3].Length == 12)
                                current.Item9 = 0;//Self Range
                            else if (lines[3].Substring(7, 2).Equals("Si"))
                            {
                                current.Item9 = -1;
                                current.Item14 = "Sight"; //The damn sight ranges
                            }
                            else if (lines[3].Substring(7, 2).Equals("Un"))
                            {
                                current.Item9 = -1;
                                current.Item14 = "Unlimited";
                            }
                            else
                            {
                                current.Item9 = 0;
                                current.Item14 = " " + lines[3].Substring(lines[3].IndexOf("(") + 1, lines[3].Length - lines[3].IndexOf("(") - 3);
                            }
                if (range != -1)
                    current.Item9 = range;
                if (lines[3].Contains("mile"))
                    current.Item14 = " mile";
                //Gets the spell components
                if (lines[4].IndexOf("V") >= 0)
                    current.Item11.Add(Components.V);
                if (lines[4].IndexOf("S") >= 0)
                    current.Item11.Add(Components.S);
                if (lines[4].IndexOf("M") >= 0)
                {
                    current.Item11.Add(Components.M);
                    current.Item12 = lines[4].Substring(lines[4].IndexOf("M") + 3, lines[4].Length - lines[4].IndexOf("M") - 5);
                }

                //Gets the spell duration
                current.Item7 = lines[5].Substring(10);
                if (current.Item7.Contains("Concentration"))
                {
                    current.Item4 = true;
                    current.Item7 = current.Item7.Remove(0, current.Item7.IndexOf("to") + 3);
                }

                //Gets the full spell description
                int count = 6;
                current.Item2 = "";
                while (!lines[count].Contains("Classes:") && !lines[count].Contains("Subclasses:") && !lines[count].Contains("Backgrounds:"))
                {
                    current.Item2 += lines[count];
                    ++count;
                }

                //Gets the list of classes able to use the spell
                //Artificer, Bard, Cleric, Druid, Paladin, Ranger, Sorcerer, Warlock, Wizard
                string classList = lines[count];
                if (lines[count].Contains("Artificer"))
                    current.Item10.Add(Caster.Artificer);
                if (lines[count].Contains("Bard"))
                    current.Item10.Add(Caster.Bard);
                if (lines[count].Contains("Cleric"))
                    current.Item10.Add(Caster.Cleric);
                if (lines[count].Contains("Druid"))
                    current.Item10.Add(Caster.Druid);
                if (lines[count].Contains("Paladin"))
                    current.Item10.Add(Caster.Paladin);
                if (lines[count].Contains("Ranger"))
                    current.Item10.Add(Caster.Ranger);
                if (lines[count].Contains("Sorcerer"))
                    current.Item10.Add(Caster.Sorcerer);
                if (lines[count].Contains("Warlock"))
                    current.Item10.Add(Caster.Warlock);
                if (lines[count].Contains("Wizard"))
                    current.Item10.Add(Caster.Wizard);

                //Gets the source the spell was published from
                string fakeSource = lines[lines.Count() - 2].Substring(8);
                if (fakeSource.Contains("PHB"))
                    current.Item13 = Source.PHB;
                else if (fakeSource.Contains("DMG"))
                    current.Item13 = Source.DMG;
                else if (fakeSource.Contains("XGE"))
                    current.Item13 = Source.XGE;
                else if (fakeSource.Contains("TCE"))
                    current.Item13 = Source.TCE;
                else if (fakeSource.Contains("GGR"))
                    current.Item13 = Source.GGR;
                else if (fakeSource.Contains("IDRotF"))
                    current.Item13 = Source.IDRotF;
                else if (fakeSource.Contains("MTF"))
                    current.Item13 = Source.MTF;
                else if (fakeSource.Contains("MOT"))
                    current.Item13 = Source.MOT;
                else if (fakeSource.Contains("AI"))
                    current.Item13 = Source.AI;
                else if (fakeSource.Contains("SCAG"))
                    current.Item13 = Source.SCAG;
                else if (fakeSource.Contains("EEPC"))
                    current.Item13 = Source.EEPC;
                else if (fakeSource.Contains("VGM"))
                    current.Item13 = Source.VGM;
                else if (fakeSource.Contains("ERLW"))
                    current.Item13 = Source.ERLW;
                else if (fakeSource.Contains("AWM"))
                    current.Item13 = Source.AWM;
                else if (fakeSource.Contains("LR"))
                    current.Item13 = Source.LR;
                else if (fakeSource.Contains("LLK"))
                    current.Item13 = Source.LLK;
                else if (fakeSource.Contains("OGA"))
                    current.Item13 = Source.OGA;
                else if (fakeSource.Contains("PS"))
                    current.Item13 = Source.PS;
                else if (fakeSource.Contains("TTP"))
                    current.Item13 = Source.TTP;
                else if (fakeSource.Contains("UA"))
                    current.Item13 = Source.UA;
                else if (fakeSource.Contains("WGE"))
                    current.Item13 = Source.WGE;
                spellRegistry.Add(new Spell()
                {
                    spellName = current.Item1,
                    spellDescript = current.Item2,
                    school = current.Item3,
                    concentration = current.Item4,
                    ritual = current.Item5,
                    time = current.Item6,
                    duration = current.Item7,
                    level = current.Item8,
                    range = current.Item9,
                    casterList = current.Item10,
                    componentsList = current.Item11,
                    material = current.Item12,
                    source = current.Item13,
                    specialRange = current.Item14,
                });
            }

            SortLevel();
        }

        public static void SortLevel()
        {
            List<Spell>[] levelLists = new List<Spell>[10];

            for (int i = 0; i < 10; i++)
                levelLists[i] = new List<Spell>();

            foreach (Spell s in spellRegistry)
                levelLists[s.level].Add(s);

            spellRegistry.Clear();

            foreach (List<Spell> l in levelLists)
                spellRegistry.AddRange(l);
        }

        public static IReadOnlyCollection<Spell> GetAllSpells()
        {
            return spellRegistry;
        }

        public static IReadOnlyCollection<Spell> SpellSearch(string search)
        {
            return spellRegistry.FindAll(x => x.spellName.ToLower().Contains(search.ToLower()));
        }

        public static Spell SpellSelect(string spell)
        {
            return spellRegistry.FirstOrDefault(x => x.spellName.Equals(spell));
        }
    }
}
