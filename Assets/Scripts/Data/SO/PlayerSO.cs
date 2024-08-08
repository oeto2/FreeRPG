using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Characters/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float AttackSpeed { get; set; }
}