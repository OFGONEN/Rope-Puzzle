/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
using Sirenix.OdinInspector;
using FFStudio;

public class Rope : MonoBehaviour
{
#region Fields
  [ Title( "Shared Variables" ) ]
    [ SerializeField ] RopeLenght rope_length;
    [ SerializeField ] SharedReferenceNotifier notif_rope_anchor_reference;
    [ SerializeField ] GameEvent event_level_failed;

  [ Title( "Components" ) ]
    [ SerializeField ] Renderer rope_renderer;
    [ SerializeField ] ObiRopeCursor rope_cursor;
    [ SerializeField ] ObiRope rope;
    [ SerializeField ] ObiParticleAttachment rope_attachment;

	Transform rope_anchor;
	float rope_anchor_distance;
	UnityMessage onUpdate;
#endregion

#region Properties
#endregion

#region Unity API
    void Awake()
    {
		rope_renderer.sharedMaterial = CurrentLevelData.Instance.levelData.rope_material.Material;
		onUpdate = ExtensionMethods.EmptyMethod;
	}

	void Start()
	{
		rope_length.SetValue_NotifyAlways( rope.CalculateLength() );
	}

	void Update()
	{
		onUpdate();
	}
#endregion

#region API
	public void OnRopeChangeMaterial( RopeMaterial ropeMaterial )
	{
		rope_renderer.sharedMaterial = ropeMaterial.Material;
	}

	[ Button() ]
    public void OnRopeChangeLength( float delta )
    {
		rope_length.IncrementByAmount( delta );
		rope_cursor.ChangeLength( rope_length.sharedValue );
	}

	public void OnLevelStarted()
	{
		rope_anchor          = notif_rope_anchor_reference.sharedValue as Transform;
		rope_anchor_distance = Vector3.Distance( rope.GetParticlePosition( 0 ), rope_anchor.position ) + GameSettings.Instance.rope_detach_buffer;
		onUpdate             = CheckRopeAnchor;
	}

	public void OnLevelFinished()
	{
		onUpdate = ExtensionMethods.EmptyMethod;
	}

	public void OnRopeAnchorAttachStart()
	{
		onUpdate = ExtensionMethods.EmptyMethod;
	}

	public void OnRopeAnchorDetachDone()
	{
		onUpdate = CheckRopeAnchor;
	}
#endregion

#region Implementation
	void CheckRopeAnchor()
	{
		if( Vector3.Distance( rope.GetParticlePosition( 0 ), rope_anchor.position ) > rope_anchor_distance )
		{
			onUpdate = ExtensionMethods.EmptyMethod;
			rope_attachment.breakThreshold = 0;
			event_level_failed.Raise();

			FFLogger.Log( "Rope Detached", this );
		}
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
	[ Button() ]
	void LogRopeLength()
	{
		FFLogger.Log( "Rope Length: " + rope.CalculateLength() );
	}

	[ Button() ]
	void DeatchRope()
	{
		rope_attachment.breakThreshold = 0;
	}

	[ Button() ]
	void CacheParticleAttachment()
	{
		UnityEditor.EditorUtility.SetDirty( gameObject );

		var attachments = GetComponentsInChildren< ObiParticleAttachment >();

		for( var i = 0; i < attachments.Length; i++ )
		{
			var attachment = attachments[ i ];
			if( attachment.attachmentType == ObiParticleAttachment.AttachmentType.Dynamic )
				rope_attachment = attachment;
		}
	}
#endif
#endregion
}