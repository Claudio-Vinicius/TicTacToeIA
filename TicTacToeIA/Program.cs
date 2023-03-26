using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToeIA.Core;
using TicTacToeIA.Model;

namespace TicTacToeIA
{
    class Program
    {
        static void Main(string[] args)
        {
            //onsole.WriteLine("Hello World!");

           //Gera um arbitro para validar as jogadas posteriormente
            var Arbitro = new RegrasNegocio();

            //gera uma lista de tabeleiros que se~rá preenchida com todas as combinações possiveis
            var ListaHistorico = new List<Tabuleiro>();

            //convert o enum Pecas para um List<int>
            var PecasCombinacao = new List<int>();
            PecasCombinacao.Add((int)Pecas.Embranco);
            PecasCombinacao.Add((int)Pecas.JogadorO);
            PecasCombinacao.Add((int)Pecas.JogadorX);

            // executa o método que gera todas as combinações possiveis entre as Pecas e as posições do tabuleiro sendo 3 Peças (X, O, EmBranco), e nove posições, gerando 3^9 = 19683 combinações

            Console.WriteLine("Gerando Todas as combinações posiveis\n");
            var TodasCombinacoesPossiveis = Combinar(9, PecasCombinacao);

            Console.WriteLine("Filtrando casos validos\n");
            //valida quais das 19683 combinações são possiveis em um jogo real
            foreach (var Posicao in TodasCombinacoesPossiveis)
            {
                var TabuleiroCandidato = new Tabuleiro(Posicao);
                if (Arbitro.isValid(TabuleiroCandidato))
                {
                    ListaHistorico.Add(TabuleiroCandidato);
                }
            }


            // gerando um caso de teste
            int[] JogadaEspecifica = new int[9];
            JogadaEspecifica[0] = -1;
            JogadaEspecifica[1] = -1;
            JogadaEspecifica[2] = 1;
            JogadaEspecifica[3] = 1;
            JogadaEspecifica[4] = -1;
            JogadaEspecifica[5] = -1;
            JogadaEspecifica[6] = 0;
            JogadaEspecifica[7] = 1;
            JogadaEspecifica[8] = 0;
            var TabuleiroEspecifico = new Tabuleiro(JogadaEspecifica);

            // encontra a combinação especificada
            Console.WriteLine("Busca Uma jogada especifica nos casos gerados\n");
            var TesteA = ListaHistorico.FindAll(x => ArrayCompare(x.Posicao, JogadaEspecifica));
            TesteA.ForEach(x => ImprimirJogada(x.Posicao));

            Console.WriteLine("Busca Uma as proximas jogadas válidas\n");
            // encontra todas as combinações que poderiam decorrer deste caso gerado na JogadaEspecifica
            var TesteB = ListaHistorico.FindAll(x => AcharProximasJogadas(x.Posicao, JogadaEspecifica));
            TesteB.ForEach(x => ImprimirJogada(x.Posicao));
        }

        private static List<int[]> Combinar(int Comprimento, List<int> valores)
        {
          
            var CombinacoesEncontradas = new List<int[]>();

            var ListaContadores = new List<Contador>();

            ListaContadores.Add(new Contador(1, valores.Count));

            for (int x = 1; x < Comprimento; x++)
            {
                var CountValoresCombinar = valores.Count;

                ListaContadores.Add(new Contador((int)Math.Pow((double)CountValoresCombinar, (double)x), valores.Count));
            }

            bool running = true;
            long indice = 1;
            while (running)
            {
                var combinacaoAtual = new int[Comprimento];
                string combinacaoConsole = indice + " = ";
                for (int x = 0; x < Comprimento; x++)
                {
                    var ContadorAtual = ListaContadores[x];
                    var ValorPreencher = valores[ContadorAtual.LoopAtual];
                    combinacaoAtual[x] = ValorPreencher;
                    combinacaoConsole += ValorPreencher + " - ";
                    ContadorAtual.Incrementa();
                }

                //Console.WriteLine(combinacaoConsole);
                //ImprimirJogada(combinacaoAtual);
                CombinacoesEncontradas.Add(combinacaoAtual);

                indice++;

                running = false;
                for (int x = 0; x < Comprimento; x++)
                {
                    if (combinacaoAtual[x] != valores[valores.Count - 1])
                        running = true;
                }

            }

            return CombinacoesEncontradas;
        }

        public static bool ArrayCompare(int[] arrayA, int[] arrayB)
        {
            if (arrayA.Length != arrayB.Length)
            {
                return false;
            }
            for (int i = 0; i < arrayA.Length; i++)
            {
                if (arrayA[i] != arrayB[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool AcharProximasJogadas(int[] arrayA, int[] arrayB)
        {
            var resposta = false;

            if (arrayA.Length != arrayB.Length)
            {
                resposta = false;
            }
            for (int i = 0; i < arrayA.Length; i++)
            {
                if (arrayA[i] != arrayB[i] && arrayB[i] == 0)
                {
                    resposta = true;
                }
                else if (arrayA[i] != arrayB[i])
                {
                    resposta = false;
                    break;
                }
            }
            return resposta;
        }

        public static void ImprimirJogada(int[] Jogada) 
        {
            var Texto = "";
            foreach(var posicao in Jogada)
            {
                switch (posicao)
                {
                    case 0:
                        {
                            Texto += Pecas.Embranco + " -";
                            break;
                        }
                    case 1:
                        {
                            Texto += Pecas.JogadorX + " -";
                            break;
                        }
                    case -1:
                        {
                            Texto += Pecas.JogadorO + " -";
                            break;
                        }
                }
            }

            Console.WriteLine(Texto);
        }
    }

    internal class Contador
    {
        private int ValorMax { get; set; }
        public int ValorAtual { get; set; }

        private int LoopMax { get; set; }

        public int LoopAtual { get; set; }

        public Contador(int ValorMax, int loopMax)
        {
            this.ValorMax = ValorMax;
            this.LoopMax = loopMax;
            this.ValorAtual = 0;
            this.LoopAtual = 0;
        }

        public void Incrementa()
        {
            if (this.ValorAtual + 1 >= this.ValorMax)
            {
                if (this.LoopAtual + 1 >= this.LoopMax)
                {
                    this.LoopAtual = 0;
                    this.ValorAtual = 0;
                }
                else
                {
                    this.LoopAtual++;
                    this.ValorAtual = 0;
                }
            }
            else
            {
                this.ValorAtual++;
            }
        }

    }
}
