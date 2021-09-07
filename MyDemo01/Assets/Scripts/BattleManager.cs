using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : IActorMnagerInterface
{

    private void OnTriggerEnter(Collider other)
    {
        EnWeaponController targetWc = other.GetComponentInParent<EnWeaponController>();
        if (targetWc == null)
        {
            return;
        }
        GameObject attacker = targetWc.ewm.em.gameObject;
        GameObject receiver = am.gameObject;
        Vector3 attackingDir = receiver.transform.position - attacker.transform.position;
        Vector3 counterDir = attacker.transform.position - attacker.transform.position;

        float attackingAngle1 = Vector3.Angle(attacker.transform.forward,attackingDir);
        float counterAngle1 = Vector3.Angle(receiver.transform.forward,counterDir);
        float counterAngle2 = Vector3.Angle(attacker.transform.position, receiver.transform.forward);//should be closed to 180 degree

        bool attackValid = (attackingAngle1 < 15);
        bool counterValid = (counterAngle1 < 70 && Mathf.Abs(counterAngle2 - 180) < 70);
        if (other.tag =="Weapon")
        {
            am.TryDoDamage(targetWc,attackValid,counterValid);
        }
    }
}
