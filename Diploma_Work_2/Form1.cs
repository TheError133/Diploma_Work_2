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
using System.Text.RegularExpressions;

namespace Diploma_Work_2
{
    public partial class MainForm : Form
    {
        public _Path[,] Paths = new _Path[0, 0];
        public _Node[] Nodes = new _Node[0];
        public bool[,] BestPaths = new bool[0, 0];
        private int[] IndexesForPath = new int[] { -1, -1 };
        private string FileName = "DataInfo.txt";
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        public _Path[,] addIntoPaths(_Path[,] OldPaths)
        {
            _Path[,] ExpandedPaths = new _Path[OldPaths.GetLength(0) + 1, OldPaths.GetLength(0) + 1];
            for (int i = 0; i < OldPaths.GetLength(0); i++)
            {
                if (i != ExpandedPaths.GetLength(0) - 1)
                {
                    ExpandedPaths[i, ExpandedPaths.GetLength(0) - 1] = new _Path(0, 0, 0);
                    ExpandedPaths[ExpandedPaths.GetLength(0) - 1, i] = new _Path(0, 0, 0);
                }
                else
                    ExpandedPaths[i, i] = new _Path(0, 0, 0);
                for (int j = 0; j < OldPaths.GetLength(0); j++)
                    ExpandedPaths[i, j] = OldPaths[i, j];
            }
            for (int i = 0; i < ExpandedPaths.GetLength(0); i++)
                ExpandedPaths[i, i] = new _Path(0, 0, 0);
            return ExpandedPaths;
        }
        public bool[,] addIntoBest(bool[,] OldBest)
        {
            bool[,] ExpandedBest = new bool[OldBest.GetLength(0) + 1, OldBest.GetLength(0) + 1];
            for (int i = 0; i < OldBest.GetLength(0); i++)
            {
                if (i != ExpandedBest.GetLength(0) - 1)
                {
                    ExpandedBest[i, ExpandedBest.GetLength(0) - 1] = false;
                    ExpandedBest[ExpandedBest.GetLength(0) - 1, i] = false;
                }
                else
                    ExpandedBest[i, i] = false;
                for (int j = 0; j < OldBest.GetLength(0); j++)
                    ExpandedBest[i, j] = false;
            }
            for (int i = 0; i < ExpandedBest.GetLength(0); i++)
                ExpandedBest[i, i] = false;
            return ExpandedBest;
        }
        public _Node[] addIntoNodes(_Node[] OldNodes, int X, int Y, string Name)
        {
            _Node[] ExpandedNodes = new _Node[OldNodes.Length + 1];
            for (int i = 0; i < OldNodes.Length; i++)
                ExpandedNodes[i] = OldNodes[i];
            ExpandedNodes[ExpandedNodes.Length - 1] = new _Node(Name, X, Y);
            return ExpandedNodes;
        }
        public _Path[,] deleteFromPaths(_Path[,] OldPaths, int Number)
        {
            if (OldPaths.GetLength(0) > 1)
            {
                _Path[,] ShrinkedPaths = new _Path[OldPaths.GetLength(0) - 1, OldPaths.GetLength(0) - 1];
                //1 четверть
                for (int i = 0; i < Number; i++)
                    for (int j = 0; j < Number; j++)
                        ShrinkedPaths[i, j] = OldPaths[i, j];
                //2 четверть
                for (int i = 0; i < Number; i++)
                    for (int j = Number + 1; j < OldPaths.GetLength(0); j++)
                        ShrinkedPaths[i, j - 1] = OldPaths[i, j];
                //3 четверть
                for (int i = Number + 1; i < OldPaths.GetLength(0); i++)
                    for (int j = 0; j < Number; j++)
                        ShrinkedPaths[i - 1, j] = OldPaths[i, j];
                //4 четверть
                for (int i = Number + 1; i < OldPaths.GetLength(0); i++)
                    for (int j = Number + 1; j < OldPaths.GetLength(0); j++)
                        ShrinkedPaths[i - 1, j - 1] = OldPaths[i, j];
                return ShrinkedPaths;
            }
            else
            {
                return new _Path[0, 0];
            }
        }
        public bool[,] deleteFromBest(bool[,] OldBest, int Number)
        {
            if (OldBest.GetLength(0) > 1)
            {
                bool[,] ShrinkedPaths = new bool[OldBest.GetLength(0) - 1, OldBest.GetLength(0) - 1];
                //1 четверть
                for (int i = 0; i < Number; i++)
                    for (int j = 0; j < Number; j++)
                        ShrinkedPaths[i, j] = OldBest[i, j];
                //2 четверть
                for (int i = 0; i < Number; i++)
                    for (int j = Number + 1; j < OldBest.GetLength(0); j++)
                        ShrinkedPaths[i, j - 1] = OldBest[i, j];
                //3 четверть
                for (int i = Number + 1; i < OldBest.GetLength(0); i++)
                    for (int j = 0; j < Number; j++)
                        ShrinkedPaths[i - 1, j] = OldBest[i, j];
                //4 четверть
                for (int i = Number + 1; i < OldBest.GetLength(0); i++)
                    for (int j = Number + 1; j < OldBest.GetLength(0); j++)
                        ShrinkedPaths[i - 1, j - 1] = OldBest[i, j];
                return ShrinkedPaths;
            }
            else
            {
                return new bool[0, 0];
            }
        }
        public int getNode(int X, int Y)
        {
            for (int i = 0; i < Nodes.Length; i++)
                if (Math.Pow(X - Nodes[i].X, 2) + Math.Pow(Y - Nodes[i].Y, 2) <= 25)
                    return i;
            return -1;
        }
        private bool isTooClose(int X, int Y)
        {
            for (int i = 0; i < Nodes.Length; i++)
            {
                int X0 = Nodes[i].X;
                int Y0 = Nodes[i].Y;
                if (Math.Pow(X - X0, 2) + Math.Pow(Y - Y0, 2) < 225)
                    return true;
            }
            return false;
        }
        private void redraw()
        {
            Graphics Gr = Graph.CreateGraphics();
            Gr.Clear(Color.White);
            for (int i = 0; i < Nodes.Length; i++)
                if (Nodes[i].Name != "")
                {
                    Gr.FillEllipse(Brushes.Black, Nodes[i].X - 5, Nodes[i].Y - 5, 10, 10);
                    Gr.DrawString(Nodes[i].Name, new Font("Consolas", 12), Brushes.Blue, new Point(Nodes[i].X - 10, Nodes[i].Y - 23));
                }
            for (int i = 0; i < Paths.GetLength(0); i++)
                for (int j = i + 1; j < Paths.GetLength(0); j++)
                    if (Paths[i, j].Length > 0)
                        Gr.DrawLine(Pens.Black, Nodes[i].X, Nodes[i].Y, Nodes[j].X, Nodes[j].Y);
        }
        public _Node[] deleteFromNodes(_Node[] OldNodes, int Number)
        {
            if (OldNodes.Length > 1)
            {
                _Node[] ShrinkedNodes = new _Node[OldNodes.Length - 1];
                for (int i = 0; i < OldNodes.Length; i++)
                    if (i < Number)
                        ShrinkedNodes[i] = OldNodes[i];
                    else
                        if (i > Number)
                            ShrinkedNodes[i - 1] = OldNodes[i];
                return ShrinkedNodes;
            }
            else
            {
                return new _Node[0];
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Nodes.Length > 0)
            {
                StreamWriter SW = new StreamWriter(FileName);
                string StringToWrite = "";
                for (int i = 0; i < Paths.GetLength(0); i++)
                {
                    for (int j = 0; j < Paths.GetLength(0); j++)
                    {
                        StringToWrite += Paths[i, j].Length;
                        if (j < Paths.GetLength(0) - 1)
                            StringToWrite += "   ";
                    }
                    StringToWrite += "\r\n";
                }
                StringToWrite += "-----\r\n";
                for (int i = 0; i < Paths.GetLength(0); i++)
                {
                    for (int j = 0; j < Paths.GetLength(0); j++)
                    {
                        StringToWrite += Paths[i, j].PathClass;
                        if (j < Paths.GetLength(0) - 1)
                            StringToWrite += "   ";
                    }
                    StringToWrite += "\r\n";
                }
                StringToWrite += "-----\r\n";
                for (int i = 0; i < Paths.GetLength(0); i++)
                {
                    for (int j = 0; j < Paths.GetLength(0); j++)
                    {
                        StringToWrite += Paths[i, j].Quality;
                        if (j < Paths.GetLength(0) - 1)
                            StringToWrite += "   ";
                    }
                    StringToWrite += "\r\n";
                }
                StringToWrite += "-----\r\n";
                for (int i = 0; i < Paths.GetLength(0); i++)
                {
                    StringToWrite += Nodes[i].Name + "  " + Nodes[i].X + "  " + Nodes[i].Y;
                    if (i < Paths.GetLength(0) - 1)
                        StringToWrite += "\r\n";
                }
                SW.Write(StringToWrite);
                SW.Close();
            }
            Close();
        }
        private void RadioDeletePoint_CheckedChanged(object sender, EventArgs e)
        {
            if (Nodes.Length == 0 && RadioDeletePoint.Checked)
            {
                MessageBox.Show("Список вершин пуст");
                RadioAddPoint.Checked = true;
            }
        }

        private void RadioDeletePath_CheckedChanged(object sender, EventArgs e)
        {
            if (Nodes.Length == 0 && RadioDeletePath.Checked)
            {
                MessageBox.Show("Путей нет, поскольку список вершин пуст");
                RadioAddPoint.Checked = true;
            }
        }
        private void RadioAddPath_CheckedChanged(object sender, EventArgs e)
        {
            IndexesForPath = new int[] { -1, -1 };
        }
        private void Graph_MouseClick(object sender, MouseEventArgs e)
        {
            if (CheckChangesAllowed.Checked)
            {
                //Добавление вершины
                if (RadioAddPoint.Checked)
                {
                    if (!isTooClose(e.X, e.Y))
                    {
                        AddPointForm WorkForm = new AddPointForm();
                        WorkForm.getParams(this, e.X, e.Y);
                        WorkForm.StartPosition = FormStartPosition.CenterParent;
                        WorkForm.ShowDialog();
                        redraw();
                    }
                    else
                        MessageBox.Show("Новая вершина слишком близко");
                }
                //Удаление вершины
                if (RadioDeletePoint.Checked)
                {
                    int Index = -1;
                    Index = getNode(e.X, e.Y);
                    if (Index >= 0)
                    {
                        var Result = MessageBox.Show("Подтверждение удаления вершины " + Nodes[Index].Name, "Уточнение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (Result == DialogResult.Yes)
                        {
                            Nodes = deleteFromNodes(Nodes, Index);
                            Paths = deleteFromPaths(Paths, Index);
                            BestPaths = deleteFromBest(BestPaths, Index);
                            redraw();
                        }
                    }
                }
                //Добавление пути между вершинами
                if (RadioAddPath.Checked)
                {
                    if (IndexesForPath[0] == -1)
                    {
                        int Index = -1;
                        Index = getNode(e.X, e.Y);
                        if (Index >= 0)
                            IndexesForPath[0] = Index;
                    }
                    else
                    {
                        int Index = -1;
                        Index = getNode(e.X, e.Y);
                        if (Index >= 0)
                            if (Index != IndexesForPath[0])
                            {
                                IndexesForPath[1] = Index;
                                if (Paths[IndexesForPath[0], IndexesForPath[1]].Length > 0)
                                {
                                    MessageBox.Show("Путь между вершинами уже существует");
                                    IndexesForPath = new int[] { -1, -1 };
                                    return;
                                }
                            }
                    }
                    if (IndexesForPath[1] != -1)
                    {
                        AddPathForm WForm = new AddPathForm();
                        WForm.Text = String.Format("Добавление пути между вершинами {0} и {1}", Nodes[IndexesForPath[0]].Name, Nodes[IndexesForPath[1]].Name);
                        WForm.getParams(this, IndexesForPath[0], IndexesForPath[1]);
                        WForm.StartPosition = FormStartPosition.CenterParent;
                        IndexesForPath = new int[] { -1, -1 };
                        WForm.ShowDialog();
                        redraw();
                    }
                }
                //Удаление пути
                if (RadioDeletePath.Checked)
                {
                    if (IndexesForPath[0] == -1)
                    {
                        int Index = -1;
                        Index = getNode(e.X, e.Y);
                        if (Index >= 0)
                            IndexesForPath[0] = Index;
                    }
                    else
                    {
                        int Index = -1;
                        Index = getNode(e.X, e.Y);
                        if (Index >= 0)
                            if (Index != IndexesForPath[0])
                            {
                                IndexesForPath[1] = Index;
                                if (Paths[IndexesForPath[0], IndexesForPath[1]].Length > 0)
                                {
                                    var Result = MessageBox.Show(String.Format("Подтверждение удаления пути между вершинами {0} и {1}", Nodes[IndexesForPath[0]].Name, Nodes[IndexesForPath[1]].Name), "Уточнение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (Result == DialogResult.Yes)
                                    {
                                        //В матрице смежности обнуляем всю информацию по индексам пути - удаляем путь
                                        Paths[IndexesForPath[0], IndexesForPath[1]] = new _Path(0, 0, 0);
                                        Paths[IndexesForPath[1], IndexesForPath[0]] = new _Path(0, 0, 0);
                                        redraw();
                                    }
                                }
                            }
                    }
                }
            }
            if (RadioPathInfo.Checked)
            //Информация о пути
            {
                if (IndexesForPath[0] == -1)
                {
                    int Index = -1;
                    Index = getNode(e.X, e.Y);
                    if (Index >= 0)
                        IndexesForPath[0] = Index;
                }
                else
                {
                    int Index = -1;
                    Index = getNode(e.X, e.Y);
                    if (Index >= 0)
                        if (Index != IndexesForPath[0])
                        {
                            IndexesForPath[1] = Index;
                            if (Paths[IndexesForPath[0], IndexesForPath[1]].Length > 0)
                            {
                                MessageBox.Show("Информация о пути между вершинами " + Nodes[IndexesForPath[0]].Name + " и " + Nodes[IndexesForPath[1]].Name + Environment.NewLine +
                                                "Расстояние: " + Paths[IndexesForPath[0], IndexesForPath[1]].Length + Environment.NewLine +
                                                "Класс пути: " + Paths[IndexesForPath[0], IndexesForPath[1]].PathClass + Environment.NewLine +
                                                "Изогнутость: " + Paths[IndexesForPath[0], IndexesForPath[1]].Quality);
                                IndexesForPath = new int[] { -1, -1 };
                            }
                        }
                    IndexesForPath = new int[] { -1, -1 };
                }
            }
        }
        private void загрузитьИнформациюИзФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    StreamReader SR = new StreamReader(openFileDialog1.FileName, Encoding.Default);
                    FileName = openFileDialog1.FileName;
                    string TextFromFile = SR.ReadToEnd();
                    string[] SplitResults = Regex.Split(TextFromFile, "\r\n-----\r\n");
                    string[] Results = Regex.Split(SplitResults[0], "\r\n");
                    string[][] ResultsLower = new string[Results.Length][];
                    for (int i = 0; i < Results.GetLength(0); i++)
                    {
                        ResultsLower[i] = Regex.Split(Results[i], "  ");
                    }
                    Paths = new _Path[Results.Length, Results.Length];
                    for (int i = 0; i < Results.Length; i++)
                        for (int j = 0; j < Results.Length; j++)
                        {
                            Paths[i, j] = new _Path(0, 0, 0);
                            Paths[i, j].Length = Convert.ToInt32(ResultsLower[i][j]);
                        }
                    Results = Regex.Split(SplitResults[1], "\r\n");
                    ResultsLower = new string[Results.Length][];
                    for (int i = 0; i < Results.GetLength(0); i++)
                    {
                        ResultsLower[i] = Regex.Split(Results[i], "  ");
                    }
                    for (int i = 0; i < Results.Length; i++)
                        for (int j = 0; j < Results.Length; j++)
                            Paths[i, j].PathClass = Convert.ToDouble(ResultsLower[i][j]);
                    Results = Regex.Split(SplitResults[2], "\r\n");
                    ResultsLower = new string[Results.Length][];
                    for (int i = 0; i < Results.GetLength(0); i++)
                    {
                        ResultsLower[i] = Regex.Split(Results[i], "  ");
                    }
                    for (int i = 0; i < Results.Length; i++)
                        for (int j = 0; j < Results.Length; j++)
                            Paths[i, j].Quality = Convert.ToDouble(ResultsLower[i][j]);
                    Results = Regex.Split(SplitResults[3], "\r\n");
                    ResultsLower = new string[Results.Length][];
                    for (int i = 0; i < Results.GetLength(0); i++)
                    {
                        ResultsLower[i] = Regex.Split(Results[i], "  ");
                    }
                    Nodes = new _Node[Results.Length];
                    for (int i = 0; i < Results.Length; i++)
                        Nodes[i] = new _Node(ResultsLower[i][0], Convert.ToInt32(ResultsLower[i][1]), Convert.ToInt32(ResultsLower[i][2]));
                    BestPaths = new bool[Nodes.Length, Nodes.Length];
                    SR.Close();
                    redraw();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Файл с информацией имеет неверный формат.");
            }
        }
        private void Classification_alg(int Start, ref bool[,] Best)
        {
            int[] PathNodes = new int[0];
            int[] NodeClass = new int[Nodes.Length];
            double[][] Weight = new double[Nodes.Length][];
            for (int i = 0; i < Nodes.Length; i++)
                Weight[i] = new double[Nodes.Length];
            //Веса путей на основании совокупности длины пути, его класса и качества
            for (int i = 0; i < Nodes.Length; i++)
                for (int j = 0; j < Nodes.Length; j++)
                    Weight[i][j] = Paths[i, j].Length * //Длина пути
                        (3 + (6 - Paths[i, j].PathClass)) * //Коэффициент класса, как Kcl = (3 + (6 - Cl)) - чем выше класс, тем меньше вес пути
                        (2 - Paths[i, j].Quality); //Коэффициент качества, как Kq = (2 - Q) - чем выше качество, тем меньше вес пути
            for (int i = 0; i < NodeClass.Length; i++)
            {
                if (Weight[i].Where(p => p != 0).Count() == 0)//Добавил этот класс как изолированные вершины, иначе алгоритм работает неверно
                    NodeClass[i] = 0;
                if (Weight[i].Where(p => p != 0).Count() > 2)
                    NodeClass[i] = 4;
                if (Weight[i].Where(p => p != 0).Count() == 1)//Немного подправил, поскольку неравество убирает изолированные вершины
                    NodeClass[i] = 1;
                if (Weight[i].Where(p => p != 0).Count() == 2)
                {
                    int[] indexes = new int[2];
                    int q = 0;
                    for (int j = 0; j < NodeClass.Length; j++)
                        if (Weight[i][j] != 0)
                            indexes[q++] = j;
                    if (Weight[indexes[0]][indexes[1]] == 0)
                        NodeClass[i] = 3;
                    else
                        if (Weight[indexes[0]][indexes[1]] < Weight[i][indexes[0]] + Weight[i][indexes[1]])
                            NodeClass[i] = 2;
                        else
                            NodeClass[i] = 3;
                }
            }
            string[] paths = new string[Weight.GetLength(0)];
            double[] lengths = new double[Weight.GetLength(0)];
            bool[] used = new bool[Weight.GetLength(0)];
            for (int i = 0; i < Weight.GetLength(0); i++)
            {
                if (NodeClass[i] != 0)
                {
                    used[i] = false;
                    lengths[i] = 0;
                    if (i != Start)
                        paths[i] += Start;
                }
                else
                    used[i] = true;
            }
            bool check = false;
            int index = Start;
            while (!check)
            {
                used[index] = true;
                check = true;
                for (int i = 0; i < used.Length; i++)
                {
                    if (!used[i])
                        if (Weight[index][i] != 0)
                        {
                            if (lengths[i] == 0)
                                if (index == Start)
                                {
                                    lengths[i] += Weight[index][i];
                                    paths[i] += " -> " + i;
                                }
                                else
                                {
                                    lengths[i] = lengths[index] + Weight[index][i];
                                    paths[i] = paths[index] + " -> " + i;
                                }
                            else
                                if (lengths[i] > lengths[index] + Weight[index][i])
                                {
                                    lengths[i] = lengths[index] + Weight[index][i];
                                    paths[i] = paths[index] + " -> " + i;
                                }
                        }
                    check &= used[i];
                }
                double lowest = 0;
                for (int i = 0; i < used.Length; i++)
                    if (!used[i])
                        if (lowest == 0)
                        {
                            lowest = lengths[i];
                            index = i;
                        }
                        else
                            if (lowest > lengths[i] && lengths[i] != 0)
                            {
                                lowest = lengths[i];
                                index = i;
                            }
                if (used[index] && !check)
                {
                    MessageBox.Show("Пути между вершинами не существует");
                    return;
                }
            }
            for (int i = 0; i < paths.Length; i++)
                if (i != Start)
                {
                    string[] TmpStr = Regex.Split(paths[i], " -> ");
                    int[] TmpNodes = new int[TmpStr.Length];
                    for (int j = 0; j < TmpNodes.Length; j++)
                        TmpNodes[j] = Convert.ToInt32(TmpStr[j]);
                    for (int j = 0; j < TmpNodes.Length - 1; j++)
                        BestPaths[TmpNodes[j], TmpNodes[j + 1]] = true;
                }
        }
        private void ВыделитьГрафToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Nodes.Length > 0)
            {
                for (int i = 0; i < Nodes.Length; i++)
                {
                    if (Nodes[i].Name != "")
                    {
                        int counter = 0;
                        for (int j = 0; j < Nodes.Length; j++)
                            if (Paths[i, j].Length > 0)
                                counter++;
                        if (counter == 0)
                        {
                            MessageBox.Show("Присутствуют изолированные вершины.\r\nВыделить идеальный граф невозможно.");
                            return;
                        }
                    }
                }
                for (int i = 0; i < Nodes.Length; i++)
                    for (int j = 0; j < Nodes.Length; j++)
                        BestPaths[i, j] = false;
                try
                {
                    for (int i = 0; i < Nodes.Length; i++)
                        if (Nodes[i].Name != "")
                            Classification_alg(i, ref BestPaths);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                Graphics Gr = Graph.CreateGraphics();
                for (int i = 0; i < BestPaths.GetLength(0); i++)
                    for (int j = 0; j < BestPaths.GetLength(0); j++)
                        if (BestPaths[i, j])
                            Gr.DrawLine(new Pen(Color.Red, 3), Nodes[i].X, Nodes[i].Y, Nodes[j].X, Nodes[j].Y);
                for (int i = 0; i < Nodes.Length; i++)
                    if (Nodes[i].Name != "")
                    {
                        Gr.FillEllipse(Brushes.Black, Nodes[i].X - 5, Nodes[i].Y - 5, 10, 10);
                        Gr.DrawString(Nodes[i].Name, new Font("Consolas", 12), Brushes.Blue, new Point(Nodes[i].X - 10, Nodes[i].Y - 23));
                    }
            }
            else
                MessageBox.Show("Графа нет!");
        }
        private void отрисоватьГрафЗановоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redraw();
        }
    }
    public class _Node
    {
        public int X, Y;
        public string Name;
        public _Node(string Name, int X, int Y)
        {
            this.Name = Name;
            this.X = X;
            this.Y = Y;
        }
    }
    public class _Path
    {
        public int Length;
        public double PathClass, Quality;
        public _Path(int Length, double PathClass, double Quality)
        {
            this.Length = Length;
            this.PathClass = PathClass;
            this.Quality = Quality;
        }
    }
}
