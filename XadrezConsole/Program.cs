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
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.Tab);

                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerposicaoXadrez().toPosicao();


                    bool[,] possivel = partida.Tab.peca(origem).movimentosPossives();

                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.Tab, possivel);


                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerposicaoXadrez().toPosicao();


                    partida.executaMovimento(origem, destino);


                }
            }
            catch (TabuleiroException e )
            {
                Console.WriteLine(e.Message);
            }
           
        }
    }
}
