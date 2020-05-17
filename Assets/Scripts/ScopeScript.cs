using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    bool isScoped = false;
    public GameObject weaponModel;
    public CanvasGroup overlayCanvasGroup;
    public Camera mainCam;
    public float scopedFov = 15f;
    public float normalFov;
    private void Start()
    {
        overlayCanvasGroup = GameObject.FindGameObjectWithTag("ScopeOverlay").GetComponent<CanvasGroup>();
        weaponModel.SetActive(true);
        normalFov = mainCam.fieldOfView;
    }
    private void Update()
    {
        
        if (gameObject.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                animator.SetBool("Scoped", isScoped);
                isScoped = !isScoped;
                weaponModel.SetActive(isScoped);
                if (isScoped)
                    StartCoroutine(OnScoped());
                else
                    OnUnScoped();

            }
        }
    }
    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.25f);
        overlayCanvasGroup.alpha = 1f;
        overlayCanvasGroup.blocksRaycasts = true;
        weaponModel.SetActive(false);
        //normalFov = mainCam.fieldOfView;
        mainCam.fieldOfView = scopedFov;
    }
    void OnUnScoped()
    {
        overlayCanvasGroup.alpha = 0f;
        overlayCanvasGroup.blocksRaycasts = false;
        weaponModel.SetActive(true);
        mainCam.fieldOfView = normalFov;
    }
    //private void OnDisable()
    //{
    //    weaponCam.SetActive(true);
    //}

}
