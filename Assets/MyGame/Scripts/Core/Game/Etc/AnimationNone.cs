using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationNone : MonoBehaviour
{
    public void ToNoneState(){
        GetComponent<Animator>().Play( "None" );
    }
}
