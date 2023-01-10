using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue Instance", order = 1)]
public class Dialogue : ScriptableObject
{
    [Header("Ensure that both arrays are the same length.")]
    [TextArea] public string[] _text;
    public Color[] _color;
}
