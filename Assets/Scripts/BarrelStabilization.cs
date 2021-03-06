﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelStabilization : MonoBehaviour {
    public float Kp = 350;
    public float Ki = 5;
    public float Kd = 0.1f;
    private float P, I, D;
    private float prevError;

    private Vector3 targetVector;
    public Transform turret;
    public Transform barrel;
    private bool aimMode = false;
    

    // Use this for initialization
    void Start () {
        targetVector = barrel.up.normalized;
    }
	
	// Update is called once per frame
	void Update () {
        if (!aimMode)
        {
            float angle = Vector3.Angle(turret.forward, barrel.up);
            if(angle <= 105 && angle >=85)
            {
                barrel.localRotation *= Quaternion.Euler(Input.GetAxis("Elevation"), 0, 0);
            } else if (angle > 105 && Input.GetAxis("Elevation") > 0)
            {
                barrel.localRotation *= Quaternion.Euler(1, 0, 0);
            } else if (angle < 85 && Input.GetAxis("Elevation") < 0)
            {
                barrel.localRotation *= Quaternion.Euler(-1, 0, 0);
            }

            targetVector = barrel.up.normalized;
        }
        else
        {
            float dt = Time.deltaTime;
            float targetAngle = Vector3.Angle(targetVector, Vector3.up);
            float currentAngle = Vector3.Angle(barrel.up.normalized, Vector3.up);
            float angleError = targetAngle - currentAngle;
            if (angleError < 0.7 && angleError > -0.7)
            {
                barrel.Rotate(0, 0 ,0);
            }
            else
            {
                barrel.Rotate(-Pid(angleError, dt)*dt*dt, 0, 0);
            }
        }

        if (Input.GetButtonDown("BarrelLock"))
        {
            aimMode = !aimMode;
        }
    }

    public float Pid(float currentError, float deltaTime)
    {
        P = currentError;
        I += P * deltaTime;
        D = (P - prevError) / deltaTime;
        prevError = currentError;

        return P * Kp + I * Ki + D * Kd;
    }
}
