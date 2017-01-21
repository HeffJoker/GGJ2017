using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class BaseObjectPool : MonoBehaviour
{
    #region Public Interface

    public abstract GameObject GetObject(bool activate);
    public abstract void Init();
    public abstract void Clear();

    #endregion

    #region Private Routines

    private void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        Clear();
    }

    protected GameObject CreateObject(GameObject prefab)
    {
        GameObject newObj = Instantiate<GameObject>(prefab);
        newObj.transform.parent = transform;
        newObj.SetActive(false);

        return newObj;
    }

    #endregion
}

