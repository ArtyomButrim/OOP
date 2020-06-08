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

namespace Players
{
    public partial class Form1 : Form
    {
        public static Form1 instance = null;
        public Form1()
        {
            new ProjectContainer();
            InitializeComponent();
            charactersType.DropDownStyle = ComboBoxStyle.DropDownList;

            InitialPlayers(new Wizard().GetType.Name, new EditWizardsForm(this));
            InitialPlayers(new SwordsMan().GetType.Name, new EditSwordsManForm(this));
            InitialPlayers(new Archer().GetType.Name, new EditArchersForm(this));
        }


        private void InitialPlayers(string playerTypeName, Form playerFormName)
        {
            ProjectContainer.instance.putNewForm(playerTypeName, playerFormName);
            charactersType.Items.Add(playerTypeName);
        }
    }
}
