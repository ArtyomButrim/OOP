using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Players
{
    public class SwordsMan : Warrior 
    {
        string skill;
        string canUseSecondSword;
        public string _canUseSecondSword{ get { return canUseSecondSword; } set { canUseSecondSword = value; } }

        public string _skill { get { return skill; } set { skill = value; } }

        public static List<Player> swordsmans = new List<Player>();

        public SwordsMan()
        {

        }

        public SwordsMan(int fizDamage, int kritDamage, int defence, int stamina, string skill, string canUseSecondSword, int age, string name, Weapon weapon) : base(fizDamage,defence, stamina, kritDamage,weapon,age,name )
        {
            this.skill = skill;
            this.canUseSecondSword = canUseSecondSword;
            swordsmans.Add(this);
        }
        public override List<Player> getList()
        {
            return swordsmans;
        }
        public override string getPlayerName()
        {
            return name;
        }

        public override void removePlayerFromList(string playerName)
        {
            List<Player> pl = new List<Player>();
            foreach (Player player in swordsmans)
            {
                if (player.getPlayerName() == playerName)
                {
                    continue;
                }
                else
                {
                    pl.Add(player);
                }
            }
            swordsmans = pl;
        }

        public override List<string> getAllFieldsAsStringList()
        {
            List<string> a = new List<string>();
            a.Add("Имя: " + name);
            a.Add("Возраст: " + age);
            a.Add("Физический урон: " + fizDamage);
            a.Add("Выносливость:" + stamina);
            a.Add("Вероятность нанесения критического удара: " + kritDamage);
            a.Add("Защита " + defence);
            a.Add("Возможность использовать второй меч: " + canUseSecondSword);
            a.Add("Особый навык мечника" + skill);
            return a;
        }

        public override void setWeapon(string skill, int dopDamage)
        {
            weapon._dopDamage = dopDamage;
            weapon._specialSkill = skill;
        }
    }

}

