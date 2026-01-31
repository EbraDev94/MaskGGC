using UnityEngine;

public class HandheldShake : MonoBehaviour
{
    public float frequency = 0.5f; // Sallanma hýzý
    public float amplitude = 0.1f; // Sallanma geniþliði (þiddeti)

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Perlin Noise ile yumuþak rastgele deðerler üretiyoruz
        float x = (Mathf.PerlinNoise(Time.time * frequency, 0) - 0.5f) * 2;
        float y = (Mathf.PerlinNoise(0, Time.time * frequency) - 0.5f) * 2;

        transform.localPosition = initialPosition + new Vector3(x, y, 0) * amplitude;
    }
}