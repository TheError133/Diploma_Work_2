using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diploma_Work_2
{
    public partial class AddPointForm : Form
    {
        private MainForm MForm;
        private int X, Y;
        public AddPointForm()
        {
            InitializeComponent();
        }
        public void getParams(MainForm MForm, int X, int Y)
        {
            this.MForm = MForm;
            this.X = X;
            this.Y = Y;
        }
        private bool nameUsed (_Node[] Nodes)
        {
            for (int i = 0; i < Nodes.Length; i++)
                if (Nodes[i].Name == NodeName.Text)
                    return true;
            return false;
        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (NodeName.Text != "" && !nameUsed(MForm.Nodes))
            {
                MForm.Nodes = MForm.addIntoNodes(MForm.Nodes, X, Y, NodeName.Text);
                MForm.Paths = MForm.addIntoPaths(MForm.Paths);
                MForm.BestPaths = MForm.addIntoBest(MForm.BestPaths);
                Close();
            }
            else
            {
                if (NodeName.Text == "")
                    MessageBox.Show("Введите непустое название вершины");
                if (nameUsed(MForm.Nodes))
                    MessageBox.Show("Вершина с таким названием уже существует");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
