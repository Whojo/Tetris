using System;
using System.Collections.Generic;

namespace PackingChallenge
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ParserInput.Parser();
            TooEZ filler = new TooEZ(ParserInput.board, ParserInput.pieces);
            filler.FillBoard();
            ToOutput output = new ToOutput(filler.board);
            output.Save();
        }
    }
}