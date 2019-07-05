using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsDraw : MonoBehaviour
{
    [SerializeField]
    Text fpsText = default;

    int frameCount = 0;

    float nextTime = 0;

    // Start is called before the first frame update
    void Start()
    {
#if DEVELOPMENT_BUILD
        nextTime = Time.time + 1;
#else
        gameObject.SetActive(false);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;

        if (Time.time >= nextTime)
        {
            fpsText.text = frameCount.ToString() + "fps";

            frameCount = 0;
            nextTime++;
        }
    }
    }
