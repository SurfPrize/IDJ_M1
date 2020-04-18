using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateDB
{
    public class Master
    {
        private string _name = "NO NAME";
        public string Name
        {
            get => _name; set
            {
                if (value == "" || value == null)
                {
                    _name = "NO NAME";
                }
                else
                {
                    _name = value;
                }
            }
        }

        public string region;
        public Servant servant;

        public Master(string name, string region, Servant servant)
        {
            this.Name = name;
            this.region = region;
            this.servant = servant;
        }

        public Master()
        {
        }

        public override string ToString()
        {
            string resultado;
            if (servant != null)
            {
                resultado = Name + " Region:" + region + " \n " + Name + "'S SERVANT:" + servant.ToString();
            }
            else
            {
                resultado = Name + " Region:" + region + " \n NO SERVANT";
            }

            return resultado;
        }

    }
}
