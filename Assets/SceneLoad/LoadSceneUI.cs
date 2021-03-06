using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadSceneUI : MonoBehaviour
{
    [SerializeField]
    SceneLoaderSO sceneLoaderSO;
    [SerializeField]
    UnityEvent beginAction;

    SceneAnimancerController sceneAnimancerController;

    void Awake()
    {
        sceneAnimancerController = GetComponentInChildren<SceneAnimancerController>();
        LevelManager.OnGameFinished += LoadScene;
    }

    void OnDestroy()
    {
        LevelManager.OnGameFinished -= LoadScene;
    }

    public void LoadScene(int index)
    {
        sceneAnimancerController.PlayFadeOut(() =>
        {
            sceneLoaderSO.LoadScene(index);
        });
    }

    public void LoadSameScene()
    {
        sceneAnimancerController.PlayFadeOut(() => { sceneLoaderSO.LoadSameScene(); });
    }
}
