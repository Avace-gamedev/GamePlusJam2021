using UnityEngine;

[CreateAssetMenu(fileName = "Monologue", menuName = "Dialogue/Monologue")]
public class Monologue : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] text;
}