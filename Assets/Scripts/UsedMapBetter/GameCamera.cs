using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] Material GLDraw;
    GameObject player;
    float camHeight, camWidth;
    Camera cam;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
        camHeight = 2 * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = player.transform.position.x;
        pos.y = player.transform.position.y;
        transform.position = pos;
    }

    private void OnPostRender()
    {
        float cameraLeft = transform.position.x - camWidth / 2;
        float cameraBottom = transform.position.y - camHeight / 2;

        GL.PushMatrix();
        GLDraw.SetPass(0);
        GL.LoadOrtho();

        GL.PopMatrix();
    }
}