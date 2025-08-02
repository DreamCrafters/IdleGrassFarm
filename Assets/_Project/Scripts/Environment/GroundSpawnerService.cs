using UnityEngine;

public class GroundSpawnerService : MonoBehaviour
{
    [SerializeField] private Transform _groundParent;
    [SerializeField] private GameObject _grassPrefab;
    [SerializeField] private GameObject _farmFieldPrefab;
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Vector2 _cellSize;
    [SerializeField] private Vector2Int _farmFieldStartPosition;
    [SerializeField] private int _farmFieldSize;

    public Transform GroundParent => _groundParent;
    public GameObject GrassPrefab => _grassPrefab;
    public GameObject FarmFieldPrefab => _farmFieldPrefab;
    public Vector2Int GridSize => _gridSize;
    public Vector2 CellSize => _cellSize;
    public Vector2Int FarmFieldStartPosition => _farmFieldStartPosition;
    public int FarmFieldSize => _farmFieldSize;
}