using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    private List<(int, int, int)> countryColors;
    private List<int[]> countryPixels;
    private List<string> countries;

    private SpriteRenderer spriteR;
    private Sprite spriteColor;
    private Sprite sprite;

    void Start()
    {
        GameObject thecsvp = GameObject.Find("CSVParser");
        CSVParser csvpScript = thecsvp.GetComponent<CSVParser>();
        countryColors = csvpScript.colorsOnly;
        countryPixels = csvpScript.countryPixelsOnly;
        countries = csvpScript.countriesOnly;

        spriteR = gameObject.GetComponent<SpriteRenderer>();
        Texture2D newTex = (Texture2D)GameObject.Instantiate(Resources.Load<Sprite>("Maps/smallOutlined").texture);
        sprite = Sprite.Create(newTex, spriteR.sprite.rect, new Vector2(0.5f, 0.5f));
        spriteR.sprite = sprite;
        spriteColor = Resources.Load<Sprite>("Maps/smallColorOutlined");
        
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0)) {

            int pixelWidth = 5632;
            int pixelHeight = 2048;
            float unitsToPixels = 100.0f;

            // assumes an orthographic camera
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = transform.position.z;
            pos = transform.InverseTransformPoint(pos);

            int xPixelUnity = (Mathf.RoundToInt(pos.x * unitsToPixels) + pixelWidth / 2);
            int yPixelUnity = (Mathf.RoundToInt(pos.y * unitsToPixels) + pixelHeight / 2);

            int xPixelConverted = (Mathf.RoundToInt(pos.x * unitsToPixels) + pixelWidth / 2);
            int yPixelConverted = (pixelHeight / 2 - Mathf.RoundToInt(pos.y * unitsToPixels));

            Debug.Log("Pixelclicked (" + xPixelConverted + ", " + yPixelConverted + ")");

            Color pixelColor = spriteColor.texture.GetPixel(xPixelUnity, yPixelUnity);
            int r = Mathf.RoundToInt(pixelColor.r * 255);
            int g = Mathf.RoundToInt(pixelColor.g * 255);
            int b = Mathf.RoundToInt(pixelColor.b * 255);
            string colorString = "(" + r.ToString() + ", " + g.ToString() + ", " + b.ToString() + ")";
            print("color: " + colorString);

            int idx = countryColors.IndexOf((r, g, b));
            print(countryColors.IndexOf((r, g, b)));

            print(countries[idx]);
            int[] pixels = countryPixels[idx];
            for (int i = 0; i < pixels.Length/2; i++) {
                //print("colorpixel (" + pixels[i*2] + ", " + pixels[i*2 + 1] + ")");
                sprite.texture.SetPixel(pixels[i * 2 + 1], 2048 - pixels[i * 2]-1, pixelColor);
            }
            sprite.texture.Apply();
        }

    }

}
