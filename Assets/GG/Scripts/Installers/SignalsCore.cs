using GG.Signals;
using Zenject;

namespace GG.Core
{
    public class SignalsCore : Installer<SignalsCore>
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<OnLevelLoadedSignal>();
            Container.DeclareSignal<OnStringComplate>();
            Container.DeclareSignal<OnTileBlocked>();
            Container.DeclareSignal<ConfirmButtonClicked>();
            Container.DeclareSignal<UndoButtonClicked>();
            Container.DeclareSignal<UndoAllButton>();
            Container.DeclareSignal<CallLevel>();
            Container.DeclareSignal<ScoreChanged>();
            Container.DeclareSignal<LevelEndClick>();
            Container.DeclareSignal<SaveScore>();
            Container.DeclareSignal<InıtLevels>();
            Container.DeclareSignal<OnClearBoard>();
            Container.DeclareSignal<OnConfirmActivate>();
            Container.DeclareSignal<UndoActive>();
        }

    }
}