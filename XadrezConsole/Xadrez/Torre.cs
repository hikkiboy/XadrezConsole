using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;
using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class Torre : Peca
    {
        public Torre(Tabuleiros tab, Cor cor) : base(cor, tab)
        {

        }

        public override string ToString()
        {
            return "T";
        }
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] movimentosPossives()
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            //acim 
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna);
            while(tab.posicaoValida(pos) && podeMover(pos)) 
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).Cor != this.Cor) 
                {
                    break;
                }
                pos.Linha = pos.Linha - 1;
            }

            //abaixo
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).Cor != this.Cor)
                {
                    break;
                }
                pos.Linha = pos.Linha + 1;
            }

            //direita 
            pos.definirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).Cor != this.Cor)
                {
                    break;
                }
                pos.Coluna = pos.Coluna + 1;
            }

            //esquerda

            pos.definirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).Cor != this.Cor)
                {
                    break;
                }
                pos.Coluna = pos.Coluna - 1;
            }



            return mat;

        }
    }
}
