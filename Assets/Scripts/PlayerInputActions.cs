using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use this for initialization
public class PlayerInputActions : PlayerActionSet
{
    public PlayerAction Fire;
    public PlayerAction Sonar;
    public PlayerAction Pause;

    public PlayerAction MoveUp;
    public PlayerAction MoveLeft;
    public PlayerAction MoveRight;
    public PlayerAction MoveDown;

    public PlayerAction AimUp;
    public PlayerAction AimLeft;
    public PlayerAction AimRight;
    public PlayerAction AimDown;

    public PlayerTwoAxisAction Move;
    public PlayerTwoAxisAction Aim;


    public PlayerInputActions()
    {
        Fire = CreatePlayerAction("Fire");
        Sonar = CreatePlayerAction("Sonar");
        Pause = CreatePlayerAction("Pause");

        MoveLeft = CreatePlayerAction("Move Left");
        MoveRight = CreatePlayerAction("Move Right");
        MoveDown = CreatePlayerAction("Move Down");
        MoveUp = CreatePlayerAction("Move Up");

        Move = CreateTwoAxisPlayerAction(MoveLeft, MoveRight, MoveDown, MoveUp);

        AimLeft = CreatePlayerAction("Look Left");
        AimRight = CreatePlayerAction("Look Right");
        AimUp = CreatePlayerAction("Look Up");
        AimDown = CreatePlayerAction("Look Down");

        Aim = CreateTwoAxisPlayerAction(AimLeft, AimRight, AimDown, AimUp);
    }


    public static PlayerInputActions CreateWithDefaultBindings()
    {
        var playerActions = new PlayerInputActions();

        playerActions.Fire.AddDefaultBinding(InputControlType.RightTrigger);
        playerActions.Fire.AddDefaultBinding(Mouse.LeftButton);

        playerActions.Sonar.AddDefaultBinding(Key.Space);
        playerActions.Sonar.AddDefaultBinding(Mouse.RightButton);
        playerActions.Sonar.AddDefaultBinding(InputControlType.LeftTrigger);

        playerActions.Pause.AddDefaultBinding(Key.Escape);
        playerActions.Pause.AddDefaultBinding(InputControlType.Menu);
        playerActions.Pause.AddDefaultBinding(InputControlType.Options);

        playerActions.MoveUp.AddDefaultBinding(Key.W);
        playerActions.MoveDown.AddDefaultBinding(Key.S);
        playerActions.MoveLeft.AddDefaultBinding(Key.A);
        playerActions.MoveRight.AddDefaultBinding(Key.D);

        playerActions.MoveLeft.AddDefaultBinding(InputControlType.LeftStickLeft);
        playerActions.MoveRight.AddDefaultBinding(InputControlType.LeftStickRight);
        playerActions.MoveUp.AddDefaultBinding(InputControlType.LeftStickUp);
        playerActions.MoveDown.AddDefaultBinding(InputControlType.LeftStickDown);

        playerActions.AimUp.AddDefaultBinding(Mouse.PositiveY);
        playerActions.AimDown.AddDefaultBinding(Mouse.NegativeY);
        playerActions.AimLeft.AddDefaultBinding(Mouse.NegativeX);
        playerActions.AimRight.AddDefaultBinding(Mouse.PositiveX);

        playerActions.AimUp.AddDefaultBinding(InputControlType.RightStickUp);
        playerActions.AimDown.AddDefaultBinding(InputControlType.RightStickDown);
        playerActions.AimLeft.AddDefaultBinding(InputControlType.RightStickLeft);
        playerActions.AimRight.AddDefaultBinding(InputControlType.RightStickRight);

        playerActions.ListenOptions.IncludeUnknownControllers = true;
        playerActions.ListenOptions.MaxAllowedBindings = 4;
        //playerActions.ListenOptions.MaxAllowedBindingsPerType = 1;
        //playerActions.ListenOptions.AllowDuplicateBindingsPerSet = true;
        //playerActions.ListenOptions.UnsetDuplicateBindingsOnSet = true;
        //playerActions.ListenOptions.IncludeMouseButtons = true;
        //playerActions.ListenOptions.IncludeModifiersAsFirstClassKeys = true;

        playerActions.ListenOptions.OnBindingFound = (action, binding) =>
        {
            if (binding == new KeyBindingSource(Key.Escape))
            {
                action.StopListeningForBinding();
                return false;
            }
            return true;
        };

        playerActions.ListenOptions.OnBindingAdded += (action, binding) =>
        {
            Debug.Log("Binding added... " + binding.DeviceName + ": " + binding.Name);
        };

        playerActions.ListenOptions.OnBindingRejected += (action, binding, reason) =>
        {
            Debug.Log("Binding rejected... " + reason);
        };

        return playerActions;
    }
}

