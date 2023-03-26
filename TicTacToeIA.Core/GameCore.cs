using System;
using System.Linq;
using TicTacToeIA.Model;

namespace TicTacToeIA.Core
{
    public class RegrasNegocio
    {
        public RegrasNegocio()
        {

        }

        public bool isValid(Tabuleiro tabuleiro)
        {
            // verifica dupla vitoria
            if (isPlayerOWinner(tabuleiro) && isPlayerXWinner(tabuleiro)) return false;

            // verifica se um jogador jogou mais do que deveria
            var TabuleiroList = tabuleiro.Posicao.ToList<int>();
            var CountX = TabuleiroList.Count(i => i == (int)Pecas.JogadorX);
            var CountO = TabuleiroList.Count(i => i == (int)Pecas.JogadorO);
            if (Math.Abs(CountO - CountX) > 2)
                return false;

            return true;
        }

        public bool isPlayerXWinner(Tabuleiro tabuleiro)
        {
            if ((tabuleiro.Posicao[0] + tabuleiro.Posicao[1] + tabuleiro.Posicao[2] == 3)) return true;
            else if ((tabuleiro.Posicao[3] + tabuleiro.Posicao[4] + tabuleiro.Posicao[5]) == 3) return true;
            else if ((tabuleiro.Posicao[6] + tabuleiro.Posicao[7] + tabuleiro.Posicao[8]) == 3) return true;

            else if ((tabuleiro.Posicao[0] + tabuleiro.Posicao[3] + tabuleiro.Posicao[6]) == 3) return true;
            else if ((tabuleiro.Posicao[1] + tabuleiro.Posicao[4] + tabuleiro.Posicao[7]) == 3) return true;
            else if ((tabuleiro.Posicao[2] + tabuleiro.Posicao[5] + tabuleiro.Posicao[8]) == 3) return true;

            else if ((tabuleiro.Posicao[0] + tabuleiro.Posicao[4] + tabuleiro.Posicao[8]) == 3) return true;
            else if ((tabuleiro.Posicao[2] + tabuleiro.Posicao[4] + tabuleiro.Posicao[6]) == 3) return true;
            else return false;
        }

        public bool isPlayerOWinner(Tabuleiro tabuleiro)
        {
            if ((tabuleiro.Posicao[0] + tabuleiro.Posicao[1] + tabuleiro.Posicao[2] == -3)) return true;
            else if ((tabuleiro.Posicao[3] + tabuleiro.Posicao[4] + tabuleiro.Posicao[5]) == -3) return true;
            else if ((tabuleiro.Posicao[6] + tabuleiro.Posicao[7] + tabuleiro.Posicao[8]) == -3) return true;

            else if ((tabuleiro.Posicao[0] + tabuleiro.Posicao[3] + tabuleiro.Posicao[6]) == -3) return true;
            else if ((tabuleiro.Posicao[1] + tabuleiro.Posicao[4] + tabuleiro.Posicao[7]) == -3) return true;
            else if ((tabuleiro.Posicao[2] + tabuleiro.Posicao[5] + tabuleiro.Posicao[8]) == -3) return true;

            else if ((tabuleiro.Posicao[0] + tabuleiro.Posicao[4] + tabuleiro.Posicao[8]) == -3) return true;
            else if ((tabuleiro.Posicao[2] + tabuleiro.Posicao[4] + tabuleiro.Posicao[6]) == -3) return true;
            else return false;
        }
    }

}
