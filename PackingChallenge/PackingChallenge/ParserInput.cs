using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PackingChallenge
{
    public class ParserInput
    {
        public static List<Piece[]> pieces {get; set;}
        public static Board board {get; set;}
        
        /// <summary>
        /// Parse Input file and create the board
        /// </summary>
        public static void Parser()
        {
            try
            {
                using (StreamReader file = new StreamReader("input.in"))
                {
                    //Extract Main info
                    string line = file.ReadLine();
                    string[] list = line.Split(' ');

                    int width = Int32.Parse(list[1]);
                    int height = Int32.Parse(list[2]);
                    
                    board = new Board(width, height);


                    //Extract info about pieces
                    int id = 0;
                    pieces = new List<Piece[]>();
                    while ((line = file.ReadLine()) != null)
                    {
                        list = line.Split(' ');
                        int numberShapes = Int32.Parse(list[0]);

                        int pieceWidth = Int32.Parse(list[1]);
                        int pieceHeight = Int32.Parse(list[2]);

                        string[] cases = list.Skip(3).Take(list.Length - 3).ToArray();

                        for (int i = 0; i < numberShapes; i++)
                        {
                            Piece piece = new Piece(pieceWidth, pieceHeight, id, cases);
                            Piece[] rotations = new Piece[4];
                            for (int j = 0; j < 4; j++)
                            {
                                rotations[j] = piece;
                                piece = piece.rot90();
                            }
                            
                            pieces.Add(rotations);
                            id+= 1;
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }
    }
}