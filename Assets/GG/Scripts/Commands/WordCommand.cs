using System.Collections.Generic;
using DG.Tweening;
using GG.Base;
using UnityEngine;

namespace GG.Commands
{
    public class WordCommand : ICommand
    {
        private readonly Transform _obstacleTransform;
        private readonly Vector3 _initialPosition;
        private readonly List<Vector3> _positions = new List<Vector3>();
        protected Sequence _sequence;

        public WordCommand(Transform obstacleTransform, Vector3 position)
        {
            _obstacleTransform = obstacleTransform;
            _initialPosition = obstacleTransform.position;
            _positions.Add(position);
        }

        public virtual void Execute()
        {
            _obstacleTransform.DOKill();
            
            _sequence = DOTween.Sequence();
            _sequence.Append(_obstacleTransform.DOMove(_positions[^1], 1f));


        }


        public virtual void Undo()
        {
            _obstacleTransform.DOKill();
            _sequence = DOTween.Sequence();
            if (_positions.Count > 1)
            {
                _positions.RemoveAt(_positions.Count - 1);

                _sequence.Append(_obstacleTransform.DOMove(_positions[^1], 1f));
            }
            else
            {
                _sequence.Append(_obstacleTransform.DOMove(_initialPosition, 1f));
            }
        }
    }
}