using System;
using System.Collections.Generic;

namespace PackingChallenge
{
    public class TooEZ
    {
        public Board board;
        private List<Piece[]> pieces;

        public TooEZ(Board board, List<Piece[]> pieces)
        {
            this.board = board;
            this.pieces = pieces;
        }
        
        
        /// <summary>
        /// Just Copying the list content
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<Piece[]> CopyList(List<Piece[]> list)
        {
            List<Piece[]> newList = new List<Piece[]>();

            for (int i = 0; i < list.Count; i++)
            {
                newList.Add(list[i]);
            }

            return newList;
        }

        /// <summary>
        /// Test all the different possibilities to fill the line
        /// </summary>
        /// <param name="line"> The line you had to complete </param>
        /// <param name="x"> Where the piece will be added </param>
        /// <param name="board"> version of the board being filled</param>
        /// <param name="lines">  All possible disposition </param>
        /// <returns> All possible disposition of a define line and their associated score </returns>
        private void RecGenerateLines(int line, int longest, int shortest, int x, Board board, List<Piece[]> pieces, Swich swich, ref List<(Board, int, List<Piece[]>)> lines, List<Piece[]> dlPieces)
        {
            if (x >= longest)
                lines.Add((board, board.GetScore(line, shortest, longest, swich), dlPieces));
            else
            {
                (int a, int b) = swich(x, line);
                for (int i = 0; i < pieces.Count; i++)
                {
                    for (int j = 0; j < 4; j++) // 4 = number of possible rotations
                    {
                        // Modify Board
                        Board copyB = board.Copy();
                        if (copyB.AddPiece(pieces[i][j], a, b)) // Don't generate lines from here if the piece has not been added
                        {
                            // Delete Piece
                            List<Piece[]> copyP = CopyList(pieces);
                            List<Piece[]> copyDP = CopyList(dlPieces);
                            copyP.RemoveAt(i);
                            copyDP.Add(pieces[i]);

                            RecGenerateLines(line, longest, shortest, x + 1, copyB, copyP, swich, ref lines, copyDP);
                        }
                    }
                }

                // If there is no changement needed (case != -1) or that all changement are not worth it
                RecGenerateLines(line, longest, shortest, x + 1, board.Copy(), pieces, swich, ref lines, dlPieces);
            }
        }

        /// <summary>
        /// Generate and compare all the lines and find the best the one with the highest score
        /// </summary>
        /// <param name="lines">All possible disposition of a define line</param>
        /// <returns>The best one</returns>
        private Board GetBestLine(int line, int shortest, int longest, Swich swich)
        {
            // Generate lines
            List<(Board, int, List<Piece[]>)> lines = new List<(Board, int, List<Piece[]>)>();
            RecGenerateLines(line,longest, shortest, 0, board.Copy(), pieces, swich, ref lines, new List<Piece[]>());
            
            // Get the best line
            int best = 0;
            for (int i = 1; i < lines.Count; i++)
            {
                if (lines[best].Item2 < lines[i].Item2)
                    best = i;
            }
            
            // Delete piece that ae used
            for (int i = 0; i < lines[best].Item3.Count; i++)
            {
                pieces.Remove(lines[best].Item3[i]);
            }
            
            return lines[best].Item1;
        }
        
        /// <summary>
        /// This function is the key of this algorithm
        ///
        /// It generates board by generating the best possible line from top to bottom
        /// </summary>
        public void FillBoard()
        {
            // Choose longest size
            int longest, shortest;
            Swich swich;
            if (board.height > board.width)
                (longest, shortest, swich) = (board.height, board.width, DoSwich);
            else
                (longest, shortest, swich) = (board.width, board.height, DontSwich);
            
            // Create the board
            for (int i = 0; i < shortest; i++)
            {
                board = GetBestLine(i, shortest, longest, swich);
            }
        }

        /*
         * I orignaly made my loop (int FillBoard) on the height but I found that it would be smarter to do it on the longest size
         * So that's what swich is doing
         */
        public delegate (int, int) Swich(int a, int b);

        private (int, int) DoSwich(int a, int b)
        {
            return (b, a);
        }

        private (int, int) DontSwich(int a, int b)
        {
            return (a, b);
        }
    }
}