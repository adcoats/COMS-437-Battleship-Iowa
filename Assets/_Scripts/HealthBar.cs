using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject healthBarTarget;
    
    // left: 16, pos y: -24,
    // right: 16, height: 16
    // color: A30000FF
    private Image healthBarImage;
    private float initialWidth;
    private float emptyWidth;
    private float initialHealth;
    private float lastHealth;
    private float lastDamageTime;

    public float flashTime = 0.25f;
    public Color flashColor = Color.red;
    private Color normalColor;

    //bg left: 14, posY: -24
    //   right: 14, height: 20
    // outline distance x: 3, y: 6
    //         color: 0000007F

    // Use this for initialization
    void Start ()
    {
        healthBarImage = GetComponent<Image>();
        initialWidth = healthBarImage.rectTransform.sizeDelta.x;
        initialHealth = healthBarTarget.GetComponent<Health>().health;

        initialWidth = healthBarImage.rectTransform.sizeDelta.x;
        emptyWidth = -healthBarImage.rectTransform.rect.width + initialWidth;

        lastHealth = initialHealth;
        normalColor = healthBarImage.color;
        lastDamageTime = -500;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float curHealth = healthBarTarget == null ? 0 : healthBarTarget.GetComponent<Health>().health;
        if (lastHealth != curHealth)
        {
            lastHealth = curHealth;
            lastDamageTime = Time.time;
        }

        float healthNormalized = (curHealth / initialHealth);
        healthBarImage.rectTransform.sizeDelta = new Vector2(Mathf.Lerp(emptyWidth, initialWidth, healthNormalized), healthBarImage.rectTransform.sizeDelta.y);

        healthBarImage.color = Color.Lerp(flashColor, normalColor, (Time.time - lastDamageTime) / flashTime);
    }
}
