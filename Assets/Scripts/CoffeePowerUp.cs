using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class CoffeePowerUp : MonoBehaviour
{
    public PlayerMovement PlayerMovementInstance;
    public Rigidbody2D PlayerRb;
    public float FlatSpeedBoost;
    public float AccelerationBoost;
    public float Duration;
    public float MaxRange;

    private bool Triggered = false;
    private float WaitTime = 0;
    private SpriteRenderer Renderer;

    void Start()
    {
        Renderer = gameObject.GetComponent<SpriteRenderer>();

        GameObject Player = GameObject.Find("Player");
        PlayerMovementInstance = Player.GetComponent<PlayerMovement>();
        PlayerRb = Player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Triggered)
        {
            if (WaitTime >= Duration)
            {
                EndBoost();
                CleanUp();
            }

            WaitTime += Time.deltaTime;

            return;
        }

        if (PlayerRb.gameObject.transform.position.x - gameObject.transform.position.x > MaxRange)
        {
            CleanUp();
        }
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (!Other.gameObject.CompareTag("Player") || Triggered) return;

        Triggered = true;
        HideSprite();
        StartBoost();
    }

    void StartBoost()
    {
        PlayerMovementInstance.Acceleration += AccelerationBoost;
        PlayerMovementInstance.MaxSpeed += FlatSpeedBoost;

        PlayerRb.AddForce(PlayerRb.mass * Vector2.right * FlatSpeedBoost, ForceMode2D.Impulse);
    }

    void EndBoost()
    {
        PlayerMovementInstance.Acceleration -= AccelerationBoost;
        PlayerMovementInstance.MaxSpeed -= FlatSpeedBoost;

        if (PlayerRb.velocity.x > PlayerMovementInstance.MaxSpeed)
        {
            PlayerRb.AddForce(PlayerRb.mass * Vector2.right * (PlayerMovementInstance.MaxSpeed - PlayerRb.velocity.x), ForceMode2D.Impulse);
        }
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
