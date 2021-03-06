using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    MeshRenderer meshRenderer;

    [SerializeField] Color[] myColors;

   public int colorIndex = 0;
   public float lerpTime = 1f;
     float timeToChangeIndex;
    public int lessThanAmount = 100;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    
    void Update()
    {
        if (LevelManager.Instance.startedCubes.Count <= lessThanAmount)
        {
            meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, myColors[colorIndex],
                  lerpTime * Time.deltaTime);
        }
        
        timeToChangeIndex = Mathf.Lerp(timeToChangeIndex, 1f, lerpTime * Time.deltaTime);
        if (timeToChangeIndex > .9f)
        {
            timeToChangeIndex = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= myColors.Length) ? 0 : colorIndex;
        }
    }
}
