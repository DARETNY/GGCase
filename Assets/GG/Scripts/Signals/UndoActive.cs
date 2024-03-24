namespace GG.Signals
{
    public struct UndoActive
    {
        public bool IsActive { get; }

        public UndoActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}