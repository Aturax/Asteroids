using UnityEditor;
using UnityEngine;


public class IntVariableEditor : Editor
{
    private int _currentValue = 0;

    public override void OnInspectorGUI()
    {
        IntVariable intTarget = (IntVariable)target;
        if (Application.isPlaying)
        {
            PlayEditor(intTarget);
            return;
        }        
        EditOnEditor(intTarget);                
    }

    void PlayEditor(IntVariable intTarget)
    {
        _currentValue = EditorGUILayout.IntField("Runtime Value", intTarget.Value);
        if (_currentValue != intTarget.Value)
        {
            intTarget.Value = _currentValue;
            EditorUtility.SetDirty(intTarget);
        }
        if (GUILayout.Button("Save"))
        {
            intTarget.initialValue = intTarget.Value;
            EditorUtility.SetDirty(intTarget);
        }
    }

    void EditOnEditor(IntVariable intTarget)
    {
        _currentValue = EditorGUILayout.IntField("Value", intTarget.initialValue);
        if (_currentValue != intTarget.initialValue)
        {
            intTarget.initialValue = _currentValue;
            EditorUtility.SetDirty(intTarget);
        }
    }
}