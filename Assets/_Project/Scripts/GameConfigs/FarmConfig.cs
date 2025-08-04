using UnityEngine;

[CreateAssetMenu(fileName = "FarmConfig", menuName = "Configs/FarmConfig")]
public class FarmConfig : ScriptableObject
{
    [SerializeField] private float _timeToNextStage = 1f;
    [SerializeField] private GameObject _grassPrefab;
    [SerializeField] private FarmFieldTileModel _farmFieldPrefab;
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Vector2 _cellSize;
    [SerializeField] private Vector2Int _farmFieldStartPosition;
    [SerializeField] private int _farmFieldSize;
    [SerializeField] private int _maxFarmFieldSize;

    public float TimeToNextStage => _timeToNextStage;
    public GameObject GrassPrefab => _grassPrefab;
    public FarmFieldTileModel FarmFieldPrefab => _farmFieldPrefab;
    public Vector2Int GridSize => _gridSize;
    public Vector2 CellSize => _cellSize;
    public Vector2Int FarmFieldStartPosition => _farmFieldStartPosition;
    public int FarmFieldSize => _farmFieldSize;
    public int MaxFarmFieldSize => _maxFarmFieldSize;
}
