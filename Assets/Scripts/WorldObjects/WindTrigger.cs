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

        public float radius;
        public float force;

        [SerializeField] int windDelay;

        public bool _isFanOn = false;
        private bool isWindPlaying = true;

        BoxCollider boxCollider;
       [SerializeField] ParticleSystem windEffect;

        private void Start()
        {
            boxCollider = GetComponent<BoxCollider>(); // Get the BoxCollider
            StartCoroutine( SwitchFanOnOff());
        }

        void FixedUpdate()
        {
            _fanRotation.Rotate(Vector3.up * _fanRotateSpeed * Time.deltaTime);

            if (_isFanOn)
            {
                Acceleration();
                CreateWind();
            }
            else
            {
                Deceleration();
                StopWind();
                
            }

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
            while (true)
            {
                yield return new WaitForSeconds(windDelay);
                _isFanOn = !_isFanOn;
            }
        }

        void CreateWind()
        {
            if (!isWindPlaying)
            {
                windEffect.Play();
                isWindPlaying = true;
            }

        }

        void StopWind()
        {
            windEffect.Stop();  
            isWindPlaying = false;
            
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