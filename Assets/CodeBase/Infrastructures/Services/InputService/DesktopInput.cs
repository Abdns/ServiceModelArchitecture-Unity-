using UnityEngine;

public class DesktopInput : InputService
{
    public override Vector2 Axis => UnityAxis();
    public override bool isRestartButtonPressed() =>  Input.GetKeyDown(KeyCode.Escape);

    private Vector2 UnityAxis() =>  new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
    

}

