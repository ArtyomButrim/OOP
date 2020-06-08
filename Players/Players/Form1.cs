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

namespace Players
{
    public partial class Form1 : Form
    {
        public static Form1 instance = null;
        public Form1()
        {
            instance = this;
            new ProjectContainer();
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
    }
}
