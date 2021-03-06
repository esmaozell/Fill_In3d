using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject cube;
    public List<GameObject> cubeList;

    private void Start()
    {
        LevelManager.Instance.fillAreaContainer.localPosition = new Vector3(transform.position.x + 7.5f,
            transform.position.y - .3f, transform.position.z + 3.6f);
        for (float i = -0.6f; i < 15.9f; i += .3f)
        {
            for (float j = -3; j < 9f; j += .3f)
            {
                var obj = Instantiate(cube, new Vector3(transform.position.x + i, transform.position.y, transform.position.z + j),
                    Quaternion.identity, transform);
                cubeList.Add(obj);
            }
        }

      /*  for (float i = 0; i < 4.8f; i+=.3f)
        {
            for (float j = 0; j < 2.4f; j+=.3f)
            {
                var obj = Instantiate(startCube,
                   new Vector3(startPos.position.x + i, startPos.position.y + j, startPos.position.z),
                    quaternion.identity, startPos);
            }
        }*/
    }

}
