﻿using System;
using System.Windows.Forms;
using System.IO;

using SA_Tools;
using SonicRetro.SAModel.SAEditorCommon.ModManagement;

namespace ProjectManager
{
	public partial class ModConfigEditor : Form
	{
		private Game game;
		private string projectName;
		private string projectFolder;

		ProjectSettings projectSettings;

		public ModConfigEditor()
		{
			InitializeComponent();
		}

		public void Init(Game game, string projectName, string projectFolder)
		{
			this.game = game;
			this.projectName = projectName;
			this.projectFolder = projectFolder;
		}

		void Save()
		{
			string modInfoPath = Path.Combine(projectFolder, "mod.ini");
			// save mod info
			if (game == Game.SADX)
			{
				SADXModInfo sadxModInfo = IniSerializer.Deserialize<SADXModInfo>(modInfoPath);
				sadxModInfo.Name = displayNameTextBox.Text;
				sadxModInfo.Description = descriptionTextBox.Text;

				IniSerializer.Serialize(sadxModInfo, modInfoPath);
			}
			else
			{
				SA2ModInfo sa2ModInfo = IniSerializer.Deserialize<SA2ModInfo>(modInfoPath);
				sa2ModInfo.Name = displayNameTextBox.Text;
				sa2ModInfo.Description = descriptionTextBox.Text;

				IniSerializer.Serialize(sa2ModInfo, modInfoPath);
			}

			// save project settings
			string projectSettingsPath = Path.Combine(projectFolder, "ProjectSettings.ini");

			projectSettings.OtherModsToRun.Clear();

			string[] otherMods = otherModsTextBox.Text.Split( new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries );

			foreach(string otherMod in otherMods)
			{
				if (otherMod.Length > 0)
				{
					projectSettings.OtherModsToRun.Add(otherMod);
				}
			}

			// update project settings
			projectSettings.PostBuildScript = postBuildScript.Text;

			IniSerializer.Serialize(projectSettings, projectSettingsPath);
		}

		private void ModConfigEditor_Shown(object sender, EventArgs e)
		{
			// load mod info
			if (game == Game.SADX)
			{
				SADXModInfo sadxModInfo = IniSerializer.Deserialize<SADXModInfo>(Path.Combine(projectFolder, "mod.ini"));

				displayNameTextBox.Text = sadxModInfo.Name;
				descriptionTextBox.Text = sadxModInfo.Description;
			}
			else
			{
				SA2ModInfo sa2ModInfo = IniSerializer.Deserialize<SA2ModInfo>(Path.Combine(projectFolder, "mod.ini"));

				displayNameTextBox.Text = sa2ModInfo.Name;
				descriptionTextBox.Text = sa2ModInfo.Description;
			}

			// load project settings
			string projectSettingsPath = Path.Combine(projectFolder, "ProjectSettings.ini");
			if (File.Exists(projectSettingsPath))
			{
				projectSettings = IniSerializer.Deserialize<ProjectSettings>(projectSettingsPath);
			}
			else projectSettings = new ProjectSettings();


			otherModsTextBox.Text = "";

			foreach (string otherMod in projectSettings.OtherModsToRun)
			{
				otherModsTextBox.Text = string.Format("{0}{1}{2}", otherModsTextBox.Text, (otherModsTextBox.Text.Length > 0) ? Environment.NewLine : "", otherMod);
			}

			postBuildScript.Text = projectSettings.PostBuildScript;
		}

		private void ModConfigEditor_FormClosed(object sender, FormClosedEventArgs e)
		{
			Save();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog fileDialog = new OpenFileDialog())
			{
				fileDialog.InitialDirectory = projectFolder;

				if(fileDialog.ShowDialog() == DialogResult.OK)
				{
					postBuildScript.Text = fileDialog.FileName;
				}
			}
		}
	}
}
