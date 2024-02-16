using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;
using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class Rei : Peca
    {
        public Rei (Tabuleiros tab, Cor cor) :base(cor, tab)
        {

        }

        public override string ToString()
        {
            return "R";
        }
    }
}
