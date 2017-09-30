using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Sound:Singleton <Sound>
{
    public string ResourceDir = "";
    private AudioSource m_bgSound;
    private AudioSource m_effectSound;

    protected override void Awake()
    {
        base.Awake();

        //添加播放背景音乐的AudioSource
        m_bgSound = this.gameObject.AddComponent<AudioSource>();
        m_bgSound.playOnAwake = false;
        m_bgSound.loop = true;

        m_effectSound = this.gameObject.AddComponent<AudioSource>();

    }


    
    public float BgVolume
    {
        //音乐大小
        get
        {
            return m_bgSound.volume;
        }
        set
        {
            m_bgSound.volume = value;
        }
    }


    
    public float EffectVolume
    {
        //音效大小
        get
        {
            return m_effectSound.volume;
        }
        set
        {
            m_effectSound.volume = value;
        }
    }



    
    public void PlayerBg(string audioName)
    {
        //播放音乐

        string oldName;

        //获取当前正在播放的音频文件
        if(m_bgSound .clip ==null)
        {
            oldName = null;
        }
        else
        {
            oldName = m_bgSound.clip.name;
        }

        if(oldName !=audioName )
        {
            //新的音乐

            string path;
            if(string.IsNullOrEmpty (ResourceDir ))
            {
                path = audioName;
            }
            else
            {
                path = ResourceDir + "/" + audioName;
            }

            //加载
            AudioClip clip = Resources.Load<AudioClip>(path);


            //播放
            if(clip !=null)
            {
                m_bgSound.clip = clip;//先将磁带放进去
                m_bgSound.Play();//在播放
            }
        }

    }


    
    public void StopBg()
    {
        //停止音乐
        m_bgSound.Stop();
        m_bgSound.clip = null;
    }


    //播放音效
    public void PlayerEffect(string audioName)
    {

        //路径
        string path;
        if (string.IsNullOrEmpty(ResourceDir))
        {
            path = audioName;
        }
        else
        {
            path = ResourceDir + "/" + audioName;
        }

        //音频
        AudioClip clip = Resources.Load<AudioClip>(path);

        //播放
        m_effectSound.PlayOneShot(clip);
    }
}
