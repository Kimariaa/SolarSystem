using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float mouseSensitivity = 1f;
    private float smooth = 2f;

    Transform character;
    private Vector2 currentMouseLook;
    private Vector2 appliedMousedelta;
    private float minClamp = -90f;
    private float maxClamp = 90f;
    private bool FPCamera = true;
    // Start is called before the first frame update
    void Start() => character = GameObject.Find("Ship").transform;

    // Update is called once per frame
    void Update()
    {
        
        Cursor.visible = false;
        if (Input.GetKey(KeyCode.C))
        {
            FPCamera = false;
        }
        if (Input.GetKey(KeyCode.V))
        {
            FPCamera = true;
        }
        if (FPCamera == true)
        {
            var smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * mouseSensitivity * smooth);
            appliedMousedelta = Vector2.Lerp(appliedMousedelta, smoothMouseDelta, 1 / smooth);

            currentMouseLook += appliedMousedelta;
            currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, minClamp, maxClamp);

            character.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);
            transform.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
        };
    }
}
