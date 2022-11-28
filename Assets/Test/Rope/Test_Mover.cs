/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Test_Mover : MonoBehaviour
{
#region Fields
    public float movement_speed;
    public Rigidbody _rigidbody;
#endregion

#region Properties
#endregion

#region Unity API
    void FixedUpdate()
    {
		// onFixedUpdate();

		float vertical = Input.GetAxis( "Vertical" );
		float horizontal = Input.GetAxis( "Horizontal" );

		var direction = Vector3.forward * vertical + Vector3.right * horizontal;

		_rigidbody.velocity = direction * movement_speed;
		transform.forward = direction;
	}
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
