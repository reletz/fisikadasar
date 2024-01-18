using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerAnimationManager : MonoBehaviour
{
    public float StandardSpeed;
    public string ParameterName;

    private Animator PlayerAnimator;
    private Rigidbody2D PlayerRb;

    void Start()
    {
        PlayerRb = gameObject.GetComponent<Rigidbody2D>();
        PlayerAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        PlayerAnimator.SetFloat(ParameterName, PlayerRb.velocity.x / StandardSpeed);
        if(PlayerRb.velocity.y>0)
        {
            PlayerAnimator.SetBool("isJumping", true);
        }
        else
        {
            PlayerAnimator.SetBool("isJumping",false);
        }

        if(PlayerRb.velocity.y<0)
        {
            PlayerAnimator.SetBool("isFalling", true);
        }
        else
        {
            PlayerAnimator.SetBool("isFalling", false);
        }
    }
}