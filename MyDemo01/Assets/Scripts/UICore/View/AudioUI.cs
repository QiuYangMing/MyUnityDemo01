using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioUI : View
{
    private Button btn_Close;
    private Toggle toggle_IsCloseAudio;
    //音乐的滑动条
    private Slider slider_Music;
    //音效的滑动条
    private Slider slider_MusicEffect;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_Close = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Close");
        btn_Close.onClick.AddListener(Close);
        toggle_IsCloseAudio = GameTool.GetTheChildComponent<Toggle>(this.gameObject, "Toggle_IsCloseAudio");
        toggle_IsCloseAudio.onValueChanged.AddListener(OpenOrCloseAudio);
        slider_Music = GameTool.GetTheChildComponent<Slider>(this.gameObject, "Slider_Music");
        slider_MusicEffect = GameTool.GetTheChildComponent<Slider>(this.gameObject, "Slider_MusicEffect");
        slider_Music.onValueChanged.AddListener(SetMusic);
        slider_MusicEffect.onValueChanged.AddListener(SetMusicEffect);
        //判断是否有静音
        if (AudioManager.Instance.IsCloseAudio)
        {
            toggle_IsCloseAudio.isOn = true;
        }
        slider_Music.value = AudioManager.Instance.MusicVolume;
        slider_MusicEffect.value = AudioManager.Instance.MusicEffectVolume;
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.AudioUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;
        this.uiType.uiRootType = E_UIRootType.KeepAbove;
    }
    public override string Name
    {
        get
        {
            return uiId.ToString();
        }
    }

    public override void HandEvent(string eventName, object data)
    {
        
    }
    //设置背景音乐的音量大小
    private void SetMusic(float value)
    {
        // Debug.Log("背景音乐的音量为"+value);
        AudioManager.Instance.SetMusicValue(value);
    }
    //设置音效音量的大小
    private void SetMusicEffect(float value)
    {
        // Debug.Log("音效的音量为" + value);
        AudioManager.Instance.SetMusicEffectValue(value);
    }
    private void OpenOrCloseAudio(bool isClose)
    {
        if (isClose)
        {
           
            AudioManager.Instance.PlayOrPauseMusic(true);
        }
        else
        {
           
            AudioManager.Instance.PlayOrPauseMusic(false);
        }
    }
    private void Close()
    {
        UIManager.Instance.HideSingleUI(this.uiId);
    }
}
