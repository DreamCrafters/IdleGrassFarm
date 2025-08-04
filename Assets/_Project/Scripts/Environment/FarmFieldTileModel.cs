using UnityEngine;

public class FarmFieldTileModel : MonoBehaviour
{
    [SerializeField] private Transform _cornsSpawnPoint;
    [SerializeField] private GameObject _stage1;
    [SerializeField] private GameObject _stage2;
    [SerializeField] private GameObject _stage3;
    [SerializeField] private ParticleSystem _harvestEffect;

    private TileStage _currentStage;

    public TileStage CurrentStage => _currentStage;
    public Transform CornsSpawnPoint => _cornsSpawnPoint;

    private void Awake()
    {
        SetStage(TileStage.Stage3);
    }

    public void SetStage(TileStage tileStage)
    {
        _currentStage = tileStage;
        _stage1.SetActive(tileStage == TileStage.Stage1);
        _stage2.SetActive(tileStage == TileStage.Stage2);
        _stage3.SetActive(tileStage == TileStage.Stage3);

        if (tileStage == TileStage.Stage0)
        {
            _harvestEffect.Play();
        }
    }
}
