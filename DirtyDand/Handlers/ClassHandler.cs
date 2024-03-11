using DirtyDandD.Classes;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using static DirtyDandD.Globals.GlobalVariables;


namespace DirtyDandD.Handlers
{
    public static class ClassHandler
    {
        private static List<BaseClass> classRegistry = new List<BaseClass>();

        public static void InitClassHandler()
        {

            string classesFile = File.ReadAllText("./Resources\\Classes5ETemp.txt");
            string[] classes = classesFile.Split('~');

            foreach (string c in classes)
            {
                (string, Dice, List<ArmorType>, List<WeaponType>, List<Weapon>, List<ArtisiansTools>, List<Abilities>, List<Skills>, int, DataTable, List<Feature>) current =
                ("", Dice.D10, new List<ArmorType>(), new List<WeaponType>(), new List<Weapon>(), new List<ArtisiansTools>(), new List<Abilities>(), new List<Skills>(), 1, new DataTable(), new List<Feature>());

                List<string> lines = c.Split('\r').ToList();
                if (lines[0] == "")
                    lines.RemoveAt(0);



                int nameNum = 0, diceNum = 31, armorNum = 36, weaponNum = 37, toolNum = 38, stNum = 39, skillNum = 40, tableNum = 6, featureStart;
                //Gets Class Name
                if (lines[nameNum].Contains("\n"))
                    lines[nameNum].Remove(0, 1);
                current.Item1 = lines[nameNum];

                //Gets Class Hit Dice
                if (lines[diceNum].Contains("d6"))
                    current.Item2 = Dice.D6;
                else if (lines[diceNum].Contains("d8"))
                    current.Item2 = Dice.D8;
                else if (lines[diceNum].Contains("d10"))
                    current.Item2 = Dice.D10;
                else if (lines[diceNum].Contains("d12"))
                    current.Item2 = Dice.D12;

                //Gets List of Armor Proficiencies
                if (lines[armorNum].ToLower().Contains("light"))
                    current.Item3.Add(ArmorType.Light);
                if (lines[armorNum].ToLower().Contains("medium"))
                    current.Item3.Add(ArmorType.Medium);
                if (lines[armorNum].ToLower().Contains("all"))
                {
                    current.Item3.Add(ArmorType.Light);
                    current.Item3.Add(ArmorType.Medium);
                    current.Item3.Add(ArmorType.Heavy);
                }
                if (lines[armorNum].ToLower().Contains("shields"))
                    current.Item3.Add(ArmorType.Shield);

                //Gets List of General Weapon Proficiencies
                lines[weaponNum] = lines[weaponNum].Remove(0, 10);
                string temp = lines[8];
                while (lines[weaponNum].Length > 0)
                {
                    temp = lines[weaponNum];
                    if (lines[weaponNum].ToLower().Contains("simple"))
                    {
                        current.Item4.Add(WeaponType.Simple);
                        if (lines[weaponNum].Length > 14)
                            lines[weaponNum] = lines[weaponNum].Remove(0, 16);
                        else
                            break;
                    }
                    else if (lines[weaponNum].ToLower().Contains("martial"))
                    {
                        current.Item4.Add(WeaponType.Martial);
                        if (lines[weaponNum].Length > 15)
                            lines[weaponNum] = lines[weaponNum].Remove(0, 17);
                        else
                            break;
                    }
                }

                current.Item5 = null;
                current.Item6.Add(ArtisiansTools.ThievesTools);
                current.Item7.Add(Abilities.Str);
                current.Item7.Add(Abilities.Con);
                current.Item8.Add(Skills.Acrobatics);
                current.Item8.Add(Skills.AnimalHandling);
                current.Item8.Add(Skills.Athletics);
                current.Item8.Add(Skills.History);
                current.Item8.Add(Skills.Insight);
                current.Item8.Add(Skills.Intimidation);
                current.Item8.Add(Skills.Perception);
                current.Item8.Add(Skills.Survival);
                current.Item9 = 2;
                current.Item10.Columns.Add("Level", typeof(string));
                current.Item10.Columns.Add("prof bonus", typeof(string));
                current.Item10.Columns.Add("Features", typeof(string));
                current.Item10.Rows.Add("1st", "+2", "Fighting Stle, Second Wind");
                current.Item10.Rows.Add("2nd", "+2", "Action Surge (x1)");



                if (current.Item1.Contains("Blood"))
                    continue;
                int featureColIndex = 0;
                for (int i = 0; i < current.Item10.Columns.Count; i++)
                    if (current.Item10.Columns[i].ColumnName.Equals("Features"))
                    {
                        featureColIndex = i;
                        break;
                    }
                string firstFeat = current.Item10.Rows[0][featureColIndex].ToString();
                firstFeat = firstFeat.Substring(0, firstFeat.IndexOf(","));

                current.Item11.Add(new Feature() { name = "Test", level = 2, info = new List<string> { "test this is a teast for feature", "this is a second test for thew same feature" } });

                classRegistry.Add(new BaseClass()
                {
                    name = current.Item1,
                    hitDice = current.Item2,
                    armorProficiency = current.Item3,
                    weaponProficiency = current.Item4,
                    specificWeaponProficiency = current.Item5,
                    toolProficiency = current.Item6,
                    savingThrows = current.Item7,
                    skills = current.Item8,
                    skillCount = current.Item9,
                    classTable = current.Item10,
                    features = current.Item11,
                });
            }
            /*classRegistry.Add(new BaseClass
                (
            #region Blood Hunter
                    "Blood Hunter",
                    Dice.D10,
                    new List<ArmorType>
                    {
                        ArmorType.Light,
                        ArmorType.Medium,
                        ArmorType.Shield
                    },
                    new List<WeaponType>
                    {
                        WeaponType.Simple,
                        WeaponType.Martial
                    },
                    null,
                    new List<ArtisiansTools>
                    {
                        ArtisiansTools.AlchemistsSupplies
                    },
                    new List<Abilities>
                    {
                        Abilities.Dex,
                        Abilities.Int
                    },
                    new List<Skills>
                    {
                        Skills.Athletics,
                        Skills.Acrobatics,
                        Skills.Arcana,
                        Skills.History,
                        Skills.Insight,
                        Skills.Investigation,
                        Skills.Religion,
                        Skills.Survival
                    },
                    3,
                    new List<DataColumn>
                    {
                        new DataColumn("Level", typeof(string)),
                        new DataColumn("Proficiency Bonus", typeof(string)),
                        new DataColumn("Hemocraft Die", typeof(Dice)),
                        new DataColumn("Blood Curses Known", typeof(int)),
                        new DataColumn("Features", typeof(string)),

                    },
                    new List<object[]>
                    {
                        new object[] {"1st", "+2", Dice.D4, 1, "Hunter's Bane, Blood Maledict" },
                        new object[] {"2nd", "+2", Dice.D4, 1, "Fighting Style, Crimson Rite" },
                        new object[] {"3rd", "+2", Dice.D4, 1, "Blood Hunter Order" },
                        new object[] {"4th", "+2", Dice.D4, 1, "Ability Score Improvement" },
                        new object[] {"5th", "+3", Dice.D6, 1, "Extra Attack" },
                        new object[] {"6th", "+3", Dice.D6, 2, "Brand of Castigation, Blood Maledict (2/rest)" },
                        new object[] {"7th", "+3", Dice.D6, 2, "Order Feature, Primal Rite" },
                        new object[] {"8th", "+3", Dice.D6, 2, "Ability Score Improvement"},
                        new object[] {"9th", "+4", Dice.D6, 2, "Grim Psychometry" },
                        new object[] {"10th", "+4", Dice.D6, 3, "Dark Augmentation" },
                        new object[] {"11th", "+4", Dice.D8, 3, "Order Feature" },
                        new object[] {"12th", "+4", Dice.D8, 3, "Ability Score Improvement" },
                        new object[] {"13th", "+5", Dice.D8, 3, "Brand of Tethering, Blood Maledict (3/rest)" },
                        new object[] {"14th", "+5", Dice.D8, 4, "Hardened Soul, Esoteric Rite" },
                        new object[] {"15th", "+5", Dice.D8, 4, "Order Feature" },
                        new object[] {"16th", "+5", Dice.D8, 4, "Ability Score Improvemnt" },
                        new object[] {"17th", "+6", Dice.D10, 4, "Blood Maledict (4/rest)" },
                        new object[] {"18th", "+6", Dice.D10, 5, "Order Feature" },
                        new object[] {"19th", "+6", Dice.D10, 5, "Ability Score Improvement" },
                        new object[] {"20th", "+6", Dice.D10, 5, "Sanguine Mastery" }

                    },
                    new List<Feature>
                    {
                        new Feature()
                        {
                            name = "Hunter's Bane",
                            level = 1,
                            info = new List<string>
                            {
                                "Beginning at 1st level, you have survived the Hunter’s Bane, a dangerous, long-guarded ritual that alters your life’s blood, forever binding you to the darkness and honing your senses against it. You have advantage on Wisdom (Survival) checks to track fey, fiends, or undead, as well as on Intelligence ability checks to recall information about them.",
                                "The Hunter’s Bane also empowers your body to control and shape hemocraft magic, using your own blood and life essence to fuel your abilities. Some of your features require your target to make a saving throw to resist the feature’s effects. The saving throw DC is calculated as follows:",
                                "Hemocraft save DC| = 8 + your proficiency bonus + your Intelligence modifier."
                            }
                        },
                        new Feature()
                        {
                            name = "Blood Maledict",
                            level = 1,
                            info = new List<string>
                            {
                                "At 1st level, you gain the ability to channel, and sometimes sacrifice, a part of your vital essence to curse and manipulate creatures through hemocraft magic. You gain one Blood Curse of your choice. You learn one additional blood curse of your choice, and you can choose one of the blood curses you know and replace it with another blood curse, at 6th, 10th, 14th, and 18th level.",
                                "When you use your Blood Maledict, you choose which curse to invoke. While invoking a blood curse, but before it affects the target, you can choose to amplify the curse by losing a number of hit points equal to one roll of your hemocraft die, as shown in the Hemocraft Die column of the Blood Hunter table. An amplified curse gains an additional effect, noted in the curse’s description. Creatures that do not have blood in their bodies are immune to blood curses, unless you have amplified the curse.",
                                "You can use this feature once. Beginning at 6th level, you can use your Blood Maledict feature twice, at 13th level you can use it three times between rests, and at 17th level, you can use it four times between rests. You regain all expended uses when you finish a short or long rest."
                            }
                        },
                        new Feature()
                        {
                            name = "Fighting Style",
                            level = 2,
                            info = new List<string>
                            {
                                "At 2nd level, you adopt a style of fighting as your specialty. Choose one of the following options. You can’t take a Fighting Style option more than once, even if you later get to choose again.",
                                "Archery| You gain a +2 bonus to attack rolls you make with ranged weapons.",
                                "Dueling| When you are wielding a melee weapon in one hand and no other weapons, you gain a +2 bonus to damage rolls with that weapon.",
                                "Great Weapon Fighting| When you roll a 1 or 2 on a non-rite damage die for an attack you make with a melee weapon that you are wielding with two hands, you can reroll the die and must use the new roll. The weapon must have the two-handed or versatile property for you to gain this benefit.",
                                "Two-Weapon Fighting| When you engage in two-weapon fighting, you can add your ability modifier to the damage of the second attack."
                            }
                        }
                    }
                    #endregion
                )
                );*/
        }

        public static List<Feature> GetFeatures((string, int, string) character)
        {
            List<Feature> temp;
            temp = new List<Feature>(classRegistry.FirstOrDefault(y => y.GetName().Equals(character.Item1)).GetFeatures(character.Item2));
            //temp.AddRange(subclassRegistry.FirstOrDefault(y => y.GetName().Equal(character.Item3)).GetFeatures(character.Item2));
            return temp;
        }

    }
}
