using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;

namespace XadrezConsole.Tabuleiro
{
    internal class Peca
    {
        public int Posicao { get; set; }

        public Cor Cor { get; protected set; }

        public int QteMovimentos { get; protected set; }

        public Tabuleiros tab {  get; protected set; }

        public Peca(int posicao, Cor cor, Tabuleiros tab)
        {
            Posicao = posicao;
            Cor = cor;
            this.tab = tab;
            QteMovimentos = 0;
        }


    }
}
