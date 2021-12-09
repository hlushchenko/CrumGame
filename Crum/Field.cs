using System;
using System.Data.Common;
using System.Text;

namespace Crum
{
    public class Field
    {
        public int Width { get;}
        public int Height { get;}

        public int[,] Data;

        public Field(int width, int height)
        {
            Width = width;
            Height = height;
            Data = new int[width, height];
        }

        public bool Place(int x1, int y1, int x2, int y2, int player)
        {
            if (Data[x1, y1] != 0 || Data[x2, y2] != 0) return false;
            if (x1 < 0 || y1 < 0 || x1 >= Width || y1 >= Width) return false;
            if (x2 < 0 || y2 < 0 || x2 >= Width || y2 >= Width) return false;
            int deltaX = Math.Abs(x1 - x2);
            int deltaY = Math.Abs(y1 - y2);
            if (deltaX > 1) return false;
            if (deltaY > 1) return false;
            if (deltaX == deltaY) return false;
            Data[x1, y1] = player;
            Data[x2, y2] = player;
            return true;
        }

        public int FreePositions()
        {
            int result = 0;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (Data[x, y] != 0) continue;
                    if (x != 0 && Data[x - 1, y] == 0) result++;
                    if (x != Width - 1 && Data[x + 1, y] == 0) result++;
                    if (y != 0 && Data[x, y-1] == 0) result++;
                    if (y != Height - 1 && Data[x, y+1] == 0) result++;
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    result.Append(Data[j, i] switch { 
                        1 => "▓▓",
                        2 => "░░",
                        _ => ".."
                    });
                }

                result.Append('\n');
            }
            return result.ToString();
        }

        public Field Copy()
        {
            Field result = new Field(Width, Height);
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    result.Data[i, j] = Data[i, j];
                }
            }
            return result;
        }
    }
}