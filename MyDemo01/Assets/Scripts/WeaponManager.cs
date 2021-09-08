using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[DefaultExecutionOrder(10)]
public class WeaponManager : IActorMnagerInterface
{
    private WeaponController whR;
    private WeaponController whL;
    private BoxCollider weaponBox;
    private GameObject weaponR;
    private GameObject fireWeapon;
    private AudioSource audioS;
    private AudioClip[] audioClip;
    private void Awake()
    {
        whR = transform.DeepFind("weaponHandleR").GetComponent<WeaponController>();
        whR.wm = this;
        whL = transform.DeepFind("weaponHandleL").GetComponent<WeaponController>();
        whL.wm = this;
        weaponBox = whR.GetComponentInChildren<BoxCollider>();
        weaponR = weaponBox.gameObject;
        fireWeapon = Resources.Load<GameObject>("FireWeapon");
    }
    private void Start()
    {
        audioS = am.transform.Find("Audio").GetComponent<AudioSource>();
        audioClip = Resources.LoadAll<AudioClip>("Audio/AttackEffectMusic");
    }
    public WeaponController BindWeaponController(GameObject targetobj)
    {
        WeaponController tempWc;
        tempWc = targetobj.GetComponent<WeaponController>();
        if (tempWc == null)
        {
            tempWc = targetobj.AddComponent<WeaponController>();
        }
        tempWc.wm = this;
        return tempWc;
    }
    //动画事件
    private void CounterBackEnable()
    {
        am.SetIsCounterBack(true);
    }
    private void CounterBackDisable()
    {
        am.SetIsCounterBack(false);
    }
    private void WeaponEnable()
    {
        if (GameTool.HasKey("isCloseAudio"))
        {
            if (!bool.Parse(GameTool.GetString("isCloseAudio")))
            {
                audioS.clip = audioClip[Random.Range(1, 4)];
                audioS.Play();
            } 
        }
        GameData.firestHit = true;
        weaponBox.enabled = true;
    }
    public void WeaponDisable()
    {
        weaponBox.enabled = false;
        GameData.firestHit = false;

    }
    private void WeaponCanFlyE()
    {
        if (GameTool.HasKey("isCloseAudio"))
        {
            if (!bool.Parse(GameTool.GetString("isCloseAudio")))
            {
                audioS.clip = audioClip[1];
                audioS.Play();
            }
        }
        GameData.firestHit = true;
        weaponBox.enabled = true;
        am.sm.isCanfly = true;
    }
    private void WeaponCanFlyD()
    {
        weaponBox.enabled = false;
        am.sm.isCanfly = false;
        GameData.firestHit = false;
    }
    public void FireWeaponEnable()
    {
        weaponBox.enabled = true;
        GameData.firestHit = true;
        GameObject fireWeaponObj = Instantiate(fireWeapon, whR.transform.position, weaponR.transform.rotation);
        weaponR.SetActive(false);
        fireWeaponObj.GetComponent<FireWeapon>().wc = whR;
        try
        {
            Vector3 position = am.ac.cancom.lockTarget.obj.transform.position;
            fireWeaponObj.transform.DOMove(position + new Vector3(0, 0.2f, 0), 0);
            Destroy(fireWeaponObj, 0.1f);
            StartCoroutine(MoveToFireWeapon(position));
        }
        catch (System.Exception)
        {

            Destroy(fireWeaponObj, 0.1f);
        }
        


        //am.ac.transform.position = am.ac.cancom.lockTarget.obj.transform.position - am.ac.cancom.lockTarget.obj.transform.forward*0.5f;

    }
    public void FireWeaponDisable()
    {
        weaponBox.enabled = false;
        GameData.firestHit = false;
        weaponR.SetActive(true);

    }

    IEnumerator MoveToFireWeapon(Vector3 position)
    {
        yield return new WaitForSeconds(0.5f);
        am.transform.DOMove(Vector3.MoveTowards(position, am.transform.position, 0.5f), 0.2f);
        am.transform.DOLookAt(position, 0.2f);
        weaponR.SetActive(true);

    }




}
