using TMPro;
using UnityEngine;
using VContainer;

public class PlayerView : MonoBehaviour
{
    [Inject] private readonly PlayerModel _playerModel;

    [SerializeField] private TMP_Text _coinsText;

    private void OnEnable()
    {
        _playerModel.OnCoinsChanged += UpdateCoinsDisplay;
        UpdateCoinsDisplay(_playerModel.Coins);
    }

    private void OnDisable()
    {
        _playerModel.OnCoinsChanged -= UpdateCoinsDisplay;
    }

    private void UpdateCoinsDisplay(int newAmount)
    {
        _coinsText.text = $"Coins: {newAmount}";
    }
}
