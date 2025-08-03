using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerMow : IInitializable, IDisposable, ITickable
{
    private const string FarmTag = "Farm";

    private readonly List<FarmFieldTileModel> _processingTiles = new();

    [Inject] private readonly PlayerMovement _playerMovement;
    [Inject] private readonly PlayerModel _playerModel;

    public void Initialize()
    {
        _playerMovement.OnTriggerEnterEvent += OnTriggerEnter;
        _playerMovement.OnTriggerExitEvent += OnTriggerExit;
    }

    public void Dispose()
    {
        _playerMovement.OnTriggerEnterEvent -= OnTriggerEnter;
        _playerMovement.OnTriggerExitEvent -= OnTriggerExit;
    }

    public void Tick()
    {
        foreach (var tile in _processingTiles)
        {
            if (tile.CurrentStage == TileStage.Stage3)
            {
                tile.SetStage(TileStage.Stage0);
                _playerModel.AddCorns();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(FarmTag)
            && other.gameObject.TryGetComponent(out FarmFieldTileModel farmFieldTile))
        {
            _processingTiles.Add(farmFieldTile);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(FarmTag)
            && other.gameObject.TryGetComponent(out FarmFieldTileModel farmFieldTile))
        {
            _processingTiles.Remove(farmFieldTile);
        }
    }
}
