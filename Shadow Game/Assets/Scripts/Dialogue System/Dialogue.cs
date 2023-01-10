using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue Instance", order = 1)]
public class Dialogue : ScriptableObject
{
    [Header("Ensure that both arrays are the same length.")]
    [Space(20)]
    [TextArea] public string[] _text;
    [Space(10)]
    public Color[] _color;
}
