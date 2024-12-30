using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject ball;
    public Transform ballInitTransform;
    public LineRenderer lineRenderer;
    
    
    
    public void SpawnBall(Vector3 position)
    {   
        GameObject go = Instantiate(ball, position, Quaternion.identity);
        
        go.GetComponent<SlingShot>().lineRenderer = lineRenderer;
        go.GetComponent<SlingShot>().OnDestroyed += () => SpawnBall(position);
        go.GetComponent<SlingShot>().OnFly += () => lineRenderer.SetPosition(1, position);
    }
    
    void Start()
    {
        SpawnBall(ballInitTransform.position);
    }
    
}
