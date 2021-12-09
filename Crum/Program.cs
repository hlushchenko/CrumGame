using System;

namespace Crum
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Game a = new Game(3, 3);
            a.Start();*/
            Node a = new Node(new Field(5, 5), null, 1, true);
            var b = a.Children;
        }
    }
}