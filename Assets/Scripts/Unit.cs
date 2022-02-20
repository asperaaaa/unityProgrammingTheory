// ENCAPSULATION

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    private Rigidbody u_Rb;
    private float u_GoToEnemySpeed = 3.0f;
    private float u_GoToHomeSpeed = 1.0f;
    private float u_DieDimension = 0.01f;
    private bool u_IsAtHome;
    private bool u_IsDied = false;

    //private bool isAttacking;
    [SerializeField]
    protected int u_Life = 100;

    public GameObject u_Home;

    void Awake()
    {
        u_Rb = GetComponent<Rigidbody>();
    }

    //POLYMORPHISM
    protected abstract void SearchTarget();

    //POLYMORPHISM
    protected virtual void GoTo(Vector3 position)
    {
        if (!u_IsDied)
        {
            CheckIfIsDied();
            u_Rb.AddForce(position * u_GoToEnemySpeed);
        }
    }

    //POLYMORPHISM
    protected virtual void GoTo()
    {
        if (!u_IsAtHome && !u_IsDied)
        {
            CheckIfIsDied();
            float lookZDirection = Mathf.Sign(u_Home.transform.position.z - transform.position.z);
            Vector3 lookDirection = lookZDirection * Vector3.forward;
            u_Rb.AddForce(lookDirection * u_GoToHomeSpeed);
        }
    }

    // ABSTRACTION
    void CheckIfIsDied()
    {
        if (u_Life < 1)
        {
            Die();
        }
    }

    // ABSTRACTION
    void Die()
    {
        u_IsDied = true;
        u_Rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        SphereCollider u_SC = GetComponent<SphereCollider>();
        u_SC.radius = u_DieDimension;
        transform.localScale = new Vector3(1, u_DieDimension, 1);
    }

    void OnTriggerEnter(Collider other)
    {
        string u_HomeTag = u_Home.gameObject.tag;
        if (other.gameObject.CompareTag(u_HomeTag))
        {
            u_IsAtHome = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        string u_HomeTag = u_Home.gameObject.tag;
        if (other.gameObject.CompareTag(u_HomeTag))
        {
            u_IsAtHome = false;
        }
    }
}
