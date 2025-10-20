using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lr4_2
{
    public partial class Form1 : Form
    {
        Model model;
        public Form1()
        {
            InitializeComponent();
            model = new Model();
            model.observers += new System.EventHandler(this.UpdateFromModel);
            UpdateFromModel(this, null);
        }

        private void textBoxA_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            model.setValueA(Int32.Parse(textBoxA.Text));

        }
        private void numericUpDownA_ValueChanged(object sender, EventArgs e)
        {
            model.setValueA(Decimal.ToInt32(numericUpDownA.Value));

        }
        private void trackBarA_Scroll(object sender, EventArgs e)
        {
            model.setValueA(trackBarA.Value);
        }

       
        private void textBoxB_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                model.setValueB(Int32.Parse(textBoxB.Text));

        }
        private void numericUpDownB_ValueChanged(object sender, EventArgs e)
        {
            model.setValueB(Decimal.ToInt32(numericUpDownB.Value));
        }
        private void trackBarB_Scroll(object sender, EventArgs e)
        {
            model.setValueB(trackBarB.Value);
        }


        private void textBoxC_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            model.setValueC(Int32.Parse(textBoxC.Text));
        }

        private void numericUpDownC_ValueChanged(object sender, EventArgs e)
        {
            model.setValueC(Decimal.ToInt32(numericUpDownC.Value));
        }

        private void trackBarC_Scroll(object sender, EventArgs e)
        {
            model.setValueC(trackBarC.Value);
        }

        private void UpdateFromModel(object sender, EventArgs e)
        {
            textBoxA.Text = model.getValueA().ToString();
            numericUpDownA.Value = model.getValueA();
            trackBarA.Value = model.getValueA();

            textBoxB.Text = model.getValueB().ToString();
            numericUpDownB.Value = model.getValueB();
            trackBarB.Value = model.getValueB();

            textBoxC.Text = model.getValueC().ToString();
            numericUpDownC.Value = model.getValueC();
            trackBarC.Value = model.getValueC();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(model.getValueA().ToString());
            MessageBox.Show(model.getValueB().ToString());
            MessageBox.Show(model.getValueC().ToString());
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            model.SaveValues();
        }

        private void textBoxA_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBoxA.Text, "[^0-9]"))
            {
                textBoxA.Text = model.getValueA().ToString();
            }
        }

        private void textBoxB_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBoxB.Text, "[^0-9]"))
            {
                textBoxB.Text = model.getValueB().ToString();
            }
        }

        private void textBoxC_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBoxC.Text, "[^0-9]"))
            {
                textBoxC.Text = model.getValueC().ToString();
            }
        }
    }
    public class Model
    {
        private int valueA, valueB, valueC;
        public System.EventHandler observers;
        public Model()
        {
            this.valueA = Properties.Settings.Default.A;
            this.valueB = Properties.Settings.Default.B;
            this.valueC = Properties.Settings.Default.C;
        }
        ~Model() { }

        public void setValueA(int value)
        {
            if (value >= 0 && value <= 100)
            {
                this.valueA = value;
                if (value > valueC)
                    valueC = value;
                    //setValueC(value);
                if (value > valueB && (value <= valueC))
                    valueB = value;
                    //setValueB(value);
            }
            observers.Invoke(this, null);
        }
        public void setValueB(int value)
        {
            if (value >= 0 && value <= 100)
            {
                if(value <= valueC && valueA <= value)
                {
                    this.valueB = value;
                }
            }
            observers.Invoke(this, null);
        }
        public void setValueC(int value)
        {
            if (value >= 0 && value <= 100)
            {
                this.valueC = value;
                if (value < valueA)
                    valueA = value;
                    //setValueA(value);
                if (value < valueB && (valueA <= value))
                    valueB = value;
                    //setValueB(value);
            }
            observers.Invoke(this, null);
        }
        public int getValueA() { return valueA; }
        public int getValueB() { return valueB; }
        public int getValueC() { return valueC; }

        public void SaveValues()
        {
            Properties.Settings.Default.A = valueA;
            Properties.Settings.Default.B = valueB;
            Properties.Settings.Default.C = valueC;

            Properties.Settings.Default.Save();
        }
    }
}
