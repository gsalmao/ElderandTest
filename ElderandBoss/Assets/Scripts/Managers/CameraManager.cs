using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Elderand.Managers
{
    public class CameraManager : MonoBehaviour
    {
        private static event Action<float, float, int> OnShakeCamera = delegate { };
        private static event Action<int, int> OnChangePriority = delegate { };

        [SerializeField] private List<CinemachineVirtualCamera> vCams;

        private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

        private void OnEnable()
        {
            OnShakeCamera += InstanceShakeCamera;
            OnChangePriority += InstanceChangePriority;
        }

        private void OnDisable()
        {
            OnShakeCamera -= InstanceShakeCamera;
            OnChangePriority -= InstanceChangePriority;
        }
        
        #region Shake Camera
        [Button("Shake")]
        public static void ShakeCamera(float intensity, float time, int camIndex) => OnShakeCamera(intensity, time, camIndex);
        private void InstanceShakeCamera(float intensity, float time, int camIndex) => StartCoroutine(ShakeCameraCR(intensity, time, camIndex));
        private IEnumerator ShakeCameraCR(float intensity, float shakeTime, int camIndex)
        {
            cinemachineBasicMultiChannelPerlin = vCams[camIndex].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            float initValue = intensity;
            float lerpT = 0;

            while (lerpT < shakeTime)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(initValue, 0, lerpT / shakeTime);
                lerpT += Time.deltaTime;
                yield return 0;
            }
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        }
        #endregion

        #region Change Priority

        public static void ChangePriority(int index, int priority) => OnChangePriority(index, priority);
        private void InstanceChangePriority(int index, int priority) => vCams[index].Priority = priority;

        #endregion
    }
}