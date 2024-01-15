using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public Image RunFill;
    public float JumpForce;
    public float Acceleration;
    public float MaxSpeed;
    public int CurrentLane;
    public GameObject[] Lanes;
    
    [Range(1,3)] public float fadeSpeed;
    private float[] LanePositions;
    private float runProgress=0f;
    private BoxCollider2D PlayerCollider;
    private Rigidbody2D PlayerRb;
    private short LastInput;
    private bool CanJump;
    private bool Invicible=false;
    private int Index;
    private bool isFull=true;
    private bool TurnOpaque=false;
    private bool TurnPermeable=false;
    private bool PermeableLastFrame=false;

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
        // Adds progress to the Race Bar
        runProgress+=Time.deltaTime*PlayerRb.velocity.x;
        RunFill.fillAmount=runProgress/1200;
        //adds progress to score bar (via triggering the other script's fuction)
        ScoreManager.instance.AddPoint(runProgress);

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
            AudioManager.Instance.PlaySFX("Jump");
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

        if(Invicible)
        {
            TurnOpaque=true;
            if(this.GetComponent<SpriteRenderer>().color.a>1/3 && isFull)
            {
            float opacity = this.GetComponent<SpriteRenderer>().color.a;
            opacity -= Time.deltaTime * fadeSpeed;
            this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, opacity);
            }
            if((!isFull && this.GetComponent<SpriteRenderer>().color.a>=1/3 && this.GetComponent<SpriteRenderer>().color.a<2) || (!isFull && this.GetComponent<SpriteRenderer>().color.a<=1/3))
            {
            float opacity = this.GetComponent<SpriteRenderer>().color.a;
            opacity += Time.deltaTime * fadeSpeed;
            this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, opacity);
            }
            if(this.GetComponent<SpriteRenderer>().color.a>=1)
            {
                isFull=true;
            }
            if(this.GetComponent<SpriteRenderer>().color.a<=1/3)
            {
                isFull=false;
            }
        }

        if(!Invicible && TurnOpaque)
        {
            TurnOpaque=false;
            this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, 1);
        }
    }

    void SwitchLane(int TargetLane)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, LanePositions[TargetLane] - LanePositions[CurrentLane] + gameObject.transform.position.y, gameObject.transform.position.z);
        if(!Invicible)
        {
            gameObject.layer = Lanes[TargetLane].layer;
        }
        if(Invicible)
        {
            gameObject.layer = Lanes[TargetLane].layer+4;
        }
        if(Lanes[TargetLane].layer == 8)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "BottomLane";
        }
        if(Lanes[TargetLane].layer == 7)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "MiddleLane";
        }
        if(Lanes[TargetLane].layer == 6)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "TopLane";
        }
        CurrentLane = TargetLane;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Changes the Speed
        if(collision.gameObject.tag == "Obstacle")
        {
            PlayerRb.velocity=new Vector2(0, PlayerRb.velocity.y);
            StartCoroutine(Invicibility());
        }
    }

    private IEnumerator Invicibility()
    {
        Invicible=true;
        TurnPermeable=true;
        if(TurnPermeable && !PermeableLastFrame)
        {
            PermeableLastFrame=true;
            gameObject.layer += 4;
            yield return new WaitForSeconds(2f);
            Invicible=false;
            PermeableLastFrame=false;
            TurnPermeable=false;
            gameObject.layer -= 4;
        }
    }
}

