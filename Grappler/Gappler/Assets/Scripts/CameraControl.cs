using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
   [SerializeField]
    float smooth = 0.95f;
    [SerializeField]
    float offsetY = .5f;
    [SerializeField]
    float offsetZ = 0;
    Transform playerTransform;
    Transform playerTransformEmpty;
    bool locked;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationX = 0F;
    float rotationY = 0F;

    private List<float> rotArrayX = new List<float>();
    float rotAverageX = 0F;

    private List<float> rotArrayY = new List<float>();
    float rotAverageY = 0F;

    public float frameCounter = 20;

    Quaternion originalRotation;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerTransformEmpty = GameObject.Find("CameraAnchor").transform;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            locked = !locked;
        transform.position = Vector3.Lerp(new Vector3(playerTransformEmpty.position.x, playerTransformEmpty.position.y + offsetY, playerTransformEmpty.position.z + offsetZ), playerTransformEmpty.position, smooth);
        playerTransform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        if (!locked)
        {
            if (axes == RotationAxes.MouseXAndY)
            {
                rotAverageY = 0f;
                rotAverageX = 0f;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationX += Input.GetAxis("Mouse X") * sensitivityX;

                rotArrayY.Add(rotationY);
                rotArrayX.Add(rotationX);

                if (rotArrayY.Count >= frameCounter)
                {
                    rotArrayY.RemoveAt(0);
                }
                if (rotArrayX.Count >= frameCounter)
                {
                    rotArrayX.RemoveAt(0);
                }

                for (int j = 0; j < rotArrayY.Count; j++)
                {
                    rotAverageY += rotArrayY[j];
                }
                for (int i = 0; i < rotArrayX.Count; i++)
                {
                    rotAverageX += rotArrayX[i];
                }

                rotAverageY /= rotArrayY.Count;
                rotAverageX /= rotArrayX.Count;

                //rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
                if (rotAverageY > maximumY)
                    rotAverageY = maximumY;
                if (rotAverageY < minimumY)
                    rotAverageY = minimumY;
                rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

                Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
                Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);

                transform.localRotation = originalRotation * xQuaternion * yQuaternion;
            }
            else if (axes == RotationAxes.MouseX)
            {
                rotAverageX = 0f;

                rotationX += Input.GetAxis("Mouse X") * sensitivityX;

                rotArrayX.Add(rotationX);

                if (rotArrayX.Count >= frameCounter)
                {
                    rotArrayX.RemoveAt(0);
                }
                for (int i = 0; i < rotArrayX.Count; i++)
                {
                    rotAverageX += rotArrayX[i];
                }
                rotAverageX /= rotArrayX.Count;

                rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

                Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;
            }
            else
            {
                rotAverageY = 0f;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

                rotArrayY.Add(rotationY);

                if (rotArrayY.Count >= frameCounter)
                {
                    rotArrayY.RemoveAt(0);
                }
                for (int j = 0; j < rotArrayY.Count; j++)
                {
                    rotAverageY += rotArrayY[j];
                }
                rotAverageY /= rotArrayY.Count;

                rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);

                Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
                transform.localRotation = originalRotation * yQuaternion;
            }
        }
    }
    public void SetSensitivity(Slider slider) {
        float val = slider.value;
        sensitivityX = val;
        sensitivityY = val;
    }

    public void SetBuffer(Slider slider)
    {
        float val = slider.value;
        frameCounter = val;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}
