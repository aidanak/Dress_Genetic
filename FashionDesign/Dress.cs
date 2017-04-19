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
    /// Represents dress components
    /// </summary>
    public class Dress:IComparable<Dress>
    {
        /// <summary>
        /// Binarycode for skirt part 
        /// </summary>
        string skirt;
        /// <summary>
        /// Binarycode for sleeve part 
        /// </summary>
        string sleeve;
        /// <summary>
        /// Binarycode for body part 
        /// </summary>
        string body;
        /// <summary>
        /// Full binary representation of dress
        /// </summary>
        public string bincode;
        /// <summary>
        /// Rating of dress given by user
        /// </summary>
        public int fitness;
        /// <summary>
        /// Initializes a new instance of the Dress class.
        /// </summary>
        public Dress()
        {
            this.skirt = Convert.ToString(RandomGenerator.rnd.Next(0, 3), 2);
            while (skirt.Length != 3) skirt = "0" + skirt;
            this.body = Convert.ToString(RandomGenerator.rnd.Next(0, 3), 2);
            while (body.Length != 3) body = "0" + body;
            this.sleeve = Convert.ToString(RandomGenerator.rnd.Next(0, 3), 2);
            while (sleeve.Length != 3) sleeve = "0" + sleeve;
            this.bincode = GenerateBincode();
        }
        /// <summary>
        /// Initializes a new instance of the Dress class with given parameters
        /// </summary>
        /// <param name="body">Binarycode for body part </param>
        /// <param name="sleeve">Binarycode for sleeve part </param>
        /// <param name="skirt">Binarycode for skirt part </param>
        /// <param name="bincode">Full binary representation of dress</param>
        public Dress(string body,string sleeve,string skirt,string bincode)
        {
            this.skirt = skirt;
            this.body = body;
            this.sleeve = sleeve;
            this.bincode = bincode;
        }
        /// <summary>
        /// Generates binarycode of dress based on binarycode of its parts
        /// </summary>
        /// <returns>Full binary representation of dress</returns>
        public string GenerateBincode()
        {
            string bincode = "";
            bincode += body;
            bincode += sleeve;
            bincode += skirt;
            return bincode;
        }
        /// <summary>
        /// Creates DressImage UserControl based on images taken from database
        /// </summary>
        /// <returns>Dressimage object</returns>
        public DressImage CreateDressControl()
        {
            DressImage di = new DressImage();
            di.pictureBox1.BackgroundImage= Image.FromFile("Resources/body/"+body+".png");
            di.pictureBox3.BackgroundImage = Image.FromFile("Resources/sleeves/" + sleeve + ".png");
            di.pictureBox2.BackgroundImage = Image.FromFile("Resources/skirts/" + skirt + ".png");
            return di;
        }
        /// <summary>
        /// Compares this instance with a specified Object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified Object.
        /// </summary>
        /// <param name="obj">Dress object</param>
        /// <returns>Integer value</returns>
        public int CompareTo(Dress obj)
        {
            if (this.fitness > obj.fitness)
                return -1;
            if (this.fitness < obj.fitness)
                return 1;
            else
                return 0;
        }
    }
}
