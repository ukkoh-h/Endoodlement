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
		public bool dash;
		public bool respawn;
		public bool interract;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		
#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
			AudioManager.Instance.PlayGravel();

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
            Debug.Log("Jumping!");
            AudioManager.Instance.PlaySFX("Jump");
        }

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
            Debug.Log("Sprinting!");

        }
        public void OnDash(InputValue value)
        {
            DashInput(value.isPressed);
            Debug.Log("Dashing!");
			AudioManager.Instance.PlaySFX("Dash");
        }

        public void OnRespawn(InputValue value)
        {
            RespawnInput(value.isPressed);
            Debug.Log("Dashing!");
        }

		public void OnInterract(InputValue value)
		{
			InterractInput(value.isPressed);
			Debug.Log("Interract");
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

        public void DashInput(bool newDashState)
        {
            dash = newDashState;
        }

        public void RespawnInput(bool newRespawnState)
        {
            respawn = newRespawnState;
        }

		public void InterractInput(bool newInterractState)
		{
			interract = newInterractState;
		}

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}