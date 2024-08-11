using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInfoPopup : UIBase
{
    [SerializeField] private Image monsterImage; //몬스터 이미지
    [SerializeField] private TextMeshProUGUI monsterNameText; //몬스터 이름 텍스트
    [SerializeField] private TextMeshProUGUI monsterGradeText; //몬스터 등급 텍스트
    [SerializeField] private TextMeshProUGUI monsterSpeedText; //몬스터 속도 텍스트
    [SerializeField] private TextMeshProUGUI monsterHealthText; //몬스터 체력 텍스트

    
    //정보 세팅하기
    public void InfoSetting(Sprite sprite_,string name_,  string grade_, string speed_, string health_)
    {
        monsterImage.sprite = sprite_;
        monsterNameText.text = $"Name : {name_}";
        monsterGradeText.text = $"Grade : {grade_}";
        monsterSpeedText.text = $"Speed : {speed_}";
        monsterHealthText.text = $"Health : {health_}";
    }
}
