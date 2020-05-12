using CSF;
using CSF.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static CSF.VerCheckLang;

public class VersionCheckUI : MonoBehaviour {


    public Slider sliderProg;
    public Text txtInfo;

    public GameObject goConfirm;

    public Text txtConfirmTitle;
    public Text txtConfirmContent;
    public Button btnConfirm;
    public Button btnCancel;
    public Text btnConfirmText;
    public Text btnCancelText;

    public Text txtVersion;

    private Action confirmCB;
    private Action cancelCB;
    // Use this for initializationO
    void Awake ()
    {
        sliderProg.value = 0;
        goConfirm.SetVisible(false);
        btnConfirm.onClick.AddListener(btnConfirm_Click);
        btnCancel.onClick.AddListener(btnCancel_Click);

        //跟据语言类型更换Logo图标
        EVLangType lang = VerCheckLang.LangType;
        string logoName = "Logo";
        if (lang == EVLangType.ZH_CN)
            logoName = "Logo_CN";
        foreach (Transform logo in transform.Find("BG").transform)
            logo.gameObject.SetActive(logo.name == logoName);    }

    public void Confirm(Action confirmcb, Action cancelcb, string content, string title = null,bool isAlert = true)
    {
        if (goConfirm.IsVisible()) return;
        confirmCB = confirmcb;
        cancelCB = cancelcb;
        goConfirm.SetVisible(true);        
        txtConfirmTitle.text = title;
        txtConfirmContent.text = content;
        btnConfirmText.text = VerCheckLang.Confirm;
        btnCancelText.text = VerCheckLang.Cancel;
        btnCancel.gameObject.SetActive(!isAlert); //Alert 不显示取消       
        Mgr.UI.UIAnim(goConfirm, EUIAnim.ScaleIn).Run();
    }
    /// <summary>确认</summary>
    void btnConfirm_Click()
    {
        CloseConfirm(confirmCB).Run();
    }
    /// <summary>取消</summary>
    void btnCancel_Click()
    {
        CloseConfirm(cancelCB).Run();
    }
    async CTask CloseConfirm(Action action)
    {
        await Mgr.UI.UIAnim(goConfirm, EUIAnim.ScaleOut);
        goConfirm.SetVisible(false);
        action?.Invoke();

    }
}
