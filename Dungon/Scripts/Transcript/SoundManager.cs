using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 声音管理类
/// 1，通过声音组件去控制声音的播放audiosorce.PlayOneShot
/// </summary>
public class SoundManager : MonoBehaviour {

    public static SoundManager _instance;
    private Dictionary <string,AudioClip > audioDict=new Dictionary <string,AudioClip >();
    public AudioClip[] audioClipArray;//声音文件的数组
    public AudioSource audioSource;
    public bool isQuiet = false;
    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        foreach(AudioClip ac in audioClipArray )
        {
            audioDict.Add(ac.name,ac);//第一个参数为audio的名字，第二个为实际的音频
        }
    }

    public void Play(string audioName)
    {
        if (isQuiet)
            return;
        AudioClip ac;
        if(audioDict.TryGetValue (audioName,out ac))
        {
            //AudioSource.PlayClipAtPoint(ac, Vector3.zero);//播放声音？？？？？？
            this.audioSource.PlayOneShot(ac);//看看这个？？？？？？(通过一个声音组件去播放 一段声音
        }
    }

    public void Play(string audioName,AudioSource audiosource)
    {
        if (isQuiet)
            return;
        AudioClip ac;
        if (audioDict.TryGetValue(audioName, out ac))
        {
            //AudioSource.PlayClipAtPoint(ac, Vector3.zero);//播放声音？？？？？？
            audioSource.PlayOneShot(ac);//看看这个？？？？？？
        }
    }
}
