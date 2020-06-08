using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players
{
    public class Weapon
    {
        string specialSkill;
        int dopDamage;

        public string _specialSkill { get { return specialSkill; } set { specialSkill = value; } }

        public int _dopDamage { get { return dopDamage; } set { dopDamage = value; } }

        public Weapon()
        {

        }

        public Weapon(string specialSkill, int dopDamage)
        {
            this.dopDamage = dopDamage;
            this.specialSkill = specialSkill;
        }
    }
}
