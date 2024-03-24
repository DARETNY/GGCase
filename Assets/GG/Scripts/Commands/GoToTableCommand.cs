using System.Collections.Generic;
using DG.Tweening;
using GG.Enum;
using GG.Runtime;
using UnityEngine;

namespace GG.Commands
{
    public class GoToTableCommand : WordCommand
    {
        private readonly WordSample wordSample;
        private readonly List<WordSample> allSamples;
        private List<WordSample> affectedSamples;

        public GoToTableCommand(WordSample wordSample,
                                ref List<WordSample> allSamples,
                                Transform obstacleTransform,
                                Vector3 position) :
                base(obstacleTransform, position)
        {
            this.wordSample = wordSample;
            this.allSamples = allSamples;
            affectedSamples = new List<WordSample>();
        }

        public override void Execute()
        {

            base.Execute();
            _sequence.OnStart(() => { wordSample.SetState(WordState.Moving); });
            _sequence.OnComplete(() => { wordSample.SetState(WordState.Unmoveable); });

            int id = wordSample.GetID();
            foreach (var sample in allSamples)
            {
                if (sample.dataHolder.Children.Contains(id))
                {
                    sample.dataHolder.Children.Remove(id);

                    
                    sample.Refresh();

                    affectedSamples.Add(sample);
                }
            }
        }

        public override void Undo()
        {
            base.Undo();
            _sequence.OnStart(() => { wordSample.SetState(WordState.Moving); });
            _sequence.OnComplete(() =>
            {   
                wordSample.SetState(WordState.Moveable);
                int id = wordSample.GetID();
                foreach (var sample in affectedSamples)
                {
                    sample.dataHolder.Children.Add(id);
                    sample.Refresh();
                }

                affectedSamples.Clear();
            });

            // int id = wordSample.GetID();
            // foreach (var sample in affectedSamples)
            // {
            //     sample.dataHolder.Children.Add(id);
            //     sample.Refresh();
            // }
            //
            // affectedSamples.Clear();

        }
    }
}