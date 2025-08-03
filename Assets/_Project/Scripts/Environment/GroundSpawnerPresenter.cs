using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GroundSpawnerPresenter : IStartable
{
    [Inject] private readonly FarmConfig _farmConfig;
    [Inject] private readonly GroundSpawnerModel _groundSpawner;
    [Inject] private readonly FarmTileFactory _tileFactory;

    public void Start()
    {
        if (_groundSpawner == null)
        {
            Debug.LogError("GroundSpawnerData is not assigned.");
            return;
        }

        SpawnGround();
    }

    private void SpawnGround()
    {
        for (int x = 0; x < _farmConfig.GridSize.x; x++)
        {
            for (int y = 0; y < _farmConfig.GridSize.y; y++)
            {
                Vector2 position = new Vector2(x, y) * _farmConfig.CellSize;
                position -= new Vector2(_farmConfig.GridSize.x / 2f * _farmConfig.CellSize.x, _farmConfig.GridSize.y / 2f * _farmConfig.CellSize.y);
                position += new Vector2(_farmConfig.CellSize.x / 2f, _farmConfig.CellSize.y / 2f);
                Vector3 worldPosition = new(position.x, _groundSpawner.GroundParent.position.y, position.y);

                if (x >= _farmConfig.FarmFieldStartPosition.x && x < _farmConfig.FarmFieldStartPosition.x + _farmConfig.FarmFieldSize
                    && y >= _farmConfig.FarmFieldStartPosition.y && y < _farmConfig.FarmFieldStartPosition.y + _farmConfig.FarmFieldSize)
                {
                    FarmFieldTileModel farmFieldInstance = _tileFactory.CreateTile(worldPosition, _groundSpawner.GroundParent);
                    farmFieldInstance.name = $"FarmField_{x}_{y}";
                }
                else
                {
                    GameObject grassInstance = Object.Instantiate(_farmConfig.GrassPrefab, worldPosition, Quaternion.identity, _groundSpawner.GroundParent);
                    grassInstance.name = $"Grass_{x}_{y}";
                }
            }
        }
    }
}
