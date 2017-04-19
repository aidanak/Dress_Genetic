using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FashionDesign
{
    /// <summary>
       /// Represents tools to check the existence of dress in database
    /// </summary>
    public class DressSearcher
    {
        /// <summary>
        /// Searches matching dress from database based on binarycode
        /// </summary>
        /// <param name="bincode">Binarycode of final dress</param>
        /// <returns>List of PictureBox controls which displays images from database</returns>
        public static List <PictureBox> SearchDressFromDatabase(string bincode)
        {
            List<PictureBox> realdress = new List<PictureBox>();
            DirectoryInfo dir = new DirectoryInfo(@"C:\Users\a_kurmasheva\Desktop\FashionDesign-master\FashionDesign-master\FashionDesign\bin\Debug\Resources\dress");
            FileInfo[] realdressimages = null;
            realdressimages = dir.GetFiles();
            //MessageBox.Show(bincode);
            string imagename="", imagename1;
            foreach (FileInfo f in realdressimages)
            {
                imagename = f.Name.Substring(0, 9);
                if (imagename == bincode)
                {
                    PictureBox picturebox = new PictureBox();
                    Bitmap image = new Bitmap(f.FullName);
                    picturebox.BackgroundImage = image;
                    picturebox.BackgroundImageLayout= System.Windows.Forms.ImageLayout.Stretch;
                    realdress.Add(picturebox);
                }
               
            }
            foreach (FileInfo f in realdressimages)
            {
                imagename= f.Name.Substring(0, 9);
                imagename1 = imagename.Substring(3);
                if (imagename!=bincode && imagename1 == bincode.Substring(3))
                {
                    PictureBox picturebox = new PictureBox();
                    Bitmap image = new Bitmap(f.FullName);
                    picturebox.BackgroundImage = image;
                    picturebox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    realdress.Add(picturebox);
                    break;
                }

            }

            return realdress;
        }
    }
}
