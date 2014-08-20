using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[XmlRoot("Settings")]
public class XmlSettings {

    [XmlAttribute]
    public int Age;

    [XmlAttribute]
    public float Time;

    [XmlAttribute]
    public int Gender;

    enum Genders {
        Male,
        Female
    };
	
    public XmlSettings(int age, int timer, int gender)
    {
        Age = age;
        Time = timer;
        Gender = gender;
    }
    public XmlSettings()
    {


    }
    
    public override string ToString()
    {
        return ("Age: " + Age + " Timer: " + Time + " Gender: " + Gender);
    }
	
}
