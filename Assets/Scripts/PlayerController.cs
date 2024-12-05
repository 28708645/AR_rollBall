using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody),typeof(SphereCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("References to get")]
    [SerializeField]
    private Rigidbody _rigidBody;
    [SerializeField]
    private FixedJoystick _joystick;
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private GameManager _gm;
    [Header("Moving parameters")]
    [SerializeField]
    [Range(0, 100)] private int speed;
    [SerializeField]
    [Range(0, 2)] private float jumpforce;
    private List<Collider> groundColliders= new List<Collider>();

    private bool grounded;
    private void Start()
    {
        _rigidBody = this.GetComponent<Rigidbody>();
        _joystick = GameObject.FindGameObjectWithTag("FixedJoystick").GetComponent<FixedJoystick>();
        _audio = this.GetComponent<AudioSource>();
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _gm.startPlay();
    }
    private void FixedUpdate()
    {
        if (this.transform.position.y < -100)
        {
            resetPos();
        }
        //Avoid moving the ball if timer not launched
        if (_gm.getGameState() == GAMESTATE.play)
        {
            _rigidBody.velocity = new Vector3(_joystick.Horizontal * speed, _rigidBody.velocity.y, _joystick.Vertical * speed);
        }
    }

    private void resetPos()
    {
        Vector3 pos = _gm.getCenterPlan();
        Vector3 nPos = new Vector3(pos.x, pos.y + 0.001f, pos.z);
        this.transform.position = nPos;
    }

    //Check if on the ground
    private void OnCollisionEnter(Collision collision)
    {
       
        if (!grounded)
        {
            grounded = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!grounded)
        {
            grounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (grounded) 
        {
            grounded = false;
        }
    }
    //Check collision with collectibles
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Collectible"))
        {
            _gm.updateCount();
            Debug.Log("Collectible Found");
            _audio.Play();
            Destroy(go);
        }
    }
    public void jumpAnimation()
    {
        if (grounded)
        {
            _rigidBody.AddForce(Vector3.up* jumpforce, ForceMode.Impulse);
        }
    }
}
