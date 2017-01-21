using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerInput : MonoBehaviour {

    public SubMovement PlayerMovement;
    public float AngleDeviation = 5f;

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


        /*
        if(Input.GetMouseButtonUp(0))
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlayerMovement.MoveTo(target);
        }
        */
    }
}
