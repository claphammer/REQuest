// ====================================================================================================================
//*** This one is by Will
// ====================================================================================================================

using UnityEngine;
using System.Collections;

public class MMDirIndicator : MonoBehaviour
{
	public float speed = 5f;
	public Transform target;			// target to follow (make this the player)
	public Vector3 yOffset = Vector3.zero;		// offset from target's pivot
	public Transform camTr;				// main camera to bind to target
	private Transform tr;				// for storing the current Pivot GameObject transforms

	void Start()
	{
		tr = this.transform;
		if (target)
		{
			tr.position = target.position + yOffset;
			tr.rotation = target.rotation;
		}
	}

	void Update()
	{
		if (target)
		{

			tr.position = target.position + yOffset;
			tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * speed);
		}
	}


	// ====================================================================================================================
}

