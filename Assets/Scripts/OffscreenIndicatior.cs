using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OffscreenIndicatior : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public string targetTag = "HurtBox"; // Tag to look for
    public float radius = 14f; // Radius of the circle
    public int numberOfRays = 36; // Number of rays to cast
    float HitPoint;

    public List<GameObject> HurtBoxes;
    public List<GameObject> Indicatorlist;
    public GameObject IndicatiorPrefab;

    SpriteRenderer spriteRenderer;
    float spriteWidth;
    float spriteHeight;
    float PlayerDist;
    Camera camera;

    Dictionary<GameObject, GameObject> Indicators = new Dictionary<GameObject, GameObject>();


    void Start()
    {
        camera = Camera.main;
        spriteRenderer = IndicatiorPrefab.GetComponent<SpriteRenderer>();

        var bounds = spriteRenderer.bounds;
        spriteWidth = bounds.size.x / 2f;
        spriteHeight = bounds.size.y / 2f;
        //UpdateTargets();

    }

    void UpdateTargets()
    {
        foreach (var Hurtbox in HurtBoxes) { 
        var Indicator = Instantiate(IndicatiorPrefab);
            Indicatorlist.Add(Indicator);
        Indicator.SetActive(false);
        Indicators.Add(Hurtbox, Indicator);
        }
    }

    void Update()
    {


        foreach (KeyValuePair<GameObject,GameObject> entry in Indicators)
        {
            var HurtBox = entry.Key;
            var Indicator = entry.Value;

            UpdateTargetInfo(HurtBox, Indicator);

        }


        CastRaysInCircle();


        foreach (var Hurtbox in HurtBoxes.ToList())
        {
                PlayerDist = Vector3.Distance(Hurtbox.transform.position, this.transform.position);
                if (PlayerDist >= radius)
                {
                    HurtBoxes.Remove(Hurtbox);
          //      Indicator.Destroy();
                Indicators.Clear();
                UpdateTargets();
            }

        }

    
     





    }

    void UpdateTargetInfo(GameObject HurtBox, GameObject Indicator)
    {
        var screenPos = camera.WorldToViewportPoint(HurtBox.transform.position);
        bool Offscreen = screenPos.x <= 0 || screenPos.x >= 1 || screenPos.y <= 0 || screenPos.y >= 1;


        if(IndicatiorPrefab != null) { 
        if (Offscreen)
        {
            Indicator.SetActive(true);
            var SpriteSizeInView = camera.WorldToViewportPoint(new Vector3(spriteWidth, spriteHeight, 0)) - camera.WorldToViewportPoint(Vector3.zero);

            screenPos.x = Mathf.Clamp(screenPos.x, SpriteSizeInView.x, 1 - SpriteSizeInView.x);
            screenPos.y = Mathf.Clamp(screenPos.y, SpriteSizeInView.x, 1 - SpriteSizeInView.y);

            var WorldPos = camera.ViewportToWorldPoint(screenPos);
            WorldPos.z = 0;
            Indicator.transform.position = WorldPos;

            Vector3 Dir = HurtBox.transform.position - Indicator.transform.position;
            float Ang = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
            Indicator.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Ang));


            

        }
        else
        {
            Indicator.SetActive(false);


        }
        }
    }


    void CastRaysInCircle()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfRays;
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, radius))
            {
                if (hit.collider.CompareTag(targetTag))
                {
                    if (HurtBoxes.IndexOf(hit.collider.gameObject) == -1)
                    {
                        HurtBoxes.Add(hit.collider.gameObject);
                        Indicators.Clear();
                        UpdateTargets();
                    }
                   
                }
                else
                {
                    Debug.DrawRay(transform.position, direction * hit.distance, Color.green);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, direction * radius, Color.blue);
            }
        }
    }
}
