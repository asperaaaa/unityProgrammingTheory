using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Unit
{
    public bool s_IsAttacking;

    void Update()
    {
        if (u_IsCloseToTarget)
        {
            StartAttack();
        }
        else
        {
            StopAttack();
            SearchTarget();
        }
    }

    //POLYMORPHISM
    protected override void SearchTarget()
    {
        if (u_Enemy == null)
        {
            List<GameObject> targets = UnitUtils.FindEnemies(u_EnemyTag);
            if (targets.Count > 0)
            {
                int index = Random.Range(0, targets.Count);
                u_Enemy = targets[index];

                //POLYMORPHISM
                GoTo(u_Enemy);
            }
            else
            {
                //POLYMORPHISM
                GoTo();
            }
        }
        else
        {
            if (u_Enemy.gameObject.GetComponent<Soldier>().IsDied)
            {
                u_Enemy = null;
            }
            else
            {
                //POLYMORPHISM
                GoTo(u_Enemy);
            }
        }
    }

    void StartAttack()
    {
        s_IsAttacking = true;
        u_Rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        //UnitUtils.DrawLine(gameObject, u_Enemy);
    }

    void StopAttack()
    {
        s_IsAttacking = false;
        u_Rb.constraints = RigidbodyConstraints.None;
    }
}
