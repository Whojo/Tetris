using System;
using System.Collections.Generic;
using System.IO;

namespace PackingChallenge
{
    public class ToOutput
    {
        private Board board;

        public ToOutput(Board board)
        {
            this.board = board;
        }

        /// <summary>
        /// Save the filled board to the output file
        /// </summary>
        public void Save(string name = "output.out")
        {
            try
            {
                using (StreamWriter file = new StreamWriter(name))
                {
                    for (int i = 0; i < board.height; i++)
                    {
                        string text = "";
                        for (int j = 0; j < board.width; j++)
                        {
                            text += board.cases[j, i] + " ";
                        }
                        file.WriteLine(text);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be written:");
                Console.WriteLine(e.Message);
            }
        }
    }

}
