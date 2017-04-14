using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FashionDesign
{
    public class Dress:IComparable<Dress>
    {
        string skirt;
        string sleeve;
        string body;
        public string bincode;
        public int fitness;
        string skirtcol;
        string bodycol;
        string sleevecol;
        int i = 0;
        public Dress()
        {
            this.skirt = Convert.ToString(RandomGenerator.rnd.Next(0, 9), 2);
            while (skirt.Length != 4) skirt = "0" + skirt;
            this.body = Convert.ToString(RandomGenerator.rnd.Next(0, 34), 2);
            while (body.Length != 6) body = "0" + body;
            this.sleeve = Convert.ToString(RandomGenerator.rnd.Next(0, 9), 2);
            while (sleeve.Length != 4) sleeve = "0" + sleeve;
            this.skirtcol = Convert.ToString(RandomGenerator.rnd.Next(0, 8), 2);
            while (skirtcol.Length != 3) skirtcol = "0" + skirtcol;
            this.sleevecol = Convert.ToString(RandomGenerator.rnd.Next(0, 8), 2);
            while (sleevecol.Length != 3) sleevecol= "0" + sleevecol;
            this.bodycol= Convert.ToString(RandomGenerator.rnd.Next(0, 8), 2);
            while (bodycol.Length != 3) bodycol = "0" + bodycol;
            this.bincode = GenerateBincode();
        }
        public Dress(string body,string bodycol,string sleeve,string sleevecol,string skirt,string skirtcol,string bincode)
        {
            this.skirt = skirt;
            this.body = body;
            this.sleeve = sleeve;
            this.skirtcol = skirtcol;
            this.bodycol = bodycol;
            this.sleevecol = sleevecol;
            this.bincode = bincode;
        }
        public string GenerateBincode()
        {
            string bincode = "";
            bincode += body;
            bincode += bodycol;
            bincode += sleeve;
            bincode += sleevecol;
            bincode += skirt;
            bincode += skirtcol;
            return bincode;
        }
        public DressImage CreateDressControl()
        {
            DressImage di = new DressImage();
            di.pictureBox1.BackgroundImage= Image.FromFile("Resources/body/"+body+".png");
            di.pictureBox3.BackgroundImage = Image.FromFile("Resources/sleeves/" + sleeve + ".png");
            di.pictureBox2.BackgroundImage = Image.FromFile("Resources/skirts/" + skirt + ".png");
            di.pictureBox4.BackColor = GeneticAlgorithm.color[sleevecol];
            di.pictureBox5.BackColor = GeneticAlgorithm.color[bodycol];
            di.pictureBox6.BackColor = GeneticAlgorithm.color[skirtcol];
            return di;
        }
        public int CompareTo(Dress obj)
        {
            if (this.fitness > obj.fitness)
                return 1;
            if (this.fitness < obj.fitness)
                return -1;
            else
                return 0;
        }
    }
}
