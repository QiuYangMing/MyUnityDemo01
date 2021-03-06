using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory
{
    private MyDataBase weaponDB;
    public WeaponFactory(MyDataBase _weaponDB)
    {
        weaponDB = _weaponDB;
    }

    public GameObject CreateWeapon(string weaponName,Vector3 pos,Quaternion rot)
    {
        GameObject prefab = Resources.Load(weaponName) as GameObject;
        GameObject obj = GameObject.Instantiate(prefab,pos,rot);
        AddWeaponData(obj, weaponName, "ATK");
        return obj;
    }
    public GameObject CreateWeapon(string weaponName, Transform parent)
    {
        GameObject prefab = Resources.Load(weaponName) as GameObject;
        GameObject obj = GameObject.Instantiate(prefab);
        obj.transform.parent = parent;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        AddWeaponData(obj, weaponName, "ATK");
        return obj;
    }
    
    private void AddWeaponData(GameObject obj, string weaponName,string proprety)
    {
        WeaponData wdata = obj.AddComponent<WeaponData>();
        wdata.ATK = weaponDB.enemtDataBase[weaponName][proprety].f;
    }

}
