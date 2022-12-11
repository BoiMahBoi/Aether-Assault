using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCountdown : MonoBehaviour
{

    public int countdown;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Delete", countdown);
    }

    void Delete()
    {
        Destroy(gameObject);
    }
}
