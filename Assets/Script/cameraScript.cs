using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class cameraScript : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _torch;
    [SerializeField] private Camera _cameraPOV;
    [SerializeField] private Image _torchImage;

    private bool _torchOn = false;
    private Vector3 _rotation;
    private float _x;
    private void Start()
    {
        _rotation = _camera.rotation.eulerAngles;
        _torch.SetActive(false);
    }

    private void Update()
    {
        _x = Input.GetAxis("Mouse Y");


        _rotation.x = _rotation.x - _x;
        
        _rotation.x = Mathf.Clamp(_rotation.x, -60, 60);
        _rotation.y = _player.rotation.eulerAngles.y;
        _rotation.z = _camera.rotation.eulerAngles.z;

        if (!_torchOn && Input.GetKeyDown(KeyCode.T))
        {
            _torchOn = true;
            _torchImage.color = Color.white;
            _torch.SetActive(true);
        }
        else if(_torchOn && Input.GetKeyDown(KeyCode.T))
        {
            _torchImage.color = Color.black;
            _torch.SetActive(false);
            _torchOn = false;
        }
    }
    private void LateUpdate()
    {
        _camera.rotation = Quaternion.Euler(_rotation);
    }
}
