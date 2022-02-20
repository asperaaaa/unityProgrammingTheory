using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class UnitB : Unit
{    
    private string u_EnemyTag = "UnitA";

    void Update()
    {
        SearchEnemy();
    }

    //POLYMORPHISM
    protected override void SearchEnemy()
    {
        GameObject[] targets = GetListEnemies();

        if (targets.Length > 0)
        {
            Vector3 lookDirection = (targets[0].transform.position - transform.position).normalized;
            //POLYMORPHISM
            GoTo(lookDirection);
        }
        else
        {
            //POLYMORPHISM
            GoTo();
        }
    }

    // ABSTRACTION
    private GameObject[] GetListEnemies()
    {
        return GameObject.FindGameObjectsWithTag(u_EnemyTag);
    }
}
