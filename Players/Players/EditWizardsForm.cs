using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Players
{
    public partial class EditWizardsForm : Form
    {
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
    }
}
