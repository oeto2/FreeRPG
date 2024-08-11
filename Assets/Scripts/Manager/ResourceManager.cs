using System.Collections.Generic;
using UnityEngine;
public class ResourceManager : Singleton<ResourceManager>
{
    public Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();

    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(Sprite))
        {
            if (_sprites.TryGetValue(path, out Sprite sprite))
                return sprite as T;

            Sprite sp = Resources.Load<Sprite>(path);
            _sprites.Add(path, sp);
            
            if(sp == null)
                Logger.LogError($"{path}, 스프라이트를 불러오지 못했습니다");
            
            return sp as T;
        }
        
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Logger.LogWarning($"Failed to load prefab : {path}");
            return null;
        }
        return Instantiate(prefab, parent);
    }

    public GameObject Instantiate(GameObject prefab, Transform parent = null)
    {
        GameObject go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}