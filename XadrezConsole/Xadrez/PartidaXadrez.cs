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

        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;


        public PartidaXadrez()
        {
            Termida = false;
            Tab = new Tabuleiros(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            colocarpecas();

        }

        public void executaMovimento (Posicao origem, Posicao destino)
        {
            Peca p = Tab.retirarPeca(origem);
            p.incrementaMovimento();
            Peca capturada = Tab.retirarPeca (destino);
            Tab.colocarPeca(p, destino);
            if(capturada != null)
            {
                Capturadas.Add(capturada);
            }
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

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            Pecas.Add(peca);
        }

        public HashSet<Peca> pecasCapturadas (Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca> ();
            foreach(Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }

            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private void colocarpecas()
        {
            colocarNovaPeca('c', 1, new Torre(Tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(Tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(Tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(Tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(Tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(Tab, Cor.Branca));
            
            
            colocarNovaPeca('d', 8, new Rei(Tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(Tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(Tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(Tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(Tab, Cor.Preta));
            colocarNovaPeca('c', 7, new Torre(Tab, Cor.Preta));


        }
    }
}
