using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players
{
    class ProjectContainer
    {

        public static ProjectContainer instance;
        private Dictionary<string, Form> playerDictionary = new Dictionary<string, Form>();
        private Dictionary<string, List<Player>> players = new Dictionary<string, List<Player>>();

        private Player currentPlayer = null;

        public ProjectContainer()
        {
            instance = this;
        }

        public void putNewForm(string playerType, Form form)
        {
            if (getExistingForms(playerType) == null)
            {
                playerDictionary.Add(playerType, form);
            }
        }

        public Form getExistingForms(string playerType)
        {
            Form existingForm = null;
            if (playerDictionary.TryGetValue(playerType, out existingForm))
            {
                return existingForm;
            }
            else
            {
                return null;
            }
        }

        public List<Player> getExistingPlayer(string playerType)
        {
            List<Player> existingPlayers = null;
            if (this.players.TryGetValue(playerType, out existingPlayers))
            {
                return existingPlayers;
            }
            else
            {
                return null;
            }
        }

        public void putNewPlayers(string playersType, List<Player> players)
        {
            if (this.players.ContainsKey(playersType))
            {
                this.players[playersType] = players;
            }
            else
            {
                this.players.Add(playersType, players);
            }
        }

        public void putCurrPlayer(Player player)
        {
            currentPlayer = player;
        }

        public Player getCurrPlayer()
        {
            return currentPlayer;
        }

    }
}
