using Tabuleiro;
using XadrezConsole.Tabuleiro;
using XadrezConsole.Xadrez;

namespace XadrezConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            try
            {
                PartidaXadrez partida = new PartidaXadrez();

                while (!partida.Termida)
                {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirPartida(partida);

                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerposicaoXadrez().toPosicao();
                        partida.validarOrigem(origem);


                        bool[,] possivel = partida.Tab.peca(origem).movimentosPossives();

                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.Tab, possivel);


                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerposicaoXadrez().toPosicao();
                        partida.validarDestino(origem, destino);


                        partida.realizaJogada(origem, destino);
                    }
                    catch(TabuleiroException e)
                    {
                        Console.Write(e.Message);
                        Console.ReadLine();
                    }
                }

                Console.Clear();
                Tela.imprimirPartida(partida);

                }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
