using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterBoat : MonoBehaviour
{
    //visible Properties
    public Transform Motor;
    public float SteerPower = 2f;
    public float Power = 250f;
    public float MaxSpeed = 10f;
    public float Drag = 0.1f;

    //used Components
    protected Rigidbody Rigidbody;
    protected Quaternion StartRotation;
    protected ParticleSystem ParticleSystem;
    protected Camera Camera;

    Vector3 lastPosition;

    //internal Properties
    protected Vector3 CamVel;


    public void Awake()
    {
        lastPosition = Motor.position;
        ParticleSystem = GetComponentInChildren<ParticleSystem>();
        Rigidbody = GetComponent<Rigidbody>();
        StartRotation = Motor.localRotation;
        Camera = Camera.main;
        Rigidbody.angularDrag = 1f;
    }

    public void FixedUpdate()
    {
        //default direction
        var forceDirection = transform.forward;
        var steer = 0;

        //steer direction [-1,0,1]
        if (Input.GetKey(KeyCode.A))
            steer = -1;
        if (Input.GetKey(KeyCode.D))
            steer = 1;

        //Rotational Force
        if (Math.Abs(Rigidbody.angularVelocity.magnitude) < 0.50f)
        {
            //Debug.Log(Rigidbody.angularVelocity.magnitude* steer + "   " + steer);
            //Debug.Log(Rigidbody.angularVelocity.magnitude);
            Rigidbody.AddTorque(steer * transform.up * (SteerPower /15f));
            //Rigidbody.AddForceAtPosition(steer * transform.right * SteerPower / 100f, Motor.position);
        }


        //compute vectors
        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);
        var targetVel = Vector3.zero;

        //forward/backward poewr
        if (Input.GetKey(KeyCode.W))
            PhysicsHelper.ApplyForceToReachVelocity(Rigidbody, forward * MaxSpeed, Power);
        if (Input.GetKey(KeyCode.S))
            PhysicsHelper.ApplyForceToReachVelocity(Rigidbody, forward * -MaxSpeed, Power);

       
        //moving forward
        var movingForward = Vector3.Cross(transform.forward, Rigidbody.velocity).y < 0;

        //move in direction
        Rigidbody.velocity = Quaternion.AngleAxis(Vector3.SignedAngle(Rigidbody.velocity, (movingForward ? 1f : 0f) * transform.forward, Vector3.up) * Drag, Vector3.up) * Rigidbody.velocity;

        Vector3 velocity = (Motor.position - lastPosition) / Time.deltaTime;
        
        lastPosition = transform.position;
    }

}