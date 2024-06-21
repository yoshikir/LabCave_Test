using UnityEngine;
using UnityEngine.InputSystem;

namespace KartGame.KartSystems {

    public class KeyboardInput : BaseInput
    {
        public bool isAccelerating;
        public bool isBraking;
        public float turnValue;

        /*public string TurnInputName = "Horizontal";
        public string AccelerateButtonName = "Accelerate";
        public string BrakeButtonName = "Brake";*/

        public void AccelerateInput(InputAction.CallbackContext context) {
            isAccelerating = context.action.triggered;
        }

        public void BrakeInput(InputAction.CallbackContext context) {
            isBraking = context.action.triggered;
        }

        public void TurnInput(InputAction.CallbackContext context) {
            turnValue = context.action.ReadValue<Vector2>().x;
        }

        public override InputData GenerateInput() {
            return new InputData
            {
                Accelerate = isAccelerating,
                Brake = isBraking,
                TurnInput = turnValue
            };
        }
    }
}
