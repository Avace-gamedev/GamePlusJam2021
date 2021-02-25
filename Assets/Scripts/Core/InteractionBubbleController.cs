using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBubbleController : MonoBehaviour
{
    [SerializeField] GameObject keyboardCommand;
    [SerializeField] GameObject gamepadCommand;

    void Awake()
    {
        keyboardCommand.SetActive(false);
        gamepadCommand.SetActive(false);
    }

    void Start()
    {
        SetCorrectCommand();
    }

    void Update()
    {
        SetCorrectCommand();
    }

    void SetCorrectCommand()
    {
        switch (InputManager.playerInput.currentControlScheme)
        {
            case "Keyboard&Mouse":
                SetKeyboard();
                break;
            case "Gamepad":
                SetGamepad();
                break;
            default:
                SetKeyboard();
                break;
        }
    }

    void SetGamepad()
    {
        keyboardCommand.SetActive(false);
        gamepadCommand.SetActive(true);
    }

    void SetKeyboard()
    {
        keyboardCommand.SetActive(true);
        gamepadCommand.SetActive(false);
    }
}
