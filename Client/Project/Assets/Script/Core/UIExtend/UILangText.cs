using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using CSF;

//[RequireComponent(typeof(Text))]
public class UILangText : MonoBehaviour
{
    [SerializeField]
    private string key;

    private Text textTarget;
    void Start()
    {
        textTarget = gameObject.GetComponent<Text>();
        if(Mgr.ILR!=null)
            textTarget.text = Mgr.ILR.CallHotFixGetLang(key); //查找Key
    }

    public string Key
    {
        get { return key; }
        set
        {
            if (key != value)
            {
                key = value;
                if (Mgr.ILR != null)
                    Value = Mgr.ILR.CallHotFixGetLang(key);
                if (textTarget != null) //重新查找值
                    textTarget.text = Value;
            }
        }
    }
    
    public void Refresh()
    {
        Value = Mgr.ILR.CallHotFixGetLang(key);
    }

    public string Value
    {
        get
        {
            if (textTarget == null)
                return string.Empty;
            return textTarget.text;
        }
        set
        {
            if (textTarget == null)
            {
                textTarget = gameObject.GetComponent<Text>();
            }
            textTarget.text = value;
        }
    }
}
