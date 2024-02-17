using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;
using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class PartidaXadrez
    {
        public Tabuleiros Tab { get; private set; }
        private int Turno;
        private Cor JogadorAtual;
        public bool Termida {  get; private set; }

        public PartidaXadrez()
        {
            Termida = false;
            Tab = new Tabuleiros(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            colocarpecas();
        }

        public void executaMovimento (Posicao origem, Posicao destino)
        {
            Peca p = Tab.retirarPeca(origem);
            p.incrementaMovimento();
            Peca capturada = Tab.retirarPeca (destino);
            Tab.colocarPeca(p, destino);
        }

        private void colocarpecas()
        {
            Tab.colocarPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('c', 1).toPosicao());

            Tab.colocarPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('c', 2).toPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('d', 2).toPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('e', 2).toPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('e', 1).toPosicao());
            Tab.colocarPeca(new Rei(Tab, Cor.Branca), new PosicaoXadrez('d', 1).toPosicao());

            Tab.colocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('c', 7).toPosicao());

            Tab.colocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('c', 8).toPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('d', 7).toPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('e', 7).toPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('e', 8).toPosicao());
            Tab.colocarPeca(new Rei(Tab, Cor.Preta), new PosicaoXadrez('d', 8).toPosicao());
        }
    }
}
