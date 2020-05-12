using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ImageScroll: MonoBehaviour
{
    RawImage img;
    float speed = 0.01f;
    // Use this for initialization
    void Start()
    {
        this.img = this.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        float s = this.speed * Time.deltaTime;
        Rect r = this.img.uvRect;
        r.x += s;
        this.img.uvRect = r;
    }
}