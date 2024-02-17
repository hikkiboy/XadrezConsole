using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;
using XadrezConsole.Tabuleiro;
using XadrezConsole.Xadrez;

namespace XadrezConsole
{
    internal class Tela
    {
        public static void imprimirTabuleiro(Tabuleiros tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < tab.Colunas; j++)
                {
                    ImprimirPeca(tab.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");

        }

        public static void imprimirTabuleiro(Tabuleiros tab, bool[,] posicoesPossiveis)
        {

            ConsoleColor fundo = Console.BackgroundColor;
            ConsoleColor fundodiferente = ConsoleColor.DarkGray;
            Console.BackgroundColor = fundo;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < tab.Colunas; j++)
                {

                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundodiferente;
                    }
                    else
                    {
                        Console.BackgroundColor = fundo;
                    }
                    ImprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundo;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundo;

        }

        public static PosicaoXadrez lerposicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);

        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(' ');
            }
        }
    }
}
