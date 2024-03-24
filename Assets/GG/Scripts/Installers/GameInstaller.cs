using GG.Base;
using GG.Commands;
using GG.Commands.SignalCommand;
using GG.Runtime;
using UnityEngine;
using Zenject;

namespace GG.Core
{
    public class GameInstaller : MonoInstaller
    {
        public Board board;
        public TextAsset grammar;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            SignalsCore.Install(Container);

            Container.BindInterfacesAndSelfTo<LevelCommand>().AsCached();
            Container.Bind<Board>().FromInstance(board).AsCached();
            Container.Bind<LoadCommand>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveCommand>().AsCached();
            Container.Bind<CmdHandler>().AsCached();
            Container.Bind<Score>().FromNew().AsCached();
            Container.Bind<BillBoard>().FromNew().AsCached();
            Container.Bind<InputHandler>().AsCached().WithArguments(Camera.main);
            Container.BindFactory<Vector3, char,Transform, GameObject, Board.Factory>()
                    .FromFactory<CustomBoardFactory>();
            Container.BindInterfacesAndSelfTo<StringChecker>().AsCached().WithArguments(grammar);
            Container.BindInterfacesTo<TileBlock>().AsCached();
        }
    }
}