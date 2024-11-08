using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private GameObject loginButton;
    [SerializeField] private GameObject loadingBar;
    private Slider sliderLoading;
    private float process;
    private const int processCount = 1;

    private void Start()
    {
        loginButton.SetActive(true);
        loadingBar.SetActive(false);
        sliderLoading = loadingBar.GetComponent<Slider>();
    }
    private void FixedUpdate()
    {
        if (process > 0)
            if (sliderLoading.value < process / processCount)
            {
                if (sliderLoading.value == 0)
                {
                    sliderLoading.value += Time.deltaTime;
                }

                sliderLoading.value += Time.deltaTime * 6 * (process / processCount - sliderLoading.value);
            }
    }
    public void LoadScene()
    {
        StartCoroutine(IELoadScene());
    }
    private IEnumerator IELoadScene()
    {
        loginButton.SetActive(false);
        loadingBar.SetActive(true);
        process = 1;

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Menu");
    }
}
