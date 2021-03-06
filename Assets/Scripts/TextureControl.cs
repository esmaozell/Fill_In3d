using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureControl : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      var comp = other.GetComponent<FillAreaController>();
      if (comp)
      {
         transform.GetComponent<MeshRenderer>().enabled = false;
      }
   }
}
