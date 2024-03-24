using GG.Base;
using GG.Runtime;
using UnityEngine;

namespace GG.Commands
{
    public class InputHandler : IMovemetHandler
    {
        private readonly Camera _camera;
        public Vector2 MousePosition { get; set; }

        public InputHandler(Camera camera) => _camera = camera;

        public bool GetInput()
        {
            MousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            var hit = Cast();
            if (!hit || hit.collider is null ||
                !hit.collider.TryGetComponent(out WordSample wordSample) ||
                !wordSample.IsInteractable())
                return false;
            return true;
        }

        public RaycastHit2D Cast() => Physics2D.Raycast(MousePosition, Vector2.zero);


    }

}