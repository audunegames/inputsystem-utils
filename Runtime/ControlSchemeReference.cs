using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

namespace Audune.Utils.InputSystem
{
  // Class that defines a reference to a control scheme with its associated devices
  public sealed class ControlSchemeReference : IEquatable<ControlSchemeReference>
  {
    // Control scheme reference values
    public readonly InputControlScheme controlScheme;
    public readonly InputDevice[] devices;


    // Constructor
    public ControlSchemeReference(InputControlScheme controlScheme, InputDevice[] devices)
    {
      this.controlScheme = controlScheme;
      this.devices = devices;
    }

    // Return the string representation of the control scheme reference
    public override string ToString()
    {
      return $"{controlScheme.name} with {string.Join(", ", devices.Select(device => device.name))}";
    }


    #region Equatable implementation
    // Return if the control scheme equals another object
    public override bool Equals(object obj)
    {
      return Equals(obj as ControlSchemeReference);
    }

    // Return if the control scheme equals another control scheme
    public bool Equals(ControlSchemeReference other)
    {
      return other is not null &&
        controlScheme.Equals(other.controlScheme) &&
        EqualityComparer<InputDevice[]>.Default.Equals(devices, other.devices);
    }

    // Return the hash code of the control scheme
    public override int GetHashCode()
    {
      return HashCode.Combine(controlScheme, devices);
    }
    #endregion

    #region Equality operators
    // Return if the control scheme reference equals another control scheme reference
    public static bool operator ==(ControlSchemeReference left, ControlSchemeReference right)
    {
      return EqualityComparer<ControlSchemeReference>.Default.Equals(left, right);
    }

    // Return if the control scheme reference equals another control scheme reference
    public static bool operator !=(ControlSchemeReference left, ControlSchemeReference right)
    {
      return !(left == right);
    }
    #endregion

    #region Implicit operators
    // Return the control scheme of the control scheme reference
    public static implicit operator InputControlScheme(ControlSchemeReference controlSchemeReference)
    {
      return controlSchemeReference.controlScheme;
    }
    #endregion
  }
}
