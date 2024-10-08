using UnityEngine;

public class ControlDireccion : MonoBehaviour
{
    public enum TipoInput
    {
        AWSD,
        Arrows
    }

    public TipoInput InputAct = TipoInput.AWSD;

    float Giro = 0;

    private CarController carController;
    private InputReader _inputReader;

    //---------------------------------------------------------//

    // Use this for initialization
    void Start()
    {
        carController = GetComponent<CarController>();
        _inputReader = GetComponent<InputReader>();
        _inputReader.onMove += HandleInput;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     switch (InputAct)
    //     {
    //         case TipoInput.AWSD:
    //
    //             if (Input.GetKey(KeyCode.A))
    //             {
    //                 Giro = -1;
    //             }
    //             else if (Input.GetKey(KeyCode.D))
    //             {
    //                 Giro = 1;
    //             }
    //             else
    //             {
    //                 Giro = 0;
    //             }
    //
    //             break;
    //         case TipoInput.Arrows:
    //             if (Input.GetKey(KeyCode.LeftArrow))
    //             {
    //                 Giro = -1;
    //             }
    //             else if (Input.GetKey(KeyCode.RightArrow))
    //             {
    //                 Giro = 1;
    //             }
    //             else
    //             {
    //                 Giro = 0;
    //             }
    //
    //             break;
    //     }
    //
    //     carController.SetGiro(Giro);
    // }

    public float GetGiro()
    {
        return Giro;
    }

    private void HandleInput(Vector2 dir)
    {
        if (dir.x < 0)
            carController.SetGiro(-1);
        else if (dir.x > 0)
            carController.SetGiro(1);
        else
            carController.SetGiro(0);
    }
}