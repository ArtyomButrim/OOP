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
    public partial class EditArchersForm : Form
    {
        Form form;

        public EditArchersForm(Form sender)
        {
            InitializeComponent();
            form = sender;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int age = 0;
            int fizDamage = 0;
            int kritDamage = 0;
            int stamina = 0;
            int defence = 0;
            int shotAcc = 0;

            Archer a = new Archer();
            int k = 0;
            if (Int32.TryParse(txtAge.Text, out age) && (txtName.Text != "") && Int32.TryParse(txtFizDamage.Text, out fizDamage) && Int32.TryParse(txtKritDamage.Text, out kritDamage) && Int32.TryParse(txtStamina.Text, out stamina) && Int32.TryParse(txtDefense.Text, out defence) && (txtArrow.Text != "") && Int32.TryParse(txtShotAccur.Text, out shotAcc))
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
                                a = (Archer)tr;
                                a.name = txtName.Text;
                                a.age = age;
                                a._fizDamage = Convert.ToInt32(txtFizDamage.Text);
                                a._kritDamage = Convert.ToInt32(txtKritDamage.Text);
                                a._shotAccuracy = Convert.ToInt32(txtShotAccur.Text);
                                a._defence = Convert.ToInt32(txtDefense.Text);
                                a._stamina = Convert.ToInt32(txtStamina.Text);
                                a._arrow = txtArrow.Text;


                                if (Int32.TryParse(txtDopDamage.Text, out k) && (txtSpecialSkill.Text != ""))
                                {
                                    a.setWeapon(txtSpecialSkill.Text, k);
                                }
                                else
                                {
                                    MessageBox.Show("Некорректно введены данные оружия!!!");
                                    return;
                                }
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
                if (Int32.TryParse(txtDopDamage.Text, out k) && (txtSpecialSkill.Text != ""))
                {
                    Weapon weapon = new Weapon(txtSpecialSkill.Text, k);
                    a = new Archer(Convert.ToInt32(txtFizDamage.Text), Convert.ToInt32(txtKritDamage.Text), Convert.ToInt32(txtDefense.Text), Convert.ToInt32(txtStamina.Text),txtArrow.Text, Convert.ToInt32(txtShotAccur.Text), Convert.ToInt32(txtAge.Text), txtName.Text, weapon);
                    ProjectContainer.instance.putNewPlayers(a.GetType().Name, a.getList());
                    form.Enabled = true;
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show("Некорректно введены данные оружия!!");
                }
            }
            else
            {
                MessageBox.Show("Неккоректно введы данные");
            }
        }

        private void txtArrow_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        private void EditArchersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                form.Enabled = true;
                Hide();
            }
        }

        private void EditArchersForm_VisibleChanged(object sender, EventArgs e)
        {
            Player pl = ProjectContainer.instance.getCurrPlayer();
            if (pl != null)
            {
                Archer a = (Archer)pl;
                txtName.Text = a.name;
                txtName.ReadOnly = true;
                txtAge.Text = a.age.ToString();
                txtFizDamage.Text = a._fizDamage.ToString();
                txtKritDamage.Text = a._kritDamage.ToString();
                txtDefense.Text = a._defence.ToString();
                txtStamina.Text = a._stamina.ToString();
                txtShotAccur.Text = a._shotAccuracy.ToString();
                txtArrow.Text = a._arrow;
                txtSpecialSkill.Text = a._weapon._specialSkill;
                txtDopDamage.Text = a._weapon._dopDamage.ToString();

            }
            else
            {
                txtName.Text = "";
                txtName.ReadOnly = false;
                txtAge.Text = "";
                txtFizDamage.Text = "";
                txtKritDamage.Text = "";
                txtDefense.Text = "";
                txtStamina.Text = "";
                txtShotAccur.Text = "";
                txtArrow.Text = "";
                txtSpecialSkill.Text = "";
                txtDopDamage.Text = "";
            }
        }
    }
}
