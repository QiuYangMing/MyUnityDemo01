using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnWeaponController : MonoBehaviour {

    public EnWeaponManager ewm;
    public WeaponData wdata;
    private void Awake()
    {
        wdata = GetComponentInChildren<WeaponData>();
        if (wdata == null)
        {
            return;
        }
    }

    public float GetATK()
    {
        return wdata.ATK + ewm.em.esm.Atk;
    }
}
