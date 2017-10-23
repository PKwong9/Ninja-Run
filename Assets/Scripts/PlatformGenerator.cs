using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    //Platforms
    public Transform generationPoint;
    public float distanceBetween;
    public float distanceBetweenMin;
    public float distanceBetweenMax;
    private float[] platformWidths;
    private float platformWidth;
    private int platformSelector;
    public ObjectPooler[] theObjectPools;

    //Clatrops
    public float randomSpikeThreshold;
    public ObjectPooler spikePool;
    public float spikeHeight;
    private float spikeXPosition;

    //Enemy 
    public float randomEnemyThreshold;
    public ObjectPooler enemyPool;
    public float enemyHeight;
    private float enemyXPosition;
    public int enemyCount;

    void Start()
    {
        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }
    }

    void Update()
    {
        if(transform.position.x < generationPoint.position.x) //Platform Generation
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
            platformSelector = Random.Range(0, theObjectPools.Length);
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] /2) + distanceBetween, transform.position.y);

            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if(Random.Range(0f, 100f) < randomSpikeThreshold) //Caltrop generation
            {

                GameObject newSpike = spikePool.GetPooledObject();

                spikeXPosition = Random.Range(-platformWidths[platformSelector] / 2, platformWidths[platformSelector] / 2 + 1f);

                Vector3 spikePosition = new Vector3(spikeXPosition, spikeHeight, 0f);

                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
            } else if (Random.Range(0f, 100f) < randomEnemyThreshold) //Enemy Generation
            {

                GameObject newEnemy = enemyPool.GetPooledObject();

                enemyXPosition = Random.Range(0f, platformWidths[platformSelector] / 2 + 1f);

                Vector3 enemyPosition = new Vector3(enemyXPosition, enemyHeight, 0f);

                newEnemy.transform.position = transform.position + enemyPosition;
                newEnemy.transform.rotation = transform.rotation;
                newEnemy.SetActive(true);

            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y);
        }
    }
}