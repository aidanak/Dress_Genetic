using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionDesign
{
    /// <summary>
    /// Represents tools parsing binary code that describes dress to create the Dress object
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Parses binary code of dress to Dress object 
        /// </summary>
        /// <param name="child"> Binary code that describes the dress </param>
        /// <returns>Returns Dress object</returns>
        public static Dress ParseStringToDress(string child)
        {
            string body = "", sleeve = "", skirt = "";
            for (int i = 0; i < 9; i++)
            {
                if (i < 3)
                {
                    body += child[i];
                }
                else if (i >= 3 && i < 6)
                {
                    sleeve += child[i];
                }
                else if (i >= 6 && i < 9)
                {
                    skirt += child[i];
                }
            }
            Dress dress = new Dress(body, sleeve, skirt, child);
            return dress;
        }
    }
}
