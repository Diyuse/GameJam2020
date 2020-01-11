using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Dropdown resDropdown;

    private Resolution[] resolutions;
    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        // resDropdown.AddOptions(resolutions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
