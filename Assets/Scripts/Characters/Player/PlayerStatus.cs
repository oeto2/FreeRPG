using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private PlayerSO playerSO; // 몬스터 SO

    //변수 : 플레이어 스텟관련
    public float Damage { get; set; } //데미지
    public float AttackSpeed { get; set; } //공격속도
    public float AttackRange { get; set; } //사정거리
    public float AttackCoolTime { get; set; } //공격쿨타임
    

    private void Awake()
    {
        Initialize_PlayerData();
    }

    //플레이어 SO 데이터 복사하기
    private void Initialize_PlayerData()
    {
        Damage = playerSO.Damage;
        AttackSpeed = playerSO.AttackSpeed;
        AttackRange = playerSO.AttackRange;
        AttackCoolTime = playerSO.AttackCoolTime;
    }
}