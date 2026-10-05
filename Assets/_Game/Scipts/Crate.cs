using UnityEngine;

public class Crate : MonoBehaviour
{
    //[Header("Values")]

    [Header("Setup")]
    [SerializeField] private bool dimGraphic = true;
    [SerializeField] private Vector4 multiplayerForDim = new Vector4(0.9f,0.9f,0.9f,1);
    

    [Header("Debug")]
    [Header("Please do not change anything. This area is only for checking values.")]
    [SerializeField] private BoxCollider2D colliderOfTheCrate;
    [SerializeField] private LayerMask excludePlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!colliderOfTheCrate)
        {
            BoxCollider2D[] allBoxColliders = this.GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D forecheObject in allBoxColliders)
            {
                if (forecheObject.isTrigger == false && !colliderOfTheCrate)
                {
                    colliderOfTheCrate = forecheObject;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggelCreate(bool newState = false)
    {
        //this.gameObject.SetActive(newState);
        //colliderOfTheCrate.enabled = newState;
        colliderOfTheCrate.excludeLayers = excludePlayer;
        if (dimGraphic)
        {
            Color colorOfSprite = this.transform.GetComponent<SpriteRenderer>().color;
            if (!newState)
            {
                colorOfSprite = new Color(colorOfSprite.r * multiplayerForDim.x,
                                         colorOfSprite.g * multiplayerForDim.y,
                                         colorOfSprite.b * multiplayerForDim.z,
                                         colorOfSprite.a * multiplayerForDim.w);
            }
            else
            {
                colorOfSprite = new Color(100 * colorOfSprite.r / multiplayerForDim.x,
                                            100 * colorOfSprite.g / multiplayerForDim.y,
                                            100 * colorOfSprite.b / multiplayerForDim.z,
                                            100 * colorOfSprite.a / multiplayerForDim.w);
            }
            

            this.transform.GetComponent<SpriteRenderer>().color = colorOfSprite;
        }
    }
}
