using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//单例模式（两种）
//1、不继承于MonoBehaviour
//2、有继承于MonoBehaviour
//区别：有继承于MonoBehaviour的可以使用脚本生命周期函数

//1、不继承于MonoBehaviour
public class Singleton<T> where T : new()
{

    //一个静态的字段
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
    protected Singleton()
    {

    }
}
//2、有继承于MonoBehaviour
public class UnitySingleton<T> : MonoBehaviour where T : Component
{
    private static GameObject unitySingletonObj;
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                if (unitySingletonObj == null)
                {
                    unitySingletonObj = GameObject.Find("UnitySingletonObj");
                    if (unitySingletonObj == null)
                    {
                        Debug.LogError("场景里面找不到UnitySingletonObj这个物体");
                    }
                    DontDestroyOnLoad(unitySingletonObj);
                }

                instance = unitySingletonObj.GetComponent<T>();
            }
            return instance;
        }
    }
        protected UnitySingleton()
        {

        }
}

