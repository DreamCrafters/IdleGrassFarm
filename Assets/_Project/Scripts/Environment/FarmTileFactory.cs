using UnityEngine;
using VContainer;
using VContainer.Unity;

public class FarmTileFactory
{
    private readonly IObjectResolver _resolver;
    private readonly IContainerBuilder _builder;
    private readonly GameObject _tilePrefab;

    public FarmTileFactory(IContainerBuilder builder, IObjectResolver resolver, GameObject tilePrefab)
    {
        _resolver = resolver;
        _builder = builder;
        _tilePrefab = tilePrefab;
    }

    public GameObject CreateTile(Vector3 position, Transform parent = null)
    {
        GameObject tileGO = _resolver.Instantiate(_tilePrefab, position, Quaternion.identity, parent);
        FarmFieldTileService tileService = tileGO.GetComponent<FarmFieldTileService>();

        // Можно добавить презентер в какой-то менеджер тикабельных объектов
        // Или регистрировать его отдельно

        return tileGO;
    }
}
