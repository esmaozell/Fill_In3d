using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockState
{
    Default = 0,
    Collected
}

public class FillAreaController : MonoBehaviour
{
    public Color targetColor;
    public Collider trigger;
    public MeshRenderer mR;
    public BlockState BlockState
    {
        get
        {
            return blockState;
        }

        set
        {
            blockState = value;

            switch (blockState)
            {
                case BlockState.Default:

                    OnCreated?.Invoke(this);

                    break;
                case BlockState.Collected:

                    OnCollected?.Invoke(this);               

                    break;
                default:
                    break;
            }
        }
    }

    public Action<FillAreaController> OnCreated { get; set; }
    public Action<FillAreaController> OnCollected { get; set; }

    BlockState blockState = BlockState.Default;

    private void Update()
    {
        if (LevelManager.Instance.blocksFromImage.Count <= GetComponent<ColorChanger>().lessThanAmount)
        {
            for (int i = 0; i < LevelManager.Instance.blocksFromImage.Count; i++)
            {        
                    LevelManager.Instance.blocksFromImage[i].GetComponent<ColorChanger>().enabled = true;               
            }            
        }

    }



}
