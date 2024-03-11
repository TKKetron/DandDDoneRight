using System.Collections.Generic;
using static DirtyDandD.Globals.GlobalVariables;

namespace DirtyDandD.Classes
{
    public class Character
    {
        string name;
        public List<BaseClass> classes;
        List<int> levels;
        Race race;
        Background background;
        Alignment alignment;
        string playerName;
        int EXP;
        int STR;
        int DEX;
        int CON;
        int INT;
        int WIS;
        int CHA;
        List<Abilities> savingThrows;
        List<bool> proficiencies;
        List<bool> experties;
        int AC;
        int initiative;
        int speed;
        int HP;
        Dice hitDice;
        List<Weapon> weapons;
        List<Item> items;
        bool[] deathSavesS;
        bool[] deathSavesF;
        int CP;
        int SP;
        int EP;
        int GP;
        int PP;





    }
}