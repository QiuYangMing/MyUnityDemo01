using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Unity3D游戏引擎一共支持4个音乐格式的文件
//.AIFF 适用于较短的音乐文件可用作游戏打斗音效
//.WAV  适用于较短的音乐文件可用作游戏打斗音效
//.MP3 适用于较长的音乐文件可用作游戏背景音乐
//.OGG 适用于较长的音乐文件可用作游戏背景音乐
public class AudioManager : UnitySingleton<AudioManager>
{
    //背景音乐
    private AudioSource audioSource_Music;
    private AudioClip[] musicClip ;
    //音效
    private AudioSource audioSource_MusicEffect;
    private Dictionary<string, AudioClip> musicEffectClipDic = new Dictionary<string, AudioClip>();
    //音乐音量的大小
    private float musicVolume;

    public float MusicVolume
    {
        get { return musicVolume; }
        set { musicVolume = value; }
    }
    //音效音量的大小
    private float musicEffectVolume;

    public float MusicEffectVolume
    {
        get { return musicEffectVolume; }
        set { musicEffectVolume = value; }
    }
    //是否静音(默认不关闭)
    private bool isCloseAudio = false;
    public bool IsCloseAudio
    {
        get { return isCloseAudio; }
        set { isCloseAudio = value; }
    }
    void Awake()
    {
        InitAudio();
    }

    private void Update()
    {
        if (!audioSource_Music.isPlaying)
        {
            audioSource_Music.Play();
        }
    }
    public void InitAudio()
    {
        audioSource_Music = GameTool.GetTheChildComponent<AudioSource>(this.transform.parent.gameObject, "AudioSource_Music");
        musicClip = Resources.LoadAll<AudioClip>("Audio/Music");
        audioSource_Music.clip = musicClip[0];
        audioSource_MusicEffect = GameTool.GetTheChildComponent<AudioSource>(this.transform.parent.gameObject, "AudioSource_MusicEffect");
        AudioClip[] musicEffectClips = Resources.LoadAll<AudioClip>("Audio/MusicEffect");
        for (int i = 0; i < musicEffectClips.Length; i++)
        {
            musicEffectClipDic.Add(musicEffectClips[i].name, musicEffectClips[i]);
        }
        //判断内存中是否有记录静音的值
        if (GameTool.HasKey("isCloseAudio"))
        {
            isCloseAudio = bool.Parse(GameTool.GetString("isCloseAudio"));
        }
        else
        {
            GameTool.SetString("isCloseAudio", "false");
            isCloseAudio = false;
        }
        //判断内存中是否有音乐以及音效的值
        if (GameTool.HasKey("MusicVolume"))
        {
            musicVolume = GameTool.GetFloat("MusicVolume");
        }
        else
        {
            musicVolume = 0.5f;
        }
        SetMusicValue(musicVolume);
        if (GameTool.HasKey("MusicEffectVolume"))
        {
            musicEffectVolume = GameTool.GetFloat("MusicEffectVolume");
        }
        else
        {
            musicEffectVolume = 0.5f;
        }
        SetMusicEffectValue(musicEffectVolume);
        //播放背景音乐
        PlayOrPauseMusic(isCloseAudio);
    }
    //供外界调用，播放或者暂停音乐的方法(传入true代表静音，false代表不静音)
    public void PlayOrPauseMusic(bool isClose)
    {
        if (audioSource_Music.clip == null)
        {
            return;
        }
        if (isClose)
        {
            //静音
            audioSource_Music.Pause();
            isCloseAudio = true;
        }
        else
        {
            //播放
            audioSource_Music.Play();
            isCloseAudio = false;
        }
        GameTool.SetString("isCloseAudio", isClose.ToString());
    }
    //供外界调用，播放音效的方法
    public void PlayEffectMusic(string name)
    {
        
        if (!isCloseAudio)
        {
            if (musicEffectClipDic.ContainsKey(name))
            {
                audioSource_MusicEffect.clip = musicEffectClipDic[name];
                audioSource_MusicEffect.Play();
            }

        }

    }
    //供外界调用，切换音乐的方法
    public void PlayMusic(int number)
    {
       

        if (!isCloseAudio)
        {
            if (musicClip.Length > number)
            {

                audioSource_Music.clip = musicClip[number];
                audioSource_Music.Play();
            }

        }

    }
    //供外界调用，调节音量大小的方法
    public void SetMusicValue(float value)
    {
        audioSource_Music.volume = value;
        GameTool.SetFloat("MusicVolume", value);
    }
    public void SetMusicEffectValue(float value)
    {
        audioSource_MusicEffect.volume = value;
        GameTool.SetFloat("MusicEffectVolume", value);
    }
}
