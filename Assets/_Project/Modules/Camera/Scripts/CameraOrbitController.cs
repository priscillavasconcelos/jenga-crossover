using Jenga.Builder;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Jenga.Camera
{
    public class CameraOrbitController : MonoBehaviour
    {
        [SerializeField] private BuildStacksManager _buildStacksManager;

        [Range(0.01f, 20)]
        [SerializeField] private float _distanceBetweenCameraAndTarget;

        [Range(0.1f, 5f)]
        [SerializeField] private float _mouseRotateSpeed = 0.8f;
        
        [Range(0.01f, 10)]
        [SerializeField] private float _slerpValue = 0.25f;

        [Range(0.01f, 20)]
        [SerializeField] private float _wheelSpeed = 10;

        [SerializeField] private Vector3 _heightOffset;
        
        private Transform _target;
        private Transform _transform;
        
        private Quaternion _cameraRot;

        private float _minXRotAngle = -80;
        private float _maxXRotAngle = 80;

        private float _rotX;
        private float _rotY;

        private float _zoomAmount = 0;
        private float _maxToClamp = 10;

        private Vector3 _position;

        private void Awake()
        {
            _transform = transform;
            _buildStacksManager.BuildingIsDone += FinishedBuilding;
        }

        private void FinishedBuilding(List<FocusButtonBehavour> obj)
        {
            foreach (var behavour in obj) 
            {
                behavour.StackSelected += ChangeTarget;
            }

            ChangeTarget(obj[obj.Count - 1].GetStack());
            
        }

        private void ChangeTarget(GameObject obj)
        {
            _target = obj.transform;
        }

        private void Update()
        {
            if (_target == null)
                return;

            if (Input.GetMouseButton(1))
            {
                _rotX += -Input.GetAxis("Mouse Y") * _mouseRotateSpeed; 
                _rotY += Input.GetAxis("Mouse X") * _mouseRotateSpeed;
            }

            if (_rotX < _minXRotAngle)
            {
                _rotX = _minXRotAngle;
            }
            else if (_rotX > _maxXRotAngle)
            {
                _rotX = _maxXRotAngle;
            }

            var wheel = Input.GetAxis("Mouse ScrollWheel");
            _zoomAmount += wheel;
            _zoomAmount = Mathf.Clamp(_zoomAmount, -_maxToClamp, _maxToClamp);
            
            _distanceBetweenCameraAndTarget -= wheel * _wheelSpeed;
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            Vector3 dir = new Vector3(0, 0, -_distanceBetweenCameraAndTarget); 

            Quaternion newQ = Quaternion.Euler(_rotX, _rotY, 0); 
            
            _cameraRot = Quaternion.Slerp(_cameraRot, newQ, _slerpValue);

            _position = _target.position + _heightOffset;

            _transform.position = _position + _cameraRot * dir;
            _transform.LookAt(_position);

        }

    }
}