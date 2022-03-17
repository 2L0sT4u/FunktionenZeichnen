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
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false; 
            label6.Visible = false;
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
                    g.DrawString(Convert.ToString(ax/zoom), s, Brushes.Black, 9, -1*ax);
                }
                ax += 50;
                ix += 1;
            }
        }

        private void DrawLinFunctionGraph(Double x1, Double y1, Double x2, Double y2)
        {
            label1.Text = "m = " + Convert.ToString((y2 - y1) / (x2 - x1));
            label2.Text = "n = " + Convert.ToString(y1 - x1 * ((y2 - y1) / (x2 - x1)));
            label3.Text = "x0 = " + Convert.ToString(-1 * (y1 - x1 * ((y2 - y1) / (x2 - x1))) / ((y2 - y1) / (x2 - x1)));
            label4.Text = "Sx( " + Convert.ToString(-1 * (y1 - x1 * ((y2 - y1) / (x2 - x1))) / ((y2 - y1) / (x2 - x1))) + " / 0 )";
            label5.Text = "Sy( 0 / " + Convert.ToString(y1 - x1 * ((y2 - y1) / (x2 - x1))) + " )";
            label6.Text = "f(x) = " + Math.Round(((y2 - y1) / (x2 - x1)), 2).ToString() + "x + " + Math.Round((y1 - x1 * ((y2 - y1) / (x2 - x1))), 2).ToString();
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;

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

            label1.Text = "Sx1( " + Convert.ToString(Math.Round((-1 * ((b / a) / 2) - Math.Sqrt(-1 * ((b / a) + (b / a)) / 4) - c / a), 2)) + " / 0 )";
            label1.Visible = true;
            label2.Text = "Sx2( " + Convert.ToString(Math.Round(-1 * ((b / a) / 2) + Math.Sqrt((-1 * (b / a + b / a) / 4) - c / a), 2)) + " / 0 )";
            label2.Visible = true;
            label3.Text = "S( " + Convert.ToString(Math.Round(-1 * ((b / a) / 2), 2)) + " / " + Convert.ToString(Math.Round(((-1 * (b / a + b / a) / 4) + c / a), 2)) + " )";
            label3.Visible = true;
            label4.Text = "a = " + Math.Round(a, 2).ToString();
            label4.Visible = true;
            label5.Text = "b = " + Math.Round(b, 2).ToString();
            label5.Visible = true;
            label6.Text = "c = " + Math.Round(c, 2).ToString();
            label6.Visible = true;
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