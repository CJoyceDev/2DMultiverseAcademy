using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WindTriggerSystem
{
    public class WindTrigger : MonoBehaviour
    {
        
        [SerializeField] private Transform _fanRotation;
        [SerializeField] private float _fanRotateSpeed;
        [SerializeField] private float _fanAcceleration = 0.2f;
        [SerializeField] private float _minFanSpeed = 0.0f;
        [SerializeField] private float _maxFanSpeed = 1500f;
        public bool IsHidden;
        public float radius;
        public float force;

        [SerializeField] int windDelay;
        int PrewindDelay;

        public bool _isFanOn = false;
        public bool _isPreFanOn = true;

        private bool isWindPlaying = false;
        private bool isPreWindPlaying = false;

        [SerializeField] bool fanStaysOn;

        BoxCollider boxCollider;
       [SerializeField] ParticleSystem windEffect;
        [SerializeField] ParticleSystem PrewindEffect;

        [SerializeField] AudioClip FanSound;
        public bool IsHazard;

        [SerializeField] GameObject windAnim;

        private void Start()
        {
            PrewindDelay = windDelay;

            boxCollider = GetComponent<BoxCollider>(); // Get the BoxCollider
            PrewindEffect.Stop();
            windEffect.Stop();
            // StartCoroutine(PreFanOnOff());
            /*StartCoroutine( SwitchFanOnOff());
            windEffect.Stop();*/
        }

        void FixedUpdate()
        {
            _fanRotation.Rotate(Vector3.forward * _fanRotateSpeed * Time.deltaTime);

            if (_isFanOn)
            {
                /*if (FanSound != null && IsHazard)
                {
                    SoundHandler.instance.PlaySound(FanSound, transform, 0.2f, windDelay);
                }*/
                Acceleration();
                CreateWind();
            }
            else
            {
                Deceleration();
            }


            if (_isPreFanOn)
            {
                
                CreatePreWind();
            }
/*            else
            {
                
                StopPreWind();

            }*/


            //Sets the collision box = to the box colider on the object. CJ
            Vector3 boxCenter = transform.TransformPoint(boxCollider.center); 
            Vector3 boxSize = Vector3.Scale(boxCollider.size, transform.lossyScale) * 0.5f; 
            Collider[] colliders = Physics.OverlapBox(boxCenter, boxSize, transform.rotation);

          
            if (_isFanOn)
            {
                foreach (Collider hit in colliders)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        //Pushes the character away from the fan CJ
                        Vector3 pushDirection = transform.position.normalized;
                        pushDirection = transform.rotation * pushDirection;

                        rb.AddForce(pushDirection * force, ForceMode.Impulse);

                        print("Hit fan and pushed away");
                    }
                }
            }
            else
            {
                if (fanStaysOn)
                {
                    foreach (Collider hit in colliders)
                    {
                        Rigidbody rb = hit.GetComponent<Rigidbody>();

                        if (rb != null)
                        {
                            //Pushes the character away from the fan CJ
                            Vector3 pushDirection = transform.position.normalized;
                            pushDirection = transform.rotation * pushDirection;

                            rb.AddForce(pushDirection * force, ForceMode.Impulse);
                        }

                    }
                }
            }
                   

           

        }

        private void Acceleration()
        {
            _fanRotateSpeed += _fanAcceleration;
           

            if (_fanRotateSpeed > _maxFanSpeed)
            {
                _fanRotateSpeed = _maxFanSpeed;
            }

     
        }

        private void Deceleration()
        {
            _fanRotateSpeed -= _fanAcceleration;
          

            if (_fanRotateSpeed < _minFanSpeed)
            {
                _fanRotateSpeed = _minFanSpeed;
            }

        }

        private IEnumerator SwitchFanOnOff()
        {
            while (!fanStaysOn)
            {
                yield return new WaitForSeconds(PrewindDelay);
                StopWind();
                _isPreFanOn = true;
            }
        }

        void CreateWind()
        {
            if (!IsHidden) 
            { 
                if (!isWindPlaying)
                {
                    SoundHandler.instance.PlaySound(FanSound, transform, 0.2f, PrewindDelay);
                    windAnim.SetActive(true);
                   // windEffect.Play();
                    StartCoroutine(SwitchFanOnOff());
                    isWindPlaying = true;

                }
            }
        }

        void StopWind()
        {
            windAnim.SetActive(false);
            //windEffect.Stop();
            _isFanOn = false;
            isWindPlaying = false;
            
        }

        private IEnumerator PreFanOnOff()
        {
            
            yield return new WaitForSeconds(PrewindDelay);
            StopPreWind();
            _isFanOn = true;


        }

        void CreatePreWind()
        {
            if (!IsHidden)
            {
                if (!isPreWindPlaying)
                {
                    PrewindEffect.Play();
                    StartCoroutine(PreFanOnOff());
                    isPreWindPlaying = true;
                }
            }
        }

        void StopPreWind()
        {
            PrewindEffect.Stop();
            isPreWindPlaying = false;
            _isPreFanOn = false;
            //_isPreFanOn = !_isPreFanOn;
        }



        void DrawBox(Vector3 center, Vector3 size, Quaternion rotation, Color color)
        {
            Vector3[] points = new Vector3[8];

            // Calculate box corners
            Vector3 halfSize = size;
            points[0] = center + rotation * new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);
            points[1] = center + rotation * new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
            points[2] = center + rotation * new Vector3(halfSize.x, -halfSize.y, halfSize.z);
            points[3] = center + rotation * new Vector3(-halfSize.x, -halfSize.y, halfSize.z);

            points[4] = center + rotation * new Vector3(-halfSize.x, halfSize.y, -halfSize.z);
            points[5] = center + rotation * new Vector3(halfSize.x, halfSize.y, -halfSize.z);
            points[6] = center + rotation * new Vector3(halfSize.x, halfSize.y, halfSize.z);
            points[7] = center + rotation * new Vector3(-halfSize.x, halfSize.y, halfSize.z);

            // Draw bottom square
            Debug.DrawLine(points[0], points[1], color);
            Debug.DrawLine(points[1], points[2], color);
            Debug.DrawLine(points[2], points[3], color);
            Debug.DrawLine(points[3], points[0], color);

            // Draw top square
            Debug.DrawLine(points[4], points[5], color);
            Debug.DrawLine(points[5], points[6], color);
            Debug.DrawLine(points[6], points[7], color);
            Debug.DrawLine(points[7], points[4], color);

            // Draw vertical edges
            Debug.DrawLine(points[0], points[4], color);
            Debug.DrawLine(points[1], points[5], color);
            Debug.DrawLine(points[2], points[6], color);
            Debug.DrawLine(points[3], points[7], color);
        }


    }
}