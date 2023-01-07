using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cerberus_Platform_UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeController : MonoBehaviour
    {
        private CanvasGroup cg;
        public float fadeTime = 1f; // Fade Time
        float accumTime = 0f;
        private Coroutine fadeCor;

        // Start is called before the first frame update
        void Start()
        {
            cg = gameObject.GetComponent<CanvasGroup>(); // Required CanvasGroup
        }

        public void StartFadeIn() // Called FadeIn
        {
            if (fadeCor != null)
            {
                StopAllCoroutines();
                fadeCor = null;
            }
            fadeCor = StartCoroutine(FadeIn());
        }

        public void StartFadeOut()
        {
            if (fadeCor != null)
            {
                StopAllCoroutines();
                fadeCor = null;
            }
            fadeCor = StartCoroutine(FadeOut());
        }


        private IEnumerator FadeIn() // Called FadeOut
        {
            //yield return new WaitForSeconds(0.2f); //SetDelay
            accumTime = 0f;
            while (accumTime < fadeTime)
            {
                cg.alpha = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
                yield return 0;
                accumTime += Time.deltaTime;
            }
            cg.alpha = 1f;
        }

        private IEnumerator FadeOut()
        {
            //yield return new WaitForSeconds(3.0f); //SetDelay
            accumTime = 0f;
            while (accumTime < fadeTime)
            {
                cg.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
                yield return 0;
                accumTime += Time.deltaTime;
            }
            cg.alpha = 0f;
        }
    }
}