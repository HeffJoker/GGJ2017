using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCountManager : SingletonBase<EnemyCountManager> {

    public Text CountText;
    public GameObject CountPanel;

    private int _count = 0;

    public void SetCount(int count)
    {
        CountText.text = count.ToString();
        _count = count;
    }

    public void Increment()
    {
        ++_count;
        CountText.text = _count.ToString();
    }

    public void Decrement()
    {
        --_count;
        CountText.text = _count.ToString();
    }

    public void ShowPanel()
    {
        CountPanel.gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        CountPanel.gameObject.SetActive(false);
    }
}
