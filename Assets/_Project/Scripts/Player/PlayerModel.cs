using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerModel : IInitializable
{
    [Inject] private readonly GroundSpawnerPresenter _groundSpawnerPresenter;
    [Inject] private readonly PlayerConfig _playerConfig;

    private int _coins;
    private int _cornsCount;
    private int _upgradeCost;

    public int Coins => _coins;
    public int CornsCount => _cornsCount;
    public int UpgradeCost => _upgradeCost;

    public event System.Action<int> OnCoinsChanged;
    public event System.Action<Vector3> OnCornAdded;
    public event System.Action<Vector3> OnCornRemoved;

    public void Initialize()
    {
        _coins = 0;
        _cornsCount = 0;
        _upgradeCost = _playerConfig.StartUpgradeCost;

        OnCoinsChanged?.Invoke(_coins);
    }

    public void TryUpgrade()
    {
        if (_coins >= _upgradeCost && _groundSpawnerPresenter.TryIncreaseFarmFieldSize())
        {
            _coins -= _upgradeCost;
            _upgradeCost *= 2;
            OnCoinsChanged?.Invoke(_coins);
        }
    }

    public void AddCorns(Vector3 fromPosition)
    {
        _cornsCount++;
        OnCornAdded?.Invoke(fromPosition);
    }

    public void SellCorns(Vector3 toPosition)
    {
        if (_cornsCount <= 0)
        {
            return;
        }

        _coins++;
        _cornsCount--;
        OnCoinsChanged?.Invoke(_coins);
        OnCornRemoved?.Invoke(toPosition);
    }

    public bool TryRemoveCoins(int amount)
    {
        if (_coins >= amount)
        {
            _coins -= amount;
            OnCoinsChanged?.Invoke(_coins);
            return true;
        }

        return false;
    }
}
