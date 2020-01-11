using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            transform.Translate(-1,0,0);
        } else if (Input.GetKeyDown("a"))
        {
            transform.Translate(0,0,-1);
        } else if (Input.GetKeyDown("s"))
        {
            transform.Translate(1,0,0);
        } else if (Input.GetKeyDown("d"))
        {
            transform.Translate(0,0,1);
        }
    }
}
