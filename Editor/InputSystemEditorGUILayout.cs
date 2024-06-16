using UnityEditor;
using UnityEngine;

namespace Audune.Utils.InputSystem.Editor
{
  // Class that defines GUI layout methods for input system in the Unity editor
  public static class InputSystemEditorGUILayout
  {
    // Draw a dropdown for action maps
    public static void ActionMapDropdown(GUIContent label, SerializedProperty property, params GUILayoutOption[] options)
    {
      var rect = EditorGUILayout.GetControlRect(label != null, EditorGUIUtility.singleLineHeight, options);
      InputSystemEditorGUI.ActionMapDropdown(rect, label, property);
    }

    // Draw a dropdown for control schemes
    public static void ControlSchemeDropdown(GUIContent label, SerializedProperty property, params GUILayoutOption[] options)
    {
      var rect = EditorGUILayout.GetControlRect(label != null, EditorGUIUtility.singleLineHeight, options);
      InputSystemEditorGUI.ControlSchemeDropdown(rect, label, property);
    }
  }
}