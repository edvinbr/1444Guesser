using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float camMoveSpeed = 10.0f;
    private float camZoomSpeed = 3.0f;
    private float camSizeDefault;

    void Start()
    {
        camSizeDefault = Camera.main.GetComponent<Camera>().orthographicSize;
    }

    void Update() {
        float camSize = Camera.main.GetComponent<Camera>().orthographicSize;
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(new Vector3(camMoveSpeed * Time.deltaTime * camSize / camSizeDefault, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(new Vector3(-camMoveSpeed * Time.deltaTime * camSize / camSizeDefault, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(new Vector3(0, -camMoveSpeed * Time.deltaTime * camSize / camSizeDefault, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(new Vector3(0, camMoveSpeed * Time.deltaTime * camSize / camSizeDefault, 0));
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f) {
            camSize = Mathf.Clamp(camSize + scroll * camZoomSpeed, 2.0f, 20.0f);
            Camera.main.GetComponent<Camera>().orthographicSize = camSize;
        }
    }
}
