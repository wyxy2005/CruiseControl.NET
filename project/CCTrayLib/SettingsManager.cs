using System;
using System.IO;
using System.Xml.Serialization;

namespace ThoughtWorks.CruiseControl.CCTrayLib
{
	/// <summary>
	/// Utility class for managing CruiseControl.NET Monitor settings.
	/// </summary>
	public class SettingsManager
	{
		/// <summary>
		/// Utility class, not intended for instantiation.
		/// </summary>
		private SettingsManager()
		{
		}

		private const string DEFAULT_SETTINGS_FILE = "cctray-settings.xml";

		private static string _settingsFileName = DEFAULT_SETTINGS_FILE;

		/// <summary>
		/// The filename of the settings file to be used by the executing application.
		/// </summary>
		public static string SettingsFileName
		{
			set { _settingsFileName = value; }
		}

		/// <summary>
		/// Gets the absolute path and filename to the settings file to be used
		/// by the executing application.
		/// </summary>
		private static string SettingsPathAndFileName
		{
			get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _settingsFileName); }
		}

		/// <summary>
		/// Writes the specified settings using Xml serialisation.
		/// </summary>
		/// <param name="settings">The settings to write.</param>
		public static void WriteSettings(Settings settings)
		{
			XmlSerializer serializer = new XmlSerializer(typeof (Settings));
			using (TextWriter writer = new StreamWriter(SettingsPathAndFileName))
			{
				serializer.Serialize(writer, settings);
			}
		}

		/// <summary>
		/// Loads and returns the settings to be used, via Xml deserialisation.
		/// </summary>
		/// <returns>The deserialised settings.</returns>
		public static Settings LoadSettings()
		{
			if (!File.Exists(SettingsPathAndFileName))
			{
				Settings defaults = Settings.CreateDefaultSettings();
				WriteSettings(defaults);
				return defaults;
			}

			// file exists, so deserialise it
			XmlSerializer serializer = new XmlSerializer(typeof (Settings));
			using (TextReader reader = new StreamReader(SettingsPathAndFileName))
			{
				return (Settings) serializer.Deserialize(reader);
			}
		}
	}
}