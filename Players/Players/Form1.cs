using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics.PerformanceData;
using Plugin;

namespace Players
{
    public partial class Form1 : Form
    {
        public static Form1 instance = null;
        public Form1()
        {
            instance = this;
            new ProjectContainer();

            ProjectContainer.instance.putNewSerializer(new JSONSerializer());
            ProjectContainer.instance.putNewSerializer(new BinarySerializer());
            ProjectContainer.instance.putNewSerializer(new MySerializer());

           // LoadBase64Coding();
            LoadZBase32Coding();

            InitializeComponent();

            charactersType.DropDownStyle = ComboBoxStyle.DropDownList;

            InitialPlayers(new Wizard().GetType().Name, new EditWizardsForm(this));
            InitialPlayers(new SwordsMan().GetType().Name, new EditSwordsManForm(this));
            InitialPlayers(new Archer().GetType().Name, new EditArchersForm(this));
        }


        private void InitialPlayers(string playerTypeName, Form playerFormName)
        {
            ProjectContainer.instance.putNewForm(playerTypeName, playerFormName);
            charactersType.Items.Add(playerTypeName);
        }

        private void charactersType_SelectedIndexChanged(object sender, EventArgs e)
        {
            playerInfo.Items.Clear();
            AddPlayer.Enabled = true;
            EditPanel.Visible = false;

            List<Player> players = ProjectContainer.instance.getExistingPlayer(charactersType.SelectedItem.ToString());

            if (players != null)
            {
                foreach (Player pl in players)
                {
                    playerInfo.Items.Add(pl.getPlayerName());
                }
            }
            ProjectContainer.instance.putCurrPlayer(null);
        }

        private void AddPlayer_Click(object sender, EventArgs e)
        {
            ProjectContainer.instance.putCurrPlayer(null);
            Form f = ProjectContainer.instance.getExistingForms(charactersType.SelectedItem.ToString());

            if (f != null)
            {
                f.Visible = true;
                Enabled = false;
            }
        }

        private void Form1_EnabledChanged(object sender, EventArgs e)
        {
            playerInfo.Items.Clear();
            EditPanel.Visible = false;

            List<Player> players = ProjectContainer.instance.getExistingPlayer(charactersType.SelectedItem.ToString());

            if (players != null)
            {
                foreach(Player pl in players)
                {
                    playerInfo.Items.Add(pl.getPlayerName());
                }
            }
        }

        private void playerInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DeletePlayer_Click(object sender, EventArgs e)
        {
            CheckedListBox.CheckedItemCollection checkedPlayers = playerInfo.CheckedItems;
            if (checkedPlayers != null && checkedPlayers.Count > 0)
            {
                EditPanel.Visible = false;
                List<Player> newPlayers = new List<Player>();
                string playersType = charactersType.SelectedItem.ToString();
                foreach (object obj in checkedPlayers)
                {
                    deleteCheckedPlayer(playersType, obj.ToString());
                }

                playerInfo.Items.Clear();
                newPlayers = ProjectContainer.instance.getExistingPlayer(playersType);
                foreach(Player player in newPlayers)
                {
                    playerInfo.Items.Add(player.getPlayerName());
                }
                ProjectContainer.instance.putCurrPlayer(null);
            }
        }

        void deleteCheckedPlayer(string playersType, string playersName)
        {
            List<Player> players = ProjectContainer.instance.getExistingPlayer(playersType);
            List<Player> newPlayers = new List<Player>();

            if (players != null)
            {
                foreach (Player player in players)
                {
                    if (player.getPlayerName() == playersName)
                    {
                        player.removePlayerFromList(player.getPlayerName());
                        continue;
                    }
                    else
                    {
                        newPlayers.Add(player);
                    }
                }
                ProjectContainer.instance.putNewPlayers(playersType, newPlayers);
            }
        }

        private void playerInfo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            EditPanel.Visible = false;
            if (playerInfo.SelectedItem != null)
            {
                List<Player> players = ProjectContainer.instance.getExistingPlayer(charactersType.SelectedItem.ToString());

                if (players != null)
                {
                    foreach (Player pl in players)
                    {
                        if (pl.getPlayerName() == playerInfo.SelectedItem.ToString())
                        {
                            ProjectContainer.instance.putCurrPlayer(pl);
                            listBox1.Items.Clear();
                            listBox1.Items.AddRange(pl.getAllFieldsAsStringList().ToArray());
                            break;
                        }
                    }
                }
                EditPanel.Visible = true;
            }
        }

        private void editInfo_Click(object sender, EventArgs e)
        {
            Form form = ProjectContainer.instance.getExistingForms(charactersType.SelectedItem.ToString());
            if (form != null)
            {
                form.Visible = true;
                this.Enabled = false;
            }
            else
            {
                MessageBox.Show("Форма не существует!");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<Player> players = null;
            List<object> checkedPlayer = new List<object>();
            List<string> checkedPlayerNames = new List<string>();

            for (int i = 0; i < playerInfo.CheckedItems.Count; i++)
            {
                checkedPlayerNames.Add(playerInfo.CheckedItems[i].ToString());
            }

            players = ProjectContainer.instance.getExistingPlayer(charactersType.SelectedItem.ToString());

            if (players != null)
            {
                foreach (Player pl in players)
                {
                    if (checkedPlayerNames.Contains(pl.getPlayerName()))
                    {
                        checkedPlayer.Add(pl);
                    }
                }
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JSON files (*.json)|*.json|Binary files (*.binar)|*.binar|Special files (*.special)|*.special|JSON files with zbase32 (*.jsonz32)|*.jsonz32|JSON files with base64 (*.json64)|*.json64|Binary files with zbase32 (*.binarz32)|*.binarz32|Binary files eith base64 (*.binar64)|*.binar64|Special files with zbase32(*.specialz32)|*.specialz32|Special files with base64(*.special64)|*.special64";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string ext = Path.GetExtension(saveFileDialog1.FileName);
                string[] ext2 = ext.Split('.');
                MainSerializer serializer = ProjectContainer.instance.getSerializer(ext2[ext2.Length - 1]);

                if (serializer != null)
                {
                    Stream myStream;
                    if ((myStream = saveFileDialog1.OpenFile()) != null && saveFileDialog1.FileName != "" && checkedPlayer.Count != 0)
                    {
                        serializer.Serialize(checkedPlayer, myStream);
                    }
                }
                else
                {
                    string pluginName = FindPluginNameByExtention(ext2[ext2.Length - 1]);
                    string serializerName = FindSerializerNameByExtention(ext2[ext2.Length - 1]);
                    
                    MainSerializer newSerializer = ProjectContainer.instance.getSerializer(serializerName);
                    PluginClass newPlugin = ProjectContainer.instance.getPluginFromDictionary(pluginName);

                    Stream myStream;
                    if ((myStream = saveFileDialog1.OpenFile()) != null && saveFileDialog1.FileName != "" && checkedPlayer.Count != 0)
                    {
                        newSerializer.SerializeWithPlugin(checkedPlayer, myStream, newPlugin);
                    }
                }
            }
        }

        public string FindPluginNameByExtention(string extention)
        {
            if (extention == "json64")
            {
                return "64";
            }
            else if (extention == "jsonz32")
            {
                return "z32";
            }
            else if (extention == "json32")
            {
                return "32";
            }
            else if (extention == "binar64")
            {
                return "64";
            }
            else if (extention == "binarz32")
            {
                return "z32";
            }
            else if (extention == "binar32")
            {
                return "32";
            }
            else if (extention == "special64")
            {
                return "64";
            }
            else if (extention == "special32")
            {
                return "64";
            }
            else 
            {
                return "z32";
            }
        }

        public string FindSerializerNameByExtention(string extention)
        {
            if (extention == "json64")
            {
                return "json";
            }
            else if (extention == "jsonz32")
            {
                return "json";
            }
            else if (extention == "json32")
            {
                return "json";
            }
            else if (extention == "binar64")
            {
                return "binar";
            }
            else if (extention == "binarz32")
            {
                return "binar";
            }
            else if (extention == "binar32")
            {
                return "binar";
            }
            else if (extention == "special64")
            {
                return "special";
            }
            else if (extention == "special32")
            {
                return "special";
            }
            else
            {
                return "special";
            }
        }

        private void playerInfo_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                btnSave.Enabled = true;
            }
            else if (playerInfo.CheckedItems.Count == 1)
            {
                btnSave.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void LoadZBase32Coding()
        {
            string pluginsLocation = Directory.GetCurrentDirectory() + "//Plugin";

            var pluginFiles = Directory.GetFiles(pluginsLocation, "*.dll");
            
            foreach (var file in pluginFiles)
            {
                Assembly assemblyOfPlugin = Assembly.LoadFrom(file);

                var types = assemblyOfPlugin.GetTypes().
                                Where(t => t.GetInterfaces().
                                Where(i => i.FullName == typeof(PluginClass).FullName).Any());

                foreach (var type in types)
                {
                    var plugin = assemblyOfPlugin.CreateInstance(type.FullName) as PluginClass;
                    ProjectContainer.instance.addNewPluginToDictionary(plugin.getExtention(), plugin);
                }
            }
        }
    }
}
