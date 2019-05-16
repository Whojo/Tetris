using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PackingChallenge
{
    public class Board
    {
        public int width {get; set;}
        public int height {get; set;}
        public int[,] cases {get; set;} 

        public Board(int width, int height)
        {
            this.width = width;
            this.height = height;
            
            cases = new int[width,height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    cases[i, j] = -1;
                }
            }
        }

        private Board(int width, int height, int[,] cases)
        {
            this.width = width;
            this.height = height;
            this.cases = cases;
        }
        
        /// <summary>
        /// Create a Copy of the cases 
        /// </summary>
        /// <param name="newTab"></param>
        /// <param name="oldTab"></param>
        /// <returns></returns>
        public Board Copy()
        {
            int width = cases.GetLength(0);
            int height = cases.GetLength(1);
            int[,] copy = new int[width, height];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    copy[j, i] = cases[j, i];
                }
            }

            return new Board(width, height, copy);
        }

        /// <summary>
        /// Try to insert the piece in the board at coordinates (x, y) which is the up-right corner 
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool AddPiece(Piece piece, int x, int y)
        {
            // Check if the space is free
            int i = 0;
            int j = piece.width;
            for (; i < piece.height && y + i < height && j >= piece.width; i++)
            {
                for (j = 0; j < piece.width && x + j < width && (cases[x + j, y + i] == -1 || piece.cases[j, i] == -1); j++) ;
            }

            // Add piece
            if (i >= piece.height && j >= piece.width)
            {
                for (i = 0; i < piece.height; i++)
                {
                    for (j = 0; j < piece.width; j++)
                    {
                        if (piece.cases[j, i] != -1)
                            cases[x + j, y + i] = piece.id;
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// This function is very important, it is the one that will evaluate how to fill the board, line per line
        ///
        /// This function is trying to maximize the number of cases fill in the line and minimize the number of cases 
        /// fill in the following line
        /// </summary>
        /// <param name="line">Line to evaluate</param>
        /// <returns>The score</returns>
        public int GetScore(int line, int shortest, int longest, TooEZ.Swich swich)
        {
            int casesFill1 = 0;
            int casesFill2 = 0;
            for (int i = 0; i < longest; i++)
            {
                (int a, int b) = swich(i, line); 
                casesFill1 += (cases[a, b] == -1) ? 0 : 1;
            }

            if (line < shortest - 1)
            {
                for (int i = 0; i < longest; i++)
                {
                    (int a, int b) = swich(i, line + 1); 
                    casesFill2 += (cases[a, b] == -1) ? 0 : 1;
                }
            }

            return 3 * casesFill1 + casesFill2;
        }
    }
}