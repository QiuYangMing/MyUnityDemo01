using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(5)]

public class EnWeaponManager : IActorMnagerInterface {

    private EnWeaponController whR;
    private EnWeaponController whL;
    private BoxCollider weaponBoxR;
    private BoxCollider WeaponBoxL;
    private GameObject ArrowObj;
    private GameObject bow;
    private AudioClip[] audioClip;
    private void Start()
    {
        whR = transform.DeepFind("weaponHandleR").GetComponent<EnWeaponController>();
        whR.ewm = this;
        
        if (em.bc.ei.EnName == "BlackKnight")
        {
        whL = transform.DeepFind("weaponHandleL").GetComponent<EnWeaponController>();
        whL.ewm = this;
        WeaponBoxL = whL.GetComponentInChildren<BoxCollider>();
        ArrowObj = Resources.Load<GameObject>("EnArrow");
        bow = whL.transform.Find("Bow").gameObject;
        }
        weaponBoxR = whR.GetComponentInChildren<BoxCollider>();
        audioClip = Resources.LoadAll<AudioClip>("Audio/AttackEffectMusic");
    }

    public EnWeaponController BindWeaponController(GameObject targetobj)
    {
        EnWeaponController tempWc;
        tempWc = targetobj.GetComponent<EnWeaponController>();
        if (tempWc == null)
        {
            tempWc = targetobj.AddComponent<EnWeaponController>();
        }
        tempWc.ewm = this;
        return tempWc;
    }
    private void WeaponEnableL()
    {
        WeaponBoxL.enabled = true;
        em.esm.isCanfly = false;
    }
    private void WeaponDisableL()
    {
        WeaponBoxL.enabled = false;
        em.esm.isCanfly = false;
    }
    private void WeaponEnable()
    {
        if (GameTool.HasKey("isCloseAudio"))
        {
            if (!bool.Parse(GameTool.GetString("isCloseAudio")))
            {
                em.audioS.clip = audioClip[Random.Range(1, 4)];
                em.audioS.Play();
            }
        }
      
        weaponBoxR.enabled = true;
        em.esm.isCanfly = false;
    }
    public void WeaponDisable()
    {
        weaponBoxR.enabled = false;
        em.esm.isCanfly = false;

    }
    private void WeaponCanFlyE()
    {
        weaponBoxR.enabled = true;
        em.esm.isCanfly = true;
    }
    private void WeaponCanFlyD()
    {
        weaponBoxR.enabled = false;
        em.esm.isCanfly = false;
    }
    private void OnfireArrow()
    {
        GameObject arrow = Instantiate(ArrowObj, whR.transform.position, Quaternion.identity);
    }
    private void ShowBow()
    {
        WeaponBoxL.gameObject.SetActive(false);
        weaponBoxR.gameObject.SetActive(false);
        bow.SetActive(true);
    }
    private void HideBow()
    {
        bow.SetActive(false);
        weaponBoxR.gameObject.SetActive(true);
        WeaponBoxL.gameObject.SetActive(true);

    }
}
