using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public IEnumerator Shake(float time, float speed)
    {
        Vector3 originPos = transform.localPosition;
        float doutime = 0f;
        while (doutime < time)
        {
            float x = Random.Range(-0.2f, 0.2f) * speed;
            transform.localPosition = new Vector3(x, originPos.y, originPos.z);
            doutime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;
    }
}
