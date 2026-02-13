
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    protected string panelName;
    protected bool enableInput = true;

    protected virtual void Awake()
    {
    }

    protected virtual void Start(){}

    protected virtual void Update()
    {
        if (enableInput)
        {
            HandleInput();
        }
    }

    protected virtual void HandleInput()
    {
    }

    // public virtual void SetActive(bool active)
    // {
    //     gameObject.SetActive(active);
    // }

    public virtual void OnPanelOpen(string openPanelName)
    {
        panelName = openPanelName;

        OpenAnimation();
        //SetActive(true);

    }

    protected virtual void OpenAnimation()
    {
        //enableInput = false;
    }



    public virtual void OnPanelClose()
    {
        //enableInput = false;

        //SetActive(false);
        Destroy(gameObject);

    }
}

