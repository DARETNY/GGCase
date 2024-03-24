using System;
using GG.Signals;
using UnityEngine;
using Zenject;

namespace GG.Commands.SignalCommand
{
    public class TileBlock : IInitializable,IDisposable
    {
        private SignalBus _signalBus;

        public TileBlock(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<OnTileBlocked>(OnTileBlocked);
        }

        private void OnTileBlocked()
        {
            Debug.Log("Tile is blocked");
        }
        public void Dispose()
        {
            _signalBus.Unsubscribe<OnTileBlocked>(OnTileBlocked);
        }
    }
}