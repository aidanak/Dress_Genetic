using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace FashionDesign
{
    /// <summary>
    /// Represents fields and methods that generate new population of dress and displays them on the form 
    /// </summary>
    public class GeneticAlgorithm
    {
        /// <summary>
        /// Represents a List object that contains Dress objects and stores each population of dresses
        /// </summary>
        public static List<Dress> dresspopulation = new List<Dress>();
        /// <summary>
        /// Represents a List object that contains objects of DressImage UserControl 
        /// </summary>
        public static List<DressImage> dressimages = new List<DressImage>();
        /// <summary>
        /// Represents a field of type Panel that contains DressImage UserControls
        /// </summary>
        public Panel panel;
        /// <summary>
        /// Generate initial population of dresses and displays them on form
        /// </summary>
        /// <param name="form">Form object that contains panel which displays DressImage UserControl</param>
        public void Initializing(Form form)
        {
            int i = 0;
            while(true)
            {
                if (i == 4)
                {
                    break;
                }
                bool bl = false;
                Dress dress = new Dress();
                DressImage di = dress.CreateDressControl();
                for (int j = 0; j < dresspopulation.Count; j++)
                {
                    if (dress.bincode == dresspopulation[j].bincode)
                    {
                        bl = true;

                    }
                }
                if (!bl)
                {
                    dressimages.Add(di);
                    dresspopulation.Add(dress);
                    i++;
                }
                    
            }
            ShowDressonForm(dressimages, form,false);
        }
        /// <summary>
        /// Displays objects of DressImage on Panel object which is one of the controls of Form
        /// </summary>
        /// <param name="dressimages">Parameter of type List that contains objects of DressImage UserControl</param>
        /// <param name="form">Parameter of type Form that contains panel which displays DressImage UserControl</param>
        /// <param name="isFinal">Parameter of type bool which indictes whether it is the last generation</param>
        public void ShowDressonForm(List<DressImage> dressimages, Form form,bool isFinal)
        {
            foreach (Control d in form.Controls)
            {
                if (d.GetType() == typeof(Panel))
                {
                    form.Controls.Remove(d);
                }
            }
            panel = new Panel();
            panel.Size = new Size(540, 680);
            if (!isFinal)
            {
                int x = 0;
                int y = 0;
                for (int i = 0; i < 4; i++)
                {
                    dressimages[i].Location = new Point(x, y);
                    x += 265;
                    if (x >= 530)
                    {
                        x = 0;
                        y += 340;
                    }
                    panel.Controls.Add(dressimages[i]);
                }
            }
            else
            {
                dresspopulation.Sort();
                DressImage dressimage = dresspopulation[0].CreateDressControl();
                dressimage.Location = new Point(145, 10);
                foreach(Control c in form.Controls)
                {
                    if(c is Button)
                    {
                        form.Controls.Remove(c);
                    }
                }
                dressimage.textBox1.Text = dresspopulation[0].fitness.ToString();
                panel.Controls.Add(dressimage);
                List <PictureBox> realdress=DressSearcher.SearchDressFromDatabase(dresspopulation[0].bincode);
                int x = 30;
                foreach (PictureBox p in realdress)
                {
                    p.Location=new Point(x, 360);
                    p.Size = new Size(180, 300);
                    x += 190;
                    panel.Controls.Add(p);
                }
                
            }
            form.Controls.Add(panel);
        }
        /// <summary>
        /// Generates new population of Dress object and displays them on Form 
        /// </summary>
        /// <param name="form">Parameter of type Form that contains panel which displays DressImage UserControl</param>
        public void GeneratePopulation(Form form)
        {
            int bl;
            dresspopulation.Sort();
            List<string> children = new List<string>();
            children.Add(dresspopulation[0].bincode);
            for (int i = 1; i <=3; i++)
            {
                bl = RandomGenerator.rnd.Next(0, 1);
                if (bl == 0)
                {
                   GenerateDress(children,dresspopulation[0].bincode, dresspopulation[i].bincode); 
                }
                else
                {
                   GenerateDress(children, dresspopulation[i].bincode, dresspopulation[0].bincode);
                }
            }
           
            dresspopulation.Clear();
            dressimages.Clear();
            for (int i = 0; i < 4; i++)
            {
                dresspopulation.Add(Parser.ParseStringToDress(children[i]));
                DressImage dress = Parser.ParseStringToDress(children[i]).CreateDressControl();
                dressimages.Add(dress);
            }
            ShowDressonForm(dressimages, form,false);
        }
        /// <summary>
        /// Creates new Dress object resulted from Crossover and Mutation operations of two Dress objects from previous generation 
        /// </summary>
        /// <param name="children">Parameter of type List which stores binarycode of generated Dress objects</param>
        /// <param name="parent1">Parameter of type string which defines binarycode of the first parent of generated dress</param>
        /// <param name="parent2">Parameter of type string which defines binarycode of the second parent of generated dress</param>
        public void GenerateDress(List <string> children,string parent1,string parent2)
        {
            string child;
            while (true)
            {
                child = Mutate(Crossover(parent1, parent2));
                if (ExistenceChecker.CheckIfDressExists(child) && !children.Contains(child))
                {
                    break;
                }
                
            }
            children.Add(child);
        }
        /// <summary>
        /// Randomly combines bits of first parent and bits of second parent to generate new dress
        /// </summary>
        /// <param name="p1">Binarycode of first parent</param>
        /// <param name="p2">Binaryycode of second parent </param>
        /// <returns>Binarycode of generated dress</returns>
        public string Crossover(string p1,string p2)
        {
            string child = "";
            int separator=RandomGenerator.rnd.Next(0, 8);
            for(int i = 0; i < separator; i++)
            {
                child += p1[i];
            }
            for(int i = separator; i < 9; i++)
            {
                child += p2[i];
            }
            return child;
        }
        /// <summary>
        /// Changes one random bit of generated dress
        /// </summary>
        /// <param name="child">Binarycode of generated dress</param>
        /// <returns>Binarycode of generated dress</returns>
        public string Mutate(string child)
        {
            StringBuilder sb = new StringBuilder(child);
            int mutationind= RandomGenerator.rnd.Next(0, 8);
            if (child[mutationind] == '0')
            {
                sb[mutationind] = '1';
            }
            else
            {
                sb[mutationind] = '0';
            }
            child = sb.ToString();
            return child;
        }
    }
}
