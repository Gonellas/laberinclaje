using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntryName : MonoBehaviour
{
    public string nombreGuardado;    
    public TMP_InputField inputField;

    private void Start()
    {
        inputField.interactable = true; // Permite la interacción con el InputField        
    }

    public void ConfirmInput()
    {        
            PlayerPrefs.SetString("TextoIngresado", inputField.text);
            PlayerPrefs.Save();
            nombreGuardado = PlayerPrefs.GetString("TextoIngresado");
            Debug.LogWarning(nombreGuardado);
    }
}
