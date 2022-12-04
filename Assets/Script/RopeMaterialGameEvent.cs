/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "event_rope_material", menuName = "FF/Game/Rope Material Event" ) ]
public class RopeMaterialGameEvent : GameEvent
{
	public RopeMaterial eventValue;

	public void Raise( RopeMaterial value )
	{
		eventValue = value;
		Raise();
	}
}