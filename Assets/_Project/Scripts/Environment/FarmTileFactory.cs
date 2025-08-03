using UnityEngine;
using VContainer;
using VContainer.Unity;

public class FarmTileFactory
{
    [Inject] private readonly FarmConfig _farmConfig;
    [Inject] private readonly IObjectResolver _resolver;
    [Inject] private readonly FarmFieldTileManager _providerManager;

    public FarmFieldTileModel CreateTile(Vector3 position, Transform parent = null)
    {
        FarmFieldTileModel tileObject = _resolver.Instantiate(_farmConfig.FarmFieldPrefab, position, Quaternion.identity, parent);
        FarmFieldTilePresenter presenter = new(tileObject);
        _resolver.Inject(presenter);
        _providerManager.RegisterProvider(presenter);
        return tileObject;
    }
}
