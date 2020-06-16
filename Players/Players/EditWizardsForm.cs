using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Plugin;

namespace Players
{
    [Serializable]
    public partial class EditWizardsForm : Form
    {
        bool playerAddedShowEnabled = false;
        Form form;
        public EditWizardsForm(Form sender)
        {
            form = sender;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int age = 0;
            Wizard a = new Wizard();

            int damage = 0;
            int mana = 0;
            if (Int32.TryParse(txtAge.Text, out age) && (txtName.Text != "") && (txtElement.Text != "") && (Int32.TryParse(txtMagicDamage.Text,out damage)) && (Int32.TryParse(txtMana.Text, out mana)))
            {
                List<Player> t = ProjectContainer.instance.getExistingPlayer(a.GetType().Name);
                if (t != null)
                {
                    foreach (Player tr in t)
                    {
                        if (tr.getPlayerName() == txtName.Text)
                        {
                            try
                            {
                                a = (Wizard)tr;
                                a.name = txtName.Text;
                                a.age = age;
                                a._mana = Convert.ToInt32(txtMana.Text);
                                a._magicType = txtElement.Text;
                                a._magicDamage = Convert.ToInt32(txtMagicDamage.Text);

                                ProjectContainer.instance.putNewPlayers(a.GetType().Name, a.getList());
                                form.Enabled = true;
                                this.Visible = false;
                                return;
                            }
                            catch (Exception exc)
                            {
                                exc.ToString();
                                MessageBox.Show("Форма не подходит!");
                            }

                        }
                    }

                }
                    a = new Wizard(Convert.ToInt32(txtMagicDamage.Text), Convert.ToInt32(txtMana.Text), txtElement.Text, txtName.Text, Convert.ToInt32(txtAge.Text));
                    ProjectContainer.instance.putNewPlayers(a.GetType().Name, a.getList());
                    form.Enabled = true;
                    this.Visible = false;

            }
            else
            {
                MessageBox.Show("Данные введены некоректно!");
            }
        }

        private void EditWizardsForm_VisibleChanged(object sender, EventArgs e)
        {
            Player pl = ProjectContainer.instance.getCurrPlayer();
            if (pl != null)
            {
                Wizard a = (Wizard)pl;
                txtName.Text = a.name;
                txtName.ReadOnly = true;
                txtAge.Text = a.age.ToString();
                txtMagicDamage.Text = a._magicDamage.ToString();
                txtElement.Text = a._magicType;
                txtMana.Text = a._mana.ToString();
            }
            else
            {
                txtName.Text = "";
                txtName.ReadOnly = false;
                txtAge.Text = "";
                txtMagicDamage.Text = "";
                txtElement.Text = "";
                txtMana.Text = "";
            }
        }

        private void EditWizardsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                form.Enabled = true;
                Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Wizard wizard = new Wizard();
            openFileDialog.Title = "Открыть...";

            openFileDialog.Filter = "JSON files|*.json|Binary files|*.binar|Special files|*.special|JSON files with zbase32 (*.jsonz32)|*.jsonz32|JSON files with base64 (*.json64)|*.json64|Binary files with zbase32 (*.binarz32)|*.binarz32|Binary files eith base64 (*.binar64)|*.binar64|Special files with zbase32(*.specialz32)|*.specialz32|Special files with base64(*.special64)|*.special64";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    string[] st = openFileDialog.FileName.Split('.');
                    List<Wizard> a;

                    MainSerializer serializer = ProjectContainer.instance.getSerializer(st[st.Length - 1]);

                    if (serializer == null)
                    {
                        string pluginName = FindPluginNameByExtention(st[st.Length - 1]);
                        string serializerName = FindSerializerNameByExtention(st[st.Length - 1]);

                        MainSerializer newSerializer = ProjectContainer.instance.getSerializer(serializerName);
                        PluginClass plugin = ProjectContainer.instance.getPluginFromDictionary(pluginName);

                        a = newSerializer.DeserializeWithPlugin<Wizard>(openFileDialog.FileName, plugin);
                    }
                    else
                    {
                        a = serializer.Deserialize<Wizard>(openFileDialog.FileName);
                    }

                    if (a != null)
                    {
                        foreach (Wizard player in a)
                        {
                            bool isAdd = true;
                            foreach (Player pl in Wizard.wizards)
                            {
                                if (player.getPlayerName() == pl.getPlayerName())
                                {
                                    isAdd = false;
                                }
                            }
                            if (isAdd)
                            {
                                Wizard.wizards.Add(player);
                            }
                        }

                        List<Player> players = new List<Player>(a);
                        ProjectContainer.instance.putNewPlayers(wizard.GetType().Name, Wizard.wizards);

                        txtName.Text = a[a.Count - 1].getPlayerName();
                        txtName.ReadOnly = false;
                        txtAge.Text = a[a.Count - 1]._age.ToString();
                        txtElement.Text = a[a.Count - 1]._magicType;
                        txtMagicDamage.Text = a[a.Count - 1]._magicDamage.ToString();
                        txtMana.Text = a[a.Count - 1]._mana.ToString();
                        playerAddedShowEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Невозможно получить информацию " + wizard.GetType().Name + " с файла");
                    }
                }
            }
            if (playerAddedShowEnabled)
            {
                playerAddedShowEnabled = false;
                MessageBox.Show("Информация о " + wizard.GetType().Name + " была успешно добавлена");
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
    }
 }
