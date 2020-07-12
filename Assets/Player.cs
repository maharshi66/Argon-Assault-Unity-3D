using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")] [SerializeField] float speed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;
	
	[SerializeField] float positionPitchFactor= -5f;
	[SerializeField] float controlPitchFactor = -20f;
	[SerializeField] float positionYawFactor = 5f;
	[SerializeField] float controlRollFactor= -20f;
	
	float xThrow, yThrow;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
		ProcessTranslation();
		ProcessRotation();		
	}

	private void ProcessRotation()
	{
		float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
		float pitchDueToControlThrow = yThrow * controlPitchFactor;
		float pitch = pitchDueToControlThrow + pitchDueToPosition;

		float yaw = transform.localPosition.x * positionYawFactor;
		float roll = xThrow * controlRollFactor;
		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
	}

	private void ProcessTranslation()
	{
		float clampedXPos, clampedYPos;

		xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		float xOffsetThisFrame = xThrow * speed * Time.deltaTime;
		float rawNewXPos = transform.localPosition.x + xOffsetThisFrame;
		clampedXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);
		yThrow = CrossPlatformInputManager.GetAxis("Vertical");
		float yOffsetThisFrame = yThrow * speed * Time.deltaTime;
		float rawNewYPos = transform.localPosition.y + yOffsetThisFrame;
		clampedYPos = Mathf.Clamp(rawNewYPos, -yRange, yRange);//restricts between limits
		transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
	}
}
