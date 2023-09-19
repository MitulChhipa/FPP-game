using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] private float _smooth;
    [SerializeField] private float _swayMultiplier;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Vector2 _clampingMax;
    [SerializeField] private Vector2 _clampingMin;
    private Vector3 _rotationVector;
    private Quaternion _rotation;
    

    private void Update()
    {
        //swaying the weapon container in y axis using mouse input and in x axis using mouse input and player velocity
        _rotationVector.y = Input.GetAxis("Mouse X") * _swayMultiplier;
        _rotationVector.x = -Input.GetAxis("Mouse Y") * _swayMultiplier + _characterController.velocity.y*2f;

        _rotationVector.x = Mathf.Clamp(_rotationVector.x, _clampingMin.x, _clampingMax.x);
        _rotationVector.y = Mathf.Clamp(_rotationVector.y, _clampingMin.y, _clampingMax.y);

        _rotation = Quaternion.Euler(_rotationVector);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _rotation, _smooth*Time.deltaTime);

    }
}
