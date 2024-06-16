using UnityEditor;
using UnityEngine;

namespace Audune.Utils.InputSystem.Editor
{
  // Class that defines a drawer for a reference to an input control scheme
  [CustomPropertyDrawer(typeof(ControlSchemeAttribute))]
  public class ControlSchemeReferenceAttributeDrawer : PropertyDrawer
  {
    // Draw the property
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      InputSystemEditorGUI.ControlSchemeDropdown(position, label, property);
    }

    // Return the property height
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
      return EditorGUIUtility.singleLineHeight;
    }
  }
}