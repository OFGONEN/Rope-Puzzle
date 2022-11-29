/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public class ColorSetter : MonoBehaviour
	{
#region Fields
		[ TitleGroup( "Setup" ), SerializeField ] Color[] color_array;
		[ ShowInInspector, ReadOnly ] Color color_current;
		[ ShowInInspector, ReadOnly ] int color_index;

		RecycledTween recycledTween = new RecycledTween();

		static int SHADER_ID_COLOR = Shader.PropertyToID( "_BaseColor" );
		Renderer theRenderer;
		MaterialPropertyBlock propertyBlock;
#endregion

#region Properties
#endregion

#region Unity API
		void Awake()
		{
			theRenderer = GetComponent< Renderer >();

			propertyBlock = new MaterialPropertyBlock();
		}
#endregion

#region API
		public void SetColor( int index )
		{
			color_index   = index;
			color_current = color_array[ index ];
			SetColor();
		}

		public void SetColorTween( int index )
		{
			color_index = index;

			recycledTween.Recycle( DOTween.To( GetColor, SetColor, color_array[ index ], GameSettings.Instance.button_color_change_duration )
			.SetEase( GameSettings.Instance.button_color_change_ease ) );
		}
		
		public void SetAlpha( float alpha )
		{
			color_current = color_current.SetAlpha( alpha );
			SetColor();
		}
#endregion

#region Implementation
		void SetColor( Color color )
		{
			color_current = color;
			SetColor();
		}

		void SetColor()
		{
			theRenderer.GetPropertyBlock( propertyBlock );
			propertyBlock.SetColor( SHADER_ID_COLOR, color_current );
			theRenderer.SetPropertyBlock( propertyBlock );
		}

		Color GetColor()
		{
			return color_current;
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}