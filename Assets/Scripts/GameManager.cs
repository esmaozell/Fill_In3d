using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    static GameManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        if (LevelManager.Instance.HandleCreateNextLevel())
        {
            Debug.Log("Created New Level!");
           
        }
        else
        {
            Debug.Log("No More Level!");
        }

        LevelManager.Instance.LevelCompleted += OnLevelCompleted;
    }

    void OnLevelCompleted()
    {
        Debug.Log("Current Level Completed!");
    }

    
    private void Update()
    {
        if (LevelManager.Instance.endGameCubes.Count<=0)
        {
            //todo bu if in icine newgame kontrolu yapilirsa daha saglikli olur start();
        }
        
    }
}
