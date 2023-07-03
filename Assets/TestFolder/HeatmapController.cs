using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class HeatmapController : MonoBehaviour
{
    [Serializable]
    public class Settings
    {
        public int heightInParticles;
        public float maxColoringDistance;
        public bool ignoreYforColoring = false;
        public Gradient gradient;
        public float particleDistance;
        public float colorMultiplier;
        public float particleSize;
        public float colorCutoff;
        public Material particleMaterial;
        public int maxParticleNumber = 50000;

        public string pathForReadingData;
    }
    public Settings settings = new();
    private HeatmapVisualisation heatmapVisualisation;

    private bool eventsAreLoaded = true;
    private bool particleSystemIsInitialized = false;

    public List <Vector3> Points;
    private void Awake()
    {
        heatmapVisualisation = new HeatmapVisualisation(settings);
    }

    private void Start()
    {
        InitializeParticleSystem();
        AddSelectedEventsToHeatmap();
    }

    /// <summary>
    /// Loads events from file into events property (that also makes them display in heatmap configuration)
    /// </summary>
    public void LoadEvents()
    {
        /*Stopwatch stopwatch = new();
        stopwatch.Start();

        eventReader = new JSONEventReader(settings.pathForReadingData);

        if (eventReader.ReaderIsAvailable())
        {
            events = eventReader.ReadEvents();
            eventsAreLoaded = true;
        }
        else
        {
            eventsAreLoaded = false;
            Debug.Log("Error while trying to read events. Event reader is not available");
        }

        stopwatch.Stop();
        Debug.Log("LoadEvents - Elapsed Time is " + stopwatch.ElapsedMilliseconds + " ms");*/
    }

    /// <summary>
    /// Creates and configures particle system (and particle array)
    /// </summary>
    public void InitializeParticleSystem()
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();

        heatmapVisualisation.InitializeParticleSystem(gameObject);
        heatmapVisualisation.InitializeParticleArray();
        particleSystemIsInitialized = true;

        stopwatch.Stop();
        Debug.Log("InitializeParticleSystem - Elapsed Time is " + stopwatch.ElapsedMilliseconds + " ms");
    }

    /// <summary>
    /// Resets heatmap color(color values) to default
    /// </summary>
    public void ResetHeatmap()
    {
        heatmapVisualisation.ResetParticlesColor();
        heatmapVisualisation.UpdateParticlesInParticleSystem();
    }

    /// <summary>
    /// Adds selected (in Editor window) events to heatmap and updates heatmap with their values
    /// </summary>
    public void AddSelectedEventsToHeatmap()
    {

        Stopwatch stopwatch = new();
        stopwatch.Start();

        heatmapVisualisation.ResetParticlesColor();

        heatmapVisualisation.AddEventToHeatMap(Points.ToArray());


        heatmapVisualisation.UpdateParticlesInParticleSystem();

        stopwatch.Stop();
        Debug.Log("AddEventsToHeatMap  - Elapsed Time is " + stopwatch.ElapsedMilliseconds + " ms");
    }

    /// <summary>
    /// Status of Load Events action. (see HeatmapGUI.cs for usage))
    /// </summary>
    public bool IsLoadEventsActive()
    {
        return !string.IsNullOrEmpty(settings.pathForReadingData);
    }

    /// <summary>
    /// Status of Initialize Particle System action.  (see HeatmapGUI.cs for usage)
    /// </summary>
    public bool IsInitializeParticleSystemActive()
    {
        return GetComponent<BoxCollider>() != null;
    }

    /// <summary>
    /// Status of Add Events to Heatmap action.  (see HeatmapGUI.cs for usage)
    /// </summary>
    public bool IsAddEventToHeatMapActive()
    {
        return eventsAreLoaded && particleSystemIsInitialized;
    }

    /// <summary>
    /// Status of Reset Heatmap action. (see HeatmapGUI.cs for usage)
    /// </summary>
    public bool IsResetHeatmapActive()
    {
        return particleSystemIsInitialized;
    }
}