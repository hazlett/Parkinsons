using UnityEngine;
using System.Collections;

public class HandbarRotation : MonoBehaviour {

    public Rigidbody cart;
    private float scale = 90.0f;
    private bool switchDirections = false;
    private Vector3 direction = Vector3.right;

    void Start()
    {
        SetScale();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "cart")
        {
            SwitchDirections();
        }
    }
    void Update()
    {
        gameObject.transform.Rotate(direction * Time.deltaTime * scale, Space.Self);
    }

    void SwitchDirections()
    {
        if (switchDirections)
        {
            switchDirections = false;
            direction = Vector3.right;
        }
        else
        {
            switchDirections = true;
            direction = Vector3.left;
        }
    }

    void SetScale()
    {
        if (PlayerSettings.Instance.Age >= 60 && PlayerSettings.Instance.Age < 65)
        {
            scale = 53.0f;
        }
        else if (PlayerSettings.Instance.Age >= 65 && PlayerSettings.Instance.Age < 70)
        {
            scale = 50.0f;
        }
        else if (PlayerSettings.Instance.Age >= 70 && PlayerSettings.Instance.Age < 75)
        {
            scale = 48.0f;
        }
        else if (PlayerSettings.Instance.Age >= 75 && PlayerSettings.Instance.Age < 80)
        {
            scale = 45.0f;
        }
        else if (PlayerSettings.Instance.Age >= 80 && PlayerSettings.Instance.Age < 85)
        {
            scale = 43.0f;
        }
        else if (PlayerSettings.Instance.Age >= 85 && PlayerSettings.Instance.Age < 90)
        {
            scale = 40.0f;
        }
        else if (PlayerSettings.Instance.Age >= 90)
        {
            scale = 37.0f;
        }
        else
        {
            scale = 65.0f;
        }
    }
}
