using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingManager : MonoBehaviour
{

    [Header("Collectible Prefab")]
    [SerializeField]
    private GameObject prefab;

    public void spawnRings(int number,Vector3 centerPos, float xbounds, float zbounds)
    {

        for(int i = 0; i < number; i++)
        {
            float x = Random.Range(centerPos.x - xbounds, centerPos.x + xbounds);
            float z = Random.Range(centerPos.z - zbounds, centerPos.z + zbounds);
            Vector3 spawnPos = new Vector3(x, centerPos.y, z);
            GameObject.Instantiate(prefab, spawnPos, Quaternion.identity, this.transform);

        }
    }
}
