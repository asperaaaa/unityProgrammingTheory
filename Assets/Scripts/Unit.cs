// ENCAPSULATION

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{

    [SerializeField]
    private GameObject u_Home;
    [SerializeField]
    private int u_Life = 100;
    private float u_GoToEnemySpeed = 3.0f;
    private float u_GoToHomeSpeed = 1.0f;
    private float u_DieDimension = 0.01f;
    private bool u_IsAtHome;
    private bool u_IsDied = false;

    [SerializeField]
    protected bool u_IsCloseToTarget;
    [SerializeField]
    protected string u_EnemyTag;
    [SerializeField]
    protected GameObject u_Enemy = null;
    protected Rigidbody u_Rb;
    

    public bool IsDied
    {
        get { return u_IsDied; }
    }

    void Awake()
    {
        u_Rb = GetComponent<Rigidbody>();
    }

    //POLYMORPHISM
    protected abstract void SearchTarget();

    //POLYMORPHISM
    protected virtual void GoTo(GameObject target)
    {
        CheckIfIsDied();

        if (!u_IsDied)
        {            
            UnitUtils.DrawLine(gameObject, target);
            Vector3 lookDirection = (target.transform.position - transform.position).normalized;
            u_Rb.AddForce(lookDirection * u_GoToEnemySpeed);
        }
    }

    //POLYMORPHISM
    protected virtual void GoTo()
    {
        CheckIfIsDied();
        if (!u_IsAtHome && !u_IsDied)
        {            
            float lookZDirection = Mathf.Sign(u_Home.transform.position.z - transform.position.z);
            Vector3 lookDirection = lookZDirection * Vector3.forward;
            u_Rb.AddForce(lookDirection * u_GoToHomeSpeed);            
        }
    }

    public bool GetIfUnitIsDied()
    {
        return true;
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
        transform.eulerAngles = Vector3.up;
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

        if (u_Enemy != null && u_Enemy.name == other.gameObject.name)
        {
            u_IsCloseToTarget = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        string u_HomeTag = u_Home.gameObject.tag;
        if (other.gameObject.CompareTag(u_HomeTag))
        {
            u_IsAtHome = false;
        }

        
        if (u_Enemy != null && u_Enemy.name == other.gameObject.name)
        {
         u_IsCloseToTarget = false;
        }        
    }   
}
