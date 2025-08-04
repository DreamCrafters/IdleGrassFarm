using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerSellPresenter : IInitializable, IDisposable, ITickable
{
    private const string SellTag = "Sell";

    [Inject] private readonly PlayerModel _playerModel;
    [Inject] private readonly PlayerMovement _playerMovement;
    [Inject] private readonly PlayerConfig _playerConfig;

    private Transform _sellArea = null;
    private float _sellingTimer;

    public void Initialize()
    {
        _playerMovement.OnTriggerEnterEvent += OnTriggerEnter;
        _playerMovement.OnTriggerExitEvent += OnTriggerExit;
        _sellArea = null;
    }

    public void Dispose()
    {
        _playerMovement.OnTriggerEnterEvent -= OnTriggerEnter;
        _playerMovement.OnTriggerExitEvent -= OnTriggerExit;
    }

    public void Tick()
    {
        _sellingTimer += Time.deltaTime;

        if (_sellArea != null && _sellingTimer >= _playerConfig.SellingDuration)
        {
            _playerModel.SellCorns(_sellArea.position);
            _sellingTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(SellTag) && other.TryGetComponent(out SellArea sellArea))
        {
            _sellArea = sellArea.SellPoint;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(SellTag))
        {
            _sellArea = null;
        }
    }
}
