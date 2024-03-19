using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InputService : IInputService
{
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    public abstract Vector2 Axis { get; }
    public abstract bool isRestartButtonPressed();

}
