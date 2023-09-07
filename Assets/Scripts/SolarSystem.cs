using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    public GameObject Sun;
    GameObject[] planets;
    readonly float G = 100f;
    private float _time;
    // Start is called before the first frame update
    void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        StartMove();
    }

    private void FixedUpdate()
    {
        Gravity();
        
    }

    void StartMove()
    {
        foreach (GameObject p1 in planets)
        {
            foreach (GameObject p2 in planets)
            {
                if (!p1.Equals(p2))
                {
                    float m2 = p2.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(p1.transform.position, p2.transform.position);
                    p1.transform.LookAt(p2.transform);
                    p1.GetComponent<Rigidbody>().velocity += p1.transform.right * Mathf.Sqrt((G * m2) / r);

                }
            }
        }
    }



    void Gravity()
    {
        foreach(GameObject p1 in planets)
        {
            foreach (GameObject p2 in planets)
            {
                if (!p1.Equals(p2))
                {
                    float m1 = p1.GetComponent<Rigidbody>().mass;
                    float m2 = p2.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(p1.transform.position, p2.transform.position);
                    p1.GetComponent<Rigidbody>().AddForce((p2.transform.position - p1.transform.position).normalized * (G * (m1 * m2) / (r * r)));
                }
            }    
        }
    }
}
