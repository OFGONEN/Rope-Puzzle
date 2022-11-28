/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class RopeAnchor : MonoBehaviour
{
#region Fields
  [ Title( "Shared Variables" ) ]
    [ SerializeField ] SharedInput_JoyStick shared_input_joystick;

  [ Title( "Fired Events" ) ]
    [ SerializeField ] GameEvent event_input_joystick_enable;
    [ SerializeField ] GameEvent event_input_joystick_disable;

  [ Title( "Components" ) ]
    [ SerializeField ] Rigidbody _rigidbody;
    [ SerializeField ] Collider _collider;

    UnityMessage onFixedUpdate;
    UnityMessage onFingerDown;
    UnityMessage onFingerUp;
#endregion

#region Properties
#endregion

#region Unity API
    void Awake()
    {
		EmptyDelegates();
	}

	void FixedUpdate()
	{
		onFixedUpdate();
	}
#endregion

#region API
    public void OnLevelStart()
    {
		onFingerDown = StartMovement;
		FFLogger.Log( "On Level Start", this );
	}

    public void OnFingerDown()
    {
		FFLogger.Log( "On Finger Down", this );
		onFingerDown();
	}

    public void OnFingerUp()
    {
		FFLogger.Log( "On Finger Up", this );
		onFingerUp();
	}
#endregion

#region Implementation
    void StartMovement()
    {
		onFingerDown  = ExtensionMethods.EmptyMethod;
		onFingerUp    = StopMovement;
		onFixedUpdate = Movement;

		event_input_joystick_enable.Raise();
	}

    void StopMovement()
    {
		EmptyDelegates();

		_rigidbody.velocity = Vector3.zero;

		onFingerDown = StartMovement;

		event_input_joystick_disable.Raise();
	}

    void Movement()
    {
		_rigidbody.velocity = shared_input_joystick.sharedValue.ConvertV3_Z() * GameSettings.Instance.rope_movement_speed;
		transform.LookAtDirectionOverTimeFixedTime( shared_input_joystick.sharedValue.ConvertV3_Z(), GameSettings.Instance.rope_movement_rotate_speed );
	}

    void EmptyDelegates()
    {
		onFixedUpdate = ExtensionMethods.EmptyMethod;
		onFingerDown  = ExtensionMethods.EmptyMethod;
		onFingerUp    = ExtensionMethods.EmptyMethod;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
