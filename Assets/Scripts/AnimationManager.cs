using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation(string animationName)
    {
        anim.SetBool(animationName, true);
    }

    public void StopAnimation(string animationName)
    {
        anim.SetBool(animationName, false);
    }

    public void SetSprintAnimation(bool set, float speedMultiplier)
    {
        anim.SetFloat("WalkSpeedMultiplier", speedMultiplier);
    }
}
