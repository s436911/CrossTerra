using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public abstract class EditorBase : EditorWindow {
	void OnEnable() {
		OnInit();
	}

	void OnProjectChange() {
		OnInit();
	}

	/*
	void OnHierarchyChange() {
		OnInit();
	}*/

	void OnDestroy() {
		OnEnd();
	}

	protected abstract void OnInit();
	protected abstract void OnEnd();

	/// <summary> 繪製水平區塊 </summary>
	protected void BeginH() {
		GUILayout.BeginHorizontal();
	}

	protected void BeginH(string value = null, float size = 0) {
		if (string.IsNullOrEmpty(value)) {
			if (size > 0) {
				EditorGUILayout.BeginHorizontal(GUILayout.Width(size));
			} else {
				EditorGUILayout.BeginHorizontal();
			}
		} else {
			if (size > 0) {
				EditorGUILayout.BeginHorizontal(value, GUILayout.Width(size));
			} else {
				EditorGUILayout.BeginHorizontal(value);
			}
		}
	}

	/// <summary> 繪製垂直區塊 </summary>
	protected void BeginV() {
		GUILayout.BeginVertical();
	}

	protected void BeginV(string ask) {
		GUILayout.BeginVertical(ask);
	}

	protected void BeginV(string value = null, float size = 0) {
		if (string.IsNullOrEmpty(value)) {
			if (size > 0) {
				EditorGUILayout.BeginVertical(GUILayout.Width(size));
			} else {
				EditorGUILayout.BeginVertical();
			}
		} else {
			if (size > 0) {
				EditorGUILayout.BeginVertical(value, GUILayout.Width(size));
			} else {
				EditorGUILayout.BeginVertical(value);
			}
		}
	}

	protected void EndH() {
		GUILayout.EndHorizontal();
	}

	protected void EndV() {
		GUILayout.EndVertical();
	}
}
