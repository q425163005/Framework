using System.Collections;
using System.Collections.Generic;
using CSF.Tasks;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace MapEditor
{
    public class MonsterTipsUI : MonoBehaviour
    {
        public SkeletonGraphic goModel;

        public Text txtName;
        public Text txtBose;
        public Text txtHP;
        public Text txtDefend;
        public Text txtAttack;
        public Text txtRound;
        public Text txtPos;
        public GameObject[] Elem;

        private MonsterConfig Config;
        MonsterItem MonsterItem;
        private RectTransform rectTransform;
        
        void Awake()
        {            
            rectTransform = transform as RectTransform;
            gameObject.SetVisible(false);
        }

        public void Show(MonsterConfig config, MonsterItem monsterItem = null)
        {
            MonsterItem = monsterItem;
            txtPos.text = string.Empty;
            gameObject.SetVisible(true);
            if (config == Config) return;            
            Config = config;
            txtName.text = $"{config.name.Value}(Lv.{config.level})[{config.id}]";
            txtBose.gameObject.SetVisible(config.type!=0);
            txtAttack.text = config.attrs[0].ToString();
            txtDefend.text = config.attrs[1].ToString();
            txtHP.text = config.attrs[2].ToString();
            txtRound.text = config.attackInterval.ToString();
            for (int i = 0; i < Elem.Length; i++)
                Elem[i].SetVisible(config.elemType==i);
            LoadMode().Run();
        }

        public void RefPos(int grid,int x,int y)
        {
            txtPos.text = $"格子: {grid}  偏移: {x},{y}";
        }


        public void Close()
        {
            gameObject.SetVisible(false);
        }
             
        async CTask LoadMode()
        {
            if (Config == null) return;
            goModel.skeletonDataAsset = await MapEditor.I.LoadMonsterModel("Enemy/" + Config.model);
            goModel.Initialize(true);
            string animName = "Idle01"; //goModel.skeletonDataAsset.GetSkeletonData(true).Animations.Items[0].Name;
            goModel.AnimationState.SetAnimation(0, animName, true);

            double scale = Config.scale;
            if (scale == 0)
                scale = Config.type == 0 ? 0.5f : 1f;
            goModel.transform.localScale = Vector3.one * (float)scale;

            goModel.rectTransform.sizeDelta = new Vector2(goModel.SkeletonData.Width, goModel.SkeletonData.Height* (float)scale);
        }

        private void Update()
        {
            if (MonsterItem == null || !gameObject.IsVisible()) return;
            if (Input.GetKeyDown(KeyCode.KeypadPlus)|| Input.GetKeyDown(KeyCode.Equals))
            {
                MonsterItem.SetHaloSize(1);
               
            }
            else if(Input.GetKeyDown(KeyCode.KeypadMinus)|| Input.GetKeyDown(KeyCode.Minus))
            {
                MonsterItem.SetHaloSize(-1);
            }
        }
    }
}
