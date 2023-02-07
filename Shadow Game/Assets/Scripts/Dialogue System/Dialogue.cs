using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue Instance", order = 1)]
public class Dialogue : ScriptableObject
{
    [Space(20)]
    [TextArea] public string[] _text;
    [Space(10)]
    public Color[] _color;
}
