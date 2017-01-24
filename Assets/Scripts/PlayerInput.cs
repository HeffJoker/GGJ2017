using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerInput : MonoBehaviour {

    public SubMovement PlayerMovement;
    public WeaponSlot[] Weapons;
    public float AngleDeviation = 5f;
    public float DebounceTime = 0.5f;
    public Animator Echo;
    public AudioSource EchoSound;

    private int _currWeapon = 0;
    private float _currTime = 0;
    private float _echoTime = 0;
    private PlayerInputActions _input;
    private Vector3 _prevDir = Vector3.right;

    private bool _useMouse = false;


    void Awake()
    {
        _input = PlayerInputActions.CreateWithDefaultBindings();
    }

    // Update is called once per frame
    void Update() {

        UseGamepad();

        _currTime -= Time.deltaTime;
        _echoTime -= Time.deltaTime;
    }

    private void UseGamepad()
    {
        if (_input.Move.HasChanged)
            PlayerMovement.Move(_input.Move.Vector);

        Vector3 lookDir;
        if (_input.ActiveDevice.IsAttached)
        {
            lookDir = _input.Aim.Vector;
            lookDir.Normalize();
        }
        else
        {
            lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            lookDir.Normalize();
            // bug fix since controller gives a larger vector
            lookDir *= 2.5f;
        }

        
        Debug.Log("Look Dir = " + lookDir.ToString());
        //Debug.Log("Normalize = " + lookDir.normalized.ToString());

        if (lookDir != Vector3.zero)
        {
            PlayerMovement.LookAt(lookDir);
            _prevDir = lookDir;
        }
        else
        {
            lookDir = _prevDir;
        }

        if(_input.Fire.IsPressed && _currTime <= 0)
        {
            WeaponSlot currWeapon = Weapons[_currWeapon];
            currWeapon.Fire(lookDir);

            ++_currWeapon;

            if (_currWeapon >= Weapons.Length)
                _currWeapon = 0;

            _currTime = DebounceTime;
        }

        if (_input.Sonar.IsPressed && _echoTime <= 0)
        {
            if (EchoSound != null && !EchoSound.isPlaying)
                EchoSound.Play();

            Echo.SetBool("DoEcho", true);

            _echoTime = DebounceTime;
        }
        else
            Echo.SetBool("DoEcho", false);

        if (_input.Pause.WasPressed)
            GameStateManager.Instance.Pause();


        /*
        if(Input.GetMouseButtonUp(0))
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlayerMovement.MoveTo(target);
        }
        */
    }
}
