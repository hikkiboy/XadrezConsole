using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;

namespace XadrezConsole.Tabuleiro
{
    internal class Tabuleiros
    {
        public int Linhas {  get; set; }
        public int Colunas { get; set; }

        private Peca[,] pecas;

        public Tabuleiros(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            pecas = new Peca[linhas,colunas];
        }

        public Peca peca(int linhas, int colunas)
        {
            return pecas[linhas,colunas];
        }

        public Peca peca(Posicao pos)
        {
            return pecas[pos.Linha, pos.Coluna];
        }

        public bool existePeca(Posicao pos)
        {
            validarPosicao(pos);
            return peca(pos) != null;
        }

        public void colocarPeca (Peca p, Posicao pos)
        {
            if (existePeca(pos))
            {
                throw new TabuleiroException("Posição ja ocupada");
            }
            pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }

        public Peca retirarPeca  (Posicao pos)
        {
            if(peca(pos) == null)
            {
                return null;
            }
            Peca aux = peca(pos);
            aux.Posicao = null;
            pecas[pos.Linha, pos.Coluna] = null;
            return aux;
        }

        public bool posicaoValida (Posicao pos)
        {
            if(pos.Linha <  0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
            {
                return false;
            }
            return true;
        }

        public void validarPosicao(Posicao pos)
        {
            if (!posicaoValida(pos))
            {
                throw new TabuleiroException("Posição Invalida");
            }
        }
    }
}
