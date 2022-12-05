/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
    public class CameraFollow : MonoBehaviour
    {
#region Fields
    [ Title( "Setup" ) ]
        [ SerializeField ] SharedReferenceNotifier notifier_reference_transform_target;

        Transform transform_target;
        Vector3 followOffset;

		float camera_height;

		UnityMessage updateMethod;
#endregion

#region Properties
#endregion

#region Unity API
        void OnDisable()
        {
			updateMethod = ExtensionMethods.EmptyMethod;
		}

        void Awake()
        {
            updateMethod = ExtensionMethods.EmptyMethod;
        }

        private void Start()
        {
			transform_target = notifier_reference_transform_target.SharedValue as Transform;

			transform.position    = transform_target.position + GameSettings.Instance.camera_follow_offset;
			transform.eulerAngles = GameSettings.Instance.camera_follow_rotation;

			camera_height = transform.position.y;
        }

        void FixedUpdate()
        {
            updateMethod();
        }
#endregion

#region API
        public void LevelRevealedResponse()
        {
            updateMethod = FollowTarget;
        }

        public void LevelFinishedResponse()
        {
            updateMethod = ExtensionMethods.EmptyMethod;
        }
#endregion

#region Implementation
        void FollowTarget()
        {
            // Info: Simple follow logic.
            var target_position    = transform_target.position + GameSettings.Instance.camera_follow_offset;
                transform.position = Vector3.Lerp( transform.position, target_position.SetY( camera_height ), GameSettings.Instance.camera_follow_speed * Time.fixedDeltaTime );
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
        [ Button() ]
        void UpdateCameraPosition()
        {
			UnityEditor.EditorUtility.SetDirty( gameObject );
			var playerPosition        = GameObject.FindGameObjectWithTag( "Player" ).transform.position;
			    transform.position    = playerPosition + GameSettings.Instance.camera_follow_offset;
			    transform.eulerAngles = GameSettings.Instance.camera_follow_rotation;
		}

        [ Button() ]
        void LogCameraOffset()
        {
			var playerPosition = GameObject.FindGameObjectWithTag( "Player" ).transform.position;
			var offset         = playerPosition - transform.position;

            FFLogger.Log( "Offset: " + offset );
		}
#endif
#endregion
    }
}