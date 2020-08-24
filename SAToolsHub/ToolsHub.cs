﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SonicRetro.SAModel.SAEditorCommon;
using Ookii.Dialogs;

namespace SAToolsHub
{
	public partial class SAToolsHub : Form
	{
		//Additional Windows
		private GamePaths gamePathsDiag;
		private newProj projectCreateDiag;
		private editProj projectEditorDiag;

		//Variabels
		DirectoryInfo projectDirectory;
		

		//Additional Code/Functions
		private void PopulateTreeView()
		{
			TreeNode rootNode;

			DirectoryInfo info = projectDirectory;
			if (info.Exists)
			{
				rootNode = new TreeNode(info.Name);
				rootNode.Tag = info;
				GetDirectories(info.GetDirectories(), rootNode);
				treeView1.Nodes.Add(rootNode);
			}
		}

		private void GetDirectories(DirectoryInfo[] subDirs,
			TreeNode nodeToAddTo)
		{
			TreeNode aNode;
			DirectoryInfo[] subSubDirs;
			foreach (DirectoryInfo subDir in subDirs)
			{
				aNode = new TreeNode(subDir.Name, 0, 0);
				aNode.Tag = subDir;
				aNode.ImageKey = "folder";
				subSubDirs = subDir.GetDirectories();
				if (subSubDirs.Length != 0)
				{
					GetDirectories(subSubDirs, aNode);
				}
				nodeToAddTo.Nodes.Add(aNode);
			}
		}

		void treeView1_NodeMouseClick(object sender,
	TreeNodeMouseClickEventArgs e)
		{
			listView1.ContextMenuStrip = contextMenuStrip1;
			TreeNode newSelected = e.Node;
			listView1.Items.Clear();
			DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
			ListViewItem.ListViewSubItem[] subItems;
			ListViewItem item = null;

			foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
			{
				item = new ListViewItem(dir.Name, 0);
				subItems = new ListViewItem.ListViewSubItem[]
					{new ListViewItem.ListViewSubItem(item, "Directory"),
			 new ListViewItem.ListViewSubItem(item,
				dir.LastAccessTime.ToShortDateString())};
				item.SubItems.AddRange(subItems);
				listView1.Items.Add(item);
			}
			foreach (FileInfo file in nodeDirInfo.GetFiles())
			{
				item = new ListViewItem(file.Name, 1);
				string fileName = (file.Name.ToLower());
				string fileType = (file.Extension.ToLower());

				switch (fileType)
				{
					case ".sa1mdl":
					case ".sa2mdl":
					case ".sa2bmdl":
						{
							subItems = new ListViewItem.ListViewSubItem[]
								{ new ListViewItem.ListViewSubItem(item, "Model File"),
							new ListViewItem.ListViewSubItem(item,
								file.LastAccessTime.ToShortDateString())};
							item.ImageIndex = 2;
							break;
						}
					case ".sa1lvl":
					case ".sa2lvl":
					case ".sa2blvl":
						{
							subItems = new ListViewItem.ListViewSubItem[]
								{ new ListViewItem.ListViewSubItem(item, "Level File"),
							new ListViewItem.ListViewSubItem(item,
								file.LastAccessTime.ToShortDateString())};
							break;
						}
					case ".ini":
						{
							subItems = new ListViewItem.ListViewSubItem[]
								{ new ListViewItem.ListViewSubItem(item, "Data File"),
							new ListViewItem.ListViewSubItem(item,
								file.LastAccessTime.ToShortDateString())};
							break;
						}
					case ".txt":
						{
							subItems = new ListViewItem.ListViewSubItem[]
								{ new ListViewItem.ListViewSubItem(item, "Text File"),
							new ListViewItem.ListViewSubItem(item,
								file.LastAccessTime.ToShortDateString())};
							break;
						}
					case ".bin":
						{
							if (fileName.Contains("cam"))
							{
								subItems = new ListViewItem.ListViewSubItem[]
									{ new ListViewItem.ListViewSubItem(item, "Camera Layout"),
								new ListViewItem.ListViewSubItem(item,
									file.LastAccessTime.ToShortDateString())};
							}
							else if (fileName.Contains("set"))
							{
								subItems = new ListViewItem.ListViewSubItem[]
									{ new ListViewItem.ListViewSubItem(item, "Object Layout"),
								new ListViewItem.ListViewSubItem(item,
									file.LastAccessTime.ToShortDateString())};
							}
							else
							{
								subItems = new ListViewItem.ListViewSubItem[]
									{ new ListViewItem.ListViewSubItem(item, "Binary File"),
								new ListViewItem.ListViewSubItem(item,
									file.LastAccessTime.ToShortDateString())};
							}
							break;
						}
					case ".pvm":
					case ".pvmx":
					case ".gvm":
						{
							subItems = new ListViewItem.ListViewSubItem[]
								{ new ListViewItem.ListViewSubItem(item, "Texture Archive"),
							new ListViewItem.ListViewSubItem(item,
								file.LastAccessTime.ToShortDateString())};
							break;
						}
					case ".prs":
						{
							if (fileName.Contains("mdl"))
							{
								subItems = new ListViewItem.ListViewSubItem[]
								{ new ListViewItem.ListViewSubItem(item, "Compressed Model"),
							new ListViewItem.ListViewSubItem(item,
								file.LastAccessTime.ToShortDateString())};
							}
							else if (fileName.Contains("tex"))
							{
								subItems = new ListViewItem.ListViewSubItem[]
								{ new ListViewItem.ListViewSubItem(item, "Compressed Texture Archive"),
							new ListViewItem.ListViewSubItem(item,
								file.LastAccessTime.ToShortDateString())};
							}
							else if (fileName.Contains("mtn"))
							{
								subItems = new ListViewItem.ListViewSubItem[]
								{ new ListViewItem.ListViewSubItem(item, "Compressed Animations Archive"),
							new ListViewItem.ListViewSubItem(item,
								file.LastAccessTime.ToShortDateString())};
							}
							else
							{
							subItems = new ListViewItem.ListViewSubItem[]
								{ new ListViewItem.ListViewSubItem(item, "Compressed File"),
							new ListViewItem.ListViewSubItem(item,
								file.LastAccessTime.ToShortDateString())};
							}
							break;
						}
					case ".saanim":
						{
							subItems = new ListViewItem.ListViewSubItem[]
								{ new ListViewItem.ListViewSubItem(item, "Animation File"),
							new ListViewItem.ListViewSubItem(item,
								file.LastAccessTime.ToShortDateString())};
							break;
						}
					default:
						{
							subItems = new ListViewItem.ListViewSubItem[]
								{ new ListViewItem.ListViewSubItem(item, "File"),
							new ListViewItem.ListViewSubItem(item,
								file.LastAccessTime.ToShortDateString())};
							break;
						}
				}
				
				item.SubItems.AddRange(subItems);
				listView1.Items.Add(item);
			}

			listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}

		public SAToolsHub()
		{
			InitializeComponent();

			gamePathsDiag = new GamePaths();
			projectCreateDiag = new newProj();
			projectEditorDiag = new editProj();
		}

		//Tool Strip Functions
		//Settings
		private void setGamePathsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			gamePathsDiag.ShowDialog();
		}

		private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		//Projects
		
		private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			projectCreateDiag.ShowDialog();
		}

		private void openProjectToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			treeView1.Nodes.Clear();
			listView1.Items.Clear();
			//projectSelectDiag.ShowDialog();
			var folderDialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
			var folderResult = folderDialog.ShowDialog();
			if (folderResult.HasValue && folderResult.Value)
			{
				projectDirectory = new DirectoryInfo(folderDialog.SelectedPath);
				PopulateTreeView();
				this.treeView1.NodeMouseClick +=
					new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
			}

		}

		private void editProjectInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			projectEditorDiag.ShowDialog();
		}

		private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		//Project Build
		private void autoBuildToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void manualBuildToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void configureRunOptionsToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void buildRunGameToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}


		//Tools
		//General Tools Initializers
		private void sAMDLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// launch samdl
			string samdlPath = "";

#if DEBUG
			samdlPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../../../SAMDL/bin/Debug/SAMDL.exe";
#endif
#if !DEBUG
			samdlPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../SAMDL/SAMDL.exe";
#endif

			Console.WriteLine(samdlPath);

			System.Diagnostics.ProcessStartInfo samdlStartInfo = new System.Diagnostics.ProcessStartInfo(
				Path.GetFullPath(samdlPath)//,
				/*Path.GetFullPath(projectFolder)*/);

			System.Diagnostics.Process samdlProcess = System.Diagnostics.Process.Start(samdlStartInfo);

		}

		private void sALVLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// launch salvl
			string salvlPath = "";

#if DEBUG
			salvlPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../../../SALVL/bin/Debug/SALVL.exe";
#endif
#if !DEBUG
			salvlPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../SALVL/SALVL.exe";
#endif

			Console.WriteLine(salvlPath);

			System.Diagnostics.ProcessStartInfo salvlStartInfo = new System.Diagnostics.ProcessStartInfo(
				Path.GetFullPath(salvlPath)//,
				/*Path.GetFullPath(projectFolder)*/);

			System.Diagnostics.Process salvlProcess = System.Diagnostics.Process.Start(salvlStartInfo);
		}

		private void textureEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// launch TextureEditor
			string texEditPath = "";

#if DEBUG
			texEditPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../../../TextureEditor/bin/Debug/TextureEditor.exe";
#endif
#if !DEBUG
			texEditPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../TextureEditor/TextureEditor.exe";
#endif

			Console.WriteLine(texEditPath);

			System.Diagnostics.ProcessStartInfo texEditStartInfo = new System.Diagnostics.ProcessStartInfo(
				Path.GetFullPath(texEditPath)//,
				/*Path.GetFullPath(projectFolder)*/);

			System.Diagnostics.Process texEditProcess = System.Diagnostics.Process.Start(texEditStartInfo);
		}

		//SADX Tools Initializers
		private void sADXLVL2ToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void sADXTweakerToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void sADXsndSharpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// launch TextureEditor
			string sndSharpPath = "";

#if DEBUG
			sndSharpPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../../../SADXsndSharp/bin/Debug/SADXsndSharp.exe";
#endif
#if !DEBUG
			sndSharpPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../SADXsndSharp/SADXsndSharp.exe";
#endif

			Console.WriteLine(sndSharpPath);

			System.Diagnostics.ProcessStartInfo sndSharpStartInfo = new System.Diagnostics.ProcessStartInfo(
				Path.GetFullPath(sndSharpPath)//,
				/*Path.GetFullPath(projectFolder)*/);

			System.Diagnostics.Process sndSharpProcess = System.Diagnostics.Process.Start(sndSharpStartInfo);
		}

		private void sAFontEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// launch TextureEditor
			string saFontPath = "";

#if DEBUG
			saFontPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../../../SADXFontEdit/bin/Debug/SADXFontEdit.exe";
#endif
#if !DEBUG
			saFontPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../SADXFontEdit/SADXFontEdit.exe";
#endif

			Console.WriteLine(saFontPath);

			System.Diagnostics.ProcessStartInfo saFontStartInfo = new System.Diagnostics.ProcessStartInfo(
				Path.GetFullPath(saFontPath)//,
				/*Path.GetFullPath(projectFolder)*/);

			System.Diagnostics.Process saFontProcess = System.Diagnostics.Process.Start(saFontStartInfo);
		}

		private void sASaveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// launch SA Save
			string saSavePath = "";

#if DEBUG
			saSavePath = Path.GetDirectoryName(Application.ExecutablePath) + "/../../../SASave/bin/Debug/SASave.exe";
#endif
#if !DEBUG
			saSavePath = Path.GetDirectoryName(Application.ExecutablePath) + "/../SASave/SASave.exe";
#endif

			Console.WriteLine(saSavePath);

			System.Diagnostics.ProcessStartInfo saSaveStartInfo = new System.Diagnostics.ProcessStartInfo(
				Path.GetFullPath(saSavePath)//,
				/*Path.GetFullPath(projectFolder)*/);

			System.Diagnostics.Process saSaveProcess = System.Diagnostics.Process.Start(saSaveStartInfo);
		}

		//SA2 Tools Initializers
		private void sA2EventViewerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// launch SA2 Event Viewer
			string sa2EventPath = "";

#if DEBUG
			sa2EventPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../../../SA2EventViewer/bin/Debug/SA2EventViewer.exe";
#endif
#if !DEBUG
			sa2EventPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../SA2EventViewer/SA2EventViewer.exe";
#endif

			Console.WriteLine(sa2EventPath);

			System.Diagnostics.ProcessStartInfo sa2EventStartInfo = new System.Diagnostics.ProcessStartInfo(
				Path.GetFullPath(sa2EventPath)//,
				/*Path.GetFullPath(projectFolder)*/);

			System.Diagnostics.Process saSaveProcess = System.Diagnostics.Process.Start(sa2EventStartInfo);
		}

		private void sA2CutsceneTextEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// launch SA2 Cutscene Text Editor
			string sa2EvTextPath = "";

#if DEBUG
			sa2EvTextPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../../../SA2CutsceneTextEditor/bin/Debug/SA2CutsceneTextEditor.exe";
#endif
#if !DEBUG
			sa2EvTextPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../SA2PC/SA2CutsceneTextEditor/SA2CutsceneTextEditor.exe";
#endif

			Console.WriteLine(sa2EvTextPath);

			System.Diagnostics.ProcessStartInfo sa2EvTextStartInfo = new System.Diagnostics.ProcessStartInfo(
				Path.GetFullPath(sa2EvTextPath)//,
				/*Path.GetFullPath(projectFolder)*/);

			System.Diagnostics.Process saSaveProcess = System.Diagnostics.Process.Start(sa2EvTextStartInfo);
		}

		private void sA2MessageEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// launch SA2 Cutscene Text Editor
			string sa2MsgTextPath = "";

#if DEBUG
			sa2MsgTextPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../../../SA2MessageFileEditor/bin/Debug/SA2MessageFileEditor.exe";
#endif
#if !DEBUG
			sa2MsgTextPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../SA2PC/SA2MessageFileEditor/SA2MessageFileEditor.exe";
#endif

			Console.WriteLine(sa2MsgTextPath);

			System.Diagnostics.ProcessStartInfo sa2MsgTextStartInfo = new System.Diagnostics.ProcessStartInfo(
				Path.GetFullPath(sa2MsgTextPath)//,
				/*Path.GetFullPath(projectFolder)*/);

			System.Diagnostics.Process saSaveProcess = System.Diagnostics.Process.Start(sa2MsgTextStartInfo);
		}

		private void sA2StageSelectEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// launch SA2 Cutscene Text Editor
			string sa2StgSelPath = "";

#if DEBUG
			sa2StgSelPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../../../SA2StageSelEdit/bin/Debug/SA2StageSelEdit.exe";
#endif
#if !DEBUG
			sa2StgSelPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../SA2PC/SA2StageSelEdit/SA2StageSelEdit.exe";
#endif

			Console.WriteLine(sa2StgSelPath);

			System.Diagnostics.ProcessStartInfo sa2StgSelStartInfo = new System.Diagnostics.ProcessStartInfo(
				Path.GetFullPath(sa2StgSelPath)//,
				/*Path.GetFullPath(projectFolder)*/);

			System.Diagnostics.Process saSaveProcess = System.Diagnostics.Process.Start(sa2StgSelStartInfo);
		}

		//Data Extractor/Convert (new Split UI)
		private void splitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// launch Data Tool
			string dataToolPath = "";

#if DEBUG
			dataToolPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../../../DataExtractor/bin/Debug/DataExtractor.exe";
#endif
#if !DEBUG
			dataToolPath = Path.GetDirectoryName(Application.ExecutablePath) + "/../DataExtractor/DataExtractor.exe";
#endif

			Console.WriteLine(dataToolPath);

			System.Diagnostics.ProcessStartInfo dataToolStartInfo = new System.Diagnostics.ProcessStartInfo(
				Path.GetFullPath(dataToolPath)//,
				/*Path.GetFullPath(projectFolder)*/);

			System.Diagnostics.Process saSaveProcess = System.Diagnostics.Process.Start(dataToolStartInfo);
		}

		//Help Links
		//Resources
		private void sAToolsWikiToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start("https://github.com/sonicretro/sa_tools/wiki");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Something went wrong, could not open link in browser.");
			}
		}

		private void retrosSCHGForSADXToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start("https://info.sonicretro.org/SCHG:Sonic_Adventure_DX:_PC");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Something went wrong, could not open link in browser.");
			}
		}

		private void retrosSCHGForSA2ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start("https://info.sonicretro.org/SCHG:Sonic_Adventure_2_(PC)");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Something went wrong, could not open link in browser.");
			}
		}

		private void sADXGitWikiToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start("https://github.com/kellsnc/sadx-modding-guide/wiki");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Something went wrong, could not open link in browser.");
			}
		}

		//GitHub Issue Tracker
		private void gitHubIssueTrackerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start("https://github.com/sonicretro/sa_tools/issues");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Something went wrong, could not open link in browser.");
			}
		}

		//Toolstrip Buttons
		private void tsNewProj_Click(object sender, EventArgs e)
		{
			newProjectToolStripMenuItem_Click(sender, e);
		}

		private void tsOpenProj_Click(object sender, EventArgs e)
		{
			openProjectToolStripMenuItem1_Click(sender, e);
		}

		private void tsEditProj_Click(object sender, EventArgs e)
		{
			editProjectInfoToolStripMenuItem_Click(sender, e);
		}

		private void tsBuildAuto_Click(object sender, EventArgs e)
		{
			autoBuildToolStripMenuItem_Click(sender, e);
		}

		private void tsBuildManual_Click(object sender, EventArgs e)
		{
			manualBuildToolStripMenuItem_Click(sender, e);
		}

		private void tsBuildRun_Click(object sender, EventArgs e)
		{
			buildRunGameToolStripMenuItem_Click(sender, e);
		}

		private void tsUpdate_Click(object sender, EventArgs e)
		{
			checkForUpdatesToolStripMenuItem_Click(sender, e);
		}

		private void tsSAMDL_Click(object sender, EventArgs e)
		{
			sAMDLToolStripMenuItem_Click(sender, e);
		}

		private void tsSALVL_Click(object sender, EventArgs e)
		{
			sALVLToolStripMenuItem_Click(sender, e);
		}

		private void tsTexEdit_Click(object sender, EventArgs e)
		{
			textureEditorToolStripMenuItem_Click(sender, e);
		}


	}
}