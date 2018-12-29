using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    // We serialize a sroll speed field here and use that
    // to scroll the background material below
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    Material backgroundMaterial;
    Vector2 offset;

    void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        backgroundMaterial.mainTextureOffset += offset * Time.deltaTime;   
    }
}
