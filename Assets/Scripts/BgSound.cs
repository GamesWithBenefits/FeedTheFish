using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSound : MonoBehaviour
{
    public static BgSound BgInstance;
    public AudioSource audi; 
    private void Awake()
    {
        if (BgInstance != null && BgInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        BgInstance = this;
        DontDestroyOnLoad(this);
    } 
    private void Start()
    {
        audi = GetComponent<AudioSource>();
    }
}
