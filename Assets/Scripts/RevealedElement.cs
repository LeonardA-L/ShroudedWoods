using UnityEngine;

public class RevealedElement : MonoBehaviour
{
    public int textureSize = 512;
    public float k = 4;
    private readonly Color c_color = new Color(0, 0, 0, 0);

    private Material material;
    private Texture2D texture;
    private bool isEnabled = false;
	
	void Start ()
    {
        Renderer rndr = GetComponent<Renderer>();
        if(rndr == null)
            return;

        foreach (Material mat in rndr.materials)
        {
            if (mat.shader.name == "Custom/Reveal")
            {
                material = mat;
                break;
            }
        }

        if (material == null)
            return;

        texture = new Texture2D((int)(textureSize / k), (int)(textureSize / k));
        for (int x = 0; x < (int)(textureSize / k); ++x)
            for (int y = 0; y < (int)(textureSize / k); ++y)
                texture.SetPixel(x, y, c_color);

        texture.Apply();
        material.SetTexture("_DrawingTex", texture);
        isEnabled = true;
	}

    public void PaintOn(Vector2 textureCoord, Texture2D splashTexture)
    {
        if (isEnabled)
        {
            // Iterate over patch
            int x = (int)(textureCoord.x * textureSize / k) - (int)(splashTexture.width / (1*2));
            int y = (int)(textureCoord.y * textureSize / k) - (int)(splashTexture.height / (1*2));
            for (int i = 0; i < (int)((float)splashTexture.width / 1); ++i)
                for (int j = 0; j < (int)((float)splashTexture.height / 1); ++j)
                {
                    int newX = x + i;
                    int newY = y + j;
                    Color existingColor = texture.GetPixel(newX, newY);
                    Color blankColor = new Color(0, 0, 0, 1);
                    Color targetColor = splashTexture.GetPixel((int)(i / 1), (int)(j / 1));

                    float alpha = targetColor.a;

                    // Lerp from white to revealed texture color
                    Color result = Color.Lerp(blankColor, existingColor, alpha);
                    result.a = existingColor.a + alpha;
                    texture.SetPixel(newX, newY, result);
                }
            
            texture.Apply();
        }
    }
}
