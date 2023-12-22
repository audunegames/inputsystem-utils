using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

namespace Audune.Utils.InputSystem
{
  // Class that defines utility methods for actions and bindings
  public static class InputActionUtils
  {
    #region Getting binding references of InputActions that match a control scheme
    // Return the bindings of an action that match the specified control scheme
    public static IEnumerable<BindingReference> GetBindingReferences(this InputAction action, InputControlScheme controlScheme)
    {
      for (var i = 0; i < action.bindings.Count; i++)
      {
        var binding = action.bindings[i];
        if (InputBinding.MaskByGroup(controlScheme.bindingGroup).Matches(binding))
          yield return new BindingReference(action, controlScheme, binding, i);
      }
    }

    // Return the combined binding references of an action that match the specified control scheme
    public static IEnumerable<BindingReferenceGroup> GetCombinedBindingReferences(this InputAction action, InputControlScheme controlScheme)
    {
      return GetBindingReferences(action, controlScheme)
        .GroupBy(binding => binding.binding.name)
        .Select(bindingGroup => new BindingReferenceGroup(action, controlScheme, bindingGroup, bindingGroup.Key));
    }

    // Return the combined binding references of an action with the specified composite name that match the specified control scheme
    public static BindingReferenceGroup GetGroupedBindingReferences(this InputAction action, InputControlScheme controlScheme, string partOfCompositeName)
    {
      return GetCombinedBindingReferences(action, controlScheme)
        .FirstOrDefault(bindings => bindings.partOfCompositeName == partOfCompositeName);
    }
    #endregion

    #region Getting binding references of an enumerable of InputActions that match a control scheme
    // Return the binding references of an enumerable of actions that match the specified control scheme
    public static IEnumerable<BindingReference> GetBindingReferences(this IEnumerable<InputAction> actions, InputControlScheme controlScheme)
    {
      return actions.SelectMany(action => GetBindingReferences(action, controlScheme));
    }

    // Return the combined binding references of an enumerable of actions that match the specified control scheme
    public static IEnumerable<BindingReferenceGroup> GetCombinedBindingReferences(this IEnumerable<InputAction> actions, InputControlScheme controlScheme)
    {
      return actions.SelectMany(action => GetCombinedBindingReferences(action, controlScheme));
    }
    #endregion

    #region Upating runtime bindings
    // Return a binding group with updated bindings for the binding group
    public static BindingReferenceGroup WithUpdatedBindings(this BindingReferenceGroup bindings)
    {
      return GetGroupedBindingReferences(bindings.action, bindings.controlScheme, bindings.partOfCompositeName);
    }
    #endregion
  }
}