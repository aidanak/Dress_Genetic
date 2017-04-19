using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionDesign
{
    /// <summary>
    /// Represents tools for checking existence of generated dress</summary>
    public static class ExistenceChecker
    {
        /// <summary>
        /// Checks if dress exists 
        /// </summary>
        /// <param name="child">
        /// Binary code that describes the dress</param>
        /// <returns>Returns true if dress exists</returns>
        public static bool CheckIfDressExists(string child)
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
                else if (i >= 6 && i <9)
                {
                    skirt += child[i];
                }
            }
            if (Convert.ToInt32(body, 2) > 2 || Convert.ToInt32(sleeve, 2) > 2 || Convert.ToInt32(skirt, 2) > 2)
            {
                return false;
            }
            return true;
        }
    }
}
