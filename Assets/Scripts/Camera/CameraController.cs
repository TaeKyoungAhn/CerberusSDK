using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Cerberus_Platform
{
    public class CameraController : MonoBehaviour
    {
        public CameraMode cameraMode; //Enum Class

        #region Orbit Camera Values
        public Transform target;  //Taget Object
        public float distance = 5.0f;
        public float xSpeed = 20.0f;
        public float ySpeed = 20.0f;
        public float yMinLimit = -90.0f;
        public float yMaxLimit = 90.0f;
        public float distanceMin = 2f;
        public float distanceMax = 10f;
        public float smoothTime = 2f;
        float rotationYAxis = 0.0f;
        float rotationXAxis = 0.0f;
        float velocityX = 0.0f;
        float velocityY = 0.0f;
        #endregion

        #region Fly Camera Values
        public float mainSpeed = 0.12f; //regular speed
        public float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
        public float maxShift = 1000.0f; //Maximum speed when holdin gshift
        public float camSens = 0.25f; //How sensitive it with mouse
        public bool rotateOnlyIfMousedown = true;
        public bool movementStaysFlat = false;

        private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
        private float totalRun = 1.0f;
        #endregion

        public bool _cameraModeInit = false; //Use status Camera Control 

        private void Awake()
        {
            //Default CameraMode
            if (cameraMode == CameraMode.FlyCamera)
            {
                transform.position = new Vector3(0, 2, -5);
                transform.rotation = Quaternion.Euler(25, 0, 0);
            }
        }

        void Start()
        {
            Vector3 angles = transform.eulerAngles;
            rotationYAxis = angles.y;
            rotationXAxis = angles.x;
            // Make the rigid body not change rotation
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().freezeRotation = true;
            }
        }

        void LateUpdate()
        {
            //Change Camera Mode
            //The setting that is required is Old InputManager & New InputManager(Both)
            //PlayerSettings-Player-OtherSettings-Configuration-ActiveInputHandling-"Both"
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (cameraMode == CameraMode.GeneralCamera)
                {
                    cameraMode = CameraMode.FlyCamera;
                }
                else
                {
                    cameraMode = CameraMode.GeneralCamera;
                }
            }
            switch (cameraMode)
            {
                case CameraMode.GeneralCamera:
                    {
                        if (target != null) //&& cameraMode == CameraMode.GeneralCamera)
                        {
                            if (Input.GetMouseButton(1))
                            {
                                velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
                                velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
                            }
                            rotationYAxis += velocityX;
                            rotationXAxis -= velocityY;
                            rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
                            Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
                            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
                            Quaternion rotation = toRotation;

                            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
                            //if (Physics.Linecast(target.position, transform.position, out hit))
                            //{
                            //    distance -= hit.distance;
                            //}
                            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
                            Vector3 position = rotation * negDistance + target.position;

                            transform.rotation = rotation;
                            transform.position = position;
                            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
                            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
                        }

                    }
                    break;
                case CameraMode.FlyCamera:
                    {
                        if (Input.GetMouseButtonDown(1))
                        {
                            lastMouse = Input.mousePosition; //  reset when we begin
                        }

                        if (!rotateOnlyIfMousedown ||
                            (rotateOnlyIfMousedown && Input.GetMouseButton(1)))
                        {
                            lastMouse = Input.mousePosition - lastMouse;
                            lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
                            lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
                            transform.eulerAngles = lastMouse;
                            lastMouse = Input.mousePosition;
                            //Mouse  camera angle done.  
                        }

                        //Keyboard commands
                        Vector3 p = GetBaseInput();
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            totalRun += Time.deltaTime;
                            p = p * totalRun * shiftAdd;
                            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
                            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
                            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
                        }
                        else
                        {
                            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                            p = p * mainSpeed;
                        }

                        p = p * Time.deltaTime;
                        Vector3 newPosition = transform.position;
                        if (Input.GetKey(KeyCode.Space)
                            || (movementStaysFlat && !(rotateOnlyIfMousedown && Input.GetMouseButton(1))))
                        { //If player wants to move on X and Z axis only
                            transform.Translate(p);
                            newPosition.x = transform.position.x;
                            newPosition.z = transform.position.z;
                            transform.position = newPosition;
                        }
                        else
                        {
                            transform.Translate(p);
                        }
                    }
                    break;
                default:
                    {
                        //Do-Nothing
                    }
                    break;

            }

        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }

        private Vector3 GetBaseInput()
        { 
            //returns the basic values, if it's 0 than it's not active.
            Vector3 p_Velocity = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                p_Velocity += new Vector3(0, 0, 1);
            }
            if (Input.GetKey(KeyCode.S))
            {
                p_Velocity += new Vector3(0, 0, -1);
            }
            if (Input.GetKey(KeyCode.A))
            {
                p_Velocity += new Vector3(-1, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                p_Velocity += new Vector3(1, 0, 0);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                p_Velocity += new Vector3(0, -1, 0);
            }
            if (Input.GetKey(KeyCode.E))
            {
                p_Velocity += new Vector3(0, 1, 0);
            }
            return p_Velocity;
        }

    }
}
