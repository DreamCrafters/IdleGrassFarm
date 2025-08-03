public class PlayerModel
{
    private int _coins;
    private int _cornsCount;

    public int Coins => _coins;
    public int CornsCount => _cornsCount;

    public event System.Action<int> OnCoinsChanged;
    public event System.Action<int> OnCornsCountChanged;

    public void AddCorns(int amount = 1)
    {
        _cornsCount += amount;
        OnCornsCountChanged?.Invoke(_cornsCount);
    }

    public void SellCorns(int amount)
    {
        if (_cornsCount < amount)
        {
            return;
        }

        _coins += amount;
        _cornsCount -= amount;
        OnCoinsChanged?.Invoke(_coins);
        OnCornsCountChanged?.Invoke(_cornsCount);
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
