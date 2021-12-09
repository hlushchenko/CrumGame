using System;

namespace Crum
{
    public class Game
    {
        public Field Field;
        public bool GameOver = false;

        public Game(int height, int width)
        {
            Field = new Field(width, height);
        }

        public void Start()
        {
            while (Field.FreePositions() != 0)
            {
                Node player1 = new Node(Field.Copy(), null, 1, true);
                player1 = Node.Minimax(player1, 3, Int32.MinValue, Int32.MaxValue);
                while (player1.Parent?.Parent != null)
                {
                    player1 = player1.Parent;
                }
                Field.Data = player1.State.Data;
                Console.WriteLine(Field);
                Node player2 = new Node(Field.Copy(), null, 2, true);
                player2 = Node.Minimax(player2, 2, Int32.MinValue, Int32.MaxValue);
                while (player2.Parent?.Parent != null)
                {
                    player2 = player2.Parent;
                }
                Field.Data = player2.State.Data;
                Console.WriteLine(Field);
            }
        }

    }
}