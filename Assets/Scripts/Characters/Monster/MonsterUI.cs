using System;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    private MonsterController _monsterController;

    private void Awake()
    {
        _monsterController = GetComponent<MonsterController>();
    }

    //체력 Slider 적용
    public void UpdateMonsterHPUI()
    {
        float maxHp = _monsterController.monsterStatus.MaxHealth;
        float curHP = _monsterController.monsterStatus.Health;
        float value = Mathf.Clamp(curHP / maxHp, 0f, 1f) ; // Slider Value 계산값
        hpSlider.value = value;
    }
}
