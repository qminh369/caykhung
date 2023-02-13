using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caykhung
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // tao cau truc Canh
        public class Egde
        {
            private int source;
            private int end ;
            private int weight;

            public int Source { get => source; set => source = value; }
            public int End { get => end; set => end = value; }
            public int Weight { get => weight; set => weight = value; }

            public Egde(int source, int end, int weight)
            {
                this.source = source;
                this.end = end;
                this.weight = weight;
            }
        }
        // tao Do thi = Danh sach cac canh
        public List<Egde> Graph;
        int SoDinh;
        // khoi tao do thi
        public void InitGraph()
        {
            Graph = new List<Egde>();
            // gia su ta can nhap do thi co thong tin nhu sau
            Graph.Add(new Egde(1, 2, 33));
            Graph.Add(new Egde(1, 3, 17));
            Graph.Add(new Egde(2, 3, 18));
            Graph.Add(new Egde(2, 4, 20));
            Graph.Add(new Egde(3, 4, 16));
            Graph.Add(new Egde(3, 5, 4));
            Graph.Add(new Egde(4, 5, 9));
            Graph.Add(new Egde(4, 6, 8));
            Graph.Add(new Egde(5, 6, 14));

            // vi co tinh chat trong ma tran doi xung nen ta cung khong can viet code dai nhu vay
            List<int> dsDinh = new List<int>();
            string msgDoThi = Graph.Count + "Canh:\n";
            foreach (Egde egde in Graph)
            {
                msgDoThi += "Canh (" + egde.Source + "," + egde.End + ")=" + egde.Weight + "\n";
                if (!dsDinh.Contains(egde.Source))
                    dsDinh.Add(egde.Source);
                if (!dsDinh.Contains(egde.End))
                    dsDinh.Add(egde.End);
            }
            SoDinh = dsDinh.Count;
            string msgInfo = "Do thi co " + SoDinh + " dinh\n" + msgDoThi;
            MessageBox.Show(msgInfo);
        }
        public List<Egde> Prim()
        {
            List<Egde> Tree = new List<Egde>();
            List<int> DanhDau = new List<int>();
            DanhDau.Add(Graph[0].Source);
            while (DanhDau.Count< SoDinh)
            {
                var Egdes = Graph.Where(p => (DanhDau.Contains(p.Source) && !DanhDau.Contains(p.End)) || (DanhDau.Contains(p.End) && !DanhDau.Contains(p.Source))) ;
                var minw = Egdes.Min(p => p.Weight);
                var minEgde=Egdes.Where(p=> p.Weight == minw).First();
                Tree.Add(minEgde);
                Graph.Remove(minEgde);
                if (!DanhDau.Contains(minEgde.Source))
                    DanhDau.Add(minEgde.Source);
                if (!DanhDau.Contains(minEgde.End))
                    DanhDau.Add(minEgde.End);
            }
            return Tree;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitGraph();
            List<Egde> Tree = Prim();
            string msg = "Cay khung toi thieu la:\n";
            foreach (var Egde in Tree)
            {
                msg += Egde.Source + "....." + Egde.End + "....." + Egde.Weight + "\n";
            }
            msg += "Weight:" + Tree.Sum(p => p.Weight);
            MessageBox.Show(msg);
        }
    }
}
