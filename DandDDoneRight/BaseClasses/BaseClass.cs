using System.Collections.Generic;
using System.Data;
using static DirtyDandD.Globals.GlobalVariables;

namespace DirtyDandD.Classes
{
    public class BaseClass
    {
        public string name { get; init; }
        public Dice hitDice { get; init; }

        public List<ArmorType> armorProficiency { get; init; }
        public List<WeaponType> weaponProficiency { get; init; }
        public List<Weapon> specificWeaponProficiency { get; init; }
        public List<ArtisiansTools> toolProficiency { get; init; }


        public List<Abilities> savingThrows { get; init; }
        public List<Skills> skills { get; init; }
        public int skillCount { get; init; }

        public DataTable classTable { get; init; }

        public List<Feature> features { get; init; }
        public BaseClass() 
        { 
        }
        public BaseClass(string name1, Dice hitDice1, List<ArmorType> armorProficiency1, List<WeaponType> weaponProficiency1, List<Weapon> specificWeaponProficiency1, List<ArtisiansTools> toolProficiency1, List<Abilities> savingThrows1, List<Skills> skills1, int skillCount1, List<DataColumn> columns, List<object[]> rows, List<Feature> features1)
        {
            name = name1;
            hitDice = hitDice1;

            armorProficiency = armorProficiency1;
            weaponProficiency = weaponProficiency1;
            specificWeaponProficiency = specificWeaponProficiency1;
            toolProficiency = toolProficiency1;


            savingThrows = savingThrows1;
            skills = skills1;
            skillCount = skillCount1;

            classTable = new DataTable();

            foreach (DataColumn c in columns)
                classTable.Columns.Add(c);
            foreach (object[] r in rows)
                classTable.Rows.Add(r);

            features = features1;
        }

        public string GetName()
        {
            return name;
        }
        public DataTable GetClassTable()
        {
            return classTable;
        }
        public Dice GetDice()
        {
            return hitDice;
        }
        public List<ArmorType> GetArmorTypes()
        {
            return armorProficiency;
        }
        public List<WeaponType> GetWeaponTypes()
        {
            return weaponProficiency;
        }
        public List<Weapon> GetWeapons()
        {
            return specificWeaponProficiency;
        }
        public List<ArtisiansTools> GetArtisiansTools()
        {
            return toolProficiency;
        }
        public List<Abilities> GetSavingThrows()
        {
            return savingThrows;
        }
        public List<Skills> GetSkills()
        {
            return skills;
        }
        public int SkillAmount()
        {
            return skillCount;
        }

        public List<Feature> GetFeatures(int level)
        {
            return features.FindAll(x => x.level < level);
        }
    }
}