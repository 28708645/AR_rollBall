using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRing : MonoBehaviour
{
    [SerializeField]
    [Range(0, 360)] private float rotationvalue;


    // Update is called once per frame
    private void FixedUpdate()
    {
        this.transform.Rotate(Vector3.up*rotationvalue);
    }
}
