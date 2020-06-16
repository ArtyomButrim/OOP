using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin;

namespace Players
{
    class ProjectContainer
    {

        public static ProjectContainer instance;
        private Dictionary<string, Form> playerDictionary = new Dictionary<string, Form>();
        private Dictionary<string, List<Player>> players = new Dictionary<string, List<Player>>();
        private Dictionary<string, MainSerializer> serializers = new Dictionary<string, MainSerializer>();
        private Dictionary<string, PluginClass> plugins = new Dictionary<string, PluginClass>();
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

        public void putNewSerializer(MainSerializer serializer)
        {
            if (!serializers.ContainsKey(serializer.GetExtention()))
            {
                serializers.Add(serializer.GetExtention(), serializer);
            }
            else
            {
                serializers[serializer.GetExtention()] = serializer;
            }
        }

        public MainSerializer getSerializer(string serial)
        {
            MainSerializer serializer = null;
            if (serializers.TryGetValue(serial, out serializer))
            {
                return serializer;
            }
            else
            {
                return null;
            }
        }

        public MainSerializer[] GetSerializersArray()
        {
            return serializers.Values.ToArray();
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

        public void addNewPluginToDictionary(string dictionaryKey, PluginClass pluginName)
        {
            
            if (plugins.ContainsKey(dictionaryKey))
            {
                plugins[dictionaryKey] = pluginName;
            }
            else
            {
                plugins.Add(dictionaryKey, pluginName);
            }
        }

        public PluginClass getPluginFromDictionary(string pluginName)
        {
            PluginClass findingPlugin = null;
            if (plugins.TryGetValue(pluginName,out findingPlugin))
            {
                return findingPlugin;
            }
            else
            {
                return null;
            }
        }

    }
}
