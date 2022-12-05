/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public class TriggerListener_Exit : TriggerListener
	{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
        void OnTriggerExit( Collider other )
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