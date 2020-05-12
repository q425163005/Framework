using CSF.Tasks;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace HotFix_Project.Sound
{
    public class SoundMgr
    {
        private GameObject gameObject;

        //private int bgMusicIndex = 0;
        //背景音乐名称集合，用于随机背景音乐
        private string[]    bgMusicList = new string[] {"bg001"};
        private AudioSource loopSource;
        private AudioSource oneShotSource;

        private bool _playEffect = true;

        public bool isPlayEffect
        {
            set
            {
                _playEffect          = value;
                oneShotSource.volume = value ? 1 : 0;
                PlayerPrefs.SetInt("AudioEffect", value ? 0 : 1);
                PlayerPrefs.Save();
            }
            get => _playEffect;
        }

        private bool _playMusic = true;

        public bool isPlayMusic
        {
            set
            {
                _playMusic        = value;
                loopSource.volume = value ? 1 : 0;
                PlayerPrefs.SetInt("AudioMusic", value ? 0 : 1);
                PlayerPrefs.Save();
            }
            get => _playMusic;
        }


        /// <summary>
        /// 声音管理器
        /// </summary>
        public SoundMgr()
        {
            gameObject = new GameObject("__SoundMgr");
            GameObject.DontDestroyOnLoad(gameObject);


            if (loopSource == null)
            {
                loopSource             = gameObject.AddComponent<AudioSource>();
                loopSource.loop        = true;
                loopSource.playOnAwake = false;
            }

            if (oneShotSource == null)
            {
                oneShotSource          = gameObject.AddComponent<AudioSource>();
                oneShotSource.playOnAwake = false;
            }

            isPlayEffect = PlayerPrefs.GetInt("AudioEffect") == 0;
            isPlayMusic  = PlayerPrefs.GetInt("AudioMusic")  == 0;
        }

        /// <summary>
        /// 播放声音，多个同名声音可同时播放
        /// </summary>
        /// <param name="audioEnum"></param>
        public void PlaySound(AudioEnum audioEnum)
        {
            PlaySound(audioEnum.ToString());
        }

        public void PlaySound(string audioName)
        {
            playSound(audioName).Run();
        }

        private async CTask playSound(string audioName)
        {
            AudioClip clip = await LoadHelper.LoadSound(audioName);
            if (oneShotSource == null)
                oneShotSource = gameObject.AddComponent<AudioSource>();
            oneShotSource.PlayOneShot(clip);
        }


        //播放背景音乐 登陆完成播放背景-战斗结束播放
        public async CTask PlayBkgMusic()
        {
            //随机一个背景音乐索引
            System.Random rd    = new System.Random();
            int           index = rd.Next(0, bgMusicList.Length);
            if (index > bgMusicList.Length)
            {
                index = 0;
                CSF.CLog.Error("背景音乐随机数大于背景音乐数据--" + index + "  当前数组大小  " + bgMusicList.Length);
            }

            string bkgName = bgMusicList[index];
            //播放背景音乐
            if (loopSource.isPlaying) loopSource.Stop();
            loopSource.clip = await LoadHelper.LoadSound(bkgName);
            loopSource.Play();
            //Play(currAudioEnum, true, true, true, 1, 1);
        }

        public async CTask PlayWarBGAudio()
        {
            if (loopSource.isPlaying) loopSource.Stop();
            loopSource.clip = await LoadHelper.LoadSound(AudioEnum.war_run.ToString());
            loopSource.Play();
        }


        //停止播放背景音乐，战斗开始停止播放-登陆界面停止播放
        public void StopBkgMusic()
        {
            loopSource.Stop();
        }

        public void Dispose()
        {
        }
    }
}