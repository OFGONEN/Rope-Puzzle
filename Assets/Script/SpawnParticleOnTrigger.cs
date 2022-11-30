/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

public class SpawnParticleOnTrigger : MonoBehaviour
{
#region Fields
    [ SerializeField ] ParticleData particle_data_array;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void SpawnParticle( Collision collision )
    {
		Transform parent = particle_data_array.parent ? transform : null;
		particle_data_array.Raise( collision.GetContact( 0 ).point, parent );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
