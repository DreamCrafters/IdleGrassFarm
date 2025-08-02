using UnityEngine;
using VContainer;

public class FarmFieldTilePresenter
{
    [Inject] private readonly FarmSettings _farmSettings;
    private readonly FarmFieldTileService _tileService;
    private float _tickTimer;

    public FarmFieldTilePresenter(FarmFieldTileService tileService)
    {
        _tileService = tileService;
    }

    public void Tick()
    {
        if (_tileService == null || _tileService.CurrentStage == TileStage.Stage3) return;
        
        _tickTimer += Time.deltaTime;

        if (_tickTimer >= _farmSettings.TimeToNextStage)
        {
            _tickTimer = 0f;
            _tileService.SetStage(_tileService.CurrentStage + 1);
        }
    }
}