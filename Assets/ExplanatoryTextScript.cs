using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class ExplanatoryTextScript : MonoBehaviour
{
// can ignore the update, it's just to make the coroutines get called for example
    void Start()
    {
		    Invoke("startFadeIn", 5f);
			Invoke("startFadeOut", 15f);
    }
 
 
	void startFadeIn()
	{
            StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Text>()));
	}

 	void startFadeOut()
	{
            StartCoroutine(FadeTextToZeroAlpha(1f,  GetComponent<Text>()));
	}

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
 
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}