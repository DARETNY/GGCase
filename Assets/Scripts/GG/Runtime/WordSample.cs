using System.Collections.Generic;
using System.Linq;
using GG.Data;
using GG.Enum;
using TMPro;
using UnityEngine;

namespace GG.Runtime
{
   
    public class WordSample : MonoBehaviour
    {
        public DataHolder dataHolder;

        [SerializeField]
        private SpriteRenderer _graphics;

        [SerializeField]
        private List<State> _states = new List<State>();

        [SerializeField] private BoxCollider2D _collider2D;
        [SerializeField] private TextMeshPro _textMeshPro;

        public void SetID(int i) => dataHolder.Id = i;

        public int GetID() => dataHolder.Id;

        public bool IsInteractable() => dataHolder.State == WordState.Moveable;

        public void SetEachChildren(List<int> numbers) => dataHolder.Children = numbers;

        public void SetLetter(char i) => dataHolder.CharValue = i;

        public void SetState(WordState wordState)
        {
            foreach (var state in _states.Where(state => state.wordState == wordState))
            {

                _graphics.color = state.color;
                _graphics.sortingOrder = state.order;
                _collider2D.enabled = state.colEnable;
                _textMeshPro.sortingOrder = state.order;
                break;
            }

            dataHolder.State = wordState;
        }

        public void Refresh()
        {
            SetState(dataHolder.Children.Count == 0 ? WordState.Moveable : WordState.Unmoveable);
        }

        [System.Serializable]
        private class State
        {
            public WordState wordState;
            public Color color;
            public int order;
            public bool colEnable;
        }
    }


}