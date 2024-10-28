using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class Interactable : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObj;
    private IInteractable curInteractable;

    public GameObject infoPanel;
    public TextMeshProUGUI promtText;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * maxCheckDistance, Color.red );
        if(Time.time - lastCheckTime > checkRate)
        {
            Ray ray = cam.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit, maxCheckDistance, layerMask))
            {
                if(hit.collider.gameObject != curInteractGameObj)
                {
                    //Debug.Log($"RayHit :{hit.collider.gameObject.name}");
                    curInteractGameObj = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObj = null;
                curInteractable = null;
                infoPanel.gameObject.SetActive(false);
                promtText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        infoPanel.gameObject.SetActive(true);
        promtText.gameObject.SetActive(true );
        promtText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractable()
    {
        if(curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameObj = null;
            curInteractable = null;
            infoPanel.gameObject.SetActive(false);
            promtText.gameObject.SetActive(false);
        }
    }
}
