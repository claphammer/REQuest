// ====================================================================================================================
//*** This one is by Will
// ====================================================================================================================

using UnityEngine;

public class CameraMove : MonoBehaviour
{
	public float speed = .5f;
	public Transform target;			// target to follow (make this the player)
	public Transform camTr;				// main camera to bind to target
	private Transform tr;				// for storing the current Pivot GameObject transforms

	void Start()
	{
		tr = this.transform;
		if (target)
		{
			tr.position = target.position;
			tr.rotation = target.rotation;
		}
	}

	void LateUpdate()
	{
		if (target)
		{
			Vector3 difference = target.position - tr.position;
			tr.position = Vector3.Slerp(tr.position, target.position, Time.deltaTime * Mathf.Clamp(difference.magnitude, 0f, 2f));
			tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * speed);									//wc
		}
	}

	public void Follow(Transform t)
	{
		target = t;
	}

	// ====================================================================================================================
}
