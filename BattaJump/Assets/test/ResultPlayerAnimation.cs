using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPlayerAnimation : MonoBehaviour
{
    [SerializeField]
    Animator animator = default;

    [SerializeField]
    ScoreCountUp scoreCountUp = default;

    [SerializeField]
    ScoreDataManager scoreData = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreCountUp.IsEnd)
        {
            if (scoreData.GetNowScore() > 10000)
            {
                animator.SetTrigger("Rejoice");
            }
            else
            {
                animator.SetTrigger("Sad");
            }
        }
    }
}
