using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using InControl;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private int m_Jump;

		public int player_num;
		public InputDevice controller;

		private void Start(){

			controller = PlayerControllers.getPlayerController (player_num);
			InputManager.AttachDevice (controller);

//			if (player_num == 1) {
//				//controller = InputManager.Devices [0];
//				InputManager.AttachDevice(PlayerControllers.Player1);
//				controller = PlayerControllers.Player1;
//			} else if (player_num == 2) {
//				InputManager.AttachDevice(PlayerControllers.Player2);
//				controller = PlayerControllers.Player2;
//			} else if (player_num == 3) {
//				InputManager.AttachDevice(PlayerControllers.Player3);
//				controller = PlayerControllers.Player3;
//			} else if (player_num == 4) {
//				InputManager.AttachDevice(PlayerControllers.Player4);
//				controller = PlayerControllers.Player4;
//			}
		}


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
			if (controller == null) {
				controller = PlayerControllers.getPlayerController (player_num);
				return;
			}

            if (m_Jump == 0)
            {
                // Read the jump input in Update so button presses aren't missed.
                //m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
				//m_Jump = controller.Action1.WasPressed;

				if (controller.Action1.WasPressed) {
					m_Jump = 1;
				} else if (controller.Action1.WasReleased) {
					m_Jump = 2;
				}
            }
        }


        private void FixedUpdate()
        {
			if (controller == null) {
				Debug.Log ("no control " + player_num.ToString ());
				return;
			}

            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            //float h = CrossPlatformInputManager.GetAxis("Horizontal");
			float h = controller.LeftStickX;
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = 0;
        }
    }
}
