using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Diploma_Work_2
{
    public partial class AddPathForm : Form
    {
        private MainForm MForm;
        private int Index1, Index2;
        public AddPathForm()
        {
            InitializeComponent();
            ComboClasses.SelectedIndex = 0;
        }
        public void getParams(MainForm MForm, int Index1, int Index2)
        {
            this.MForm = MForm;
            this.Index1 = Index1;
            this.Index2 = Index2;
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void AddPathButton_Click(object sender, EventArgs e)
        {
            int Length;
            double Quality;
            _Path NewPath, PathByClass = new _Path(0, 0, 0), PathByPrec = new _Path(0, 0, 0);
            if (TextLength.Text.Length > 0 && int.TryParse(TextLength.Text, out Length) && Length > 0)
                if (TextQuality.Text.Length > 0 && double.TryParse(TextQuality.Text, out Quality) && Quality >= 0 && Quality <= 1)
                {
                    NewPath = new _Path(Length, ComboClasses.SelectedIndex + 1, Quality);
                    MForm.Paths[Index1, Index2] = NewPath;
                    MForm.Paths[Index2, Index1] = NewPath;
                    Close();
                }
                else
                    MessageBox.Show("Укажите значение изогнутости в диапазоне 0 - 1");
            else
                MessageBox.Show("Укажите числовое расстояние между вершинами");
        }
    }
}
