﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        public bool Termida { get; private set; }

        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaXadrez()
        {
            Termida = false;
            Tab = new Tabuleiros(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Xeque = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            VulneravelEnPassant = null;
            colocarpecas();

        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.retirarPeca(origem);
            p.incrementaMovimento();
            Peca pecacapturada = Tab.retirarPeca(destino);
            Tab.colocarPeca(p, destino);
            if (pecacapturada != null)
            {
                Capturadas.Add(pecacapturada);
            }

            //#jogadaEspecial RoquePequeno
            if(p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.retirarPeca(origemT);
                T.incrementaMovimento();
                Tab.colocarPeca(T, destinoT);
            }
            //#jogadaEspecial RoqueBig
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.retirarPeca(origemT);
                T.incrementaMovimento();
                Tab.colocarPeca(T, destinoT);
            }

            //google en passant
            if(p is Peao)
            {
                if(origem.Coluna != destino.Coluna && pecacapturada == null)
                {
                    Posicao posP;
                    if(p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecacapturada = Tab.retirarPeca(posP);
                    Capturadas.Add(pecacapturada);

                }
            }


            return pecacapturada;

        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca capturada)
        {
            Peca p = Tab.retirarPeca(destino);
            p.decrementarMovimento();
            if (capturada != null)
            {
                Tab.colocarPeca(capturada, destino);
                Capturadas.Remove(capturada);
            }
            Tab.colocarPeca(p, origem);

            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.retirarPeca(destinoT);
                T.decrementarMovimento();
                Tab.colocarPeca(T, origemT);
            }

            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.retirarPeca(destinoT);
                T.incrementaMovimento();
                Tab.colocarPeca(T, origemT);
            }

            //en passant
            if( p is Peao)
            {
                if(origem.Coluna != destino.Coluna && capturada == VulneravelEnPassant)
                {
                    Peca peao = Tab.retirarPeca(destino);
                    Posicao posp;
                    if (p.Cor == Cor.Branca)
                    {
                        posp = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posp = new Posicao(4, destino.Coluna);
                    }
                    Tab.colocarPeca(peao, posp);
                }
            }

        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca capturda = executaMovimento(origem, destino);
            Peca p = Tab.peca(destino);

            if (estaEmXeque(JogadorAtual))
            {
                desfazMovimento(origem, destino, capturda);
                throw new TabuleiroException("Não pode se colocar em xeque");
            }

            //Proooooooooooooomotion
            if(p is Peao)
            {
                if ((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7) )
                {
                    p = Tab.retirarPeca(destino);
                    Pecas.Remove(p);
                    Peca Dama = new Dama(Tab, p.Cor);
                    Tab.colocarPeca(Dama, destino);
                    Pecas.Add(Dama);
                }
            }





            if (estaEmXeque(adversaria(JogadorAtual)))
            {
                Xeque = true;

            }
            else
            {
                Xeque = false;
            }

            if (testeXequeMate(adversaria(JogadorAtual)))
            {
                Termida = true;
            }
            else
            {
                Turno++;
                mudaJogador();
            }

            
            //#JogadaEspecial En Passant
            if(p is Peao &&(destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
            }



        }

        private void mudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
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
            if (Tab.peca(pos) == null)
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
            if (!Tab.peca(origem).possivelMover(destino))
            {
                throw new TabuleiroException("Posição de destino invalida");
            }
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            Pecas.Add(peca);
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
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
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }

            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);


            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossives();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequeMate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossives();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j] == true)
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }


        private void colocarpecas()
        {
            colocarNovaPeca('a', 1, new Torre(Tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(Tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(Tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(Tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(Tab, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(Tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(Tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(Tab, Cor.Preta, this));



        }
    }
}
