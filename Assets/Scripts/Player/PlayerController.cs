using System;
using TMPro;
using UnityEngine;
using Utility;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        public Transform orientation;
        public Transform cameraPosition;

        public CharacterController characterController;

        public LayerMask groundLayer;

        [Header("Player Movement")]
        public float walkSpeed = 6f;
        public float sprintSpeed = 10f;
        public float gravity = 9.81f;
        
        private float _moveSpeed;
        private float _yVelocity;
        private bool _isGrounded;

        private Vector2 _lookDelta;
        private Vector2 _moveAxis;
        public Vector2 LookDelta => _lookDelta;

        private Vector3 _moveDirection;
        private float _sideSpeed;
        
        private Vector3 _lastGroundedNormal;
        
        private Vector3 _slopeDirection;

        private void Start()
        {
            Camera.main.GetComponent<PlayerCamera>().playerController = this;
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            _moveSpeed = walkSpeed;
        }

        private void Update()
        {
            Move();
            Look();
            Sprint();
            MovePlayer();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.CheckLayerMask(groundLayer))
            {
                _lastGroundedNormal = hit.normal;
            }
        }

        private void MovePlayer()
        {
            Vector3 targetMoveDirection = orientation.forward * _moveAxis.y + orientation.right * _moveAxis.x;
            
            var gravityForce = gravity * Time.deltaTime;
            
            if (characterController.isGrounded)
                _yVelocity = -gravityForce;
            else
                _yVelocity -= gravityForce;
            
            _yVelocity = Mathf.Clamp(_yVelocity, -80f, 80f);
            
            _slopeDirection = Vector3.Normalize(targetMoveDirection - Vector3.Project(targetMoveDirection, _lastGroundedNormal));
            var slopeDot = Vector3.Dot(_slopeDirection, Vector3.up);
            if (slopeDot < 0.0f)
                targetMoveDirection = _slopeDirection;
            
            targetMoveDirection.y += _yVelocity;
            
            var forwardDot = Vector3.Dot(_moveDirection.normalized, orientation.forward);
            var rightDot = Vector3.Dot(_moveDirection.normalized, orientation.right);
            
            var isBackward = forwardDot < -0.5f;
            var isForward = forwardDot > 0.5f;

            Vector3 targetMotion;

            if (isForward)
                targetMotion = targetMoveDirection * _moveSpeed;
            else
                targetMotion = targetMoveDirection * walkSpeed;

            
            // smooth out the movement
            Vector3 velocity = Vector3.zero;
            _moveDirection = Vector3.SmoothDamp(_moveDirection, targetMotion, ref velocity, 0.02f);

            float sideVelocity = 0f;
            _sideSpeed = Mathf.SmoothDamp(_sideSpeed, rightDot, ref sideVelocity, 0.02f);
            
            characterController.Move(targetMotion * Time.deltaTime);

            // var horizontalSmoothedDirection = _moveDirection;
            // horizontalSmoothedDirection.y = 0;
            //
            // var speedSign = isForward 
            //     ? 1
            //     : isBackward
            //         ? -1
            //         : 0;
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            var position = transform.position;
            var directionPosition = position + _moveDirection * 4f;
            var slopeDirection = position + _slopeDirection * 4f;
            var normalPosition = position + _lastGroundedNormal * 4f;
            UnityEditor.Handles.DrawBezier(position, directionPosition, position, directionPosition, Color.blue, null, 5f);
            UnityEditor.Handles.DrawBezier(position, slopeDirection, position, slopeDirection, Color.green, null, 5f);
            UnityEditor.Handles.DrawBezier(position, normalPosition, position, normalPosition, Color.magenta, null, 5f);
#endif
        }


        private void Move()
        {
            _moveAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        private void Look()
        {
            _lookDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        private void Sprint()
        {
            _moveSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        }
    }
}
