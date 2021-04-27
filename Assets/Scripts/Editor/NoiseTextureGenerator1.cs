using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NoiseTextureGenerator1 : MonoBehaviour
{
    // Start is called before the first frame update
    [MenuItem("Test/Create Noise Texture")]
    static void CreateTexture()
    {
        Texture3D tex3D = new Texture3D(92,92,92, UnityEngine.Experimental.Rendering.DefaultFormat.LDR, UnityEngine.Experimental.Rendering.TextureCreationFlags.None);

        Color[] pixels = new Color[92*92*92];

        for(int i = 0; i < 92*92*92; i++)
        {
          
            float value = Random.Range(0.8f, 1.0f);
            pixels[i] = new Color(value, value, value, 1f);
                
        }

        tex3D.SetPixels(pixels);
        tex3D.Apply();

        AssetDatabase.CreateAsset(tex3D, "Assets/WhiteNoise.asset");
    }

    
}
