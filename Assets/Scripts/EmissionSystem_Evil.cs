using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionSystem_Evil : MonoBehaviour
{

    public GameObject shoot_position;
    public GameObject ninStarPool;

    void Start()
    {

             ninStarPool = GameObject.FindGameObjectWithTag("EvilNinStarPool");
    }

    public void ShootNinjaStar()
    {
        GameObject ninjaStar = ninStarPool.GetComponent<ObjectPooler>().GetPooledObject();
        ninjaStar.GetComponent<Transform>().position = new Vector2(
        shoot_position.transform.position.x,
        shoot_position.transform.position.y);
        ninjaStar.SetActive(true);

    }
}