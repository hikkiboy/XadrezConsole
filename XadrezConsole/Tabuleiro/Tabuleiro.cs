using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
