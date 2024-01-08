using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    public float Acceleration;
    public float MaxSpeed;
    public int CurrentLane;
    public GameObject[] Lanes;

    private float[] LanePositions;
    private BoxCollider2D PlayerCollider;
    private short LastInput;

    void Awake()
    {
        PlayerCollider = gameObject.GetComponent<BoxCollider2D>();

        Array.Resize(ref LanePositions, Lanes.Length);

        for (int i = 0; i < LanePositions.Length; i++)
        {
            LanePositions[i] = Lanes[i].transform.position.y + 0.5f * PlayerCollider.size.y;
        }

        SwitchLane(CurrentLane);
    }

    void FixedUpdate()
    {
        if (LastInput == -1)
        {
            if (CurrentLane > 0)
            {
                SwitchLane(CurrentLane - 1);
            }
        }

        if (LastInput == 1)
        {
            if (CurrentLane < Lanes.Length - 1)
            {
                SwitchLane(CurrentLane + 1);
            }
        }

        LastInput = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LastInput = -1;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            LastInput = 1;
        }
    }

    void SwitchLane(int TargetLane)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, LanePositions[TargetLane], gameObject.transform.position.z);
        gameObject.layer = Lanes[TargetLane].layer;
        CurrentLane = TargetLane;
    }
}
