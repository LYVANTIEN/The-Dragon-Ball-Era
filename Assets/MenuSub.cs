using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuSub : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
