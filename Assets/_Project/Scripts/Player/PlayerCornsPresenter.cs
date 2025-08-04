using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerCornsPresenter : IInitializable, IDisposable
{
    [Inject] private readonly PlayerModel _playerModel;
    [Inject] private readonly PlayerCornsModel _playerCornsModel;

    public void Initialize()
    {
        _playerModel.OnCornAdded += AddCorns;
        _playerModel.OnCornRemoved += RemoveCorns;
    }

    public void Dispose()
    {
        _playerModel.OnCornAdded -= AddCorns;
        _playerModel.OnCornRemoved -= RemoveCorns;
    }

    private void AddCorns(Vector3 fromPosition)
    {
        _playerCornsModel.SpawnCorn(fromPosition);
    }

    private void RemoveCorns(Vector3 toPosition)
    {
        _playerCornsModel.ReleaseCorn(toPosition);
    }
}
