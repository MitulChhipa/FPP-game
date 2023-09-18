using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] private float _smooth;
    [SerializeField] private float _swayMultiplier;
    [SerializeField] private CharacterController _characterController;
    private Vector3 _rotationVector;
    private Quaternion _rotation;
    

    private void Update()
    {
        _rotationVector.y = Input.GetAxis("Mouse X") * _swayMultiplier;
        _rotationVector.x = -Input.GetAxis("Mouse Y") * _swayMultiplier + _characterController.velocity.y*2f;

        _rotation = Quaternion.Euler(_rotationVector);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _rotation, _smooth*Time.deltaTime);

    }
}
