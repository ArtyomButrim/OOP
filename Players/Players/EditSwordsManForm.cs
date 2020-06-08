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
    public partial class EditSwordsManForm : Form
    {
        Form form;
        public EditSwordsManForm(Form sender)
        {
            form = sender;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int age = 0;
            int fizDamage = 0;
            int kritDamage =0;
            int stamina = 0;
            int defence = 0;
    
            SwordsMan a = new SwordsMan();
            int k = 0;
            if (Int32.TryParse(txtAge.Text, out age) && (txtName.Text != "") && Int32.TryParse(txtFizDamage.Text, out fizDamage) && Int32.TryParse(txtKritDamage.Text, out kritDamage) && Int32.TryParse(txtStamina.Text, out stamina) && Int32.TryParse(txtDefense.Text, out defence) && (txtCanUsesecondSword.Text != "") && (txtSwordsManSpecialSkill.Text != ""))
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
                                a = (SwordsMan)tr;
                                a.name = txtName.Text;
                                a.age = age;
                                a._fizDamage = Convert.ToInt32(txtFizDamage.Text);
                                a._kritDamage = Convert.ToInt32(txtKritDamage.Text);
                                a._canUseSecondSword = txtCanUsesecondSword.Text;
                                a._defence = Convert.ToInt32(txtDefense.Text);
                                a._stamina = Convert.ToInt32(txtStamina.Text);
                                a._skill = txtSwordsManSpecialSkill.Text;


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
                                MessageBox.Show("Форма не походит!");
                            }

                        }
                    }

                }
                if (Int32.TryParse(txtDopDamage.Text, out k) && (txtSpecialSkill.Text != ""))
                {
                    Weapon weapon = new Weapon(txtSpecialSkill.Text, k);
                    a = new SwordsMan(Convert.ToInt32(txtFizDamage.Text), Convert.ToInt32(txtKritDamage.Text), Convert.ToInt32(txtDefense.Text), Convert.ToInt32(txtStamina.Text), txtSwordsManSpecialSkill.Text, txtCanUsesecondSword.Text, Convert.ToInt32(txtAge.Text), txtName.Text, weapon);
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
                MessageBox.Show("Данные введены некорректно!!");
            }
        }

        private void txtSwordsManSpecialSkill_VisibleChanged(object sender, EventArgs e)
        {
        
        }

        private void EditSwordsManForm_VisibleChanged(object sender, EventArgs e)
        {
            Player pl = ProjectContainer.instance.getCurrPlayer();
            if (pl != null)
            {
                SwordsMan a = (SwordsMan)pl;
                txtName.Text = a.name;
                txtName.ReadOnly = true;
                txtAge.Text = a.age.ToString();
                txtFizDamage.Text = a._fizDamage.ToString();
                txtKritDamage.Text = a._kritDamage.ToString();
                txtDefense.Text = a._defence.ToString();
                txtStamina.Text = a._stamina.ToString();
                txtCanUsesecondSword.Text = a._canUseSecondSword;
                txtSwordsManSpecialSkill.Text = a._skill;
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
                txtSwordsManSpecialSkill.Text = "";
                txtCanUsesecondSword.Text = "";
                txtSpecialSkill.Text = "";
                txtDopDamage.Text = "";
            }
        }
    }
}
