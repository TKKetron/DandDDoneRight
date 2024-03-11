using System.Collections.Generic;
using static DirtyDandD.Globals.GlobalVariables;

namespace DirtyDandD.Classes
{
    public class Weapon : Item
    {
        int damage;
        DamageType damageType;
        List<WeaponProperty> properties;
        bool magical;
        int[] range;


        public Weapon(int damage, DamageType damageType, bool magical, string name, string description, int[] range = null, int value = 0) : base(name, description, value)
        {
            this.damage = damage;
            this.damageType = damageType;
            this.magical = magical;
            this.range = range;
        }


    }
}
