using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    private Transform camTransform;
    private Vector3 lastcameraPosition;
    private float textureUnitSizeX;
    // Start is called before the first frame update
    void Start()
    {
        camTransform = Camera.main.transform;
        lastcameraPosition = camTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = camTransform.position - lastcameraPosition;
        transform.position -= new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastcameraPosition = camTransform.position;

        if(Mathf.Abs(camTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (camTransform.position.x-transform.position.x)%textureUnitSizeX;
            transform.position = new Vector3(camTransform.position.x +offsetPositionX, transform.position.y);
        }
    }
}
