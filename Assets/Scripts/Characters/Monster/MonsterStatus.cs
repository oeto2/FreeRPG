using UnityEngine;
using Constants;

public class MonsterStatus : MonoBehaviour
{
    [SerializeField] private MonsterSO monsterSO; // 몬스터 SO
    
    //변수 : 몬스터 스텟관련
    public string Name { get; set;} //이름
    public MonsterGrade Grade { get; set;} //등급
    public float Speed { get; set;} //속도
    public float Health { get; set;} //체력

    private void Awake()
    {
        Initialize_MonsterData();
    }

    //몬스터 SO 데이터 복사하기
    private void Initialize_MonsterData()
    {
        Name = monsterSO.Name;
        Grade = monsterSO.Grade;
        Speed = monsterSO.Speed;
        Health = monsterSO.Health;
    }
}