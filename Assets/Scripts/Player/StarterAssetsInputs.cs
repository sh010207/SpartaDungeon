using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		[Header("Inventory")]
		public Action inventory;

		[Header("Conversion")]
		public bool conversion;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());			
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
            SprintInput(value.isPressed);
        }


        public void OnInventory()
		{
			inventory?.Invoke();
			ToggleCursor();
		}

		public void OnConversion()
		{
			if (!conversion)
			{
				conversion = true;
				CharacterManager.Instance.Player.camController.FirstPersonView();
			}
			else if (conversion)
			{
				conversion = false;
				CharacterManager.Instance.Player.camController.ThirdPersonView();
			}
		}

#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}



		private void ToggleCursor()
		{
			bool toggle = Cursor.lockState == CursorLockMode.Locked;
			Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
			cursorLocked = !toggle;
		}
	}
	
}