using Tabuleiro;
using XadrezConsole.Tabuleiro;
using XadrezConsole.Xadrez;

namespace XadrezConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiros tab = new Tabuleiros(8, 8);

                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.colocarPeca(new Rei(tab, Cor.Branca), new Posicao(2, 4));

                Tela.imprimirTabuleiro(tab);
            }
            catch (TabuleiroException e )
            {
                Console.WriteLine(e.Message);
            }
           
        }
    }
}
