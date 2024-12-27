# Audune Unity Input System Utilities

[![openupm](https://img.shields.io/npm/v/com.audune.utils.inputsystem?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.audune.utils.inputsystem/)

This repository contains utility scripts for Unity's Input System package, used in Audune's own projects.

## Features

* Get references to an input binding or a group of input bindings from an input action, which include their associated bindex, action and control scheme. Rebind references via scripting.
* Return all available control schemes in the input system, and optionally filter them to only include the most specific ones. Normalize control paths using those control schemes for easier rebinding.
* Convert binding referenes to their string representation, either in plain text or as TextMesh Pro sprites.

## Installation

### Requirements

This package depends on the following packages:

* [Input System](https://docs.unity3d.com/Manual/com.unity.inputsystem.html) (Unity), version **1.5.0** or higher.

### Installing from the OpenUPM registry

To install this package as a package from the OpenUPM registry in the Unity Editor, use the following steps:

* In the Unity editor, navigate to **Edit › Project Settings... › Package Manager**.
* Add the following Scoped Registry, or edit the existing OpenUPM entry to include the new Scope:

```
Name:     package.openupm.com
URL:      https://package.openupm.com
Scope(s): com.audune.utils.inputsystem
```

* Navigate to **Window › Package Manager**.
* Click the **+** icon and click **Add package by name...**
* Enter the following name in the corresponding field and click **Add**:

```
com.audune.utils.inputsystem
```

### Installing as a Git package

To install this package as a Git package in the Unity Editor, use the following steps:

* In the Unity editor, navigate to **Window › Package Manager**.
* Click the **+** icon and click **Add package from git URL...**
* Enter the following URL in the URL field and click **Add**:

```
https://github.com/audunegames/inputsystem-utils.git
```

## Usage

The package contains the following classes:

* The `BindingReference` class is a wrapper around `Unity.InputSystem.InputBinding` and also contains the associated `Unity.InputSystem.InputAction`, `Unity.InputSystem.InputControlScheme` and binding index.
* The `BindingReferenceGroup` class is a wrapper around `IEnumerable<Unity.InputSystem.InputBinding>` and also contains the associated `Unity.InputSystem.InputAction`, `Unity.InputSystem.InputControlScheme` and part of composite name.
* The `ControlSchemeReference` class is a wrapper around `Unity.InputSystem.InputControlScheme` and contains the devices that are currently used by the control scheme.
* The `InputActionUtils` class contains extension methods for actions in the input system.
* The `InputControlSchemeUtils` class contains extension methods for control schemes in the input system.
* The `InputDisplayUtils` class contains extension methods for displaying bindings as strings.
* The `TextMeshProSprite` class is a convenient wrapper around a string containing rich text for a TextMeshPro sprite.

## Contributing

Contributions to this package are more than welcome! Contributing can be done by making a pull request with your updated code.

## License

This package is licensed under the GNU LGPL 3.0 license. See `LICENSE.txt` for more information.
