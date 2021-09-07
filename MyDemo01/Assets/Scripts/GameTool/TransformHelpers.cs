using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformHelpers
{
    public static Transform DeepFind(this Transform parent,string targetName)
    {
        Transform searchTrans = null;
        foreach (Transform child in parent)
        {
            if (child.name == targetName)
            {
                return child;
            }
            searchTrans = DeepFind(child,targetName);
            if (searchTrans != null)
            {
                return searchTrans;
            }
        }
        return searchTrans;
    }
	
}
