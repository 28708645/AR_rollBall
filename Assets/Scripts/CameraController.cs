using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Vector3 pos;
    [SerializeField]
    [Range(0, 15)]
    private int offset;

    private void LateUpdate()
    {
        pos = player.transform.position;
        this.transform.position = new Vector3(pos.x, this.transform.position.y, pos.z - offset);

    }
}
