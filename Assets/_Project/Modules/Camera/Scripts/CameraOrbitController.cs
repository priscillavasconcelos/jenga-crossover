using Jenga.Builder;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jenga.Camera
{
    public class CameraOrbitController : MonoBehaviour
    {
        [SerializeField] private BuildStacksManager _buildStacksManager;

        [Range(0.1f, 5f)]
        [SerializeField] private float _mouseRotateSpeed = 0.8f;
        
        [Range(0.01f, 10)]
        [SerializeField] private float _slerpValue = 0.25f;

        private Transform _target;
        private Transform _transform;
        
        private Quaternion _cameraRot;

        private float _distanceBetweenCameraAndTarget;

        private float _minXRotAngle = -80;
        private float _maxXRotAngle = 80;

        private float _rotX;
        private float _rotY;

        private void Awake()
        {
            _transform = transform;
            _buildStacksManager.BuildingIsDone += FinishedBuilding;
        }

        private void FinishedBuilding(List<StackController> obj)
        {
            _target = obj[0].transform;

            _distanceBetweenCameraAndTarget = Vector3.Distance(_transform.position, _target.position);
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

        }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            Vector3 dir = new Vector3(0, 0, -_distanceBetweenCameraAndTarget); 

            Quaternion newQ = Quaternion.Euler(_rotX, _rotY, 0); 
            
            _cameraRot = Quaternion.Slerp(_cameraRot, newQ, _slerpValue);

            _transform.position = _target.position + _cameraRot * dir;
            _transform.LookAt(_target.position);

        }

    }
}