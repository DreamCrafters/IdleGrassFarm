using UnityEngine;

[CreateAssetMenu(fileName = "FarmSettings", menuName = "Settings/FarmSettings")]
public class FarmSettings : ScriptableObject
{
    [SerializeField] private float _timeToNextStage = 1f;

    public float TimeToNextStage => _timeToNextStage;
}
