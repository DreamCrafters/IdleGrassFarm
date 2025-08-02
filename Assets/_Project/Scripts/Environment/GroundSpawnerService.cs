using UnityEngine;

public class GroundSpawnerService : MonoBehaviour
{
    [SerializeField] private Transform _groundParent;
    [SerializeField] private GameObject _grassPrefab;
    [SerializeField] private FarmFieldTileService _farmFieldPrefab;
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Vector2 _cellSize;
    [SerializeField] private Vector2Int _farmFieldStartPosition;
    [SerializeField] private int _farmFieldSize;

    public Transform GroundParent => _groundParent;
    public GameObject GrassPrefab => _grassPrefab;
    public FarmFieldTileService FarmFieldPrefab => _farmFieldPrefab;
    public Vector2Int GridSize => _gridSize;
    public Vector2 CellSize => _cellSize;
    public Vector2Int FarmFieldStartPosition => _farmFieldStartPosition;
    public int FarmFieldSize => _farmFieldSize;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                if (x >= _farmFieldStartPosition.x && x < _farmFieldStartPosition.x + _farmFieldSize
                    && y >= _farmFieldStartPosition.y && y < _farmFieldStartPosition.y + _farmFieldSize)
                    continue;

                Vector2 position = new Vector2(x, y) * _cellSize;
                position -= new Vector2(_gridSize.x / 2f * _cellSize.x, _gridSize.y / 2f * _cellSize.y);
                position += new Vector2(_cellSize.x / 2f, _cellSize.y / 2f);
                Vector3 worldPosition = new Vector3(position.x, _groundParent.position.y, position.y);
                
                Gizmos.DrawWireCube(worldPosition + Vector3.up, new Vector3(_cellSize.x, 2f, _cellSize.y));
            }
        }
        
        Gizmos.color = Color.red;
        for (int x = _farmFieldStartPosition.x; x < _farmFieldStartPosition.x + _farmFieldSize; x++)
        {
            for (int y = _farmFieldStartPosition.y; y < _farmFieldStartPosition.y + _farmFieldSize; y++)
            {
                Vector2 position = new Vector2(x, y) * _cellSize;
                position -= new Vector2(_gridSize.x / 2f * _cellSize.x, _gridSize.y / 2f * _cellSize.y);
                position += new Vector2(_cellSize.x / 2f, _cellSize.y / 2f);
                Vector3 worldPosition = new Vector3(position.x, _groundParent.position.y, position.y);
                
                Gizmos.DrawWireCube(worldPosition + Vector3.up, new Vector3(_cellSize.x, 2f, _cellSize.y));
            }
        }
    }
}