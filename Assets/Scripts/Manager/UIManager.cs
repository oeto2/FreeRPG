using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum UIState
{
    Main,
    Shop,
    Option
}

public class UIManager : Singleton<UIManager>
{
    //부모 UI
    public Transform parentsUI = null;
    private Dictionary<string, UIBase> _popups = new Dictionary<string, UIBase>();

    //현재 UI화면
    public UIState UiState = UIState.Main;

    public GameObject GetPopup(string popupName)
    {
        ShowPopup(popupName);

        return _popups[popupName].gameObject;
    }

    public GameObject GetPopupObject(string popupName)
    {
        if (!_popups.ContainsKey(popupName))
        {
            return null;
        }

        return _popups[popupName].gameObject;
    }

    //해당 팝업이 존재하는지
    public bool ExistPopup(string _key)
    {
        return _popups.ContainsKey(_key);
    }

    //팝업 불러오기
    public UIBase ShowPopup(string popupname, Transform parents = null)
    {
        var obj = Resources.Load("Popups/" + popupname, typeof(GameObject)) as GameObject;
        if (!obj)
        {
            Debug.LogWarning($"Failed to ShowPopup({popupname})");
            return null;
        }

        //이미 리스트에 해당 팝업이 존재한다면 return
        if (_popups.ContainsKey(popupname))
        {
            ShowPopup(_popups[popupname].gameObject);
            return null;
        }

        return ShowPopupWithPrefab(obj, popupname, parents);
    }

    public T ShowPopup<T>(Transform parents = null) where T : UIBase
    {
        return ShowPopup(typeof(T).Name, parents) as T;
    }

    public UIBase ShowPopupWithPrefab(GameObject prefab, string popupName, Transform parents = null)
    {
        if (parentsUI != null)
            parents = parentsUI;

        string name = popupName;
        var obj = Instantiate(prefab, parents);
        obj.name = name;

        return ShowPopup(obj, popupName);
    }

    public UIBase ShowPopup(GameObject obj, string popupname)
    {
        var popup = obj.GetComponent<UIBase>();
        _popups.Add(popupname, popup);

        obj.SetActive(true);
        return popup;
    }

    public void ShowPopup(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void ChangeUIState(UIState changeState_)
    {
        UiState = changeState_;
    }

    //딕셔너리 초기화
    public void ResetUIMangerData()
    {
        _popups.Clear();
    }

    //UI 닫기
    public void CloseUIPopup(string popupName_)
    {
        if (_popups.ContainsKey(popupName_))
        {
            _popups[popupName_].gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"닫으려는 {popupName_}이 존재하지 않습니다");
        }
    }

    //딕셔너리에 존재하는 컴포넌트 가져오기
    public T GetUIComponent<T>() where T : UIBase
    {
        string uiName = typeof(T).Name;

        //해당 이름의 UI가 존재하지 않는다면
        if (!_popups.ContainsKey(uiName))
        {
            Debug.LogWarning($"{uiName}이 존재하지 않아 컴포넌트를 가져오지 못했습니다.");
            return null;
        }

        var uiComponent = _popups[uiName].GetComponent<T>();
        return uiComponent;
    }
}