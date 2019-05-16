namespace PackingChallenge
{
    public class Piece
    {
        public int width {get; set;}
        public int height {get; set;}
        public int[,] cases {get; set;} 
        public int id {get; set;} 

        public Piece(int width, int height, int id, string[] cases)
        {
            this.width = width;
            this.height = height;
            this.id = id;
            
            this.cases = new int[width, height];

            for (int i = 0; i < cases.Length; i++)
            {
                this.cases[i % width, i / width] = (cases[i] == "1") ? id : - 1;
            }
        }

        public Piece(int width, int height, int id, int[,] cases)
        {
            this.width = width;
            this.height = height;
            this.id = id;
            this.cases = cases;
        }

        public Piece rot90()
        {
            int[,] newCases = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    newCases[height - i - 1, j] = cases[j, i];
                }
            }
            return new Piece(height, width, id, newCases);
        }
    }
}