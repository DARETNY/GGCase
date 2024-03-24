using System;
using System.Collections.Generic;

namespace GG
{
    [Serializable]
    public class Level
    {
        public string title;
        public Tiles[] tiles;
    }

    [Serializable]
    public class Tiles
    {
        public int id;
        public Position position;
        public string character;
        public List<int> children;
    }
    
    [Serializable]
    public class Position
    {
        public int x;
        public int y;
        public double z;
    }
}