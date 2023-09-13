using UnityEngine;

public class playerMovementCC : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _rotationSpeed, _walkSpeed, _sprintSpeed, _swimSpeed;
    [SerializeField] private PlayerHealth _ph;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravitationalAcc;
    [SerializeField] private Animator _movementAnimator;
    [SerializeField] private AudioSource _walkAudio;
    [SerializeField] private AudioSource _runAudio;
    [SerializeField] private AudioSource _jumpAudio;
    [SerializeField] private AudioSource _dropAudio;


    private float _noFoodWaterHealthChange = -5f;
    private float _y, _playerSpeed;
    private Vector3 _localDirection = new Vector3();
    private Vector3 _inputDirection = new Vector3();
    private Vector3 _gravity = new Vector3(0,0,0);
    private bool _isWalking, _shift, _jump, _onGround, _isRunning, _actionInput, _crouch;
    private float _timer;
    private bool _isSwimming;

    #region MonoFunctions
    private void Start()
    {
        //_state = playerState.idle;
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        inputStates();
        movement();
        jump();
        crouch();
        
        _timer = _timer + Time.deltaTime;
        if(_timer >=1)
        {
            HealthFunction();
            _timer= 0;
        }
    }

    private void LateUpdate()
    {
        cameraRotation();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Water"))
        {
            _isSwimming = true;
            if(Input.GetKeyDown(KeyCode.E))
            {
                _ph.changeWater(100);
            }
        }
        else
        {
            _isSwimming = false;
        }
    }
    #endregion

    #region Inputs
    private void inputStates()
    {
        _y = Input.GetAxis("Mouse X");
        _crouch = Input.GetKey(KeyCode.C);
        _actionInput = Input.GetKey(KeyCode.E);
       
        _inputDirection.x = Input.GetAxis("Horizontal");
        _inputDirection.z = Input.GetAxis("Vertical");

        _isWalking = _inputDirection.x != 0 || _inputDirection.z != 0;
        _jump = Input.GetKeyDown(KeyCode.Space);
        _shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        //Setting speed of the player
        if(_isSwimming)
        {
            _playerSpeed = _swimSpeed;
        }
        else
        {
            if (_shift && _ph.canRun)
            {
                _playerSpeed = _sprintSpeed;
                _isRunning = true;
            }
            else
            {
                _playerSpeed = _walkSpeed;
                _isRunning = false;
            }
        }
    }
    #endregion

    #region Movement

    private void movement()
    {
        _localDirection = transform.TransformDirection(_inputDirection);
        _localDirection.Normalize();
        _controller.Move((_localDirection * _playerSpeed + _gravity) * Time.deltaTime);
        
        if(_localDirection.sqrMagnitude > 0 && _controller.isGrounded && !_isSwimming)
        {
            _movementAnimator.SetBool("Movement", true);
            _movementAnimator.SetFloat("Blend", _playerSpeed / _sprintSpeed);
            if (_playerSpeed/_sprintSpeed > 0.9f)
            {
                RunSound();
            }
            else
            {
                WalkSound();
            }
        }
        else
        {
            _movementAnimator.SetBool("Movement", false);
            _walkAudio.Stop();
            _runAudio.Stop();
        }
    }

    private void cameraRotation()
    {
        transform.Rotate(0, _y * Time.deltaTime * _rotationSpeed, 0);
    }

    private void jump()
    {
        if (_isSwimming) return;
        if(_controller.isGrounded)
        {
            if (_gravity.y < -15f)
            {
                _dropAudio.Play();
                _ph.changeHealth(_gravity.y);
            }
            _gravity.y = -1f;
            if (_jump)
            {
                _gravity.y = _jumpForce;
                _jumpAudio.Play();
            }
        }
        else
        {
            _gravity.y = _gravity.y + (_gravitationalAcc * Time.deltaTime);
        }
    }

    private void crouch()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            _controller.height = 0.7f;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            _controller.height = 1.6f;
        }
    }
    #endregion

    #region Health
    private void HealthFunction()
    {
        //Water level and Food level decrease with time
        _ph.changeWater(-0.5f);
        _ph.changeFood(-0.5f);

        //Stamina functionality
        if (_isRunning && _ph.stamina > 0)
        {
            _ph.changeStamina(-10);
        }
        else if(!_isWalking && !_isRunning)
        {
            _ph.changeStamina(20);
        }
        if ( _ph.stamina > 0)
        {
            _ph.canRun = true;
        }
        else
        {
            _ph.canRun = false;
        }

        //Health decrease when Water OR Food level is 0
        if (_ph.water <= 0 || _ph.food <= 0)
        {
            _ph.changeHealth(-5f);
        }
    }
    #endregion

    #region Sound
    private void WalkSound()
    {
        if (_runAudio.isPlaying)
        {
            _runAudio.Stop();
        }
        if (_walkAudio.isPlaying)
        {
            return;
        }
        _walkAudio.Play();
    }private void RunSound()
    {
        if (_walkAudio.isPlaying)
        {
            _walkAudio.Stop();
        }
        if (_runAudio.isPlaying)
        {
            return;
        }
        _runAudio.Play();
    }
    #endregion
}
