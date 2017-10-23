using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStar : MonoBehaviour {

    public float ninjaStarSpeed;
    public GameObject NinjaStarDestructionPoint;
	
	void Start () {
        NinjaStarDestructionPoint = GameObject.FindWithTag("NinjaStarDestructionPoint");
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + ninjaStarSpeed, transform.position.y);

        if (transform.position.x > NinjaStarDestructionPoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "KillStar" || collision.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }

    }
}
