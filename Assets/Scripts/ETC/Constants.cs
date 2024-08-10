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
    
    //몬스터 종류
    public enum Monsters
    {
        Skeleton,
        EliteOrc,
        Wizard,
        Werebear,
        Orcrider
    }
    
    //몬스터 상태
    public enum MonsterState
    {
        Idle, //기본
        Walk, //걷기
        Attack, //공격
        Hit, //피격
        Dead //사망
    }
    
    //플레이어 상태
    public enum PlayerState
    {
        Idle,
        Attack
    }
    
    //프리팹 주소
    public class PrefabsPath
    {
        public const string PlayerPrefabPath = "Player"; //플레이어 프리팹 주소
        public const string SkeletonPrefabPath = "Skeleton"; //스켈레톤 프리팹 주소
        public const string EliteOrcPrefabPath = "EliteOrc"; //엘리트오크 프리팹 주소
        public const string WizardPrefabPath = "Wizard"; //위자드 프리팹 주소
        public const string WerebearPrefabPath = "Werebear"; //웨어베어 프리팹 주소
        public const string OrcRiderPrefabPath = "OrcRider"; //오크 라이더 프리팹 주소
    }
    
    //레이어 모음
    public class Layer
    {
        public const int PlayerLayerNum = 6;
        public const int MonsterLayerNum = 7;
    }
    
    //CSV 파일 주소
    public class CSVPath
    {
        public const string MonsterCSVPath = "/Data/CSV/SampleMonster.csv"; //몬스터 CSV 파일주소
    }
}
