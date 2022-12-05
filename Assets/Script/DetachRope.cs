/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

public class DetachRope : MonoBehaviour
{
#region Fields
    [ SerializeField ] SharedReferenceNotifier notif_rope_anchor_reference;
    [ SerializeField ] Rope rope;

    Transform ropeAnchorTransform;
#endregion

#region Properties
#endregion

#region Unity API
    private void Start()
    {
		ropeAnchorTransform = notif_rope_anchor_reference.sharedValue as Transform;
	}
    private void Update()
    {
        if( transform.InverseTransformPoint( ropeAnchorTransform.position ).z > 0 )
        {
			rope.DeatchRope();
		}
    }
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if( Application.isPlaying )
		    Gizmos.DrawLine( transform.position, ropeAnchorTransform.position );
	}
#endif
#endregion
}