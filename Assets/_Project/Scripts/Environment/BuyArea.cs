using TMPro;
using UnityEngine;
using VContainer;

public class BuyArea : MonoBehaviour
{
    [Inject] private PlayerModel _playerModel;

    [SerializeField] private TMP_Text _buyText;

    private void Awake()
    {
        SetBuyCost(_playerModel.UpgradeCost);
    }

    public void SetBuyCost(int cost)
    {
        _buyText.text = cost.ToString();
    }
}
