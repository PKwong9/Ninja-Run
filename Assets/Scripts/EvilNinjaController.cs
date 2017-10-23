using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilNinjaController : MonoBehaviour
{
    public bool isDead;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private Collider2D[] myColliders;

    private int animationCounter;

    public EmissionSystem_Evil theEmissionSystem;

    public GameObject EnemyDestructionPoint;
    public GameObject thePlatformGenerator;

    void Start()
    {
        animationCounter = 0;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myColliders = GetComponents<Collider2D>();

        EnemyDestructionPoint = GameObject.FindWithTag("PlatformDestructionPoint");
        thePlatformGenerator = GameObject.Find("PlatformGenerator");
    }

    void Update()
    {
        myAnimator.SetBool("isDead", isDead);

        if (transform.position.x < EnemyDestructionPoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }
    }

    public GameObject GetEnemy()
    {
        if (!gameObject.activeInHierarchy)
        {
            return gameObject;
        }

        GameObject obj = (GameObject)Instantiate(gameObject);
        obj.SetActive(false);
        return obj;
    }

    public void AnimationCount()
    {
        var currentState = myAnimator.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName("Enemy_Idle"))
        { 
            animationCounter += 1;
        } else if (currentState.IsName("Enemy_Shoot")){
            animationCounter -= 3;
        }

        if (animationCounter >= 3)
        {
            myAnimator.SetTrigger("Enemy_Shoot");
        } else if (animationCounter == 0)
        {
            myAnimator.SetTrigger("Enemy_Idle");
        }
    }

    public void ShootReady_Evil()
    {
        theEmissionSystem.ShootNinjaStar();
    }

    public void EnemyInactive()
    {
        foreach (Collider2D c in myColliders)
        {
            c.isTrigger = false;
        }
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NinjaStar")
        {

            foreach (Collider2D c in myColliders)
            {
                c.isTrigger = true;
            }
            isDead = true;
            animationCounter = 0;
            myRigidBody.velocity = new Vector2(0f, 0f);
            myAnimator.SetTrigger("Enemy_Die");
        }

        if (collision.gameObject.tag == "Player")
        {
            myAnimator.SetTrigger("Enemy_Idle");
        }
    }
}
