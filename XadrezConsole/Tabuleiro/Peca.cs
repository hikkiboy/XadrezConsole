using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;

namespace XadrezConsole.Tabuleiro
{
    internal abstract class Peca
    {
        public Posicao Posicao { get; set; }

        public Cor Cor { get; protected set; }

        public int QteMovimentos { get; protected set; }

        public Tabuleiros tab {  get; protected set; }



        public Peca(Cor cor, Tabuleiros tab)
        {
            Cor = cor;
            this.tab = tab;
            QteMovimentos = 0;
        }

        public void incrementaMovimento()
        {
            QteMovimentos++;
        }

        public abstract bool[,] movimentosPossives();

        public bool existemovepossivel()
        {
            bool[,] mat = movimentosPossives();
            for(int i = 0; i < tab.Linhas; i++) 
            {
                for(int j = 0 ; j < tab.Colunas; j++)
                {
                    if (mat[i, j] == true)
                    {
                        return true;
                    }

                }
            }
            return false;
        }
        public bool podeMover(Posicao pos)
        {
            return movimentosPossives()[pos.Linha, pos.Coluna];
        }


    }
}
