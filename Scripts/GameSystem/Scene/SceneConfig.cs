using UnityEngine;

namespace MyGameSystem.Scene
{
    public class SceneConfig : MonoBehaviour
    {
        [Header("=== Start ===")]
        public IL3DN.IL3DN_Snow startSnow;

        [Header("=== Gameplay 1 ===")]

        [Header("区块交接处")]
        public GameObject bridgeCollider;
        //public WeatherType CurrentWeather { get; private set; } = WeatherType.Sunny;
        [Header("天气")]

        [SerializeField] public IL3DN.IL3DN_Snow snow;
        [SerializeField] public GameObject snowParticles;
        [SerializeField] public GameObject snowTerrain;

    }
}
