/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
using Sirenix.OdinInspector;

public class Test_RopeCursor : MonoBehaviour
{
#region Fields
    public ObiRopeCursor rope_cursor;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    [ Button() ]
    public void ChangeLength( float length )
    {
		rope_cursor.ChangeLength( length );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
