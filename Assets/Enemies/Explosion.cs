using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public List<SpriteRenderer> mSpriteRendererList;
    private int mTileSize = 20; //Size of square pieces (in pixels)
    private float mExplosionForce = 60f;
    private float mFragmentLifetime = 6f;

    public void Explode()
    {
        foreach (SpriteRenderer Image in mSpriteRendererList.ToList())
        {
            ExplodeSingle(Image);
        }
    }

    public void ExplodeSingle(SpriteRenderer _SpriteRenderer)
    {
        Texture2D Texture = _SpriteRenderer.sprite.texture;
        Rect SpriteRect = _SpriteRenderer.sprite.textureRect;
        Vector2 SpritePivot = _SpriteRenderer.sprite.pivot;
        Vector2 PixelsPerUnit = new Vector2(_SpriteRenderer.sprite.pixelsPerUnit, _SpriteRenderer.sprite.pixelsPerUnit);

        int StartX = (int)SpriteRect.x;
        int StartY = (int)SpriteRect.y;
        int Width = (int)SpriteRect.width;
        int Height = (int)SpriteRect.height;

        for (int x = 0; x < Width; x += mTileSize)
        {
            for (int y = 0; y < Height; y += mTileSize)
            {
                int TileWidth = Mathf.Min(mTileSize, Width - x);
                int TileHeight = Mathf.Min(mTileSize, Height - y);

                //
                //Get a tile from the image by isolating part of the sprite(Crop it) and make a new texture from it
                //
                Color[] Pixels = Texture.GetPixels(StartX + x, StartY + y, TileWidth, TileHeight);
                Texture2D TileTex = new Texture2D(TileWidth, TileHeight);
                TileTex.SetPixels(Pixels);
                TileTex.Apply();

                //
                //Create a new sprite from the texture we just got
                //
                Sprite TileSprite = Sprite.Create(TileTex, new Rect(0, 0, TileWidth, TileHeight), new Vector2(0.5f, 0.5f), _SpriteRenderer.sprite.pixelsPerUnit);
                GameObject TileGameObject = new GameObject("Tile");
                TileGameObject.transform.position = transform.position + new Vector3((x - SpritePivot.x + TileWidth / 2f) / PixelsPerUnit.x, (y - SpritePivot.y + TileHeight / 2f) / PixelsPerUnit.y, 0);


                //
                //Sets the sprite renderer of created tile
                //
                SpriteRenderer TileSpriteRenderer = TileGameObject.AddComponent<SpriteRenderer>();
                TileSpriteRenderer.sprite = TileSprite;
                TileSpriteRenderer.sortingLayerID = _SpriteRenderer.sortingLayerID;
                TileSpriteRenderer.sortingOrder = _SpriteRenderer.sortingOrder;


                //
                //Collision for tile
                //
                CircleCollider2D Collider = TileGameObject.AddComponent<CircleCollider2D>();
                Collider.radius = TileSprite.bounds.size.x / 2;

                //
                //Adds impulse force to rigid body so tile gets exploded
                //
                Rigidbody2D RB = TileGameObject.AddComponent<Rigidbody2D>();

                RB.gravityScale = 0.5f;
                Vector2 randomForce = Random.insideUnitCircle.normalized * mExplosionForce;
                RB.AddForce(randomForce, ForceMode2D.Impulse);

                Destroy(TileGameObject, mFragmentLifetime);
            }
        }

        Destroy(this.gameObject);
    }
}
