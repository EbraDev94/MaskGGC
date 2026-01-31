using UnityEngine;
using System.Collections;

public class ObjectShake : MonoBehaviour
{
    public float shakeDuration = 2f; // مدت زمان لرزش
    public float shakeMagnitude = 0.1f; // شدت لرزش
    public float shakeSpeed = 20f; // سرعت لرزش

    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    public void StartShake()
    {
        StopAllCoroutines(); // اگر لرزش قبلی در حال اجراست، متوقفش کن
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;

            // Perlin Noise برای حرکت نرم و طبیعی
            float x = (Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) - 0.5f) * 2f * shakeMagnitude;
            float y = (Mathf.PerlinNoise(0f, Time.time * shakeSpeed) - 0.5f) * 2f * shakeMagnitude;
            float z = (Mathf.PerlinNoise(Time.time * shakeSpeed, 1f) - 0.5f) * 2f * shakeMagnitude;

            transform.localPosition = originalPos + new Vector3(x, y, z);

            yield return null;
        }

        transform.localPosition = originalPos; // برگرداندن به موقعیت اولیه
    }
}