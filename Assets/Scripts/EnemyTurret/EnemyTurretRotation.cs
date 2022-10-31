using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretRotation : MonoBehaviour
{
    // attributes
    public Transform target;

    // builtin methods
    void Update()
    {
        TurretRotation();
    }

    // custom methods
    void TurretRotation()
    {
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.AngleAxis(angle - 90.0f, Vector3.forward);
    }
}
