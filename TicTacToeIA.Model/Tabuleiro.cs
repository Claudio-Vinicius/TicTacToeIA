using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeIA.Model
{
    public class Tabuleiro
    {
        public int[] Posicao { get; set; }

        public Tabuleiro()
        {
            this.Posicao = new int[9];
        }

        public Tabuleiro(int[] tabuleiro)
        {
            this.Posicao = tabuleiro;
        }

        public override string ToString()
        {
            var result = "{";

            foreach(int i in Posicao)
            {
                result += i.ToString() + ", ";
            }

            result += "}";

            return result;

        }
    }
}
