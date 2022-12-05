﻿/* Created by and for usage of FF Studios (2021). */

using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FFStudio
{
    public abstract class RuntimeSet< TKey, TValue > : ScriptableObject
    {
		public int setSize;
		[ ShowInInspector ]
		public List< TValue > itemList = new List< TValue >();
		[ ShowInInspector ]
		public Dictionary< TKey, TValue > itemDictionary = new Dictionary< TKey, TValue >();

		public void AddList( TValue value )
		{
#if UNITY_EDITOR
			if( !itemList.Contains( value ) )
				itemList.Add( value );
			else
				FFLogger.Log( "Trying to add same value to RunTime-LIST", value as Object );
#else
			itemList.Add( value );
#endif
		}

		public void RemoveList( TValue value )
		{
			itemList.Remove( value );
		}

		public void AddDictionary( TKey key, TValue value )
		{
#if UNITY_EDITOR
			if( !itemDictionary.ContainsKey( key ) )
				itemDictionary.Add( key, value );
			else
				FFLogger.Log( "Trying to add same value to RunTime-DICTIONARY", value as Object );
#else
			itemDictionary.Add( key, value );
#endif
		}
        
		public void RemoveDictionary( TKey key )
		{
			itemDictionary.Remove( key );
		}

		public void AddToBoth( TKey key, TValue value )
		{
			AddDictionary( key, value );
			AddList( value );
		}

		public void RemoveFromBoth( TKey key, TValue value )
		{
			RemoveDictionary( key );
			RemoveList( value );
		}

		[ Button ]
		public void ClearSet()
		{
			itemList.Clear();
			itemDictionary.Clear();
		}
        
		[ Button ]
		public void LogList()
		{
			foreach( var item in itemList )
				Debug.Log( item.ToString() );
		}

		[ Button ]
		public void LogDictionary()
		{
			foreach( var item in itemDictionary.Values )
				Debug.Log( item.ToString() );
		}
    }
}