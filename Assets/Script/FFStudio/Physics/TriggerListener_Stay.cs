/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public class TriggerListener_Stay : TriggerListener
	{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
        void OnTriggerStay( Collider other )
        {
			InvokeEvent( other );
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
}