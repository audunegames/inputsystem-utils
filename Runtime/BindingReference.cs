using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

namespace Audune.Utils.InputSystem
{
  // Struct that defines a reference to an input binding
  // This is a one-to-one mapping from an input action to an input binding
  public sealed class BindingReference : IEquatable<BindingReference>
  {
    // Binding reference values
    public readonly InputAction action;
    public readonly InputControlScheme controlScheme;
    public readonly InputBinding binding;
    public readonly int bindingIndex;


    // Return the name of the binding reference
    public string Name {
      get {
        var actionName = action.actionMap != null && !string.IsNullOrEmpty(action.actionMap.name) ? $"{action.actionMap.name}/{action.name}" : action.name;
        return !string.IsNullOrEmpty(binding.name) ? $"{actionName}/{binding.name}" : actionName;
      }
    }


    // Constructor
    public BindingReference(InputAction action, InputControlScheme controlScheme, InputBinding binding, int bindingIndex)
    {
      this.action = action;
      this.controlScheme = controlScheme;
      this.binding = binding;
      this.bindingIndex = bindingIndex;
    }

    // Return the string representation of the binding reference
    public override string ToString()
    {
      return $"{Name} bound to {binding.ToDisplayString(InputBinding.DisplayStringOptions.DontOmitDevice)}";
    }


    #region Rebinding operations
    // Apply a binding override path on the binding reference
    public void ApplyBindingOverride(string path)
    {
      action.ApplyBindingOverride(bindingIndex, path);
    }

    // Apply a binding override on the binding reference
    public void ApplyBindingOverride(InputBinding bindingOverride)
    {
      action.ApplyBindingOverride(bindingIndex, bindingOverride);
    }

    // Remove a binding override on the binding reference
    public void RemoveBindingOverride()
    {
      action.RemoveBindingOverride(bindingIndex);
    }

    // Perform interactive rebinding on the binding reference
    public RebindingOperation PerformInteractiveRebinding()
    {
      return action.PerformInteractiveRebinding(bindingIndex);
    }
    #endregion

    #region Equatable implementation
    // Return if the binding reference equals another object
    public override bool Equals(object obj)
    {
      return Equals(obj as BindingReference);
    }

    // Return if the binding reference equals another binding
    public bool Equals(BindingReference other)
    {
      return other is not null &&
        EqualityComparer<InputAction>.Default.Equals(action, other.action) &&
        controlScheme.Equals(other.controlScheme) &&
        EqualityComparer<InputBinding>.Default.Equals(binding, other.binding) &&
        bindingIndex == other.bindingIndex;
    }

    // Return the hash code of the binding reference
    public override int GetHashCode()
    {
      return HashCode.Combine(action, controlScheme, binding, bindingIndex);
    }
    #endregion

    #region Equality operators
    // Return if the binding reference equals another binding reference
    public static bool operator ==(BindingReference left, BindingReference right)
    {
      return EqualityComparer<BindingReference>.Default.Equals(left, right);
    }

    // Return if the binding reference does not equal another binding reference
    public static bool operator !=(BindingReference left, BindingReference right)
    {
      return !(left == right);
    }
    #endregion

    #region Implicit operators
    // Return the binding of the binding reference
    public static implicit operator InputBinding(BindingReference bindingReference)
    {
      return bindingReference.binding;
    }
    #endregion
  }
}
