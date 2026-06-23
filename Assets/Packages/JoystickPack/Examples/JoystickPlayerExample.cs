using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThanhDV.Joystick.Examples
{
    public class JoystickPlayerExample : MonoBehaviour
    {
        public float speed;
        public VariableJoystick variableJoystick;
        public Rigidbody rb;

        public void FixedUpdate()
        {
            Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
            rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
