using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using kernel;

/// <summary>
/// 控制层：登录场景控制脚本
/// </summary>
namespace Controller
{
    public class LoginScenesCommand : Command
    {
        public static LoginScenesCommand Instance;
        public AudioClip backGround; 
        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            //确定音频的音量
            AudioManager.SetAudioBackgroundVolumns(0.5f);
            AudioManager.SetAudioEffectVolumns(1f);

            //播放背景音乐
            AudioManager.PlayBackground(backGround); 

        }

        // 播放少年剑侠的转换音效
        public void PlayAudioEffectOfSword()
        {
            AudioManager.PlayAudioEffectA("SwordHero_MagicA");
        }

        //播放魔杖真人的转换音效 
        public void PlayAudioEffectOfMagic()
        {
            AudioManager.PlayAudioEffectB("2_FireBallEffect_MagicHero"); 
        }

        //转到下一个场景
        
        public void EnterNextScenes()
        {
            //GlobleParameterMgr.nextScenesName = ScenesEnum.LevelOne;
            //SceneManager.LoadScene(ConvertEnumToString.GetInstance().GetStrByEnumScenes(ScenesEnum.LoadingScenes));
            base.EnterNextScenes(ScenesEnum.LevelOne);
        }

    }
}

