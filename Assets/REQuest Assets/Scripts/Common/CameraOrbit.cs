// ====================================================================================================================
// Simple rotation and tilt of camera around a pivot object
// Created by Leslie Young
// http://www.plyoung.com/ or http://plyoung.wordpress.com/
// ====================================================================================================================

using UnityEngine;
//using System.Collections;
public class CameraOrbit : MonoBehaviour 
{
    public Transform pivot;							// the object being followed
	public Vector3 pivotOffset = Vector3.zero;		// offset from target's pivot

	public float distance = 6f; 					// CURR distance from target (used with zoom)
	public float minDistance = 2f; 					// MIN distance from target (used with zoom)
	public float maxDistance = 15f; 				// MAX distance from target (used with zoom)
	public float zoomSpeed = 1f; 

    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

	public bool allowXRot = true;
	public float xMinLimit = 360f;
	public float xMaxLimit = 80f;
    
	public bool allowYTilt = true;
	public float yMinLimit = 10f;
    public float yMaxLimit = 80f;

    private float x = 0.0f;
    private float y = 0.0f;
	

	private float pivX;
	private float pivY;
	private float targetX = 0f;
	private float targetY = 0f;
	private float targetDistance = 0f;
	private float xVelocity = 1f;
	private float yVelocity = 1f;
	private float zoomVelocity = 1f;

    void Start()
    {

		pivX = pivot.transform.eulerAngles.x;
		pivY = pivot.transform.eulerAngles.y;
    	var angles = transform.eulerAngles;
		targetX = x = angles.x;
		targetY = y = ClampAngle(angles.y, yMinLimit, yMaxLimit);
		targetDistance = distance;
		CamBoundToPlayer();
    }
	
    void Update()
    {
        if (pivot) //if a CamPivot is selected
        {
			// scroll wheel used to zoom in/out -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
			float scroll = Input.GetAxis("Mouse ScrollWheel");

			if (scroll > 0.0f) targetDistance -= zoomSpeed;
			else if (scroll < 0.0f) targetDistance += zoomSpeed;
			targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
			ZoomToPlayer(); 																	//wc
			
			// -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
			
			/*
			if (!gameBusy)
			{
				// right mouse button must be held down to tilt/rotate cam or player can use the left mouse button while holding Ctr
				if (Input.GetMouseButton(1) || (Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) ))
         	   	{
              	 	if (allowXRot)
					{
						targetX -= Input.GetAxis("Mouse X") * xSpeed * 0.02f;
						targetX = ClampAngle(targetX, xMinLimit,xMaxLimit);
					}
					if (allowYTilt)
					{
						targetY -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
						targetY = ClampAngle(targetY, yMinLimit, yMaxLimit);
					}
				}
				
				x = Mathf.SmoothDampAngle(x, targetX, ref xVelocity, 0.3f);
				if (allowYTilt) y = Mathf.SmoothDampAngle(y, targetY, ref yVelocity, 0.3f);
				else y = targetY;
				Quaternion rotation = Quaternion.Euler(y, x, 0);
				distance = Mathf.SmoothDamp(distance, targetDistance, ref zoomVelocity, 0.5f);

				// -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
				// apply

				Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + pivot.position + pivotOffset;
				transform.rotation = rotation;
				transform.position = position;
			}
			else
			{
			*/
			//CamBoundToPlayer();
			//}
					
		}
	
	}
		
	void CamBoundToPlayer()
	{

		x = targetX;
		y = targetY;
		Quaternion rotation = Quaternion.Euler(y, x, 0);
		distance = Mathf.SmoothDamp(distance, targetDistance, ref zoomVelocity, 0.5f);

		Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + pivot.position + pivotOffset;
		transform.rotation = rotation;
		transform.position = position;
	}
	
	void ZoomToPlayer()
	{
		pivX = pivot.transform.eulerAngles.y;
		pivY = pivot.transform.eulerAngles.x;
		y = pivX;
		x = pivY;
		Quaternion rotation = Quaternion.Euler(x, y, 0);
		distance = Mathf.SmoothDamp(distance, targetDistance, ref zoomVelocity, 0.5f);

		Vector3 position = rotation * new Vector3(0.0f, 1.5f, -distance) + pivot.position + pivotOffset;
		transform.rotation = rotation;
		transform.position = position;
	}
		
	private float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360) angle += 360;
		if (angle > 360) angle -= 360;
		return Mathf.Clamp(angle, min, max);
	}
	 
	// ====================================================================================================================
}
