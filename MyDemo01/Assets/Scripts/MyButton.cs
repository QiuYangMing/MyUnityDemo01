using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton
{
    /// <summary>
    ///长按
    /// </summary>
    public bool Ispressing = false;
    /// <summary>
    /// 按下瞬间
    /// </summary>
    public bool OnPressed = false;
    /// <summary>
    /// 松开瞬间
    /// </summary>
    public bool OnReleased = false;
    /// <summary>
    /// 制作连按的延伸时间
    /// </summary>
    public bool IsExtending = false;
    /// <summary>
    /// 长按信号
    /// </summary>
    public bool IsDelaying = false;
    private bool curState = false;
    private bool lastState = false;
    /// <summary>
    /// 连按信号延长的时间
    /// </summary>
    public float extendingDuration = 0.3f;
    /// <summary>
    /// 长按信号的推迟时间
    /// </summary>
    public float delayingDuration = 3f;
    /// <summary>
    /// 实例化计时器
    /// </summary>
    private MyTime extTimer = new MyTime();
    private MyTime delayTimer = new MyTime();

    public void Tick(bool input)
    {
        extTimer.Tick();
        delayTimer.Tick();
        curState = input;
        Ispressing = curState;
        OnPressed = false;
        OnReleased = false;
        IsExtending = false;
        IsDelaying = false;
        if (curState != lastState)
        {
            if (curState == true)
            {
                OnPressed = true;
                StartTimer(delayTimer, delayingDuration);
            }
            else
            {
                OnReleased = true;
                StartTimer(extTimer, extendingDuration);
            }
        }
        lastState = curState;
        if (extTimer.state == MyTime.STATE.RUN)
        {
            IsExtending = true;
        }
        if (delayTimer.state == MyTime.STATE.RUN)
        {
            IsDelaying = true;
        }
       
    }
    private void StartTimer(MyTime timer, float duration)
    {
        timer.duration = duration;
        timer.Go();
    }
}
