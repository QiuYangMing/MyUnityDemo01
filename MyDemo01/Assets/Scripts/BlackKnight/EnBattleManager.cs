using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnBattleManager :  IActorMnagerInterface
{
    public int num = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (!GameData.firestHit)
        {
            return;
        }
        WeaponController targetWc = other.GetComponentInParent<WeaponController>();
        if (targetWc == null)
        {
            targetWc = other.GetComponent<FireWeapon>().wc;
        }
        if (targetWc == null)
        {
            return;
        }
        GameData.firestHit = false;
        GameObject attacker = targetWc.wm.am.gameObject;
        GameObject receiver = em.gameObject;
        Vector3 attackingDir = receiver.transform.position - attacker.transform.position;
        Vector3 counterDir = attacker.transform.position - attacker.transform.position;
        float attackingAngle1 = Vector3.Angle(attacker.transform.forward, attackingDir);
        float counterAngle1 = Vector3.Angle(receiver.transform.forward, counterDir);
        float counterAngle2 = Vector3.Angle(attacker.transform.position, receiver.transform.forward);
        bool attackValid = (attackingAngle1 < 15);
        bool counterValid = (counterAngle1 < 70 && Mathf.Abs(counterAngle2 - 180) < 70);
        if (other.tag == "Weapon")
        {
            em.TryDoDamage(targetWc, attackValid, counterValid);
           
        }
    }
}
