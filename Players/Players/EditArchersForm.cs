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
            string name = "";
            int age = 0;
            Archer a = new Archer();
            int k = 0;
            float f = 0.0f;
            if (Int32.TryParse(txtAge.Text, out age) && (txtName.Text != ""))
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
                                a.name = name;
                                a.age = age;
                                a._fizDamage = Convert.ToInt32(txtFizDamage.Text);
                                a._kritDamage = Convert.ToInt32(txtKritDamage.Text);
                                a._shotAccuracy = Convert.ToInt32(txtShotAccur.Text);
                                a._defence = Convert.ToInt32(txtDefense.Text);
                                a._stamina = Convert.ToInt32(txtStamina);



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
                                MessageBox.Show("The form does not suit!");
                            }

                        }
                    }

                }
                if (Int32.TryParse(txtDopDamage.Text, out k) && (txtSpecialSkill.Text != ""))
                {
                    Weapon weapon = new Weapon(txtSpecialSkill.Text, k);
                    a = new Archer(brandTextBox.Text, i, bitmapPath, weight, wheels);
                    a.transportBitmap = (Bitmap)autoPictureBox.Image;
                    ProjectContainer.instance.putNewTransports(a.GetType().Name, a.getList());
                    form.Enabled = true;
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show("Incorrect wheels input parameters!!");
                }
            }
            else
            {
                MessageBox.Show("Incorrect passengers number or auto weight");
            }
        }
    }
}
