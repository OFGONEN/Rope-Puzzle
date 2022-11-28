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
    [ SerializeField ] SharedVector3Notifier notif_button_position;

  [ Title( "Fired Events" ) ]
    [ SerializeField ] GameEvent event_input_joystick_enable;
    [ SerializeField ] GameEvent event_input_joystick_disable;
    [ SerializeField ] GameEvent event_rope_button_attach_done;
    [ SerializeField ] GameEvent event_rope_button_detach_done;

  [ Title( "Components" ) ]
    [ SerializeField ] Rigidbody _rigidbody;
    [ SerializeField ] Collider _collider;

	RecycledSequence recycledSequence = new RecycledSequence();

	Vector3 rope_attach_position;
	Vector3 rope_attach_rotation;

	UnityMessage onFixedUpdate;
    UnityMessage onFingerDown;
    UnityMessage onFingerUp;
#endregion

#region Properties
#endregion

#region Unity API
	private void OnDisable()
	{
		recycledSequence.Kill();
	}
	
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
	}

    public void OnFingerDown()
    {
		onFingerDown();
	}

    public void OnFingerUp()
    {
		onFingerUp();
	}

	public void OnRopeAttach()
	{
		EmptyDelegates();
		event_input_joystick_disable.Raise();

		_rigidbody.isKinematic = true;

		rope_attach_position = transform.position;
		rope_attach_rotation = transform.eulerAngles;

		var buttonPosition = notif_button_position.sharedValue;

		var sequence = recycledSequence.Recycle( OnAttachDone );

		sequence.Append( transform.DOJump( buttonPosition, GameSettings.Instance.rope_attach_jump_power, 1, GameSettings.Instance.rope_attach_jump_duration ).SetEase( GameSettings.Instance.rope_attach_jump_ease ) );

		sequence.Join( transform.DOLookAt( buttonPosition, GameSettings.Instance.rope_attach_rotate_duration, AxisConstraint.Y ).SetEase( GameSettings.Instance.rope_attach_rotate_ease ) );

		sequence.AppendInterval( GameSettings.Instance.rope_attach_delay );

		sequence.Append( transform.DOMove( buttonPosition.SetY( 0 ), GameSettings.Instance.rope_attach_fall_duration )
			.SetEase( GameSettings.Instance.rope_attach_fall_ease ) );
	}

	public void OnRopeDetach()
	{
		var buttonPosition = notif_button_position.sharedValue;

		var sequence = recycledSequence.Recycle( OnDetachDone );

		sequence.Append( transform.DOMove( buttonPosition, GameSettings.Instance.rope_detach_rise_duration )
			.SetEase( GameSettings.Instance.rope_detach_rise_ease ) );

		sequence.AppendInterval( GameSettings.Instance.rope_detach_delay );

		sequence.Append( transform.DOJump( rope_attach_position.SetY( 0 ), GameSettings.Instance.rope_detach_jump_power, 1, GameSettings.Instance.rope_detach_jump_duration )
			.SetEase( GameSettings.Instance.rope_detach_jump_ease ) );

		sequence.Join( transform.DORotate( rope_attach_rotation, GameSettings.Instance.rope_detach_rotate_duration )
		.SetEase( GameSettings.Instance.rope_detach_rotate_ease ) );
	}
#endregion

#region Implementation
	void OnAttachDone()
	{
		event_rope_button_attach_done.Raise();
	}

	void OnDetachDone()
	{
		event_rope_button_detach_done.Raise();
	}

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