using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private PlayerInputMap _input;

    public bool isPlayerOne;

    public event Action<Vector2> onMove;

    void Start()
    {
        _input = new PlayerInputMap();
        _input.Enable();

        Visualizacion.Lado playerSide = GetComponent<Player>().config.side;
        isPlayerOne = playerSide == Visualizacion.Lado.Central || playerSide == Visualizacion.Lado.Izq;

        if (isPlayerOne)
        {
            _input.Player.MoveP1.performed += HandleMoveInput;
            _input.Player.MoveP1.canceled += HandleMoveInput;
        }
        else
        {
            _input.Player.MoveP2.performed += HandleMoveInput;
            _input.Player.MoveP2.canceled += HandleMoveInput;
        }
    }

    private void OnDestroy()
    {
        if (isPlayerOne)
        {
            _input.Player.MoveP1.performed -= HandleMoveInput;
            _input.Player.MoveP1.canceled -= HandleMoveInput;
        }
        else
        {
            _input.Player.MoveP2.performed -= HandleMoveInput;
            _input.Player.MoveP2.canceled -= HandleMoveInput;
        }
    }

    void HandleMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
            onMove?.Invoke(context.ReadValue<Vector2>());
        else
            onMove?.Invoke(Vector2.zero);
    }
}