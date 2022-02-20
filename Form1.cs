using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunktionenZeichnen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Hide();
            HidePkt3();
        }
        Graphics g;
        Pen p = new Pen(Color.Black, 1);
        Font s = new Font("Comic Sans MS", 8);

        float zoom = 1;
        string Funktoinsart = "linear";
        public float x1, y1, x2, y2;



        private void HidePkt3()
        {
            lblbPkt3.Visible = false;
            lblSlash3.Visible = false;
            txtbxX3.Visible = false;
            txtbxY3.Visible = false;
        }
        private void ShowPkt3()
        {
            lblbPkt3.Visible = true;
            lblSlash3.Visible = true;
            txtbxX3.Visible = true;
            txtbxY3.Visible = true;
        }
        private void Hide()
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;        
        }
        private void HideMenu()
        {
            if (panel2.Visible == true) panel2.Visible = false;
            if (panel3.Visible == true) panel3.Visible = false;
            if (panel4.Visible == true) panel4.Visible = false;
        }
        private void ShowMenu(Panel menu)
        {
            if (menu.Visible == false)
            {
                HideMenu();
                menu.Visible = true;
            }
            else
                menu.Visible = false;
        }

        private void SauberMachen()
        {
            g = CreateGraphics();
            g.Clear(BackColor);
        }

        //.............. Funktion Zeichnen

        private void DrawCoordinateSystem()
        {
            g = CreateGraphics();
            g.TranslateTransform(750, 350);
            g.DrawLine(p, -300, 0, 300, 0);
            g.DrawLine(p, 0, -300, 0, 300);

            int ix = -5;
            int ax = -250;
            for (int i = 0; i < 11; i++)
            {
                if (ix != 0)
                {
                    g.DrawLine(p, ax, -3, ax, 3);
                    g.DrawLine(p, -3, ax, 3, ax);

                    g.DrawString(Convert.ToString(ax/zoom), s, Brushes.Black, ax - 3, 6);
                    g.DrawString(Convert.ToString(ax/zoom), s, Brushes.Black, 9, ax);
                }
                ax += 50;
                ix += 1;
            }
        }

        private void DrawLinFunctionGraph(Double x1, Double y1, Double x2, Double y2)
        {
            Double m = (y2 - y1) / (x2 - x1);
            Double n = y1 - (x1 * m);

            Double X1 = -300;
            Double Y1 = X1 * m + n * zoom;
            Double X2 = 300;
            Double Y2 = X2 * m + n * zoom;

            Graphics g;
            Pen p = new Pen(Color.Red, 2);

            for (int i = 0; i < 300; i++)
            {
                while (Y1 >= 300 || Y1 <= -300)
                {
                    Y1 = X1 * m + n * zoom;
                    X1 += 1;
                }
            }
            for (int i = 0; i < 300; i++)
            {
                while (Y2 >= 300 || Y2 <= -300)
                {
                    Y2 = X2 * m + n * zoom;
                    X2 -= 1;
                }
            }

            g = CreateGraphics();
            g.TranslateTransform(750, 350);
            g.DrawLine(p, float.Parse(X1.ToString()), float.Parse(Convert.ToString(-1 * Y1)), float.Parse(X2.ToString()), float.Parse(Convert.ToString(-1 * Y2)));
        }
        private void DrawQuFunctionGraph(Double x1, Double y1, Double x2, Double y2, Double x3, Double y3)
        {
            Double a = ((y1 - y2) / (x1 - x2) - (y1 - y3) / (x1 - x3)) / (x2 - x3);
            Double b = (y1 - y2) / (x1 - x2) - a * (x1 + x2);
            Double c = y1 - b * x1 - a * x1 * x1;

            Double X, Y;
            for (int i = -300; i < 301; i++)
            {
                X = i/zoom;
                Y = X * X * a + X * b + c;

                g.DrawEllipse(p, i, float.Parse((Y * -1 * zoom).ToString()), 1, 1);
            }       
        }

        private void CalcFunction(Double x1, Double y1, Double x2, Double y2)
        {
            Double m, n, x0;

            m = (y2 - y1) / (x2 - x1);
            n = y1 - x1 * m;
            x0 = -n / m;

            if (y1 == y2) // Gerade || zur x-Achse
            {
                
            }
            else if (x1 == x2)
            {
            }
            else
            {
                
            }
        }

        private void btnFkt_Click(object sender, EventArgs e)
        {
            ShowMenu(panel2);
        }

        private void btnZ_Click(object sender, EventArgs e)
        {
            ShowMenu(panel3);
        }

        private void BtnEinAus_Click(object sender, EventArgs e)
        {
            ShowMenu(panel4);
        }

        private void btnnLinfFkt_Click(object sender, EventArgs e)
        {
            Funktoinsart = "linear";
            HidePkt3();
            HideMenu();
        }

        private void btnQuFkt_Click(object sender, EventArgs e)
        {
            Funktoinsart = "quadratisch";
            ShowPkt3();
            HideMenu();
        }

        private void btnZ1_Click(object sender, EventArgs e)
        {
            zoom = 1;
            HideMenu();
        }

        private void btnZ10_Click(object sender, EventArgs e)
        {
            zoom = 10;
            HideMenu();
        }

        private void btnZ25_Click(object sender, EventArgs e)
        {
            zoom = 25;
            HideMenu();
        }

        private void btnZ50_Click(object sender, EventArgs e)
        {
            zoom = 50;
            HideMenu();
        }

        private void btnZ100_Click(object sender, EventArgs e)
        {
            zoom = 100;
            HideMenu();
        }

        private void btnZeichnen_Click(object sender, EventArgs e)
        {
            SauberMachen();
            DrawCoordinateSystem();
            if (Funktoinsart == "linear")
            {
                DrawLinFunctionGraph(Convert.ToDouble(txtbxX1.Text), Convert.ToDouble(txtbxY1.Text), Convert.ToDouble(txtbxX2.Text), Convert.ToDouble(txtbxY2.Text));
            }
            if ( Funktoinsart == "quadratisch")
            {
                DrawQuFunctionGraph(Convert.ToDouble(txtbxX1.Text), Convert.ToDouble(txtbxY1.Text), Convert.ToDouble(txtbxX2.Text), Convert.ToDouble(txtbxY2.Text), Convert.ToDouble(txtbxX3.Text), Convert.ToDouble(txtbxY3.Text));
            }
            HideMenu();
        }
    }
}