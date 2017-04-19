using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FashionDesign
{
    /// <summary>
    /// Contains controls to display and manipulate over Dress object 
    /// </summary>
    public partial class Form1 : Form
    {
        GeneticAlgorithm ga;
        public Form1()
        {
            InitializeComponent();
            ga = new GeneticAlgorithm();
            ga.Initializing(this);
        }
        int generationnum = 0;
        /// <summary>
        /// Raises event which generates new population of dresses and displays them on Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (Control item in ga.panel.Controls.OfType<DressImage>())
            {
                GeneticAlgorithm.dresspopulation[i].fitness = Int32.Parse((item as DressImage).textBox1.Text);
                i++;
            }
            if (generationnum < 2)
            {
                if (generationnum == 1) this.button2.Text = "result";
                ga.GeneratePopulation(this);
            }

            else
            {
                ga.ShowDressonForm(GeneticAlgorithm.dressimages, this, true);
            }
            generationnum++;
            foreach (Control d in this.Controls)
            {
                if (d.GetType() == typeof(DressImage))
                {
                    (d as DressImage).textBox1.Text = "";
                }
            }
        }
    }
}
 