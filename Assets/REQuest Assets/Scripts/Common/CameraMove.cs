// ====================================================================================================================
//
//*** This one is by Will
//
// ====================================================================================================================

using UnityEngine;

public class CameraMove : MonoBehaviour
{
	public float speed = 2f;
	public Transform target;			// target to follow (cam is fixed to following this around till it is NULL)
	public bool followTarget = true;	// follow the target? (only if target is not NULL)
	public Transform camTr;
	private Transform tr;

	void Start()
	{
		tr = this.transform;
		if (target && followTarget) 
		{
			tr.position = target.position;
			tr.rotation = target.rotation;
		}
	}

	void Update()
	{

	}

	void LateUpdate()
	{
		if (target && followTarget)
		{
			Vector3 difference = target.position - tr.position;
			tr.position = Vector3.Slerp(tr.position, target.position, Time.deltaTime * Mathf.Clamp(difference.magnitude, 0f, 2f));
			tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * speed);									//wc
		}
	}

	public void Follow(bool doFollowCurrentTarget)
	{
		followTarget = doFollowCurrentTarget;
	}

	public void Follow(Transform t)
	{
		target = t;
		followTarget = true;
	}

	// ====================================================================================================================
}
