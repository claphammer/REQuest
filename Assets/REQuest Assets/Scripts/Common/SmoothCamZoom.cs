// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class MYCLASSNAME : MonoBehaviour {

/*
This camera smoothes out rotation around the y-axis and height.
Horizontal Distance to the target is always fixed.

There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves.

For every of those smoothed values we calculate the wanted value and the current value.
Then we smooth it using the Lerp function
Then we apply the smoothed values to the transform's position.
*/
	
public Transform target; // The target we are following
public float distance= 10.0f; // The distance in the x-z plane to the target
public float height= 5.0f; // the height we want the camera to be above the target

// How much we 
public float heightDamping= 2.0f;
public float rotationDamping= 3.0f;
public float mouseWheelDistanceRate= 5;
public float mouseWheelHeightRate= 5;
	
		public float yMinLimit = 10f;
    public float yMaxLimit = 80f;
	

 

// Place the script in the Camera-Control group in the component menu
//@script AddComponentMenu("Camera-Control/Smooth Follow With Zoom")

 
	void  LateUpdate ()
	{

    // Early out if we don't have a target
    if (!target)
        return;
   
    if (Input.GetAxis("Mouse ScrollWheel") != 0) 
		{
        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        distance -= Input.GetAxis("Mouse ScrollWheel") * mouseWheelDistanceRate;
        height -= Input.GetAxis("Mouse ScrollWheel") * mouseWheelHeightRate;
        } 

    // Calculate the current rotation angles
    float wantedRotationAngle = target.eulerAngles.y;
    float wantedHeight = target.position.y + height;
    float currentRotationAngle = transform.eulerAngles.y;
    float currentHeight = transform.position.y;

		//clamp the rotation
		wantedRotationAngle = ClampAngle(wantedRotationAngle, yMinLimit, yMaxLimit);
		
    // Damp the rotation around the y-axis
    currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

    // Damp the height
    currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);

 
    // Convert the angle into a rotation
    Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
 
    // Set the position of the camera on the x-z plane to: distance meters behind the target
    transform.position = target.position;
    transform.position -= currentRotation * Vector3.forward * distance;

    // Set the height of the camera
    
  //  transform.position.y = currentHeight;

    // Always look at the target
    transform.LookAt (target);

}


/*
						targetY -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
						targetY = ClampAngle(targetY, yMinLimit, yMaxLimit);
					*/	
	private float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360) angle += 360;
		if (angle > 360) angle -= 360;
		return Mathf.Clamp(angle, min, max);
	}
	}