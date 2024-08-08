using UnityEngine;
using Constants;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Characters/PlayerSO")]
public class MonsterSO : ScriptableObject
{
    // Name,Grade,Speed,Health
    [field: SerializeField] public string Name { get; set; } 
    [field: SerializeField] public MonsterGrade Grade { get; set; } 
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float Health { get; set; }
}
