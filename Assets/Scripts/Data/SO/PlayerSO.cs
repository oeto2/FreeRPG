using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Characters/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public float Damage { get; set; } //공격력
    [field: SerializeField] public float AttackSpeed { get; set; } //공격속도
    [field: SerializeField] public float AttackRange { get; set; } //사정거리
    [field: SerializeField] public float AttackCoolTime { get; set; } //공격 쿨타임
}