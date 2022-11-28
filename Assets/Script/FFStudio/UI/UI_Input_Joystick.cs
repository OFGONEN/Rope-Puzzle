/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class UI_Input_Joystick : UIEntity
{
#region Fields
  [ Title( "Shared Variable" ) ]
    public SharedInput_JoyStick input_joyStick;

  [ Title( "Components" ) ] 
    public RectTransform image_base;
    public RectTransform image_stick;

	UnityMessage onUpdate;
#endregion

#region Properties
#endregion

#region Unity API
	void Awake()
	{
		image_base.gameObject.SetActive( false );

		onUpdate = ExtensionMethods.EmptyMethod;
	}

	void Update()
	{
		onUpdate();
	}
#endregion

#region API
	public void OnJoystickUIEnable()
	{
		image_base.gameObject.SetActive( true );
		onUpdate = SetJoystickPosition;

		var position   = input_joyStick.FingerPosition;
		    position.y = uiTransform.position.y;

		uiTransform.position = position;
	}

	public void OnJoystickUIDisable()
	{
		image_base.gameObject.SetActive( false );
		onUpdate = ExtensionMethods.EmptyMethod;
	}
#endregion

#region Implementation
	void SetJoystickPosition()
	{
		image_stick.anchoredPosition = image_base.anchoredPosition + input_joyStick.SharedValue * GameSettings.Instance.ui_Entity_JoyStick_Gap;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
