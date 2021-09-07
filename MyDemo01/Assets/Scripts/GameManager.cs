using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    private MyDataBase weaponDB;
    private WeaponFactory weaponFact;
	void Awake ()
    {
        CheckSingle();
        CheckGameObject();
    }

    private void Start()
    {
       
        InitWeaponDB();
        InitWeaponFactory();


    }
    private void InitWeaponFactory()
    {
        weaponFact = new WeaponFactory(weaponDB);
    }
    private void InitWeaponDB()
    {
        weaponDB = new MyDataBase("level1Enemy");
    }
    private void CheckSingle()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(this);
    }
    private void CheckGameObject()
    {
        if (tag == "GM")
        {
            return;
        }
        Destroy(this);
    }
}
