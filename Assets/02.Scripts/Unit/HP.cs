using UnityEngine;

public class HP : MonoBehaviour
{
    public float Value;
    public float MaxValue { get; private set; }

    private void Awake()
    {
        MaxValue = Value;
    }

    public void Increase(float value)
    {
        Value += value;
    }

    public void Decrease(float value)
    {
        Value -= value;
        if (Value < 0) Value = 0;
    }
}