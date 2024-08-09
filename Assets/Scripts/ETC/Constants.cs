namespace Constants
{
    //몬스터 등급
    public enum MonsterGrade
    {
        일반,
        레어,
        매직,
        전설,
        영웅
    }
    
    //몬스터 상태
    public enum MonsterState
    {
        Idle, //기본
        Walk, //걷기
        Attack, //공격
        Hit //피격
    }
    
    //프리팹 주소
    public class PrefabsPath
    {
        public const string PlayerPrefabPath = "Player"; //플레이어 프리팹 주소
        public const string SkeletonPrefabPath = "Skeleton"; //스켈레톤 프리팹 주소
    }
}
