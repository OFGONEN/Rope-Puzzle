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
    [ SerializeField ] RopeMaterial rope_material;

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
		rope_length.SetValue_NotifyAlways( rope.CalculateLength() );
		rope_renderer.sharedMaterial = rope_material.Material;
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
		rope_cursor.ChangeLength( delta );
		rope_length.IncrementByAmount( delta );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}