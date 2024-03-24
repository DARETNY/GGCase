using System;
using System.Collections.Generic;
using UnityEngine;

namespace GG.Data
{
    public class BoardData
    {
        private readonly Level level;
        public BoardData(Level level)
        {
            this.level = level;
        }

        public string GetTitle()
        {
            return level.title.ToUpper();
        }

        public List<int> GetAllId()
        {
            List<int> ids = new List<int>();
            foreach (var level1JsTile in level.tiles)
            {
                ids.Add(level1JsTile.id);
            }
            return ids;
        }
        public List<Vector3> GetAllPositions()
        {
            List<Vector3> positions = new List<Vector3>();
            foreach (var level1JsTile in level.tiles)
            {
                positions.Add(new Vector3(level1JsTile.position.x, level1JsTile.position.y, (float)level1JsTile.position.z));
            }
            return positions;
        }
        public List<char> GetAllCharacter()
        {
            List<char> characters = new List<char>();
            foreach (var level1JsTile in level.tiles)
            {
                characters.Add(Convert.ToChar((level1JsTile.character)));
            }
            return characters;
        }

        public int[] GetAllChildren()
        {
            List<int> children = new List<int>();
            foreach (var level1JsTile in level.tiles)
            {
                children.AddRange(level1JsTile.children);
            }
            return children.ToArray();
        }

    }
}