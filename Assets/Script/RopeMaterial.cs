/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "shared_rope_material", menuName = "FF/Game/Rope Material" ) ]
public class RopeMaterial : ScriptableObject 
{
  [ Title( "Setup" ) ]
    [ SerializeField ] Material rope_material;

	public Material Material => rope_material;
}