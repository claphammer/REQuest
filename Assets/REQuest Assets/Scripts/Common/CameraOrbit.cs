// ====================================================================================================================
// RE-Quest 2013
// ====================================================================================================================

using UnityEngine;
//using System.Collections;
public class CameraOrbit : MonoBehaviour 
{
    public Transform pivot;							// the object being followed
	public Vector3 pivotOffset = Vector3.zero;		// offset from target's pivot (0,2,0)
		
	//--- Specific For Cam Zoom Smoothing 
	public float heightDamping= 2.0f;
	public float rotationDamping= 2.0f;
	public float mouseWheelDistanceRate= 5;
	public float mouseWheelHeightRate= 5;
	public float distance = 4f; 					// CURR distance from target (used with zoom)
	public float minDistance = 1f; 					// MIN distance from target (used with zoom)
	public float maxDistance = 12f; 				// MAX distance from target (used with zoom)
	public float height= 2f; 						// the height we want the camera to be above the target
	public float zoomSpeed = 1f;
	//---
	
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;
	public bool allowXRot = true;
	public float xMinLimit = -360f;
	public float xMaxLimit = 360f;
	public float minHeight = 2f;
    public float maxHeight = 60f;
	
    private float yMinLimit = 0f;
	private float yMaxLimit = 0f;
    private float x = 0.0f;
    private float y = 0.0f;
	private float pivX;
	private float pivY;
	private float targetX = 0f;
	private float targetY = 0f;
	private float targetDistance = 0f;
	//private float xVelocity = 1f;
	//private float yVelocity = 1f;
	private float zoomVelocity = 1f;
	private Transform cam;

    void Start()
    {
		cam = transform;  //Smooth Zoom Stuff
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
    		if (!pivot)
        		return;
   
   	 		if (Input.GetAxis("Mouse ScrollWheel") != 0) 
				{
        			//Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        			distance -= Input.GetAxis("Mouse ScrollWheel") * mouseWheelDistanceRate;
					distance = Mathf.Clamp(distance, minDistance, maxDistance);
        			height -= Input.GetAxis("Mouse ScrollWheel") * mouseWheelHeightRate;
					height = Mathf.Clamp(height, minHeight, maxHeight);
        		} 

    		// Calculate the current rotation angles
    		float wantedRotationAngle = pivot.eulerAngles.y;
    		float wantedHeight = pivot.position.y + height;
    		float currentRotationAngle = transform.eulerAngles.y;
    		float currentHeight = transform.position.y; // + heightOffset;
			float currentX = transform.position.x;
			float currentZ = transform.position.z;

    		// Damp the rotation around the y-axis
    		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

    		// Damp the height
    		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);

    		// Convert the angle into a rotation
    		Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
 
    		// Set the position of the camera on the x-z plane to: distance meters behind the target
    		transform.position = pivot.position;
    		transform.position -= currentRotation * Vector3.forward * distance;

    		// Set the height of the camera
    		cam.position = new Vector3 (currentX, currentHeight, currentZ);		//this variable cant be set by itself.  must be in a xyx format vec3
			Vector3 newPivot = new Vector3(0.0f, 0.0f, 0.0f) + pivot.position + pivotOffset;
			
    		// Always look at the target
    		transform.LookAt (newPivot);
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
	
	/*void ZoomToPlayer()
	{
	}
		*/
	
	private float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360) angle += 360;
		if (angle > 360) angle -= 360;
		return Mathf.Clamp(angle, min, max);
	}
	
	private float ClampHeight(float angle, float minHeight, float maxHeight)
	{
		//if (angle < -360) angle += 360;
		//if (angle > 360) angle -= 360;
		return Mathf.Clamp(angle, minHeight, maxHeight);
	}
}
