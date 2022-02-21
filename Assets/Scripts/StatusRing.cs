using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusRing : MonoBehaviour
{
    public GameObject unit;
    private float yPosition = 0.1f;
    private MeshRenderer unitMR;
    private Soldier unitSoldier;

    void Start()
    {
        unitMR = gameObject.GetComponent<MeshRenderer>();
        unitSoldier = unit.gameObject.GetComponent<Soldier>();
    }


    // Update is called once per frame
    void Update()
    {
        unitMR.enabled = unitSoldier.s_IsAttacking;
        Vector3 position = new Vector3(unit.transform.position.x, yPosition, unit.transform.position.z);
        transform.position = position;
        transform.eulerAngles = Vector3.up;
    }
}
