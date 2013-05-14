// ====================================================================================================================
//
//*** This one is by Will
//
// ====================================================================================================================

using UnityEngine;

public class CameraMove : MonoBehaviour
{
	public float speed = 10f;
	public Transform target;			// target to follow (cam is fixed to following this around till it is NULL)
	public bool followTarget = true;	// follow the target? (only if target is not NULL)
	public bool allowInput = true;		// the cam wont read keyinput if set to false
	public Transform camTr;
	public Vector2 min_xz;
	public Vector2 max_xz;
	private Transform tr;

	void Start()
	{
		tr = this.transform;
		if (target && followTarget) tr.position = target.position;
		
	
		
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
