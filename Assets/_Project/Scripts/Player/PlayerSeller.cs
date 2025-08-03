using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerSeller : IInitializable, IDisposable, ITickable
{
    private const string SellTag = "Sell";

    [Inject] private readonly PlayerModel _playerModel;
    [Inject] private readonly PlayerMovement _playerMovement;
    [Inject] private readonly PlayerConfig _playerConfig;

    private bool _isSelling;
    private float _sellingTimer;

    public void Initialize()
    {
        _playerMovement.OnTriggerEnterEvent += OnTriggerEnter;
        _playerMovement.OnTriggerExitEvent += OnTriggerExit;
        _isSelling = false;
    }

    public void Dispose()
    {
        _playerMovement.OnTriggerEnterEvent -= OnTriggerEnter;
        _playerMovement.OnTriggerExitEvent -= OnTriggerExit;
    }

    public void Tick()
    {
        _sellingTimer += Time.deltaTime;

        if (_isSelling && _sellingTimer >= _playerConfig.SellingDuration)
        {
            _playerModel.SellCorns(1);
            _sellingTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(SellTag))
        {
            _isSelling = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(SellTag))
        {
            _isSelling = false;
        }
    }
}
