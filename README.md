# Common Utilities

This repository provides common utility scripts for Unity 2022.3 LTS or highter, used in Audune's own projects.

## Input

The input utility scripts provide extension methods and extra classes for the [Input System](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.5/manual/index.html) package:

* The `Binding` class is a wrapper around `Unity.InputSystem.InputBinding` and also contains the associated `Unity.InputSystem.InputAction`, `Unity.InputSystem.InputControlScheme` and binding index.
* The `BindingGroup` class is a wrapper around `IEnumerable<Unity.InputSystem.InputBinding>` and also contains the associated `Unity.InputSystem.InputAction`, `Unity.InputSystem.InputControlScheme` and part of composite name.
* The `InputActionUtils` class contains extension methods for actions in the input system.
* The `InputControlSchemeUtils` class contains extension methods for control schemes in the input system:
* The `InputDisplayUtils` class contains extension methods for displaying bindings as strings:
* The `RuntimeControlScheme` class is a wrapper around `Unity.InputSystem.InputControlScheme` and contains the devices that are currently used by the control scheme.
* The `TextMeshProSprite` class is a convenient wrapper around a string containing rich text for a TextMeshPro sprite.
