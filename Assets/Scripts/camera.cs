using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] AudioSource AudioSource;
    //[SerializeField] AudioClip levelSound;
    [SerializeField] AudioClip bossSound;
    private bool isBossSoundPlaying = false;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(levelDesign.Instance.getScore() >= levelDesign.Instance.BossTime)
        {
            if(!isBossSoundPlaying)
            {
                AudioSource.clip = bossSound;
                AudioSource.Play();
                isBossSoundPlaying = true;
            }
        }
    }
}
