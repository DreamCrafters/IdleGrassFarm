using UnityEngine;
using VContainer.Unity;

public class GroundSpawnerPresenter : IStartable
{
    private readonly GroundSpawnerService _groundSpawner;
    private readonly FarmTileFactory _tileFactory;

    public GroundSpawnerPresenter(GroundSpawnerService groundSpawner, FarmTileFactory tileFactory)
    {
        _groundSpawner = groundSpawner;
        _tileFactory = tileFactory;
    }

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
        for (int x = 0; x < _groundSpawner.GridSize.x; x++)
        {
            for (int y = 0; y < _groundSpawner.GridSize.y; y++)
            {
                Vector2 position = new Vector2(x, y) * _groundSpawner.CellSize;
                position -= new Vector2(_groundSpawner.GridSize.x / 2f * _groundSpawner.CellSize.x, _groundSpawner.GridSize.y / 2f * _groundSpawner.CellSize.y);
                position += new Vector2(_groundSpawner.CellSize.x / 2f, _groundSpawner.CellSize.y / 2f);
                Vector3 worldPosition = new(position.x, _groundSpawner.GroundParent.position.y, position.y);

                if (x >= _groundSpawner.FarmFieldStartPosition.x && x < _groundSpawner.FarmFieldStartPosition.x + _groundSpawner.FarmFieldSize
                    && y >= _groundSpawner.FarmFieldStartPosition.y && y < _groundSpawner.FarmFieldStartPosition.y + _groundSpawner.FarmFieldSize)
                {
                    FarmFieldTileService farmFieldInstance = _tileFactory.CreateTile(worldPosition, _groundSpawner.GroundParent);
                    farmFieldInstance.name = $"FarmField_{x}_{y}";
                }
                else
                {
                    GameObject grassInstance = Object.Instantiate(_groundSpawner.GrassPrefab, worldPosition, Quaternion.identity, _groundSpawner.GroundParent);
                    grassInstance.name = $"Grass_{x}_{y}";
                }
            }
        }
    }
}
