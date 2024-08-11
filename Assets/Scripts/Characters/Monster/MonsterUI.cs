using System;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : MonoBehaviour
{
    [SerializeField] private Slider hpSlider; //체력 바
    [SerializeField] private Button infoButton; //정보 버튼
    private MonsterController _monsterController;

    private void Awake()
    {
        _monsterController = GetComponent<MonsterController>();
    }

    private void Start()
    {
        infoButton.onClick.AddListener(SetInfoButton); // 정보 버튼 세팅
    }

    //체력 Slider 적용
    public void UpdateMonsterHPUI()
    {
        float maxHp = _monsterController.monsterStatus.MaxHealth;
        float curHP = _monsterController.monsterStatus.Health;
        float value = Mathf.Clamp(curHP / maxHp, 0f, 1f) ; // Slider Value 계산값
        hpSlider.value = value;
    }
    
    //몬스터 정보 버튼 설정
    public void SetInfoButton()
    {
        UIManager.Instance.ShowPopup<MonsterInfoPopup>(); //정보 팝업 활성화
        MonsterInfoPopup monsterInfo = UIManager.Instance.GetUIComponent<MonsterInfoPopup>();
        string monsterName = _monsterController.monsterStatus.name; //몬스터 이름
        string monsterGrade = _monsterController.monsterStatus.Grade.ToString(); //몬스터 등급
        string monsterSpeed = _monsterController.monsterStatus.Speed.ToString(); //몬스터 속도
        string monsterHealth = _monsterController.monsterStatus.MaxHealth.ToString(); //몬스터 체력
        Sprite monsterSprite = ResourceManager.Instance.Load<Sprite>($"Sprites/Monster/{monsterName}"); //몬스터 이미지
        monsterInfo.InfoSetting(monsterSprite,monsterName,monsterGrade,monsterSpeed,monsterHealth);
    }
}
