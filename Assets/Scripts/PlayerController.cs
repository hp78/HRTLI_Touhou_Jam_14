using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public bool isAlive = true;
    public PlayerInput playerInput;
    public SpriteRenderer spriteRender;
    public MirrorController mirrorControl;
    public ReflectionController reflectionControl;
    
    Animator _anim;

    Vector2 _movementInput;
    float _xInput;

    float _moveForce = 5f;
    float _jumpForce = 11f;
    float _jumpThreshold = 1.5f;

    float _playerFacing = 1f;

    bool _isJumpKeyPressed = false;
    bool _inAir = false;


    int _layermask;

    Rigidbody2D _rigidbody;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _anim = GetComponent<Animator>();

        _movementInput = Vector2.zero;
        _rigidbody = GetComponent<Rigidbody2D>();

        _layermask = (1 << 8); //Player
        _layermask |= (1 << 9);//enemy
        _layermask |= (1 << 10);//camera
        _layermask |= (1 << 13);// switch
        _layermask = ~_layermask;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateJump();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _xInput = ctx.ReadValue<Vector2>().x;

        if (_xInput > 0)
        {
            _playerFacing = 1;
            spriteRender.flipX = true;
            reflectionControl.reflectSprite.flipX = false;
        }
        if (_xInput < 0)
        {
            _playerFacing = -1;
            spriteRender.flipX = false;
            reflectionControl.reflectSprite.flipX = true;

        }

        _anim.SetFloat("XVelocity", _xInput);

    }

    void UpdateMovement()
    {
        _rigidbody.linearVelocity = new Vector2(_xInput * _moveForce, _rigidbody.linearVelocity.y);
        //_anim.SetFloat("XVelocity", _rigidbody.linearVelocity.x);
        _anim.SetFloat("YVelocity", _rigidbody.linearVelocity.y);
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            _isJumpKeyPressed = true;
    }
    void UpdateJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y), 
            Vector2.down, _jumpThreshold, _layermask);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y), 
            Vector2.down, _jumpThreshold, _layermask);
        //Gizmos.color = Color.cyan;

        if (hit || hit2)
        {
            _inAir = false;
            //anim.SetTrigger("TriggerLand");

            //Debug.DrawLine(new Vector2(transform.position.x + 0.5f, transform.position.y), hit.point, Color.cyan);
            //Debug.DrawLine(new Vector2(transform.position.x - 0.5f, transform.position.y), hit2.point, Color.cyan);
        }
        else
        {
            _inAir = true;
        }

        if (!_inAir && _isJumpKeyPressed)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
            //anim.SetTrigger("TriggerJump");
        }

        _isJumpKeyPressed = false;
    }

    public void OnMirror(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            mirrorControl.gameObject.SetActive(!mirrorControl.gameObject.activeInHierarchy);
            mirrorControl.transform.position = transform.position + new Vector3(_playerFacing, 0, 0);
        }
    }
}
