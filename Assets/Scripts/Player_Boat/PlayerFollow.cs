using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour {

    public Transform PlayerTransform;

    private Vector3 _cameraOffset;

    public float height = 40;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool LookAtPlayer = false;

    public bool RotateAroundPlayer = true;

    public float RotationsSpeed = 5.0f;


    public void Awake()
    {

    }
    // Use this for initialization
    void Start () {
        _cameraOffset = transform.position - new Vector3(PlayerTransform.position.x, 0, PlayerTransform.position.z);
	}
	
	// LateUpdate is called after Update methods
	void LateUpdate () {

        if(RotateAroundPlayer)
        {
            Quaternion camTurnAngle =
                Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, Vector3.up);

            _cameraOffset = camTurnAngle * _cameraOffset;
        }
        Vector3 newPos = new Vector3(PlayerTransform.position.x, 0, PlayerTransform.position.z) + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer || RotateAroundPlayer)
            transform.LookAt(PlayerTransform);
	}
}
