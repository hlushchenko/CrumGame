using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Threading.Tasks.Sources;

namespace Crum
{
    public class Node
    {
        public Field State;
        public Node Parent;

        public List<Node> Children
        {
            get { return _children ??= MakeChildren(); }
        }

        private List<Node> _children;
        public int Player;
        public bool Maximizing;

        public Node(Field state, Node parent, int player, bool maximizing)
        {
            State = state;
            Parent = parent;
            Player = player;
            Maximizing = maximizing;
        }

        public int Score() => State.FreePositions() % 2;

        public List<Node> MakeChildren()
        {
            List<Node> nodes = new List<Node>();
            for (int x = 0; x < State.Width; x++)
            {
                for (int y = 0; y < State.Height; y++)
                {
                    if (State.Data[x, y] != 0) continue;
                    if (x != State.Width - 1 && State.Data[x + 1, y] == 0)
                    {
                        Children.Add(MakeChild(x, y, x+1, y)); 
                    }
                    if (y != State.Height - 1 && State.Data[x, y + 1] == 0)
                    {
                        Children.Add(MakeChild(x, y, x, y+1)); 
                    }
                }
            }
            return nodes;
        }

        private Node MakeChild(int x1, int y1, int x2, int y2)
        {
            Field child = State.Copy();
            child.Place(x1, y1, x2, y2, Player);
            return new Node(child, this, Player == 1 ? 2 : 1, !Maximizing);
        }
        
        public static Node Minimax(Node node,int depth, int alpha, int beta)
        {
            if (depth == 0 || node.State.FreePositions() == 0) return node;
            if (node.Maximizing)
            {
                Node valueNode = null;
                foreach (var child in node.Children)
                {
                    var current = Minimax(child, depth - 1, alpha, beta);
                    if (valueNode == null || valueNode.Score() < current.Score())
                    {
                        valueNode = current;
                    }
                    if (valueNode.Score() > beta) break;
                    alpha = Math.Max(alpha, valueNode.Score());
                }
                return valueNode;
            }
            else
            {
                Node valueNode = null;
                foreach (var child in node.Children)
                {
                    var current = Minimax(child, depth - 1, alpha, beta);
                    if (valueNode == null || valueNode.Score() > current.Score())
                    {
                        valueNode = current;
                    }
                    if (valueNode.Score() < alpha) break;
                    beta = Math.Min(beta, valueNode.Score());
                }
                return valueNode;
            }
        }
    }
}