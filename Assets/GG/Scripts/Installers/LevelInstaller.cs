using System.Collections.Generic;
using System.Linq;
using GG.UI;
using UnityEngine;
using Zenject;

namespace GG.Core
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField]
        private LevelLabel levelLabelPrefab;

        [SerializeField]
        private List<TextAsset> levelTexts = new List<TextAsset>();

        public override void InstallBindings()
        {
            List<Level> levels = levelTexts.Select(textAsset => JsonUtility.FromJson<Level>(textAsset.text)).ToList();
            Container.Bind<List<Level>>().FromInstance(levels).AsSingle();
            Container.Bind<LevelLabel>().FromInstance(levelLabelPrefab).AsSingle();
        }
    }
}