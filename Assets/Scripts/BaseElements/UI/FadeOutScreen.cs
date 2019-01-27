using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class FadeOutScreen : MonoBehaviour
{
	private enum FadeOutCycle
	{
		DARKENING, DARK, LIGHTENING, LIGHT
	}

	private const float FADE_OUT_SPEED = 1.0f;
	private FadeOutCycle fadeOutCycle = FadeOutCycle.LIGHT;
	private Image image;
	public System.Action OnLighteningEnd;
	public System.Action OnDarkeningEnd;


	private void Awake()
	{
		image = GetComponent<Image>();
		Assert.IsNotNull(image, "Missing image");
	}

	private void Update()
	{
		Color newColor = image.color;
		if (fadeOutCycle == FadeOutCycle.DARKENING)
		{
			newColor.a += FADE_OUT_SPEED * Time.deltaTime;
			image.color = newColor;
			if (newColor.a >= 1.0f)
			{
				fadeOutCycle = FadeOutCycle.DARK;
				OnDarkeningEnd?.Invoke();
			}
		}
		else if (fadeOutCycle == FadeOutCycle.LIGHTENING) 
		{
			newColor.a -= FADE_OUT_SPEED * Time.deltaTime;
			image.color = newColor;
			if (newColor.a <= 0.0f)
			{
				fadeOutCycle = FadeOutCycle.LIGHT;
				OnLighteningEnd?.Invoke();
			}
		}
	}

	public void StartDarkening()
	{
		Color newColor = image.color;
		newColor.a = 0.0f;
		image.color = newColor;
		fadeOutCycle = FadeOutCycle.DARKENING;
	}

	public void StartLightening()
	{
		Color newColor = image.color;
		newColor.a = 1.0f;
		image.color = newColor;
		fadeOutCycle = FadeOutCycle.LIGHTENING;
	}
}
