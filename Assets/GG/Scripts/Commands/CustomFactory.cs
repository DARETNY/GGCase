using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace GG.Commands
{
    public class CustomBoardFactory : IFactory<Vector3, char, Transform,GameObject >
    {
        public GameObject Create(Vector3 position, char character,Transform parent)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefab/Word");
            if (prefab == null)
                return null;
            var instance = GameObject.Instantiate(prefab);
            instance.transform.localPosition = position;
            instance.transform.SetParent(parent);
            instance.transform
                    .DOLocalMove(position, 1f).From(Vector3.up * 100).SetEase(Ease.Linear)
                    .OnComplete(() => instance.transform.DOSpiral(1, new Vector3(0, 360), SpiralMode.ExpandThenContract, 1))
                    .SetAutoKill(true);

            instance.GetComponentInChildren<TextMeshPro>().text = character.ToString();
            return instance;
        }
    }
}