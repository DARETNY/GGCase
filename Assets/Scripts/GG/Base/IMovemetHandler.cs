using UnityEngine;

namespace GG.Base
{
    public interface IMovemetHandler
    {
        bool GetInput();

        Vector2 MousePosition { get; set; }

        RaycastHit2D Cast();
    }
}