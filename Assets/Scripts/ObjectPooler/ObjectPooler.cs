using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<ObjectPoolType> _objectPoolTypes;

    private Dictionary<Types, ObjectPoolItem> _pool;

    private void Awake()
    {
        InitPool();
    }

    private void InitPool()
    {
        _pool = new Dictionary<Types, ObjectPoolItem>();

        GameObject emptyObject = new GameObject();

        foreach (ObjectPoolType objType in _objectPoolTypes)
        {
            GameObject container = Instantiate(emptyObject, transform, false);
            container.name = objType.type.ToString();

            _pool[objType.type] = new ObjectPoolItem(container.transform);

            for (int i = 0; i < objType.amount; i++)
            {
                GameObject obj = InstantiateObject(objType.type, container.transform);
                _pool[objType.type].Objects.Enqueue(obj);
            }
        }

        Destroy(emptyObject);
    }

    private GameObject InstantiateObject(Types type, Transform parent)
    {
        GameObject temp = Instantiate(_objectPoolTypes.Find(x => x.type == type).prefab, parent);
        temp.SetActive(false);
        return temp;
    }

    public GameObject GetObject(Types type, bool expandable = false)
    {
        if (_pool[type].Objects.Count > 0)
        {
            GameObject obj = _pool[type].Objects.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        
        if (expandable)
        {
            GameObject obj = InstantiateObject(type, _pool[type].Container);
            obj.SetActive(true);
            return obj;
        }

        return null;
    }
    
    public void ReturnObject(GameObject obj)
    {
        _pool[obj.GetComponent<IPooleable>().Type].Objects.Enqueue(obj);
        obj.SetActive(false);
    }

    public void ReturnObject(GameObject obj, Types type)
    {
        _pool[type].Objects.Enqueue(obj);
        obj.SetActive(false);
    }

    public int GetAmount(Types type)
    {
        return _objectPoolTypes.Find(x => x.type == type).amount;
    }

    public int GetCount(Types type)
    {
        return _pool[type].Objects.Count;
    }

    public int GetCountTypes()
    {
        return _objectPoolTypes.Count;
    }
}