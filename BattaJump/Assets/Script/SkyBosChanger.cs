using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkyBosChanger : MonoBehaviour
{
    [SerializeField]
    JumpHeightCounter jumpHeightCounter = default;

    [SerializeField]
    Material[] materials = new Material[2];

    [SerializeField]
    GameObject rocket = default;

    bool isChange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpHeightCounter.JumpHeightToKiloMetre > 10000)
        {
            rocket.SetActive(true);
        }

        if (rocket.transform.localPosition.y > -0.7 && !isChange)
        {
            ChangeSkyBox();

            isChange = true;
        }
    }

    void ChangeSkyBox()
    {
        RenderSettings.skybox = materials[1];
    }
}
