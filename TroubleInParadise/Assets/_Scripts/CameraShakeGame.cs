using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CameraShakeGame : MonoBehaviour {

	public float Magnitude = 2f;
	public float Roughness = 10f;
	public float FadeOutTime = 5f;

	void Start ()
	{
		CameraShaker.Instance.StartShake (Magnitude, Roughness, FadeOutTime);
	}
}
