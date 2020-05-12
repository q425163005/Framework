using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSF.Tasks;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace HotFix_Project
{
    /// <summary>
    /// 骨骼动画
    /// </summary>
    public class SkeletonAnimation
    {
        /// <summary>
        /// 骨骼动画组件
        /// </summary>
        public SkeletonGraphic skeletonGraphic;

        /// <summary>
        /// 骨骼动画资源
        /// </summary>
        SkeletonDataAsset dataAsset;

        private HeroModelSetting modelSetting;

        public Spine.Animation[] animations;

        public List<string> aniNameList;

        private string _dataAsstName = string.Empty;


        private string _lastAnimName   = string.Empty;
        private bool   _lastAnimIsLoop = false;
        private bool   _isLoaded       = false;

        public SkeletonAnimation(SkeletonGraphic graphic, string dataAssetName)
        {
            LoadModelSetting(dataAssetName);
            skeletonGraphic = graphic;
            LoadSkeletonData(dataAssetName).Run();
        }

        public float Width => skeletonGraphic.SkeletonData == null ? 200 : skeletonGraphic.SkeletonData.Width;

        public float Height => skeletonGraphic.SkeletonData == null ? 400 : skeletonGraphic.SkeletonData.Height;


        public void SeSkeletonData(string dataAssetName)
        {
            if (_dataAsstName != dataAssetName)
            {
                LoadModelSetting(dataAssetName);
                skeletonGraphic.skeletonDataAsset = null;
                skeletonGraphic.Clear();
                LoadSkeletonData(dataAssetName).Run();
            }
        }

        void LoadModelSetting(string dataAssetName)
        {
            string key = dataAssetName.Split('/')[1];
            if (Mgr.Config.dicHeroModelSetting.ContainsKey(key))
            {
                modelSetting = Mgr.Config.dicHeroModelSetting[key];
            }
        }

        /// <summary>
        /// 加载骨骼数据
        /// </summary>
        /// <param name="dataAssetName"></param>
        async CTask LoadSkeletonData(string dataAssetName)
        {
            _isLoaded     = false;
            _dataAsstName = dataAssetName;
            dataAsset     = await LoadHelper.LoadSkeletonData(dataAssetName);
            if (skeletonGraphic == null)
            {
                CLog.Error("获取物体骨骼动画组件错误");
                return;
            }

            skeletonGraphic.skeletonDataAsset = dataAsset;
            skeletonGraphic.Initialize(true);
            animations  = skeletonGraphic.skeletonDataAsset.GetSkeletonData(true).Animations.Items;
            aniNameList = new List<string>();
            for (int i = 0; i < animations.Length; i++)
            {
                aniNameList.Add(animations[i].Name);
            }

            PlayStaticAnimation();
            skeletonGraphic.AnimationState.TimeScale = 1;
            if (modelSetting!=null)
            {
                skeletonGraphic.gameObject.transform.localScale = Vector3.one * modelSetting.Pos[1][2];
            }
            _isLoaded = true;
        }
        
        public void RestPos()
        {
            skeletonGraphic.rectTransform.anchoredPosition = new Vector2(modelSetting.Pos[1][0], modelSetting.Pos[1][1]);
        }

        /// <summary>
        /// 等待立绘加载完成
        /// </summary>
        public virtual async CTask Await()
        {
            await CTask.WaitUntil(() => { return _isLoaded; });
        }


        /// <summary>
        /// 站立
        /// </summary>
        public void PlayIdleAnimation()
        {
            Play("Idle01", true);
        }

        /// <summary>
        /// 静止
        /// </summary>
        public void PlayStaticAnimation()
        {
            Play("Static01", true);
        }


        /// <summary>
        /// 攻击
        /// </summary>
        public void PlayAttackAnimation()
        {
            Play("Skill01", false, true);
        }

        //是否存在攻击动作
        public bool IsExistAttack()
        {
            return aniNameList.Contains("Skill01");
        }

        /// <summary>
        /// 被攻击
        /// </summary>
        public void PlayHitAnimation()
        {
            Play("Hit01", false, true);
        }

        /// <summary>
        /// 死亡
        /// </summary>
        public void PlayDieAnimation()
        {
            Play("Die01");
        }

        /// <summary>
        /// 播放动画 3-待机 0-攻击 2-被攻击 4-死亡
        /// endPlayLastAnim 结束后是播回到之前的动画
        /// </summary>
        void Play(string aniName, bool isLoop = false, bool endPlayLastAnim = false)
        {
            //if (animations.Length < index)
            //    return;
            //skeletonGraphic.AnimationState.AddAnimation(0, animations[index], isLoop, 0);
            //if (index != 0 && skeletonGraphic != null)
            //    skeletonGraphic.AnimationState.AddAnimation(0, animations[0], true, 0);
            if (!aniNameList.Contains(aniName))
                return;

            TrackEntry track = skeletonGraphic.AnimationState.SetAnimation(0, aniName, isLoop);
            if (endPlayLastAnim)
                track.Complete += Track_Complete;
            else
            {
                _lastAnimName   = aniName;
                _lastAnimIsLoop = isLoop;
            }
        }

        private void Track_Complete(TrackEntry trackEntry)
        {
            trackEntry.Complete -= Track_Complete;
            Play(_lastAnimName, _lastAnimIsLoop);
        }


        public void Dispose()
        {
            skeletonGraphic = null;
            dataAsset       = null;
            animations      = null;
        }
    }
}