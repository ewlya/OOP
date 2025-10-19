using lr4.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lr4
{
    public partial class Form1 : Form
    {
        
        private List<Circle> circles = new List<Circle>();
        private bool IsLastUnselect = false;
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
    
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkCtrlBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkIsOneBox_CheckedChanged(object sender, EventArgs e)
        {

        }
        //при нажатии на кнопку Del, все выделенные объекты должны удаляться
        private void buttonDel_Click(object sender, EventArgs e)
        {
            for(int i = circles.Count() - 1; i>=0; i--)
            {
                if (circles[i].GetColor() == Color.Red)
                    circles.RemoveAt(i);

            }

            Refresh();
        }
        //при событии Paint должны отрисовываться на форме все объекты из контейнера

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Circle c in circles)
            {
                c.DrawCircle(e.Graphics);

            }
            
        }
        //при нажатии мышкой на форме создается новый объект CCircle с координатами нажатия и помещается в контейнер

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!checkCtrlBox.Checked)
            {
                foreach (Circle c in circles)
                {
                    c.SetColor(Color.Black);

                }
                IsLastUnselect = true;
                circles.Add(new Circle(e.X, e.Y, 30, Color.Red));
            }
            else
            {
                if (IsLastUnselect)
                {
                    circles[circles.Count - 1].SetColor(Color.Black);
                    IsLastUnselect = false;
                }
                if (checkIsOneBox.Checked)
                {
                    foreach (Circle c in circles)
                    {
                        if (c.GetColor() != Color.Red && c.IsSelect(e.X, e.Y))
                        {
                            break;
                        }
                    }
                }
                else
                {
                    foreach (Circle c in circles)
                    {
                        c.IsSelect(e.X, e.Y);
                    }
                }
            }
            Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control == true)
            {
                checkCtrlBox.Checked = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control == false) {
                checkCtrlBox.Checked = false;
            }
        }
    }

    public class Circle
    {
        private int x, y, radius;
        private Color color = Color.Red;
        public Circle()
        {
            this.x = 0;
            this.y = 0;
            this.radius = 1;
        }
        public Circle(int x, int y, int radius, Color color)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.color = color;
        }
        public Circle(Circle circle)
        {
            this.radius = circle.radius;
            this.x = circle.x;
            this.y = circle.y;
            this.color = circle.color;
        }
        ~Circle()
        {

        }
        public void DrawCircle(Graphics g) {
            g.DrawEllipse(new Pen(color), x - radius, y - radius, radius*2, radius*2);

        }

        public Color GetColor()
        {
            return color;
        }
        public void SetColor(Color color)
        {
            this.color = color;
        }
        public bool IsSelect(int x, int y)
        {
            if(Math.Pow(x-this.x, 2) + Math.Pow(y-this.y, 2) <= Math.Pow(radius, 2))
            {
                color = Color.Red;
                return true;
            }
            return false;
        }
    }
}


/*
 •	Создать простейшее приложение с GUI, содержащее:
o	определение простейшего класса CCircle с координатами и постоянным радиусом; //
o	форму с объектом для рисования (например, PaintBox);
o	два элемента checkbox для управления поведением

•	Реализовать следующее поведение: //
o	при нажатии мышкой на форме создается новый объект CCircle с координатами нажатия и помещается в контейнер; //
o	при событии Paint должны отрисовываться на форме все объекты из контейнера //

•	Реализовать следующее поведение с выделением объектов:
o	хотя бы один объект из существующих на форме всегда является выделенным и отрисовывается отлично от других объектов;
o	выделенными могут быть несколько объектов;
o	выделение происходит при нажатии ЛКМ (левой клавиши мыши) на объект CCircle на форме; при нажатии на область формы,
где пересекаются несколько кругов, могут выделяться они все или какой-то один из них
o	при нажатии на кнопку Del, все выделенные объекты должны удаляться //
o	при выделении объекта с помощью ЛКМ и удерживаемой клавиши Ctrl, выделенными становятся несколько объектов

o	несколько изменяемых пользователем флагов (элементы checkbox) управляют логикой поведения:
	checkbox для указания, работает ли клавиша Ctrl
	checkbox для указания, выделяется только один объект при нажатии на их пересечение, или все, в которые попала мышка.

При работе над заданием обратите внимание на то, чтобы не нарушать инкапсуляцию объектов. 
Нельзя запрашивать у объектов их координаты и проверять, попала ли в них мышка – это должен решать сам объект. 
Нельзя запрашивать у объектов их координаты и рисовать их на форме – рисовать себя на форме должен сам объект, и так далее.

*/