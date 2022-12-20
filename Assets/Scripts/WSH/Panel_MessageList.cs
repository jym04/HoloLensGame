using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WIFramework.UI;

public class Panel_MessageList : PanelBase
{
    public GameObject uEntryPrefab;
    private RectTransform mOwnTransform;
    private int mMaxMessages = 50;
    private int mCounter = 0;

    private void Awake()
    {
        mOwnTransform = this.GetComponent<RectTransform>();
    }
    private void Start()
    {
        foreach (var v in mOwnTransform.GetComponentsInChildren<RectTransform>())
        {
            if (v != mOwnTransform)
            {
                v.name = "Element " + mCounter;
                mCounter++;
            }
        }
    }

    public void AddTextEntry(string text)
    {
        GameObject ngp = Instantiate(uEntryPrefab);
        Text t = ngp.GetComponentInChildren<Text>();
        t.text = text;

        RectTransform transform = ngp.GetComponent<RectTransform>();
        transform.SetParent(mOwnTransform, false);

        GameObject go = transform.gameObject;
        go.name = "Element " + mCounter;
        mCounter++;
    }


    /// <summary>
    /// Destroys old messages if needed and repositions the existing messages.
    /// </summary>
    private void Update()
    {
        int destroy = mOwnTransform.childCount - mMaxMessages;
        for (int i = 0; i < destroy; i++)
        {
            var child = mOwnTransform.GetChild(i).gameObject;
            Destroy(child);
        }
    }

}
