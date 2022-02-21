using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ABSTRACTION
public class UnitUtils : MonoBehaviour
{

    public static List<GameObject> FindEnemies(string enemiesTag)
    {
        List<GameObject> enemies = new List<GameObject>();

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag(enemiesTag))
        {
            if (!enemy.gameObject.GetComponent<Soldier>().IsDied)
            {
                enemies.Add(enemy);
            }
        }

        return enemies;
    }

    public static void DrawLine(GameObject from, GameObject to)
    {
        LineRenderer line = from.GetComponent<LineRenderer>();
        if (!line)
        {
            line = from.AddComponent<LineRenderer>();
        }

        line.enabled = true;
        line.startColor = Color.black;
        line.endColor = Color.black;

        // set width of the renderer
        line.startWidth = 0.1f;
        line.endWidth = 0.05f;

        // set the position
        line.SetPosition(0, from.transform.position);
        line.SetPosition(1, to.transform.position);
    }
}
