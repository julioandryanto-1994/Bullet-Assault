using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteBrewSDK;

public class InitialAnalytics : MonoBehaviour
{
    private void Start()
    {
        // Initialize ByteBrew
        ByteBrew.InitializeByteBrew();
    }
}
