using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [System.Serializable]
    public struct Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    private GameObject _poolObjects; //풀에서 생성된 오브젝트들
    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        _poolObjects = new GameObject("PoolObjects");
    }

    //오브젝트 소환 및 재사용
    public GameObject SpawnFromPool(string prefabPath)
    {
        //해당 주소를 가진 오브젝트가 있을 경우
        if (poolDictionary.ContainsKey(prefabPath))
        {
            GameObject obj = poolDictionary[prefabPath].Dequeue();
            poolDictionary[prefabPath].Enqueue(obj);
            obj.SetActive(true);
            return obj;
        }

        //해당 오브젝트가 존재하지 않을 경우
        else
        {
            Queue<GameObject> objectPool = new Queue<GameObject>(); //풀 딕셔너리 Value
            GameObject obj = ResourceManager.Instance.Instantiate(prefabPath); //오브젝트 생성
            obj.transform.parent = _poolObjects.transform; //오브젝트 부모설정
            objectPool.Enqueue(obj);
            poolDictionary.Add(prefabPath, objectPool); //해당 오브젝트 딕셔너리에 추가
            return obj;
        }
    }
}
