using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScene : UnitySingleton<AttackScene>
{
    public bool isShake;
    public bool isWaiting = false;
    public void HitPaues(Animator anim, float speed, int duration)
    {
        if (isWaiting)
        {
            return;
        }
        StartCoroutine(Pause(anim, speed, duration));
    }
    public void CameraShake(float duration, float strength)
    {
        if (!isShake)
        {
            StartCoroutine(Shake(duration, strength));
        }
    }
    IEnumerator Pause(Animator anim, float speed,int duration)
    {
        isWaiting = true;
        float pauseTime = duration / 60f;
        float defaultSpeed = anim.speed;
        anim.speed = speed;
        //Time.timeScale = 0;
        yield return new WaitForSeconds(0.1f);
        anim.speed = defaultSpeed;
        isWaiting = false;
        //Time.timeScale = 1;
    }
    IEnumerator Shake(float duration, float strength)
    {
        isShake = true;
        Transform camera = Camera.main.transform;
        Vector3 startPosition = camera.position;

        while (duration > 0)
        {
            camera.position = Random.insideUnitSphere * strength + startPosition;
            duration -= Time.deltaTime;
            yield return null;
        }
        isShake = false;
    }
}
