using UnityEngine;

public class GroundSpawnerModel : MonoBehaviour
{
    [SerializeField] private FarmConfig _farmConfig;
    [SerializeField] private Transform _groundParent;

    public Transform GroundParent => _groundParent;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int x = 0; x < _farmConfig.GridSize.x; x++)
        {
            for (int y = 0; y < _farmConfig.GridSize.y; y++)
            {
                if (x >= _farmConfig.FarmFieldStartPosition.x && x < _farmConfig.FarmFieldStartPosition.x + _farmConfig.FarmFieldSize
                    && y >= _farmConfig.FarmFieldStartPosition.y && y < _farmConfig.FarmFieldStartPosition.y + _farmConfig.FarmFieldSize)
                    continue;

                Vector2 position = new Vector2(x, y) * _farmConfig.CellSize;
                position -= new Vector2(_farmConfig.GridSize.x / 2f * _farmConfig.CellSize.x, _farmConfig.GridSize.y / 2f * _farmConfig.CellSize.y);
                position += new Vector2(_farmConfig.CellSize.x / 2f, _farmConfig.CellSize.y / 2f);
                Vector3 worldPosition = new(position.x, _groundParent.position.y, position.y);

                Gizmos.DrawWireCube(worldPosition + Vector3.up, new Vector3(_farmConfig.CellSize.x, 2f, _farmConfig.CellSize.y));
            }
        }
        
        Gizmos.color = Color.red;
        for (int x = _farmConfig.FarmFieldStartPosition.x; x < _farmConfig.FarmFieldStartPosition.x + _farmConfig.FarmFieldSize; x++)
        {
            for (int y = _farmConfig.FarmFieldStartPosition.y; y < _farmConfig.FarmFieldStartPosition.y + _farmConfig.FarmFieldSize; y++)
            {
                Vector2 position = new Vector2(x, y) * _farmConfig.CellSize;
                position -= new Vector2(_farmConfig.GridSize.x / 2f * _farmConfig.CellSize.x, _farmConfig.GridSize.y / 2f * _farmConfig.CellSize.y);
                position += new Vector2(_farmConfig.CellSize.x / 2f, _farmConfig.CellSize.y / 2f);
                Vector3 worldPosition = new(position.x, _groundParent.position.y, position.y);

                Gizmos.DrawWireCube(worldPosition + Vector3.up, new Vector3(_farmConfig.CellSize.x, 2f, _farmConfig.CellSize.y));
            }
        }
    }
}