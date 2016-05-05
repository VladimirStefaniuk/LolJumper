using UnityEngine;
using System.Collections;
using System.Collections.Generic;
  
public class PoolManager : MonoBehaviour {
     
    public List<PoolOfObjects> Pools;

    void Awake()
    { 
        if (Pools == null)
        {
            PoolOfObjects[] _pools = FindObjectsOfType<PoolOfObjects>();
            if (_pools != null)
            {
                Pools = new List<PoolOfObjects>(_pools);
            }
        }
        if (Pools == null)
        {
            Debug.LogError("Pools are still empty!");
        }
    }


    public PoolOfObjects GetPool(PoolTypesEnum poolType)
    {
        foreach (PoolOfObjects pool in Pools)
        {
            if (pool.PoolType == poolType)
            {
                return pool;
            }
        }
        return null; 
    }
     
}
