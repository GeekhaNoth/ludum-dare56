using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        public PlayerController playerController;
        
        [Header("Camera Settings")]
        [Range(0, 100)]
        public float mouseSensitivity = 80f;
        public float smoothSpeed = 10f;
        
        private Vector2 _rotation = Vector2.zero;

        private void Update()
        {
            if (playerController == null) return;
            
            CameraLook();
        }

        private void LateUpdate()
        {
            if (playerController == null) return;
            
            transform.position = playerController.cameraPosition.position;
        }

        private void CameraLook()
        {
            float mouseX = playerController.LookDelta.x * (mouseSensitivity / 100);
            float mouseY = playerController.LookDelta.y * (mouseSensitivity / 100);
            
            _rotation.y += mouseX;
            
            _rotation.x -= mouseY;
            _rotation.x = Mathf.Clamp(_rotation.x, -89.9f, 89.9f);
            
            Quaternion targetRotation = Quaternion.Euler(_rotation.x, _rotation.y, 0);
            
            // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,  0.05f);
            transform.rotation = targetRotation;
            
            targetRotation = Quaternion.Euler(0, _rotation.y, 0);
            // playerController.localOrientation = Quaternion.Slerp(playerController.localOrientation, targetRotation, 0.05f);
            playerController.orientation.rotation = targetRotation;
        }
    }
}
