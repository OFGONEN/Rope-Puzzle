/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Lean.Touch;
using Sirenix.OdinInspector;

namespace FFStudio
{
	[ CreateAssetMenu( fileName = "notif_input_joystick", menuName = "FF/Data/Shared/Input/JoyStick" ) ]
	public class SharedInput_JoyStick : SharedVector2Notifier
	{
#region Fields
		[ ShowInInspector, ReadOnly ] Vector2 finger_position;
		[ ShowInInspector, ReadOnly ] Vector2 finger_direction;

		public Vector2 FingerPosition  => finger_position;
		public Vector2 FingerDirection => finger_direction;
#endregion	

#region Properties
#endregion

#region API
		public void OnFingerDown( LeanFinger leanFinger )
		{
			finger_position = leanFinger.ScreenPosition;
		}

		public void OnFingerUp( LeanFinger leanFinger )
		{
			finger_position = leanFinger.ScreenPosition;
		}

		public void OnFingerUpdate( LeanFinger leanFinger )
		{
			finger_direction = leanFinger.ScreenPosition - finger_position;
			SharedValue      = finger_direction.normalized;
		}
#endregion
}
}