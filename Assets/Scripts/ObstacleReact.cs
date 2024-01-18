using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleReact : MonoBehaviour
{
    // Changes the Sprite and Layer when touches Player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //plays hit sfx
            AudioManager.Instance.PlaySFX("Hit");
            //changes obstacle sprite & layer so it's untouchable
            this.gameObject.GetComponent<Animator>().SetBool("isBroken", true);
            this.gameObject.layer=9;
            //stops the obstacle
            this.gameObject.GetComponent<Rigidbody2D>().velocity=new Vector2(0, this.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            this.transform.parent.GetComponent<Rigidbody2D>().velocity=new Vector2(0, this.transform.parent.GetComponent<Rigidbody2D>().velocity.y);
            //changes player's sprite
            GameObject.Find("Player").GetComponent<Animator>().SetBool("isHit", true);
        }
    }

    // Changes the Sprite when untouches Player
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //returns the player's sprite
            GameObject.Find("Player").GetComponent<Animator>().SetBool("isHit", false);
        }
    }
}
