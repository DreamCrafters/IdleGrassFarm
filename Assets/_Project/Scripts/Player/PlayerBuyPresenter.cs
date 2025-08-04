using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerBuyPresenter : IInitializable, IDisposable
{
    private const string BuyTag = "Buy";

    [Inject] private readonly PlayerMovement _playerMovement;
    [Inject] private readonly PlayerModel _playerModel;

    public void Initialize()
    {
        _playerMovement.OnTriggerEnterEvent += OnTriggerEnter;
    }

    public void Dispose()
    {
        _playerMovement.OnTriggerEnterEvent -= OnTriggerEnter;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(BuyTag) && other.TryGetComponent(out BuyArea buyArea))
        {
            _playerModel.TryUpgrade();
            buyArea.SetBuyCost(_playerModel.UpgradeCost);
        }
    }
}
