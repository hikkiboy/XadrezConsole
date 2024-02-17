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
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
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

        public void realizaJogada (Posicao origem , Posicao destino)
        {
            executaMovimento(origem, destino);
            Turno++;
            mudaJogador();
        }

        private void mudaJogador()
        {
            if(JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public void validarOrigem(Posicao pos)
        {
            if(Tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não tem peça para mover");
            }
            else if (JogadorAtual != Tab.peca(pos).Cor)
            {
                throw new TabuleiroException("Não pode mover uma peça que não é sua");
            }
            else if (!Tab.peca(pos).existemovepossivel())
            {
                throw new TabuleiroException("Não há movimentos possiveis para a peça selecionada");
            }
        }

        public void validarDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.peca(origem).podeMover(destino))
            {
                throw new TabuleiroException("Posição de destino invalida");
            }
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
