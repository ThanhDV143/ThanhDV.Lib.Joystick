# Joystick Pack

On-screen virtual joystick for Unity UI. Four joystick variants (Fixed, Floating, Dynamic, Variable) built on top of Unity's EventSystem with full **Input System Package** support.

## Requirements

- Unity 2021.3+ (UGUI)
- `com.unity.inputsystem` 1.4+ (Input System Package)
- Project Settings → Player → **Active Input Handling = Input System Package (New)**
- Scene `EventSystem` must use **`InputSystemUIInputModule`** (not `StandaloneInputModule`)

## Installation
### Unity Package Manager
```
https://github.com/ThanhDV143/ThanhDV.Lib.Joystick.git?path=/Assets/Packages/JoystickPack
```

1. In Unity, open **Window** → **Package Manager**.
2. Press the **+** button, choose "**Add package from git URL...**"
3. Enter url above and press **Add**.

### Scoped Registry

1. In Unity, open **Project Settings** → **Package Manager** → **Add New Scoped Registry**
- ``Name`` ThanhDVs
- ``URL`` https://upm.thanhdv.com
- ``Scope(s)`` thanhdv

2. In Unity, open **Window** → **Package Manager**.
- Press the **+** button, choose "**Add package by name...**" → ``thanhdv.joystick``
- or
- Press the **Packages** button, choose "**My Registries**"

## Quick start

1. Add a Canvas to your scene (if you don't have one).
2. Drag a prefab from `JoystickPack/Prefabs/` into the Canvas:
   - `Fixed Joystick`
   - `Floating Joystick`
   - `Dynamic Joystick`
   - `Variable Joystick`
3. Reference it from your script and read `Horizontal` / `Vertical` / `Direction`.

```csharp
using UnityEngine;
using ThanhDV.Joystick;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public float speed = 5f;
    public Rigidbody rb;

    void FixedUpdate()
    {
        Vector3 dir = Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical;
        rb.AddForce(dir * speed, ForceMode.VelocityChange);
    }
}
```

## Joystick types

| Type | Behaviour |
|---|---|
| **Fixed** | Stays at a fixed screen position. |
| **Floating** | Appears where the user first touches, stays anchored there until release. |
| **Dynamic** | Appears where the user first touches and follows the touch as it drags past `Move Threshold`. |
| **Variable** | Switches between Fixed / Floating / Dynamic at runtime via `SetMode()`. |

### Structure

- **Fixed**: just `Background` + `Handle`. Anchor the background to any corner.
- **Floating / Dynamic**: `Background` + `Handle` inside an empty `RectTransform` that defines the **valid touch area**.
- **Variable**: same as Floating/Dynamic; the background's initial position is where it appears in Fixed mode.

## API

### `Joystick` (base)

| Member | Type | Description |
|---|---|---|
| `Horizontal` | `float` | Current X input ([-1, 1]). |
| `Vertical` | `float` | Current Y input ([-1, 1]). |
| `Direction` | `Vector2` | `(Horizontal, Vertical)`. |
| `HandleRange` | `float` | Distance the handle can travel from center. |
| `DeadZone` | `float` | Distance from center before input registers. |
| `AxisOptions` | `AxisOptions` | `Both`, `Horizontal`, or `Vertical`. |
| `SnapX` | `bool` | Snap horizontal axis to whole values (-1, 0, 1). |
| `SnapY` | `bool` | Snap vertical axis to whole values (-1, 0, 1). |

### `DynamicJoystick`

| Member | Type | Description |
|---|---|---|
| `MoveThreshold` | `float` | Distance input must travel before the joystick body starts moving. |

### `VariableJoystick`

| Member | Type | Description |
|---|---|---|
| `MoveThreshold` | `float` | Same as DynamicJoystick. |
| `SetMode(JoystickType)` | `void` | Switch between `Fixed`, `Floating`, `Dynamic` at runtime. |

## Inspector properties

**Joystick (all types)**

| Property | Function |
|---|---|
| Handle Range | How far the visual handle moves from center. |
| Dead Zone | Minimum input distance before registering. |
| Axis Options | Restrict input to `Both` / `Horizontal` / `Vertical`. |
| Snap X / Snap Y | Snap to whole values along each axis. |
| Background | `RectTransform` of the background visual. |
| Handle | `RectTransform` of the handle visual. |

**Dynamic** adds: `Move Threshold`.
**Variable** adds: `Move Threshold`, `Joystick Type`.

## Examples

See `Examples/Example Scene.unity` for:
- `JoystickPlayerExample` — drive a Rigidbody from joystick input.
- `JoystickSetterExample` — switch joystick mode, axis, and snapping at runtime.
