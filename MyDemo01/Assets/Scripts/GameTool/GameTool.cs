using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//游戏工具类，把常用的一些（通用）方法放在这个类里面
public class GameTool : MonoBehaviour {
    //清理内存的方法(一般在切换场景的时候调用)
    public static void ClaerMemory()
    {
        //手动调用垃圾回收，不能频繁的去调用，应该在适当的情况下去调用
        //因为垃圾回收会消耗很大的性能
        GC.Collect();
        //卸载内存中没用的资源
        Resources.UnloadUnusedAssets();
    }
    //操作内存，数据持久化
    //判断系统内存中是否存在某个键
    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
    //删除内存中所有的数据
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
    //删除内存中指定的数据
    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
    //获取值
    public static int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }
    public static float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }
    public static string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }
   
    //设置值（存值）
    public static void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }
    public static void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
    public static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
    //查找子物体
    public static Transform FindTheChild(GameObject goParent, string childName)
    {
        Transform searchTrans = goParent.transform.Find(childName);
        if (searchTrans == null)
        {
            foreach (Transform trans in goParent.transform)
            {
                //递归
                searchTrans = FindTheChild(trans.gameObject, childName);
                if (searchTrans != null)
                {
                    return searchTrans;
                }
            }
        }
        return searchTrans;
    }
    //获取子物体上面的组件
    public static T GetTheChildComponent<T>(GameObject goParent, string childName) where T : Component
    {
        Transform searchTrans = FindTheChild(goParent, childName);
        if (searchTrans != null)
        {
            return searchTrans.GetComponent<T>();
        }
        else
        {
            return null;
        }
    }
    //给子物体添加组件
    public static T AddTheChildComponent<T>(GameObject goParent, string childName) where T : Component
    {
        Transform searchTrans = FindTheChild(goParent, childName);
        if (searchTrans != null)
        {
            T[] arr = searchTrans.GetComponents<T>();
            for (int i = 0; i < arr.Length; i++)
            {
                //Destroy(arr[i]);//销毁，但是不会立刻销毁，在当前帧结束前销毁
                DestroyImmediate(arr[i], true);//立刻销毁
            }
            return searchTrans.gameObject.AddComponent<T>();
        }
        else
        {
            return null;
        }
    }
    //添加子物体
    public static void AddChildToParent(Transform parentTrans, Transform childTrans, bool isResetPs = true)
    {
        childTrans.parent = parentTrans;
        if (isResetPs)
        {
            childTrans.localPosition = Vector3.zero;
        }
        childTrans.localScale = Vector3.one;
    }
    
    //主场景是否第一次被加载
    public static bool isFirseLoad = true;
}
