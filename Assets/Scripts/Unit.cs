// ENCAPSULATION

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    private Rigidbody u_Rb;
    private float u_GoToEnemySpeed = 3.0f;
    private float u_GoToHomeSpeed = 1.0f;

    //private bool isAtHome;
    //private bool isAttacking;
    //private bool isDied;

    protected int u_Life = 100;

    public GameObject u_Home;

    void Awake()
    {
        u_Rb = GetComponent<Rigidbody>();
    }

    //POLYMORPHISM
    protected abstract void SearchEnemy();

    //POLYMORPHISM
    public virtual void GoTo(Vector3 position)
    {
        u_Rb.AddForce(position * u_GoToEnemySpeed);        
    }

    //POLYMORPHISM
    public virtual void GoTo()
    {
        Vector3 lookDirection = (u_Home.transform.position - transform.position).normalized;
        u_Rb.AddForce(lookDirection * u_GoToHomeSpeed);
    }
}
