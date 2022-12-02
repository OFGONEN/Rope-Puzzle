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

  [ Title( "Components" ) ]
    [ SerializeField ] Renderer rope_renderer;
    [ SerializeField ] ObiRopeCursor rope_cursor;
    [ SerializeField ] ObiRope rope;
#endregion

#region Properties
#endregion

#region Unity API
    void Awake()
    {
		rope_renderer.sharedMaterial = CurrentLevelData.Instance.levelData.rope_material.Material;
	}

	void Start()
	{
		rope_length.SetValue_NotifyAlways( rope.CalculateLength() );
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
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
	[ Button() ]
	void LogRopeLength()
	{
		FFLogger.Log( "Rope Length: " + rope.CalculateLength() );
	}
#endif
#endregion
}