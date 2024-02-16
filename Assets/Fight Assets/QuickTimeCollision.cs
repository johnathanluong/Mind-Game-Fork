using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTimeCollision : MonoBehaviour
{
    private int perfectTiming = 0;

    public int IsPerfectTiming()
    {
        return perfectTiming;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        perfectTiming = 1;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        perfectTiming = 0;
    }
}
