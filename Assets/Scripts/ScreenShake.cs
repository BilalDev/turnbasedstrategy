using UnityEngine;
using Cinemachine;

public class ScreenShake : SingletonMonoBehaviour<ScreenShake>
{
    private CinemachineImpulseSource cinemachineImpulseSource;

    protected override void Awake()
    {
        base.Awake();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake(float intensity = 1f)
    {
            cinemachineImpulseSource.GenerateImpulse(intensity);
    }
}