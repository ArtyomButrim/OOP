using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players
{
    public abstract class Player
    {
        public string name= "";
        public int age = 0;
        public string _name { get { return name; } set { name = value; } }
        public int _age { get { return age; } set { age = value; } }
        public Player()
        {

        }

        public Player(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public abstract List<Player> getList();
        public abstract string getPlayerName();
        public abstract void removePlayerFromList(string playerName);
        public abstract List<string> getAllFieldsAsStringList();
    }
}
