using UnityEngine;

public class SwimmFog : MonoBehaviour
{
    public float waterHeight;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= waterHeight){
            RenderSettings.fog = true;
        }else{
            RenderSettings.fog = false;
        }
    }
}
