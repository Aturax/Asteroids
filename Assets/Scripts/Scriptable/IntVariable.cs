using UnityEngine;

[CreateAssetMenu(fileName = "new IntVariable", menuName = "Data/Int Variable", order = 1)]
public class IntVariable : ScriptableObject
{
    public int initialValue = 0;
    int _value = 0;
    
    public event System.Action OnValueChanged = () => { };

    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged();
        }
    }

    private void OnEnable()
    {
        _value = initialValue;
    }
}