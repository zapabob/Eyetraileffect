using UnityEngine;
using VRC.SDK3.Avatars.Components;

public class EyeTrailEffect : MonoBehaviour
{
    public VRCAvatarDescriptor avatarDescriptor;
    public float trailDuration = 0.5f;
    public Color trailColor = new Color(1f, 0.75f, 0.8f, 1f); // ピンク色に設定
    public float trailWidthMultiplier = 1f;

    private ParticleSystem particleSystem;
    private ParticleSystemRenderer particleRenderer;
    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.TrailModule trailModule;

    private void Start()
    {
        var eyeBone = avatarDescriptor.transform.Find("Body/Head/LeftEye");
        if (eyeBone == null)
        {
            eyeBone = avatarDescriptor.transform.Find("Body/Head/RightEye");
        }

        if (eyeBone != null)
        {
            particleSystem = GetComponent<ParticleSystem>();
            particleRenderer = GetComponent<ParticleSystemRenderer>();
            mainModule = particleSystem.main;
            trailModule = particleSystem.trails;

            mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
            mainModule.maxParticles = 100;
            particleRenderer.renderMode = ParticleSystemRenderMode.Billboard;
            particleRenderer.alignment = ParticleSystemRenderSpace.View;

            trailModule.enabled = true;
            trailModule.mode = ParticleSystemTrailMode.PerParticle;
            trailModule.lifetime = trailDuration;
            trailModule.worldSpace = true;
            trailModule.widthOverTrail = new ParticleSystem.MinMaxCurve(0.1f * trailWidthMultiplier, 0f);
            trailModule.colorOverLifetime = trailColor;

            transform.SetParent(eyeBone);
            transform.localPosition = Vector3.zero;
        }
    }
}