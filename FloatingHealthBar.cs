using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    // [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset; // set hp bar ui
    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue; // make hp bar accurate
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset; // make hp bar move with the unit it is assigned to
    }
}
