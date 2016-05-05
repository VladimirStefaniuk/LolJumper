using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolOfObjects : MonoBehaviour {

    public GameObject PooledObject;
    public int Size;
    public bool IsGrowing;

    private List<GameObject> _Pool;

    public PoolTypesEnum PoolType;

	// Use this for initialization
	void Awake () {

        _Pool = new List<GameObject>(0);
        if (PooledObject != null && Size > 0)
        { 
            for (int i = 0; i < Size; ++i)
            {
                CreateNewElement();
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject Get()
    {
        foreach (GameObject go in _Pool)
        {
            if(go!=null)
            if (false == go.activeSelf)
            {
                go.SetActive(true);
                return go;
            }
        }

        if (IsGrowing)
        {
            return CreateNewElement(true);
        }
        else
        {
            return null;
        }
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
    }

    GameObject CreateNewElement(bool enabled = false)
    {
        GameObject go = Instantiate(PooledObject, Vector3.zero, Quaternion.identity) as GameObject;
        go.transform.parent = this.transform; 
        _Pool.Add(go);
        go.SetActive(enabled);
        return go;
    }

    public void ReturnAll()
    {
        foreach (GameObject go in _Pool)
        {
            go.SetActive(false);
        }
    }

}
