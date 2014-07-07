using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense.DataStructures
{
    public struct Vector2Int
    {
        public int X;
        public int Y;

        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2Int(float x, float y)
        {
            X = (int)x;
            Y = (int)y;
        }
    }
}
