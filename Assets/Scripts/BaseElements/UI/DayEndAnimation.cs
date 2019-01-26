using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class DayEndAnimation : MonoBehaviour
{
	private const float FADE_OUT_SPEED = 1.0f;
	private bool isDarkening = true;
	private BaseManager baseManager;
	private Image image;


	private void Awake()
	{
		image = GetComponent<Image>();
		Assert.IsNotNull(image, "Missing image");
		baseManager = FindObjectOfType<BaseManager>();
		Assert.IsNotNull(baseManager, "Missing baseManager");
		baseManager
	}

	private void Update()
	{
		Color newColor = image.color;
		if (isDarkening)
		{
			newColor.a += FADE_OUT_SPEED * Time.deltaTime;
			image.color = newColor;
			if (newColor.a >= 1.0f)
			{
				isDarkening = false;
			}
		}
		else
		{
			newColor.a -= FADE_OUT_SPEED * Time.deltaTime;
			image.color = newColor;
			if (newColor.a <= 0.0f)
			{
				gameObject.SetActive(false);
			}
		}
	}

	private void StartFadeOut()
	{
		Color newColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		image.color = newColor;
		isDarkening = true;
		gameObject.SetActive(true);
	}

	private void EndFadeOut()
	{
		gameObject.SetActive(false);
	}
}
