using UnityEngine;

public class UIParticleRealtime : MonoBehaviour
{
    private ParticleSystem[] _particles;
    private float _deltaTime;
    private float _timeAtLastFrame;


    void Start()
    {
        _particles = GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (_particles.Length==0) return;
        _deltaTime = Time.realtimeSinceStartup - _timeAtLastFrame;
        _timeAtLastFrame = Time.realtimeSinceStartup;

        //若Time.timeScale等于0，说明游戏暂停，则让特效每帧播放，而不受其影响
        if (Time.timeScale <0.001f)
        {
            for (int i = 0; i < _particles.Length; i++)
            {
                _particles[i].Simulate(_deltaTime, false, false);
                _particles[i].Play();
            }
        }
    }
}