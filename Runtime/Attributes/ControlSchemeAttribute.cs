using System;
using UnityEngine;

namespace Audune.Utils.InputSystem
{
  // Attribute that specifies that a string should be interpreted as a name of an input control scheme
  [AttributeUsage(AttributeTargets.Field)]
  public class ControlSchemeAttribute : PropertyAttribute
  {
  }
}
