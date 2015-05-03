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
        public _Path[,] Paths = new _Path[0,0];
        public _Node[] Nodes = new _Node[0];
        private int[] IndexesForPath = new int[]{-1, -1};
        public List<int[]> Precedents = new List<int[]>();
        public MainForm()
        {
            //
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
        void test()
        {
            Paths = new _Path[3, 3];
            Nodes = new _Node[] { new _Node("1", 30, 30), new _Node("2", 50, 50), new _Node("3", 70, 70) };
            int q = 0;
            for (int i = 0; i < Paths.GetLength(0); i++)
                for (int j = 0; j < Paths.GetLength(0); j++)
                    Paths[i, j] = new _Path(q++, 0, 0);
            _Path[,] a = deleteFromPaths(Paths, 0);
            redraw();
            MessageBox.Show(Paths[0, 0].Length + " " + Paths[0, 1].Length + " " + Paths[0, 2].Length + Environment.NewLine +
                            Paths[1, 0].Length + " " + Paths[1, 1].Length + " " + Paths[1, 2].Length + Environment.NewLine +
                            Paths[2, 0].Length + " " + Paths[2, 1].Length + " " + Paths[2, 2].Length + Environment.NewLine +
                            a[0, 0].Length + " " + a[0, 1].Length + Environment.NewLine +
                            a[1, 0].Length + " " + a[1, 1].Length + Environment.NewLine);
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
            StreamWriter SW = new StreamWriter("DataInfo.txt");
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
                    StringToWrite += Paths[i, j].Curve;
                    if (j < Paths.GetLength(0) - 1)
                        StringToWrite += "   ";
                }
                StringToWrite += "\r\n";
            }
            StringToWrite += "-----\r\n";
            for (int i = 0; i < Paths.GetLength(0); i++)
                StringToWrite += Nodes[i].Name + "  " + Nodes[i].X + "  " + Nodes[i].Y + "\r\n";
            StringToWrite += "-----";
            if (Precedents.Count > 0)
                foreach (int[] Pr in Precedents)
                {
                    StringToWrite += "\r\n";
                    for (int i = 0; i < Pr.Length; i++)
                    {
                        StringToWrite += Pr[i];
                        if (i < Pr.Length - 1)
                            StringToWrite += "  ";
                    }
                }
            else
                StringToWrite += "\r\n";
            SW.Write(StringToWrite);
            SW.Close();
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
                            //Удаление прецедентов, содержащих вершину
                            for (int i = Precedents.Count - 1; i >= 0; i--)
                                if (Precedents.ElementAt(i).Contains(Index))
                                    Precedents.RemoveAt(i);
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
                                        //Удаляем прецеденты, содержащие указанные вершины
                                        for (int i = Precedents.Count - 1; i >= 0; i-- )
                                        {
                                            if (Precedents.ElementAt(i).Contains(IndexesForPath[0]) && Precedents.ElementAt(i).Contains(IndexesForPath[1]))
                                                Precedents.RemoveAt(i);
                                        }
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
                                                "Изогнутость: " + Paths[IndexesForPath[0], IndexesForPath[1]].Curve);
                                IndexesForPath = new int[] { -1, -1 };
                            }
                        }
                    IndexesForPath = new int[] { -1, -1 };
                }
            }
        }

        private void отрисоватьЗановоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redraw();
        }

        private void загрузитьИнформациюИзФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader SR = new StreamReader(openFileDialog1.FileName, Encoding.Default);
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
                        Paths[i, j].Curve = Convert.ToDouble(ResultsLower[i][j]);
                Results = Regex.Split(SplitResults[3], "\r\n");
                ResultsLower = new string[Results.Length][];
                for (int i = 0; i < Results.GetLength(0); i++)
                {
                    ResultsLower[i] = Regex.Split(Results[i], "  ");
                }
                Nodes = new _Node[Results.Length];
                for (int i = 0; i < Results.Length; i++)
                    Nodes[i] = new _Node(ResultsLower[i][0], Convert.ToInt32(ResultsLower[i][1]), Convert.ToInt32(ResultsLower[i][2]));
                if (SplitResults[4].Length > 0)
                {
                    Results = Regex.Split(SplitResults[4], "\r\n");
                    ResultsLower = new string[Results.Length][];
                    for (int i = 0; i < Results.GetLength(0); i++)
                    {
                        ResultsLower[i] = Regex.Split(Results[i], "  ");
                    }
                    int[][] Precs = new int[Results.Length][];
                    for (int i = 0; i < Results.GetLength(0); i++)
                    {
                        Precs[i] = new int[ResultsLower[i].Length];
                        for (int j = 0; j < Precs[i].Length; j++)
                            Precs[i][j] = Convert.ToInt32(ResultsLower[i][j]);
                    }
                    Precedents = Precs.ToList<int[]>();
                }
                SR.Close();
                redraw();
            }
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
        public double PathClass, Curve;
        public _Path(int Length, double PathClass, double Curve)
        {
            this.Length = Length;
            this.PathClass = PathClass;
            this.Curve = Curve;
        }
    }
}
