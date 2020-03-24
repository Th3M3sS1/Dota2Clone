using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform cameraTransform;

    float smoothSpeed = .1f;

    float minX = -20f, maxX = 20f;
    float minZ = -25f, maxZ = 15f;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float camX = Input.GetAxisRaw("Vertical");
        float camZ = - Input.GetAxisRaw("Horizontal");


        Vector3 camPos = new Vector3(cameraTransform.localPosition.x - camZ, cameraTransform.position.y, cameraTransform.localPosition.z + camX);

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, camPos, smoothSpeed);

        if (cameraTransform.position.x < minX)
            cameraTransform.position = new Vector3(minX, cameraTransform.position.y, cameraTransform.position.z);

        else if(cameraTransform.position.x > maxX)
            cameraTransform.position = new Vector3(maxX, cameraTransform.position.y, cameraTransform.position.z);

        else if (cameraTransform.position.z < minZ)
            cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, minZ);
                                                                                                           
        else if (cameraTransform.position.z > maxZ)                                                        
            cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, maxZ);
    }
}
