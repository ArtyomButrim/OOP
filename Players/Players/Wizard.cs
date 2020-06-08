using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players
{
    public class Wizard:Player
    {
        int magicDamage;
        int mana;
        string magicType;

        public int _magicDamage { get { return magicDamage; } set { magicDamage = value; } }
        public int _mana { get { return mana; } set { mana = value; } }
        public string _magicType { get { return magicType; } set { magicType = value; } }

        public static List<Player> wizards = new List<Player>();
        public Wizard()
        {

        }

        public Wizard(int magicDamage, int mana, string magicType, string name, int age): base(name, age)
        {
            this.mana = mana;
            this.magicDamage = magicDamage;
            this.magicType = magicType;
            wizards.Add(this);
        }
        public override List<Player> getList()
        {
            return wizards;
        }
        public override string getPlayerName()
        {
            return name;
        }
        public override void removePlayerFromList(string playerName)
        {
            List<Player> pl = new List<Player>();
            foreach (Player player in wizards)
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
            wizards = pl;
        }

        public override List<string> getAllFieldsAsStringList()
        {
            List<string> a = new List<string>();
            a.Add("Имя: " + name);
            a.Add("Возраст: " + age);
            a.Add("Магический урон: " + magicDamage);
            a.Add("Подвластная стихия: " + magicType);
            a.Add("Мана: " + mana);
            return a;
        }
    }
}
