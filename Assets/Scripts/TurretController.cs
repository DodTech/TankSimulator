﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretController : MonoBehaviour {

	public Transform turret;
	public Transform barrel;


	public Text lookAngleText;
	public Text turretAngleText; 
	public Text aimModeText;
	public float aimSpeed = 1;
	public float elevationSpeed;
	private Quaternion lookAngle;
	private Quaternion baseTowerAngle;

	private bool aimMode = false;

	void Start () {
		lookAngle = turret.rotation;
		baseTowerAngle = turret.rotation;
		aimModeText.text = "Aim mode: " + (aimMode ? "ON" : "OFF");
	}

	void Update () {
		if (aimMode) {
			lookAngle *= Quaternion.Euler (0, 0, Input.GetAxis ("Yaw"));
			lookAngleText.text = lookAngle.eulerAngles.ToString ();
			turret.rotation = Quaternion.Lerp (turret.rotation, lookAngle, Time.deltaTime * aimSpeed);	
		} else {
			turret.rotation = Quaternion.Lerp (turret.rotation, transform.rotation * Quaternion.Euler (270, 0, 0), Time.deltaTime * aimSpeed * 0.03f);
		//	barrel.rotation = Quaternion.Lerp (barrel.rotation, transform.rotation * Quaternion.Euler (270, 0, 0), Time.deltaTime * aimSpeed * 0.03f);
		}
	//	turret.rotation = Quaternion.Lerp(turret.rotation,Quaternion.Euler(new Vector3 (turret.rotation.eulerAngles.x,lookAngle.eulerAngles.y,turret.rotation.eulerAngles.z)),rotationSpeed);
		turretAngleText.text = turret.rotation.eulerAngles.ToString();

		barrel.rotation *= Quaternion.Euler (Input.GetAxis ("Elevation"), 0, 0);
	//	turret.rotation = Quaternion.Lerp(turret.rotation, Quaternion.Euler(lookAngle.z, turret.rotation.x,turret.rotation.y), 1);
	//	Debug.Log (barrel.localRotation.eulerAngles.x);
	/*	if (barrel.localRotation.eulerAngles.x >= 339 || barrel.localRotation.eulerAngles.x <= 8) {
			barrel.rotation *= Quaternion.Euler (Input.GetAxis ("Elevation"), 0, 0);
		} else if (barrel.localRotation.eulerAngles.x > 8) {
			barrel.rotation = Quaternion.Euler (8, barrel.rotation.y, barrel.rotation.z);
		} else if (barrel.localRotation.eulerAngles.x < 339) {
			barrel.rotation= Quaternion.Euler(339, barrel.rotation.y, barrel.rotation.z);
		}*/
	//	}


		if(Input.GetKeyUp(KeyCode.T)){
			lookAngle = transform.rotation * Quaternion.Euler (270, 0, 0);
			aimMode = !aimMode;
			aimModeText.text = "Aim mode: " + (aimMode ? "ON" : "OFF");
		}
	}
}
