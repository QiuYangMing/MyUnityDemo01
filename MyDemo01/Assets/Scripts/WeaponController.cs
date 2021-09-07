using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public WeaponManager wm;
    public WeaponData wdata;
    private void Awake()
    {
        wdata = GetComponentInChildren<WeaponData>();
        if (wdata == null )
        {
            return;
        }
    }

    public float GetATK()
    {
        if (wdata == null)
        {
            return wm.am.sm.ATK;
        }
        else
        {
            return wdata.ATK + wm.am.sm.ATK;

        }
    }
}
