//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix_Project.Login
{
    public partial class LoginUI : BaseUI
    {
        private GameObject goLogin;
        private Image Logo;
        private GameObject goSelServer;
        private InputField inputUserName;
        private Button btnLogin;
        private InputField inputPassword;
        private Dropdown dropSelLang;
        private Text txtServerName;
        private Image imgTag;
        private Text texNum;
        private GameObject btnChangServer;
        private Button btnStartGame;
        private GameObject btnLogout;
        private Text txtLogUserName;
        private Text txtVersion;
        private Button btnVisitorLogin;
        private Button btnFacebookLogin;
        private Button btnAppleLogin;
        private GameObject AccountPwdPanel;
        private GameObject OwnPanel;
 
        /// <summary>初始化UI控件</summary>
        protected override void InitializeComponent()
        {
            goLogin = Get("goLogin");
            Logo = Get<Image>("Logo");
            goSelServer = Get("goSelServer");
            inputUserName = Get<InputField>("inputUserName");
            btnLogin = Get<Button>("btnLogin");
            inputPassword = Get<InputField>("inputPassword");
            dropSelLang = Get<Dropdown>("dropSelLang");
            txtServerName = Get<Text>("txtServerName");
            imgTag = Get<Image>("imgTag");
            texNum = Get<Text>("texNum");
            btnChangServer = Get("btnChangServer");
            btnStartGame = Get<Button>("btnStartGame");
            btnLogout = Get("btnLogout");
            txtLogUserName = Get<Text>("txtLogUserName");
            txtVersion = Get<Text>("txtVersion");
            btnVisitorLogin = Get<Button>("btnVisitorLogin");
            btnFacebookLogin = Get<Button>("btnFacebookLogin");
            btnAppleLogin = Get<Button>("btnAppleLogin");
            AccountPwdPanel = Get("AccountPwdPanel");
            OwnPanel = Get("OwnPanel");

        }
    }
}

