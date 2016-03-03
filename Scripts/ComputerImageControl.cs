using UnityEngine;
using System.Collections;

//Controls image prefab for computer choice
public class ComputerImageControl : Main {

   public Material rockMat, paperMat, sciMat;

    void Start()
    {
        //Image set based on computer's choice in Main.cs; changes the material
       if( (int)p2Choice == 1)
        {
            this.GetComponent<Renderer>().sharedMaterial = rockMat;
        }
       else if ((int)p2Choice == 2)
        {
            this.GetComponent<Renderer>().sharedMaterial = paperMat;
        }
        else
        {
            this.GetComponent<Renderer>().sharedMaterial = sciMat;
        }
    }

}
