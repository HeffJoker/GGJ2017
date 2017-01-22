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
    private InputDevice _device;
    private Vector3 _prevDir = Vector3.right;


    void Awake()
    {
        //_device = InputManager.ActiveDevice;
    }

	// Update is called once per frame
	void Update () {
        _device = InputManager.ActiveDevice;

        if (_device.LeftStick.HasChanged)
            PlayerMovement.Move(_device.LeftStick.Vector);

        Vector3 lookDir = _device.RightStick.Vector;

        if (lookDir != Vector3.zero)
        {
            PlayerMovement.LookAt(lookDir);
            _prevDir = lookDir;
        }
        else
        {
            lookDir = _prevDir;
        }

        if(_device.RightTrigger.IsPressed && _currTime <= 0)
        {
            WeaponSlot currWeapon = Weapons[_currWeapon];
            currWeapon.Fire(lookDir.normalized);

            ++_currWeapon;

            if (_currWeapon >= Weapons.Length)
                _currWeapon = 0;

            _currTime = DebounceTime;
        }

        if (_device.LeftTrigger.IsPressed && _echoTime <= 0)
        {
            if (EchoSound != null && !EchoSound.isPlaying)
                EchoSound.Play();

            Echo.SetBool("DoEcho", true);

            _echoTime = DebounceTime;
        }
        else
            Echo.SetBool("DoEcho", false);

        if (_device.CommandWasPressed)
            GameStateManager.Instance.Pause();

        _currTime -= Time.deltaTime;
        _echoTime -= Time.deltaTime;

        /*
        if(Input.GetMouseButtonUp(0))
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlayerMovement.MoveTo(target);
        }
        */
    }
}
