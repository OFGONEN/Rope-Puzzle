/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

public class SharedOffsetPositionSetter : MonoBehaviour
{
#region Fields
	[ SerializeField ] Vector3 position_offset;
	[ SerializeField ] SharedVector3Notifier notif_position;
	[ SerializeField ] Transform _transform;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
	public void SetPosition()
	{
		notif_position.SetValue_NotifyAlways( transform.position + position_offset );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireCube( transform.position + position_offset, Vector3.one * 0.25f );
	}
#endif
#endregion
}