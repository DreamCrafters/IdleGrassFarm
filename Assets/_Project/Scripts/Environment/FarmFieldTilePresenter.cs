using UnityEngine;
using VContainer.Unity;

public class FarmFieldTilePresenter : ITickable
{
    private readonly FarmFieldTileService _farmFieldTileService;

    private float _tickTimer;

    public FarmFieldTilePresenter(FarmFieldTileService farmFieldTileService)
    {
        _farmFieldTileService = farmFieldTileService;
    }

    public void Tick()
    {
        Debug.Log($"Tick called for FarmFieldTilePresenter with stage: {_farmFieldTileService.CurrentStage}");
        // _tickTimer += Time.deltaTime;

        // if (_tickTimer >= 1f)
        // {
        //     _tickTimer = 0f;
        //     Debug.Log($"Current Stage: {_farmFieldTileService.CurrentStage}");
        // }
    }
}