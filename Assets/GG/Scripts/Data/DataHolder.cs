using System;
using System.Collections.Generic;
using GG.Enum;

namespace GG.Data
{
    [Serializable]
    public struct DataHolder
    {
        public int Id;
        public char CharValue;
        public List<int> Children;
        public WordState State;
    }
}