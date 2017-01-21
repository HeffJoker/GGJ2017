using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerInput : MonoBehaviour {

    public SubMovement PlayerMovement;
    public WeaponSlot[] Weapons;
    public float AngleDeviation = 5f;
    public float DebounceTime = 0.5f;
    
    private int _currWeapon = 0;
    private float _currTime = 0;
    private InputDevice _device;
    private float _prevAngle = 90f;


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
            PlayerMovement.LookAt(lookDir);

        if(_device.RightTrigger.IsPressed && _currTime <= 0)
        {
            WeaponSlot currWeapon = Weapons[_currWeapon];
            currWeapon.Fire(lookDir);

            ++_currWeapon;

            if (_currWeapon >= Weapons.Length)
                _currWeapon = 0;

            _currTime = DebounceTime;
        }

        _currTime -= Time.deltaTime;

        /*
        if(Input.GetMouseButtonUp(0))
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlayerMovement.MoveTo(target);
        }
        */
    }
}
