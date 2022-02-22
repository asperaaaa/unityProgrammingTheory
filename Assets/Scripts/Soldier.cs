using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Unit
{
    public bool s_IsAttacking;
    private bool s_IsResting;
    void Update()
    {
        CheckIfIsDied();

        if (IsDied)
        {
            s_IsAttacking = false;
        }
        else
        {
            if (u_IsCloseToTarget)
            {
                StartAttack();
                checkIfEnemyDied();
            }
            else
            {
                StopAttack();
                SearchTarget();
            }
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

        bool hitEnemy = UnitUtils.TossCoin();

        if (s_IsResting)
        {
            return;
        }

        if (hitEnemy)
        {
            u_Enemy.gameObject.GetComponent<Soldier>().u_Life -= 20; 
        }
        StartCoroutine(Resting());
    }

    void StopAttack()
    {
        s_IsAttacking = false;
        u_Rb.constraints = RigidbodyConstraints.None;
    }

    void checkIfEnemyDied()
    {
        if (u_Enemy.gameObject.GetComponent<Soldier>().IsDied)
        {
            u_Enemy = null;
            u_IsCloseToTarget = false;
        }
    }

    IEnumerator Resting()
    {
        s_IsResting = true;
        yield return new WaitForSeconds(1);
        s_IsResting = false;
    }
}
