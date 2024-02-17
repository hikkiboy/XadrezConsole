
using Tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class PosicaoXadrez
    {
        public char Coluna {  get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public override string ToString()
        {
            return "" + Linha + Coluna;
        }

        public Posicao toPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }
    }
}
