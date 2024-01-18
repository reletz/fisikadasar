using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class FreezePowerUp : MonoBehaviour
{
    public GameObject Player;
    public RogaMovement RogaMovementInstance;
    public Rigidbody2D RogaRb;
    public float Duration;
    public float NormalSpeed;
    public float NormalCatchupSpeed;
    public float MaxRange;

    private bool Triggered = false;
    private float WaitTime = 0;
    private SpriteRenderer Renderer;

    void Start()
    {
        Renderer = gameObject.GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player");

        GameObject Roga = GameObject.Find("Roga");
        RogaMovementInstance = Roga.GetComponent<RogaMovement>();
        RogaRb = Roga.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Triggered)
        {
            if (WaitTime >= Duration)
            {
                UnfreezeRoga();
                CleanUp();
            }

            WaitTime += Time.deltaTime;

            return;
        }

        if (Player.transform.position.x - gameObject.transform.position.x > MaxRange)
        {
            CleanUp();
        }
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (!Other.gameObject.CompareTag("Player") || Triggered) return;

        Triggered = true;
        AudioManager.Instance.PlaySFX("PA2");
        HideSprite();
        FreezeRoga();
    }

    void FreezeRoga()
    {
        RogaMovementInstance.Speed = 0;
        RogaMovementInstance.CatchupSpeed = 0;

        RogaRb.velocity = Vector2.zero;
    }

    void UnfreezeRoga()
    {
        RogaMovementInstance.Speed = NormalSpeed;
        RogaMovementInstance.CatchupSpeed = NormalCatchupSpeed;
    }

    void HideSprite()
    {
        Renderer.color = new Color(0, 0, 0, 0);
    }

    void CleanUp()
    {
        Destroy(gameObject);
    }
}
