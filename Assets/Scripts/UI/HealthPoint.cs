using UnityEngine;
using UnityEngine.UI;

public class HealthPoint : MonoBehaviour
{
    [SerializeField] Image sprite;

    public void SetFill(float fill)
    {
        sprite.fillAmount = fill;
    }
}