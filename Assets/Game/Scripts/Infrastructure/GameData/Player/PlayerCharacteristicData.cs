using UnityEngine;

namespace Game.Scripts.Infrastructure.GameData.Player
{
    [CreateAssetMenu (fileName = "PlayerCharacteristic", menuName = "Game/Player/PlayerCharacteristicData")]
    public class PlayerCharacteristicData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        
        [field: SerializeField, Range(0f,1f)] public float MouseSensitivityX { get; private set; }
        [field: SerializeField, Range(0f,1f)] public float MouseSensitivityY { get; private set; }
    }
}