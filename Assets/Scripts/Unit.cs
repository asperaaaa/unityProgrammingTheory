// ENCAPSULATION

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public int u_Life = 100;    
    public GameObject u_Home;
    public string u_EnemyTag;

    private float u_GoToEnemySpeed = 3.0f;
    private float u_GoToHomeSpeed = 1.0f;
    private float u_DieDimension = 0.01f;
    [SerializeField]
    private bool u_IsAtHome;    
    private bool u_IsDied = false;
    
    protected bool u_IsCloseToTarget;
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
        if (!u_IsAtHome && !u_IsDied)
        {            
            float lookZDirection = Mathf.Sign(u_Home.transform.position.z - transform.position.z);
            Vector3 lookDirection = lookZDirection * Vector3.forward;
            u_Rb.AddForce(lookDirection * u_GoToHomeSpeed);
            UnitUtils.CleanLine(gameObject);
        }
    }

    public bool GetIfUnitIsDied()
    {
        return true;
    }

    // ABSTRACTION
    public void CheckIfIsDied()
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
        UnitUtils.CleanLine(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.name == u_Home.gameObject.name)
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
        if (other.gameObject.name == u_Home.gameObject.name)
        {
            u_IsAtHome = false;
        }
        
        if (u_Enemy != null && u_Enemy.name == other.gameObject.name)
        {
         u_IsCloseToTarget = false;
        }        
    }

    private void OnTriggerStay(Collider other)
    {
        if (u_Enemy != null && u_Enemy.name == other.gameObject.name)
        {
            u_IsCloseToTarget = true;
        }
    }
}
