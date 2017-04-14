using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace FashionDesign
{
    public class GeneticAlgorithm
    {
        public static List<Dress> dresspopulation = new List<Dress>();
        public static List<DressImage> dressimages = new List<DressImage>();
        public Panel panel;
        public static Dictionary<string, Color> color = new Dictionary<string, Color>();
        public void Initializing(Form form)
        {
            color.Add("000", Color.Red);
            color.Add("001", Color.Blue);
            color.Add("010", Color.Green);
            color.Add("011", Color.Yellow);
            color.Add("100", Color.Black);
            color.Add("101", Color.Orange);
            color.Add("110", Color.Magenta);
            color.Add("111", Color.Navy);
            for (int i = 0; i < 8; i++)
            {
                Dress dress = new Dress();
                DressImage di = dress.CreateDressControl();
                dressimages.Add(di);
                dresspopulation.Add(dress);
            }
            ShowDressonForm(dressimages, form,false);
        }
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
            panel.Size = new Size(994, 780);
            if (!isFinal)
            {
                int x = 2;
                int y = 2;
                for (int i = 0; i < 8; i++)
                {
                    dressimages[i].Location = new Point(x, y);
                    x += 250;
                    if (x >= 1000)
                    {
                        x = 2;
                        y += 300;
                    }
                    panel.Controls.Add(dressimages[i]);
                }
            }
            else
            {
                DressImage dressimage = dresspopulation[0].CreateDressControl();
                dressimage.Location = new Point(360, 250);
                dressimage.textBox1.Text = dresspopulation[0].fitness.ToString();
                panel.Controls.Add(dressimage);
                
            }
            form.Controls.Add(panel);
        }
        public void GeneratePopulation(Form form)
        {
            int bl;
            dresspopulation.Sort();
            List<string> children = new List<string>();
            string child;
            for (int i = 1; i < 5; i++)
            {
                bl = RandomGenerator.rnd.Next(0, 1);
                if (bl == 0)
                {
                    child = Mutate(Crossover(dresspopulation[0].bincode, dresspopulation[i].bincode));
                    while (!CheckIfDressExists(child))
                    {
                        child= Mutate(Crossover(dresspopulation[0].bincode, dresspopulation[i].bincode));
                    }
                    children.Add(child);
                }
                else
                {
                    child = Mutate(Crossover(dresspopulation[i].bincode, dresspopulation[0].bincode));
                    while (!CheckIfDressExists(child))
                    {
                        child = Mutate(Crossover(dresspopulation[i].bincode, dresspopulation[0].bincode));
                    }
                    children.Add(child);
                }
            }
            for (int i = 2; i < 4; i++)
            {
                bl = RandomGenerator.rnd.Next(0, 1);
                if (bl == 0)
                {
                    child = Mutate(Crossover(dresspopulation[1].bincode, dresspopulation[i].bincode));
                    while (!CheckIfDressExists(child))
                    {
                        child = Mutate(Crossover(dresspopulation[1].bincode, dresspopulation[i].bincode));
                    }
                    children.Add(child);
                }
                else
                {
                    child = Mutate(Crossover(dresspopulation[i].bincode, dresspopulation[1].bincode));
                    while (!CheckIfDressExists(child))
                    {
                        child = Mutate(Crossover(dresspopulation[i].bincode, dresspopulation[1].bincode));
                    }
                    children.Add(child);
                }
            }
            for (int i = 3; i < 5; i++)
            {
                bl = RandomGenerator.rnd.Next(0, 1);
                if (bl == 0)
                {
                    child = Mutate(Crossover(dresspopulation[2].bincode, dresspopulation[i].bincode));
                    while (!CheckIfDressExists(child))
                    {
                        child = Mutate(Crossover(dresspopulation[2].bincode, dresspopulation[i].bincode));
                    }
                    children.Add(child);
                }
                else
                {
                    child = Mutate(Crossover(dresspopulation[i].bincode, dresspopulation[2].bincode));
                    while (!CheckIfDressExists(child))
                    {
                        child = Mutate(Crossover(dresspopulation[i].bincode, dresspopulation[2].bincode));
                    }
                    children.Add(child);
                }
            }
            dresspopulation.Clear();
            dressimages.Clear();
            for (int i = 0; i < 8; i++)
            {
                dresspopulation.Add(ConvertStringToDress(children[i]));
                DressImage dress = ConvertStringToDress(children[i]).CreateDressControl();
                dressimages.Add(dress);
            }
            ShowDressonForm(dressimages, form,false);
        }
        public bool CheckIfDressExists(string child)
        {
            string body = "", sleeve = "", skirt = "";
            for (int i = 0; i < 23; i++)
            {
                if (i < 6)
                {
                    body += child[i];
                }
                else if (i >= 9 && i < 13)
                {
                    sleeve += child[i];
                }
                else if (i >= 16 && i < 20)
                {
                    skirt += child[i];
                }
            }
            if (Convert.ToInt32(body, 2) > 33 || Convert.ToInt32(sleeve, 2) > 7 || Convert.ToInt32(skirt, 2) > 7)
            {
                return false;
            }
            return true;
        }
        public Dress ConvertStringToDress(string child)
        {
            string body="", sleeve="", skirt="",skirtcol="",sleevecol="",bodycol="";
            for(int i = 0; i < 23; i++)
            {
                if (i < 6)
                {
                    body += child[i];
                }
                else if(i>=6 && i < 9)
                {
                    bodycol += child[i];
                }
                else if(i>=9 && i < 13)
                {
                    sleeve += child[i];
                }
                else if(i>=13 && i < 16)
                {
                    sleevecol += child[i];
                }
                else if(i>=16 && i < 20)
                {
                    skirt += child[i];
                }
                else if(i>=20 && i <= 23)
                {
                    skirtcol += child[i];
                }
            }
            Dress dress = new Dress(body, bodycol, sleeve, sleevecol, skirt, skirtcol,child);
            return dress;
        }
        public string Crossover(string p1,string p2)
        {
            string child = "";
            int separator=RandomGenerator.rnd.Next(0, 22);
            for(int i = 0; i < separator; i++)
            {
                child += p1[i];
            }
            for(int i = separator; i < 23; i++)
            {
                child += p2[i];
            }
            return child;
        }
        public string Mutate(string child)
        {
            StringBuilder sb = new StringBuilder(child);
            int mutationind= RandomGenerator.rnd.Next(0, 22);
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
