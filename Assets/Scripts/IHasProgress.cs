using System;

namespace KitchenChaos
{
    public class OnProgressChagnedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    public interface IHasProgress
    {
        public event EventHandler<OnProgressChagnedEventArgs> onProgressChanged;

    }
}