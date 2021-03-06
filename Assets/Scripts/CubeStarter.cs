using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeStarter : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            GetComponentInParent<LevelManager>().ActivateBlocks();
            GetComponent<Collider>().enabled = false;
        }
    }
}
