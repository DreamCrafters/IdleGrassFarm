using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GroundSpawnerPresenter : IStartable
{
    private List<FarmTile> _farmTiles = new();

    [Inject] private readonly FarmConfig _farmConfig;
    [Inject] private readonly GroundSpawnerModel _groundSpawner;
    [Inject] private readonly FarmTileFactory _tileFactory;

    private int _farmFieldSize;

    public void Start()
    {
        int totalTiles = _farmConfig.GridSize.x * _farmConfig.GridSize.y;
        _farmTiles = new(totalTiles);

        for (int i = 0; i < totalTiles; i++)
        {
            _farmTiles.Add(new FarmTile());
        }

        if (_groundSpawner == null)
        {
            Debug.LogError("GroundSpawnerData is not assigned.");
            return;
        }

        _farmFieldSize = _farmConfig.FarmFieldSize;
        SpawnGround();
    }

    public bool TryIncreaseFarmFieldSize()
    {
        if (_farmFieldSize >= _farmConfig.MaxFarmFieldSize)
        {
            return false;
        }

        _farmFieldSize++;
        SpawnGround();
        return true;
    }

    private void SpawnGround()
    {
        for (int x = 0; x < _farmConfig.GridSize.x; x++)
        {
            for (int y = 0; y < _farmConfig.GridSize.y; y++)
            {
                int index = x * _farmConfig.GridSize.y + y;
                Vector2 position = new Vector2(x, y) * _farmConfig.CellSize;
                position -= new Vector2(_farmConfig.GridSize.x / 2f * _farmConfig.CellSize.x, _farmConfig.GridSize.y / 2f * _farmConfig.CellSize.y);
                position += new Vector2(_farmConfig.CellSize.x / 2f, _farmConfig.CellSize.y / 2f);
                Vector3 worldPosition = new(position.x, _groundSpawner.GroundParent.position.y, position.y);

                if (x >= _farmConfig.FarmFieldStartPosition.x && x < _farmConfig.FarmFieldStartPosition.x + _farmFieldSize
                    && y >= _farmConfig.FarmFieldStartPosition.y && y < _farmConfig.FarmFieldStartPosition.y + _farmFieldSize)
                {
                    if (_farmTiles[index].IsFarmField && _farmTiles[index].TileObject != null)
                    {
                        continue;
                    }
                    else if (_farmTiles[index].TileObject != null)
                    {
                        Object.Destroy(_farmTiles[index].TileObject);
                    }

                    FarmFieldTileModel farmFieldInstance = _tileFactory.CreateTile(worldPosition, _groundSpawner.GroundParent);
                    farmFieldInstance.name = $"FarmField_{x}_{y}";
                    _farmTiles[index] = new()
                    {
                        IsFarmField = true,
                        TileObject = farmFieldInstance.gameObject
                    };
                }
                else
                {
                    if (_farmTiles[index].IsFarmField == false && _farmTiles[index].TileObject != null)
                    {
                        continue;
                    }
                    else if (_farmTiles[index].TileObject != null)
                    {
                        Object.Destroy(_farmTiles[index].TileObject);
                    }

                    GameObject grassInstance = Object.Instantiate(_farmConfig.GrassPrefab, worldPosition, Quaternion.identity, _groundSpawner.GroundParent);
                    grassInstance.name = $"Grass_{x}_{y}";
                    _farmTiles[index] = new()
                    {
                        IsFarmField = false,
                        TileObject = grassInstance
                    };
                }
            }
        }
    }

    private class FarmTile
    {
        public bool IsFarmField;
        public GameObject TileObject;
    }
}
