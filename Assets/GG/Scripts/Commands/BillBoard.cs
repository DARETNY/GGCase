using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GG.Commands
{
    public class BillBoard
    {
        public void WriteToBillBoard(string message, Transform panel)
        {
            if (string.IsNullOrEmpty(message))
                return;

            GameObject newTextObject = new GameObject("ValidText");
            newTextObject.transform.SetParent(panel, false);

            var text = newTextObject.AddComponent<TextMeshProUGUI>();
            text.fontSize = 50;
            text.alignment = TextAlignmentOptions.Center;
            text.enableWordWrapping = false;
            text.DOFaceColor(Color.yellow, 1f).SetEase(Ease.OutBack);
            text.DOText(message.ToUpper(), 1f)
                    .OnStart(() => text.rectTransform.DOScale(Vector3.one, 2))
                    .SetEase(Ease.OutBack);


        }

        public void ResetBillBoard(Transform panel)
        {
            foreach (Transform child in panel)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

    }
}