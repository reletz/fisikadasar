using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
public class RogaMovement : MonoBehaviour
{
    public float Speed;
    public float CatchupSpeed;
    public int CurrentLane;
    public GameObject[] Lanes;
    public GameObject Player;
    public float StartCountdown;
    public float MaxDistance;
    public bool Started;

    private float[] LanePositions;
    private BoxCollider2D RogaCollider;
    private Rigidbody2D RogaRb;
    private PlayerMovement Target;

    public void StartChase()
    {
        Started = true;
    }

    void Awake()
    {
        RogaCollider = gameObject.GetComponent<BoxCollider2D>();
        RogaRb = gameObject.GetComponent<Rigidbody2D>();
        Target = Player.GetComponent<PlayerMovement>();

        Array.Resize(ref LanePositions, Lanes.Length);

        for (int i = 0; i < LanePositions.Length; i++)
        {
            LanePositions[i] = Lanes[i].transform.position.y + 0.5f * RogaCollider.size.y;
        }

        SwitchLane(CurrentLane);
    }

    IEnumerator StartDelay(float Countdown)
    {
        yield return new WaitForSeconds(Countdown);

        StartChase();
    }

    void Start()
    {
        StartCoroutine(StartDelay(StartCountdown));
    }

    void FixedUpdate()
    {
        if (!Started) return;

        if (CurrentLane != Target.CurrentLane)
        {
            SwitchLane(Target.CurrentLane);
        }

        if ((Player.transform.position - gameObject.transform.position).magnitude > MaxDistance)
        {
            RogaRb.velocity = Vector2.right * CatchupSpeed;
        }
        else
        {
            RogaRb.velocity = Vector2.right * Speed;
        }
    }

    void SwitchLane(int TargetLane)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, LanePositions[TargetLane] - LanePositions[CurrentLane] + gameObject.transform.position.y, gameObject.transform.position.z);
        CurrentLane = TargetLane;
    }
}
