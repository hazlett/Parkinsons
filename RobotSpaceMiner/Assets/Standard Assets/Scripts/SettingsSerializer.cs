using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SettingsSerializer {

    private string logPath = "Settings.xml";


    private static SettingsSerializer instance = new SettingsSerializer();

    private SettingsSerializer()
    {     
       
       
    }
    public static SettingsSerializer Instance
    {
        get { return instance; }
    }

    public void WriteSettings(XmlSettings settings)
    {
        XmlSerializer xml = new XmlSerializer(typeof(XmlSettings));
        using (Stream stream = File.Create(logPath))
        {
            xml.Serialize(stream, settings);
        }
    }
    public XmlSettings ReadSettings()
    {
        using (var file = File.Open(logPath, FileMode.Open, FileAccess.Read))
        {
            XmlSerializer xml = new XmlSerializer(typeof(XmlSettings));
            return (XmlSettings)xml.Deserialize(file);
        }
    }
}
