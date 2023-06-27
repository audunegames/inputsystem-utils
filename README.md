# Audune Input System Utilities

This repository contains utility scripts for Unity's Input System package, used in Audune's own projects.

## Installation

Audune Input System Utilities can be best installed as a git package in the Unity Editor using the following steps:

* In the Unity editor, navigate to **Window > Package Manager**.
* Click the **+** icon and click **Add package from git URL...**
* Enter the following URL in the URL field and click **Add**:

```
https://github.com/audunegames/utils-inputsystem
```

## Contents

The package contains the following classes:

* The `BindingReference` class is a wrapper around `Unity.InputSystem.InputBinding` and also contains the associated `Unity.InputSystem.InputAction`, `Unity.InputSystem.InputControlScheme` and binding index.
* The `BindingReferenceGroup` class is a wrapper around `IEnumerable<Unity.InputSystem.InputBinding>` and also contains the associated `Unity.InputSystem.InputAction`, `Unity.InputSystem.InputControlScheme` and part of composite name.
* The `ControlSchemeReference` class is a wrapper around `Unity.InputSystem.InputControlScheme` and contains the devices that are currently used by the control scheme.
* The `InputActionUtils` class contains extension methods for actions in the input system.
* The `InputControlSchemeUtils` class contains extension methods for control schemes in the input system.
* The `InputDisplayUtils` class contains extension methods for displaying bindings as strings.
* The `TextMeshProSprite` class is a convenient wrapper around a string containing rich text for a TextMeshPro sprite.
