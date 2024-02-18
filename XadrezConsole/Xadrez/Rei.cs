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

        private PartidaXadrez Partida {  get; set; }

        public Rei(Tabuleiros tab, Cor cor, PartidaXadrez partida) : base(cor, tab)
        {
            Partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.Cor != this.Cor;
        }

        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p is Torre && p.Cor == Cor & p.QteMovimentos == 0;
        }

        public override bool[,] movimentosPossives()
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            //acima 
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //ne
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //direita
            pos.definirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //sd
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //abaixo
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //so
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos.definirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // #JogadaEspecial:  Roque
            if(QteMovimentos == 0 && !Partida.Xeque)
            {
                //Roque pequeno
                Posicao PosT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (testeTorreParaRoque(PosT1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    if (tab.peca(p1) == null && tab.peca(p2) == null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }
                //roque big
                Posicao PosT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (testeTorreParaRoque(PosT2))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null)   
                    {
                        mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }

            }
            return mat;

        }
    }
}
