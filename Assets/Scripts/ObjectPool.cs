using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ObjectPool : BaseObjectPool
{
    #region Editor Properties

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int numberObjects = 0;

    #endregion

    #region Private Members

    private List<GameObject> instances = new List<GameObject>();

    #endregion

    #region Public Properties

    public GameObject Prefab
    {
        get { return prefab; }
    }

    public List<GameObject> Instances
    {
        get { return instances; }
    }

    #endregion

    #region  Public Interface

    public void Setup(GameObject prefab, int numObjs)
    {
        this.prefab = prefab;
        this.numberObjects = numObjs;
    }

    public override GameObject GetObject(bool activate)
    {
        for (int i = 0; i < instances.Count; ++i)
        {
            if (!instances[i].activeInHierarchy)
            {
                instances[i].SetActive(activate);
                return instances[i];
            }
        }

        GameObject retObj = CreateObject(prefab);
        instances.Add(retObj);
        retObj.SetActive(activate);

        return retObj;
    }

    public override void Init()
    {
        Clear();
        for (int i = 0; i < numberObjects; ++i)
        {
            instances.Add(CreateObject(prefab));
        }
    }

    public override void Clear()
    {
        for (int i = 0; i < instances.Count; ++i)
        {
            Destroy(instances[i]);
        }

        instances.Clear();
    }

    #endregion

    #region Private Routines



    #endregion
}
