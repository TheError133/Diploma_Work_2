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
            double Curve;
            _Path NewPath, PathByClass = new _Path(0, 0, 0), PathByPrec = new _Path(0, 0, 0);
            if (TextLength.Text.Length > 0 && int.TryParse(TextLength.Text, out Length) && Length > 0)
                if (TextCurve.Text.Length > 0 && double.TryParse(TextCurve.Text, out Curve) && Curve >= 0.009999 && Curve <= 1)
                {
                    NewPath = new _Path(Length, ComboClasses.SelectedIndex + 1, Curve);
                    //int[] TmpPathC = new int[0], TmpPathP = new int[0];
                    //Thread ClassThread = new Thread(Classification_alg);
                    //Thread PrecThread = new Thread(precSearch);
                    Params ParC = new Params(Index1, Index2, 0, new int[0]), ParP = new Params(Index1, Index2, 0, new int[0]);
                    bool PathByClassFailed = false, PathByPrecFailed = false;
                    //Одновременный поиск невозможен, поскольку путь из прецедентов может быть хуже найденного по алгоритму
                    precSearch(ref ParP);
                    if (ParP.PathNodes.Length < 1)
                        PathByPrecFailed = true;
                    else
                        PathByPrec = new _Path(ParP.PathLength, getAvgClass(ParP.PathNodes), getAvgCurve(ParP.PathNodes));
                    Classification_alg(ref ParC);
                    if (ParC.PathLength == 0)
                        PathByClassFailed = true;
                    else
                    {
                        PathByClass = new _Path(ParC.PathLength, getAvgClass(ParC.PathNodes), getAvgCurve(ParC.PathNodes));
                        if (PathByClass.Length < PathByPrec.Length)
                            PathByPrecFailed = true;
                    }
                    //Если пути нет в прецедентах или он хуже уже найденного по классификационному алгоритму, то мы добавим найденный по алгоритму
                    if (PathByPrecFailed && !PathByClassFailed)
                        MForm.Precedents.Add(ParC.PathNodes);
                    if (PathByClassFailed && PathByPrecFailed)
                    { }
                    else
                    {
                        _Path OldPath;
                        if (!PathByClassFailed)
                            OldPath = PathByClass;
                        else
                            OldPath = PathByPrec;
                        double NewPathQuality = 1, OldPathQuality = 1, K, NewClassK, OldClassK, NewCurveK, OldCurveK, DiffK;
                        K = NewPath.Length / OldPath.Length;
                        //Учет разницы в расстоянии (1)
                        if (K <= 0.5)
                        {
                            NewPathQuality = 2;
                            OldPathQuality = 1;
                        }
                        if (K > 0.5 && K <= 0.8)
                        {
                            NewPathQuality = 1.5;
                            OldPathQuality = 1;
                        }
                        if (K > 0.8 && K <= 1.2)
                        {
                            NewPathQuality = 1;
                            OldPathQuality = 1;
                        }
                        if (K > 1.2 && K <= 1.5)
                        {
                            NewPathQuality = 1;
                            OldPathQuality = 1.5;
                        }
                        if (K > 1.5)
                        {
                            NewPathQuality = 1;
                            OldPathQuality = 2;
                        }
                        //Преобразование класса в коэффициент (2)
                        NewClassK = 0.2 * NewPath.PathClass;
                        OldClassK = 0.2 * OldPath.PathClass;
                        //Учет изогнутости пути (3)
                        NewCurveK = 1 - NewPath.Curve + 0.01;
                        OldCurveK = 1 - OldPath.Curve + 0.01;
                        //Сравнение коэффициентов (1 + 2 + 3)
                        DiffK = NewPathQuality * NewClassK * NewCurveK - OldPathQuality * OldClassK * OldCurveK;
                        if (DiffK <= -0.7)
                            MessageBox.Show("Новый путь полностью бессмысленный.");
                        if (DiffK > -0.7 && DiffK <= -0.2)
                            MessageBox.Show("Новый путь хуже уже существующего. Не стоит его прокладывать.");
                        if (DiffK > -0.2 && DiffK <= 0.2)
                            MessageBox.Show("Новый путь примерно соответствует уже существующему. Его прокладывание может быть оправдано в будущем.");
                        if (DiffK > 0.2 && DiffK <= 0.7)
                            MessageBox.Show("Новый путь лучше уже существующего. Рекомендуется его проложить.");
                        if (DiffK > 0.7)
                            MessageBox.Show("Новый путь полностью оправдан.");
                    }
                    MForm.Paths[Index1, Index2] = NewPath;
                    MForm.Paths[Index2, Index1] = NewPath;
                    Close();
                }
                else
                    MessageBox.Show("Укажите значение изогнутости в диапазоне 0.01 - 1");
            else
                MessageBox.Show("Укажите числовое расстояние между вершинами");
        }
        private void Classification_alg(ref Params Par)
        {
            Par.PathLength = 0;
            Par.PathNodes = new int[0];
            int[] NodeClass = new int[MForm.Nodes.Length];
            int[][] matr = new int[MForm.Nodes.Length][];
            for (int i = 0; i < MForm.Nodes.Length; i++)
                matr[i] = new int[MForm.Nodes.Length];
            for (int i = 0; i < MForm.Nodes.Length; i++)
                for (int j = 0; j < MForm.Nodes.Length; j++)
                    matr[i][j] = MForm.Paths[i, j].Length;
            for (int i = 0; i < NodeClass.Length; i++)
            {
                if (matr[i].Where(p => p != 0).Count() == 0)//Добавил этот класс как изолированные вершины, иначе алгоритм работает неверно
                    NodeClass[i] = 0;
                if (matr[i].Where(p => p != 0).Count() > 2)
                    NodeClass[i] = 4;
                if (matr[i].Where(p => p != 0).Count() == 1)//Немного подправил, поскольку неравество убирает изолированные вершины
                    NodeClass[i] = 1;
                if (matr[i].Where(p => p != 0).Count() == 2)
                {
                    int[] indexes = new int[2];
                    int q = 0;
                    for (int j = 0; j < NodeClass.Length; j++)
                        if (matr[i][j] != 0)
                            indexes[q++] = j;
                    if (matr[indexes[0]][indexes[1]] == 0)
                        NodeClass[i] = 3;
                    else
                        if (matr[indexes[0]][indexes[1]] < matr[i][indexes[0]] + matr[i][indexes[1]])
                            NodeClass[i] = 2;
                        else
                            NodeClass[i] = 3;
                }
            }
            //Это условие необходимо, поскольку без него не учитываются изолированные вершины
            if (NodeClass[Par.Start] != 0 && NodeClass[Par.Finish] != 0)
            {
                string[] paths = new string[matr.GetLength(0)];
                int[] lengths = new int[matr.GetLength(0)];
                bool[] used = new bool[matr.GetLength(0)];
                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    if ((NodeClass[i] == 1 || NodeClass[i] == 2) && i == Par.Finish || NodeClass[i] == 3 || NodeClass[i] == 4)
                    {
                        used[i] = false;
                        lengths[i] = 0;
                        if (i != Par.Start)
                            paths[i] += Par.Start;
                    }
                    else
                        used[i] = true;
                }
                bool check = false;
                int index = Par.Start;
                while (!check)
                {
                    used[index] = true;
                    check = true;
                    for (int i = 0; i < used.Length; i++)
                    {
                        if (!used[i])
                            if (matr[index][i] != 0)
                            {
                                if (lengths[i] == 0)
                                    if (index == Par.Start)
                                    {
                                        lengths[i] += matr[index][i];
                                        paths[i] += " -> " + i;
                                    }
                                    else
                                    {
                                        lengths[i] = lengths[index] + matr[index][i];
                                        paths[i] = paths[index] + " -> " + i;
                                    }
                                else
                                    if (lengths[i] > lengths[index] + matr[index][i])
                                    {
                                        lengths[i] = lengths[index] + matr[index][i];
                                        paths[i] = paths[index] + " -> " + i;
                                    }
                            }
                        check &= used[i];
                    }
                    int lowest = 0;
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
                Par.PathLength = lengths[Par.Finish];
                string[] str = Regex.Split(paths[Par.Finish], " -> ");
                Par.PathNodes = new int[str.Length];
                for (int i = 0; i < Par.PathNodes.Length; i++)
                    Par.PathNodes[i] = Convert.ToInt32(str[i]);
            }
        }
        private double getAvgClass(int[] Indexes)
        {
            double[] Classes = new double[Indexes.Length - 1];
            for (int i = 0; i < Classes.Length; i++)
                Classes[i] = MForm.Paths[Indexes[i], Indexes[i + 1]].PathClass;
            return Classes.Average();
        }
        private double getAvgCurve(int[] Indexes)
        {
            double[] Curves = new double[Indexes.Length - 1];
            for (int i = 0; i < Curves.Length; i++)
                Curves[i] = MForm.Paths[Indexes[i], Indexes[i + 1]].Curve;
            return Curves.Average();
        }

        private void precSearch(ref Params Par)
        {
            Par.PathLength = 0;
            Par.PathNodes = new int[0];
            if (MForm.Precedents.Count > 0)
                foreach (int[] Pr in MForm.Precedents)
                    if (Pr.Contains(Par.Start) && Pr.Contains(Par.Finish))
                    {
                        int StartInd = -1, FinishInd = -1;
                        int LowestLength = 0;
                        int[] ShortestPath;
                        for (int i = 0; i < Pr.Length; i++)
                        {
                            if (Pr[i] == Par.Start)
                                StartInd = i;
                            if (Pr[i] == Par.Finish)
                                FinishInd = i;
                        }
                        int Amount = StartInd - FinishInd < 0 ? (StartInd - FinishInd) * -1 + 1 : StartInd - FinishInd + 1;
                        ShortestPath = new int[Amount];
                        Par.PathNodes = new int[Amount];
                        int Lowest = StartInd < FinishInd ? StartInd : FinishInd;
                        for (int i = 0; i < Amount; i++)
                            ShortestPath[i] = Pr[i + Lowest];
                        for (int i = 0; i < Par.PathNodes.Length - 1; i++)
                            LowestLength += MForm.Paths[Par.PathNodes[i], Par.PathNodes[i + 1]].Length;
                        //Проверка на случай если прецеденты содержат несколько путей, содержащих начальную и конечную вершину
                        if (Par.PathLength == 0)
                        {
                            Par.PathNodes = ShortestPath;
                            Par.PathLength = LowestLength;
                        }
                        if (Par.PathLength > LowestLength)
                        {
                            Par.PathNodes = ShortestPath;
                            Par.PathLength = LowestLength;
                        }
                    }
        }
    }
    class Params
    {
        public int Start, Finish, PathLength;
        public int[] PathNodes;
        public Params(int Start, int Finish, int PathLength, int[] PathNodes)
        {
            this.Start = Start;
            this.Finish = Finish;
            this.PathLength = PathLength;
            this.PathNodes = PathNodes;
        }
    }
}
