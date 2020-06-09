using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Gracz
{
    public string Imie;
    public string Nazwisko;
    public int Wiek;
}

public class Saving : MonoBehaviour
{

    [SerializeField]
    InputField imieInput;
    [SerializeField]
    InputField nazwiskoInput;
    [SerializeField]
    InputField wiekInput;

    [SerializeField]
    Text imieText;
    [SerializeField]
    Text nazwiskoText;
    [SerializeField]
    Text wiekText;
    
    void Start()
    {

    }

    public void SavePrefs()
    {
        PlayerPrefs.SetString("Imie", imieInput.text);
        PlayerPrefs.SetString("Nazwisko", nazwiskoInput.text);
        PlayerPrefs.SetInt("Wiek", int.Parse(wiekInput.text));
        PlayerPrefs.Save();
    }

    public void LoadPrefs()
    {
        FillData(PlayerPrefs.GetString("Imie"), PlayerPrefs.GetString("Nazwisko"), PlayerPrefs.GetInt("Wiek"));
    }

    public void SaveBin()
    {
        string sciezka = Application.persistentDataPath + "/gracz.bin";
        Debug.Log(sciezka);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream plik = File.Open(sciezka, FileMode.OpenOrCreate);
        Gracz gracz = new Gracz
        {
            Imie = imieInput.text,
            Nazwisko = nazwiskoInput.text,
            Wiek = int.Parse(wiekInput.text)
        };
        bf.Serialize(plik, gracz);
        plik.Close();
    }

    public void LoadBin()
    {
        string sciezka = Application.persistentDataPath + "/gracz.bin";
        if (File.Exists(sciezka))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream plik = File.Open(sciezka, FileMode.Open);
            Gracz gracz = (Gracz)bf.Deserialize(plik);
            plik.Close();
            FillData(gracz.Imie, gracz.Nazwisko, gracz.Wiek);
        }
    }

    void FillData(string imie, string nazwisko, int wiek)
    {
        imieText.text = "Imię: " + imie;
        nazwiskoText.text = "Nazwisko: " + nazwisko;
        wiekText.text = "Wiek: " + wiek;
    }

    void Update()
    {
        
    }
}
