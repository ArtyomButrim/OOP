using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players
{
    public abstract class Warrior : Player
    {
        protected Weapon weapon;

        protected int fizDamage;
        protected int defence;
        protected int stamina;
        protected int kritDamage;

        public abstract void setWeapon(string skill, int dopDamage);

        public Weapon _weapon { get{ return weapon; } set { weapon = value; } }
        public int _kritDamage { get { return kritDamage; } set { kritDamage = value; } }

        public int _fizDamage { get { return fizDamage; } set { fizDamage = value; } }

        public int _defence { get { return defence; } set { defence = value; } }

        public int _stamina { get { return stamina; } set { stamina = value; } }

        public Warrior()
        {

        }

        public Warrior(int fizDamage, int defence, int stamina, int kritDamage, Weapon weapon, int age, string name) : base(name, age)          
        {
            this.fizDamage = fizDamage;
            this.defence = defence;
            this.stamina = stamina;
            this.kritDamage = kritDamage;
            this.weapon = weapon;
        }
        
    }

}
