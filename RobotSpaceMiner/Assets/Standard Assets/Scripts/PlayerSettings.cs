using UnityEngine;
using System.Collections;

public class PlayerSettings {

	private static PlayerSettings instance = new PlayerSettings();
	private XmlSettings settings;
	public static PlayerSettings Instance 
	{
		get { return instance;}
	}
	public int Age { get { return settings.Age; } }
	public float Timer { get { return settings.Time; } }
	public int Gender { get { return settings.Gender; } }

	private PlayerSettings()
	{
		settings = SettingsSerializer.Instance.ReadSettings ();
	}
	public void SetSettings(XmlSettings newSettings)
	{
		this.settings = newSettings;
		SettingsSerializer.Instance.WriteSettings (settings);
	}
	public XmlSettings GetSettings()
	{
		return settings;
	}

}
