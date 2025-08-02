using UnityEngine;

public class FarmFieldTileService : MonoBehaviour
{
    [SerializeField] private GameObject _stage1;
    [SerializeField] private GameObject _stage2;
    [SerializeField] private GameObject _stage3;

    private TileStage _currentStage;

    public TileStage CurrentStage => _currentStage;

    private void Awake()
    {
        SetStage(TileStage.Stage1);
    }
    
    public void SetStage(TileStage tileStage)
    {
        _currentStage = tileStage;
        _stage1.SetActive(tileStage == TileStage.Stage1);
        _stage2.SetActive(tileStage == TileStage.Stage2);
        _stage3.SetActive(tileStage == TileStage.Stage3);
    }

    public enum TileStage
    {
        Stage1 = 1,
        Stage2 = 2,
        Stage3 = 3
    }
}
