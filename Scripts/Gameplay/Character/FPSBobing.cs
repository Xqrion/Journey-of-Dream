using UnityEngine;

namespace MyGameplay.Character
{
    public class FPSBobing : MonoBehaviour
    {
        public IL3DN.IL3DN_SimpleFPSController Controller;
        public Transform Bobtarget;
        public float sprintMultiplier = 1.5f;
        public float damping = 5;
        [Header("运动位置晃动")]
        public float posAmplitudeX = 0.05f;
        public float posFrequencyX = 10;
        public float posAmplitudeZ = 0.01f;
        public float posFrequencyZ = 10;

        private Vector3 oringinalLocalPos;
        private Vector3 targetPosOffset;
        private Vector3 currentOffset;
        [Header("运动角度晃动")]
        public float rotAmplituedX = 0.5f;
        public float rotFrequencyX = 1.5f;
        public float rotAmplituedZ = 1.2f;
        public float rotFrequencyZ = 2;

        [Header("呼吸角度晃动")]
        public float breathMultiplier = 0.5f;

        public float breathFrequency = 2f;


        private Quaternion oringinalLocalRotation;
        private Vector3 targetRotOffset;
        private Vector3 currentRotOffset;
        private void Start()
        {
            oringinalLocalPos = Bobtarget.transform.localPosition;
            oringinalLocalRotation = Bobtarget.transform.localRotation;
        }

        public void PositionBobing()
        {
            //oringinalLocalPos = Bobtarget.transform.localPosition;
            if (Controller.inputDir.magnitude > 0)
            {
                float amplitudeMultiplier = Mathf.Lerp(0, sprintMultiplier, Controller.currentSpeed/Controller.RunSpeed);
                float time = Time.time;

                float xOffset = Mathf.Sin(time * posFrequencyX * Controller.currentSpeed/Controller.RunSpeed) * posAmplitudeX * amplitudeMultiplier * Mathf.Abs(Controller.inputDir.magnitude);
                float zOffset = Mathf.Sin(time * posFrequencyZ * Controller.currentSpeed/Controller.RunSpeed) * posAmplitudeZ * amplitudeMultiplier * Mathf.Abs(Controller.inputDir.magnitude);
                targetPosOffset = new Vector3 (xOffset, zOffset,0);

            }
            else
            {
                targetPosOffset = Vector3.zero;
            }
            currentOffset = Vector3.Lerp(currentOffset, targetPosOffset, damping * Time.deltaTime);
            Bobtarget.localPosition = oringinalLocalPos + currentOffset;

        }

        public void RotationBobing()
        {
            float amplitudeMultiplier = Mathf.Lerp(0, sprintMultiplier, Controller.currentSpeed/Controller.RunSpeed);
            float time = Time.time;

            float rotx, rotz;
            //var breathFrequency = Random.Range(breathFrequencyMin, breathFrequencyMax);

            if (Controller.inputDir.magnitude == 0)
            {
                //Debug.Log(1);
                rotx = Mathf.Sin(time * breathFrequency) * rotAmplituedX *
                       breathMultiplier;
                rotz = Mathf.Sin(time * breathFrequency) * rotAmplituedZ *
                       breathMultiplier;
            }
            else
            {
                rotx = Mathf.Sin(time * rotFrequencyX * Controller.currentSpeed/Controller.RunSpeed) * rotAmplituedX * amplitudeMultiplier *
                             Mathf.Abs(Controller.inputDir.magnitude);
                rotz = Mathf.Sin(time * rotFrequencyZ * Controller.currentSpeed/Controller.RunSpeed) * rotAmplituedZ * amplitudeMultiplier *
                             Mathf.Abs(Controller.inputDir.magnitude);
            }
            targetRotOffset = new Vector3 (rotx,rotz, 0);

            currentRotOffset = Vector3.Lerp(currentRotOffset, targetRotOffset, damping * Time.deltaTime);
            Bobtarget.localRotation = Quaternion.Euler(currentRotOffset) * oringinalLocalRotation;

        }
    }
}



