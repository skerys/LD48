using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseTextureGenerator : MonoBehaviour
{
    public ComputeShader noiseShader;
    RenderTexture noiseTex;

    public int width, height;

    public List<Renderer> renderers;

    void Start()
    {
        noiseTex = new RenderTexture(width, height, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
        noiseTex.enableRandomWrite = true;
        noiseTex.filterMode = FilterMode.Point;
        noiseTex.wrapMode = TextureWrapMode.Repeat;
        noiseTex.Create();

        foreach(var renderer in renderers)
        {
            renderer.material.SetTexture("_Noise", noiseTex);
        }
        
        noiseShader.SetInt("width", width);
        noiseShader.SetInt("height", height);
    }

    void Update()
    {
        noiseShader.SetTexture(0, "Result", noiseTex);
        noiseShader.SetFloat("time", Time.time);
        noiseShader.Dispatch(0, width, height, 1);
    }
}
