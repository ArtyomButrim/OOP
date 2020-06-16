using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players
{
    [Serializable]
    public class Archer : Warrior
    {
        int shotAccuracy;
        string arrow;
        public int _shotAccuracy { get { return shotAccuracy; } set { shotAccuracy = value; } }

        public string _arrow { get { return arrow; } set { arrow = value; } }

        public static List<Player> archers = new List<Player>();

        public Archer()
        {
            
        }

        public Archer(int fizDamage, int kritDamage, int defence, int stamina, string arrow, int shotAccuracy,int age, string name, Weapon weapon) : base(fizDamage, defence, stamina, kritDamage, weapon, age, name)
        {
            this.shotAccuracy = shotAccuracy;
            this.arrow = arrow;
            archers.Add(this);
        }

        public override string getPlayerName()
        {
            return name;
        }

        public override List<Player> getList()
        {
            return archers;
        }

        public override void removePlayerFromList(string playerName)
        {
            List<Player> pl = new List<Player>();
            foreach(Player player in archers)
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
            archers = pl;
        }

        public override List<string> getAllFieldsAsStringList()
        {
            List<string> info = new List<string>();
            info.Add("Имя: " + name);
            info.Add("Возраст : " + age);
            info.Add("Характеристики персонажа: ");
            info.Add("Физический урон: " + fizDamage);
            info.Add("Выносливость:" + stamina);
            info.Add("Вероятность нанесения \nкритического удара: " + kritDamage);
            info.Add("Защита: " + defence);
            info.Add("Точность попадания: " + shotAccuracy);
            info.Add("Тип стрел: " + arrow);
            info.Add("Оружие:");
            info.Add("\tОсобый навык оружия: " + weapon._specialSkill);
            info.Add("\tДополнительный урон: " + weapon._dopDamage);
            return info;

        }

        public override void setWeapon(string skill, int dopDamage)
        {
            weapon._dopDamage = dopDamage;
            weapon._specialSkill = skill;
        }
    }
}
