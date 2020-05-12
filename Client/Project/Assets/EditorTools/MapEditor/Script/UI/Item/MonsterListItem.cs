using System.Collections;
using System.Collections.Generic;
using CSF.Tasks;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace MapEditor
{
    public class MonsterListItem : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
    {
        public SkeletonGraphic goModel;
        
        public Text txtId;
        private MonsterConfig Config;

        private RectTransform rectTransform;

        Vector2 offSet = Vector2.zero;

        void Awake()
        {            
            rectTransform = transform as RectTransform;
            gameObject.AddClick(self_Click);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UIRoot.I.MonsterTips.Show(Config);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            UIRoot.I.MonsterTips.Close();
        }

        public bool IsHave => gameObject.activeSelf;

        public void SetData(MonsterConfig config)
        {
            Config = config;
            txtId.text = Config.id.ToString();
            LoadMode().Run();
        }
        void self_Click()
        {
            UIRoot.I.MonsterGrid.AddMonster(Config.id);
        }
       
        async CTask LoadMode()
        {
            if (Config == null) return;
            goModel.skeletonDataAsset = await MapEditor.I.LoadMonsterModel("Enemy/" + Config.model);
            goModel.Initialize(true);
            string animName = "Idle01"; //;goModel.skeletonDataAsset.GetSkeletonData(true).Animations.Items[0].Name;
            goModel.AnimationState.SetAnimation(0, animName, true);
            await new WaitUntil(() => { return goModel.SkeletonData != null; });
            goModel.rectTransform.sizeDelta = new Vector2(goModel.SkeletonData.Width, goModel.SkeletonData.Height);

            float scale = 250f/ goModel.SkeletonData.Height;
            goModel.transform.localScale =  Vector3.one * scale;
        }
    }
}
