using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] private new Light light;
    [SerializeField] private Material material;
    public float time = 30.0f;
    public float maxInterval = 1.2f;
    public float minInterval = 0.2f;

    private float interval = 1.0f;
    private float timer = 30.0f;

    private void Start()
    {
        timer = time;
    }

    private void Update()
    {
        interval = minInterval + timer / time * (maxInterval - minInterval);
        timer -= Time.deltaTime;
        if (timer < 0.0f) timer = 0.0f;
        
        if (Mathf.PingPong(Time.time, interval) > (interval / 2.0f))
        {
            material.EnableKeyword("_EMISSION");
            light.gameObject.SetActive(true);
        }
        else
        {
            
            material.DisableKeyword("_EMISSION");
            light.gameObject.SetActive(false);
        }

        // material.shader.
    }
}