using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilNinjaStar : MonoBehaviour
{

    public float evilNinjaStarSpeed;
    public GameObject EvilNinjaStarDestructionPoint;

    void Start()
    {
        EvilNinjaStarDestructionPoint = GameObject.FindWithTag("PlatformDestructionPoint");
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x - evilNinjaStarSpeed, transform.position.y);

        if (transform.position.x < EvilNinjaStarDestructionPoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NinjaStar" || collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
