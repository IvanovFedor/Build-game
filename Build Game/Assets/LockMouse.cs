using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMouse : MonoBehaviour
{
    public bool Change = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && Change == true || Input.GetKeyDown(KeyCode.Q) && Change == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Change = false;
        }else if(Input.GetKeyDown(KeyCode.E) && Change == false || Input.GetKeyDown(KeyCode.Q) && Change == false)
        {   
            Cursor.lockState = CursorLockMode.Confined;
            Change = true;  
        }

        
    }
}
