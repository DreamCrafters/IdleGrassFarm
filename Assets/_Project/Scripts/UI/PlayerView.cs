using TMPro;
using UnityEngine;
using VContainer;

public class PlayerView : MonoBehaviour
{
    [Inject] private readonly PlayerModel _playerModel;

    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private TMP_Text _cornsText;

    private void OnEnable()
    {
        _playerModel.OnCoinsChanged += UpdateCoinsDisplay;
        _playerModel.OnCornsCountChanged += UpdateCornsDisplay;
        UpdateCoinsDisplay(_playerModel.Coins);
        UpdateCornsDisplay(_playerModel.CornsCount);
    }

    private void OnDisable()
    {
        _playerModel.OnCoinsChanged -= UpdateCoinsDisplay;
        _playerModel.OnCornsCountChanged -= UpdateCornsDisplay;
    }

    private void UpdateCoinsDisplay(int newAmount)
    {
        _coinsText.text = $"Coins: {newAmount}";
    }

    private void UpdateCornsDisplay(int newAmount)
    {
        _cornsText.text = $"Corns: {newAmount}";
    }
}
