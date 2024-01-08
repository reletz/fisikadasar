using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float JumpForce;
    public float Acceleration;
    public float MaxSpeed;
    public int CurrentLane;
    public GameObject[] Lanes;

    private float[] LanePositions;
    private BoxCollider2D PlayerCollider;
    private Rigidbody2D PlayerRb;
    private short LastInput;
    private bool CanJump;

    void Awake()
    {
        PlayerCollider = gameObject.GetComponent<BoxCollider2D>();
        PlayerRb = gameObject.GetComponent<Rigidbody2D>();

        Array.Resize(ref LanePositions, Lanes.Length);

        for (int i = 0; i < LanePositions.Length; i++)
        {
            LanePositions[i] = Lanes[i].transform.position.y + 0.5f * PlayerCollider.size.y;
        }

        SwitchLane(CurrentLane);
    }

    void FixedUpdate()
    {
        // temporary ground checking
        if (PlayerRb.velocity.y == 0)
        {
            CanJump = true;
        }
        else
        {
            CanJump = false;
        }

        // Move to the lane above
        if (LastInput == -1)
        {
            if (CurrentLane > 0)
            {
                SwitchLane(CurrentLane - 1);
            }
        }

        // Move to the lane below
        if (LastInput == 1)
        {
            if (CurrentLane < Lanes.Length - 1)
            {
                SwitchLane(CurrentLane + 1);
            }
        }

        // Jump when player can jump
        if (Input.GetKey(KeyCode.Space) && CanJump)
        {
            PlayerRb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }

        LastInput = 0;

        // Accelerates the player until they hit max speed
        if (PlayerRb.velocity.x < MaxSpeed)
        {
            PlayerRb.AddForce(Vector2.right * Acceleration * PlayerRb.mass);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            LastInput = -1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            LastInput = 1;
        }
    }

    void SwitchLane(int TargetLane)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, LanePositions[TargetLane] - LanePositions[CurrentLane] + gameObject.transform.position.y, gameObject.transform.position.z);
        gameObject.layer = Lanes[TargetLane].layer;
        CurrentLane = TargetLane;
    }
}
