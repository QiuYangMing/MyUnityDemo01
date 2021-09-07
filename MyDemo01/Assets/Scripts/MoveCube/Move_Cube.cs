using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move_Cube : MonoBehaviour
{

    protected string CubeName;
    public float value01;
    public float value02;
    protected float Speed;
    protected float value01_value02;

    protected virtual void Start()
    {
        value01_value02 = -1;
        Speed = 20f;

    }
    //以下为方块移动逻辑
    protected virtual void FixedUpdate()
    {
        if (value01_value02 > 0)
        {
            this.transform.Translate(new Vector3(0, 0.1f, 0) * Speed * Time.fixedDeltaTime);
        }
        else
        {
            this.transform.Translate(new Vector3(0, -0.1f, 0) * Speed * Time.fixedDeltaTime);
        }
        if (this.transform.localPosition.z > value01)
        {
            value01_value02 = -1;
        }
        else if (this.transform.localPosition.z < value02)
        {
            value01_value02 = 1;
        }
    }
    
    protected virtual void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<ActorController>().Move_CubeScript = this;

    }
    protected virtual void OnCollisionExit(Collision collision)
    {

        collision.gameObject.GetComponent<ActorController>().Move_CubeScript = null;

    }
    //滑块移动的方向
    public virtual float GetValue01_value02()
    {
        return value01_value02;
    }
    //滑块移动的速度
    public virtual float GetSpeed()
    {
        return Speed;
    }
}
