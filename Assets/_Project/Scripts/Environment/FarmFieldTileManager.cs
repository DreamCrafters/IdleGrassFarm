using System;
using System.Collections.Generic;
using VContainer.Unity;

public class FarmFieldTileManager : ITickable, IDisposable
{
    private readonly List<FarmFieldTilePresenter> _providers = new();

    public void RegisterProvider(FarmFieldTilePresenter provider)
    {
        _providers.Add(provider);
    }

    public void UnregisterProvider(FarmFieldTilePresenter provider)
    {
        _providers.Remove(provider);
    }

    public void Tick()
    {
        for (int i = _providers.Count - 1; i >= 0; i--)
        {
            if (i < _providers.Count)
            {
                _providers[i].Tick();
            }
        }
    }

    public void Dispose()
    {
        _providers.Clear();
    }
}
