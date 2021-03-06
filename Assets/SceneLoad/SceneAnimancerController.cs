using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using System;

public class SceneAnimancerController : MonoBehaviour
{
    [SerializeField]
    AnimationClip fadeIn, fadeOut;

    AnimancerComponent animancer;

    void Awake()
    {
        animancer = GetComponent<AnimancerComponent>();
    }

    public void PlayFadeIn(Action action = null)
    {
        Time.timeScale = 1f;
        var state = animancer.Play(fadeIn);
        state.Events.OnEnd = () => { action?.Invoke(); };
    }

    public void PlayFadeOut(Action action = null)
    {
        Time.timeScale = 1f;
        var state = animancer.Play(fadeOut);
        state.Events.OnEnd = () => { action?.Invoke(); };
    }

    void OnEnable()
    {
        PlayFadeIn();
    }
}
