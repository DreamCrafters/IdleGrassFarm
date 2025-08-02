using UnityEngine;
using VContainer;
using VContainer.Unity;

public class FarmTileFactory
{
    [Inject] private readonly GroundSpawnerService _groundSpawnerData;
    private readonly IObjectResolver _resolver;
    private readonly FarmFieldTileManager _providerManager;

    public FarmTileFactory(FarmFieldTileManager providerManager, IObjectResolver resolver)
    {
        _providerManager = providerManager;
        _resolver = resolver;
    }

    public FarmFieldTileService CreateTile(Vector3 position, Transform parent = null)
    {
        FarmFieldTileService tileObject = _resolver.Instantiate(_groundSpawnerData.FarmFieldPrefab, position, Quaternion.identity, parent);
        FarmFieldTilePresenter presenter = new(tileObject);
        _resolver.Inject(presenter);
        _providerManager.RegisterProvider(presenter);
        return tileObject;
    }
}
