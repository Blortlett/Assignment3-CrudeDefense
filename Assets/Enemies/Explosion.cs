using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public List<SpriteRenderer> srs;
    private int mTileSize = 16; //Size of square pieces (in pixels)
    private float mExplosionForce = 60f;
    private float mFragmentLifetime = 10f;

    public void Explode()
    {
        foreach(SpriteRenderer Image in srs.ToList())
        {
            ExplodeSingle(Image);
        }
    }

    public void ExplodeSingle(SpriteRenderer sr)
    {

        if (sr == null || sr.sprite == null)
        {
            Debug.LogError("No SpriteRenderer or sprite found!");
            return;
        }

        Texture2D texture = sr.sprite.texture;
        Rect spriteRect = sr.sprite.textureRect;
        Vector2 pivot = sr.sprite.pivot;
        Vector2 pixelsPerUnit = new Vector2(sr.sprite.pixelsPerUnit, sr.sprite.pixelsPerUnit);

        int startX = (int)spriteRect.x;
        int startY = (int)spriteRect.y;
        int width = (int)spriteRect.width;
        int height = (int)spriteRect.height;

        for (int x = 0; x < width; x += mTileSize)
        {
            for (int y = 0; y < height; y += mTileSize)
            {
                int tileWidth = Mathf.Min(mTileSize, width - x);
                int tileHeight = Mathf.Min(mTileSize, height - y);

                Color[] pixels = texture.GetPixels(startX + x, startY + y, tileWidth, tileHeight);
                Texture2D tileTex = new Texture2D(tileWidth, tileHeight);
                tileTex.SetPixels(pixels);
                tileTex.Apply();

                Sprite tileSprite = Sprite.Create(tileTex, new Rect(0, 0, tileWidth, tileHeight), new Vector2(0.5f, 0.5f), sr.sprite.pixelsPerUnit);
                GameObject tileObj = new GameObject("Tile");
                tileObj.transform.position = transform.position + new Vector3(
                    (x - pivot.x + tileWidth / 2f) / pixelsPerUnit.x,
                    (y - pivot.y + tileHeight / 2f) / pixelsPerUnit.y,
                    0);

                SpriteRenderer tileSr = tileObj.AddComponent<SpriteRenderer>();
                tileSr.sprite = tileSprite;
                tileSr.sortingLayerID = sr.sortingLayerID;
                tileSr.sortingOrder = sr.sortingOrder;

                Rigidbody2D rb = tileObj.AddComponent<Rigidbody2D>();

                CircleCollider2D collider = tileObj.AddComponent<CircleCollider2D>();
                collider.radius = tileSprite.bounds.size.x / 2;

                rb.gravityScale = 0.5f;
                Vector2 randomForce = Random.insideUnitCircle.normalized * mExplosionForce;
                rb.AddForce(randomForce, ForceMode2D.Impulse);

                Destroy(tileObj, mFragmentLifetime);
            }
        }

        Destroy(this.gameObject);
    }
}
