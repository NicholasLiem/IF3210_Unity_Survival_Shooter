using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    public float amplitude = 0.1f; // The height of the breathing effect.
    public float frequency = 1f;   // Speed of breathing.

    private Vector3 originalPosition;
    private float tempTime;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        tempTime += Time.deltaTime;
        float breathingEffect = Mathf.Sin(tempTime * frequency) * amplitude;
        transform.localPosition = new Vector3(originalPosition.x, originalPosition.y + breathingEffect, originalPosition.z);
    }
}
