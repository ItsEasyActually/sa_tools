﻿using System;
using System.Collections.Generic;
using System.IO;
using SonicRetro.SAModel;
using SA_Tools;

namespace CompareTool
{
	class Program
	{

		/*
		public struct DiffData
		{
			public string type;
			public int id;
			//UV
			public int u;
			public int v;
			//Material/Vertex Color
			public int a;
			public int r;
			public int g;
			public int b;
			//Material
			public int texid;
			public uint flags;
			//Vertex/Normal
			public float x;
			public float y;
			public float z;
			//Poly
			public short poly;
		}

		static Dictionary<int, List<DiffData>> biglist;
		*/
		static bool overwrite = true;
		static bool savediff = false;
		static int uvcount = 0;
		
		static void Main(string[] args)
		{
			string filename_src;
			string filename_dst;
			string filename_out = "Result.ini";
			if (args.Length > 1)
			{
				filename_src = Path.GetFullPath(args[0]);
				filename_dst = Path.GetFullPath(args[1]);
				Console.WriteLine("Source file: {0}", filename_src);
				Console.WriteLine("Destination file: {0}", filename_dst);
				for (int a = 0; a < args.Length; a++)
				{
					if (args[a] == "-s") savediff = true;
					if (args[a] == "-a") overwrite = false;
					if (args[a] == "-o") filename_out = Path.GetFullPath(args[a + 1]);
				}
				if (savediff)
				{
					Console.Write("Output file: {0}, ", filename_out);
					Console.Write("mode: " + (overwrite ? "Overwrite" : "Append") + "\n");
				}
			}
			else
			{
				Console.WriteLine("This tool compares two levels or models and outputs a list of differences between them.\n");
				Console.WriteLine("Usage:");
				Console.WriteLine("CompareTool <file1> <file2> [-s] [-a] [-o outputfile]\n");
				Console.WriteLine("Arguments:");
				Console.WriteLine("file1: Source level or model");
				Console.WriteLine("file2: Destination level or model");
				//Console.WriteLine("-s: Save the list of differences to an INI file");
				Console.WriteLine("-a: Append to the list of differences instead of overwriting it");
				Console.WriteLine("-o: Output filename (default is Result.ini)\n");
				Console.WriteLine("Example:");
				Console.WriteLine("CompareTool Level_PC.sa1lvl Level_Gamecube.sa1lvl\n");
				Console.WriteLine("Press ENTER to exit.");
				Console.ReadLine();
				return;
			}
			if (!File.Exists(filename_src) || !File.Exists(filename_dst))
			{
				Console.WriteLine("File {0} or {1} doesn't exist.", filename_src, filename_dst);
				Console.ReadLine();
				return;
			}
			string ext = Path.GetExtension(filename_src).ToLowerInvariant();
			//biglist = new Dictionary<int, List<DiffData>>();
			switch (ext)
			{
				case ".sa1lvl":
					LandTable land_src = LandTable.LoadFromFile(filename_src);
					LandTable land_dst = LandTable.LoadFromFile(filename_dst);
					COL[] arr_src = land_src.COL.ToArray();
					COL[] arr_dst = land_dst.COL.ToArray();
					bool same = true;
					for (int co = 0; co < arr_src.Length; co++)
					{
						if (same && !CompareCOL(arr_src[co], arr_dst[co]))
						{
							Console.WriteLine("COL order different at item {0} ({1} / {2} / {3})! Trying manual match.", co, arr_src[co].Bounds.Center.X, arr_src[co].Bounds.Center.Y, arr_src[co].Bounds.Center.Z);
							same = false;
						}
					}
					//Compare using identical order
					if (same)
					{
						for (int c = 0; c < arr_src.Length; c++)
						{
							if (arr_src[c].Model.Attach != null)
							{
								CompareAttach((BasicAttach)arr_src[c].Model.Attach, (BasicAttach)arr_dst[c].Model.Attach);
							}
						}
					}

					//Compare using different order
					else
					{
						if (arr_dst.Length != arr_src.Length) Console.WriteLine("COL count different: {0} vs {1}", arr_src.Length, arr_dst.Length);
						Dictionary<int, int> matches = new Dictionary <int,int>();
						for (int c1 = 0; c1 < arr_dst.Length; c1++)
                        {
							bool found = false;
							for (int c2 = 0; c2 < arr_dst.Length; c2++)
                            {
								if (arr_src[c1].Model.Attach != null && CompareCOL(arr_src[c1], arr_dst[c2], 0))
                                {
									if (!matches.ContainsKey(c2) && !matches.ContainsValue(c1))
									{
										matches.Add(c2, c1);
										found = true;
										Console.WriteLine("COL item {0} matched with {1}", c1, c2);
										CompareAttach((BasicAttach)arr_src[c1].Model.Attach, (BasicAttach)arr_dst[c2].Model.Attach);
									}
                                }
                            }
							//Try again but less strict
							if (!found)
							{
								for (int c2 = 0; c2 < arr_dst.Length; c2++)
								{
									if (arr_src[c1].Model.Attach != null && CompareCOL(arr_src[c1], arr_dst[c2], 1))
									{
										if (!matches.ContainsKey(c2) && !matches.ContainsValue(c1))
										{
											matches.Add(c2, c1);
											Console.WriteLine("COL item {0} partially matched with {1}", c1, c2);
											CompareAttach((BasicAttach)arr_src[c1].Model.Attach, (BasicAttach)arr_dst[c2].Model.Attach);
										}
									}
								}
							}
                        }
						Console.WriteLine("Total COL items in landtables: {0} vs {1}, matches: {2}", arr_src.Length, arr_dst.Length, matches.Count);
					}
					//if (savediff) SerializeDiffList(filename_out);
					break;
				case ".sa1mdl":
					NJS_OBJECT mdl_src = new ModelFile(filename_src).Model;
					NJS_OBJECT mdl_dst = new ModelFile(filename_dst).Model;
					if (mdl_src.Attach != null) CompareAttach((BasicAttach)mdl_src.Attach, (BasicAttach)mdl_dst.Attach);
					if (mdl_src.Children.Count > 0)
					{
						for (int id = 0; id < mdl_src.Children.Count; id++)
						{
							if (mdl_src.Children[id].Attach != null) CompareAttach((BasicAttach)mdl_src.Children[id].Attach, (BasicAttach)mdl_dst.Children[id].Attach);
						}
					}
					if (mdl_src.Sibling != null && mdl_src.Sibling.Attach != null)
						CompareAttach((BasicAttach)mdl_src.Sibling.Attach, (BasicAttach)mdl_dst.Sibling.Attach);
					//if (savediff) SerializeDiffList(filename_out);
					break;
				default:
					break;
			}
			if (savediff) Console.WriteLine("Total UV array differences: {0}", uvcount);
		}

		static void CompareAttach(BasicAttach att_src, BasicAttach att_dst)
		{
			//Compare materials
			if (att_src.Material.Count != att_dst.Material.Count)
				Console.WriteLine("Material count different! {0} vs {1}", att_src.Material.Count, att_dst.Material.Count);
			for (int m = 0; m < att_src.Material.Count; m++)
			{
				NJS_MATERIAL[] mat_src = att_src.Material.ToArray();
				NJS_MATERIAL[] mat_dst = att_dst.Material.ToArray();
				if (m >= mat_dst.Length) break;
				if (mat_src[m].TextureID != mat_dst[m].TextureID)
					Console.WriteLine("Different texture ID for material {0}: {1} vs {2}", m, mat_src[m].TextureID, mat_dst[m].TextureID);
				if (mat_src[m].DiffuseColor != mat_dst[m].DiffuseColor)
					Console.WriteLine("Different diffuse color for material {0}: {1} vs {2}", m, mat_src[m].DiffuseColor.ToArgb(), mat_dst[m].DiffuseColor.ToArgb());
				if (mat_src[m].Flags != mat_dst[m].Flags)
					Console.WriteLine("Different flags for material {0}: {1} vs {2}", m, mat_src[m].Flags.ToString("X8"), mat_dst[m].Flags.ToString("X8"));
			}

			//Compare vertices
			if (att_src.Vertex.Length != att_dst.Vertex.Length)
				Console.WriteLine("Vertex count different! {0} vs {1}", att_src.Vertex.Length, att_dst.Vertex.Length);
			for (int m = 0; m < att_src.Vertex.Length; m++)
			{
				if (m >= att_dst.Vertex.Length) break;
				if (att_src.Vertex[m].X != att_dst.Vertex[m].X || att_src.Vertex[m].Y != att_dst.Vertex[m].Y || att_src.Vertex[m].Z != att_dst.Vertex[m].Z)
					Console.WriteLine("Different vertex {0}: {1} vs {2}", m, att_src.Vertex[m], att_dst.Vertex[m]);
			}

			//Compare normals
			if (att_src.Normal.Length != att_dst.Normal.Length)
				Console.WriteLine("Normal count different! {0} vs {1}", att_src.Normal.Length, att_dst.Normal.Length);
			for (int m = 0; m < att_src.Normal.Length; m++)
			{
				if (m >= att_dst.Normal.Length) break;
				if (att_src.Normal[m].X != att_dst.Normal[m].X || att_src.Normal[m].Y != att_dst.Normal[m].Y || att_src.Normal[m].Z != att_dst.Normal[m].Z)
					Console.WriteLine("Different normal {0}: {1} vs {2}", m, att_src.Normal[m], att_dst.Normal[m]);
			}

			//Compare meshsets
			if (att_src.Mesh.Count != att_dst.Mesh.Count)
				Console.WriteLine("Mesh count different! {0} vs {1}", att_src.Mesh.Count, att_dst.Mesh.Count);
			for (int u = 0; u < att_src.Mesh.Count; u++)
			{
				//Compare attributes
				if (att_src.Mesh[u].PAttr != att_dst.Mesh[u].PAttr)
					Console.WriteLine("Attributes different for mesh {0}: {1} vs {2}", att_src.Mesh[u].PAttr.ToString("X"), att_dst.Mesh[u].PAttr.ToString("X"));

				//Compare polys
				if (att_src.Mesh[u].Poly == null) continue;
				if (att_src.Mesh[u].Poly.Count != att_dst.Mesh[u].Poly.Count)
					Console.WriteLine("Poly count different for mesh {0}: {1} vs {2}", u, att_src.Mesh[u].Poly.Count, att_dst.Mesh[u].Poly.Count);
				for (int v = 0; v < att_src.Mesh[u].Poly.Count; v++)
				{
					if (v >= att_dst.Mesh[u].Poly.Count) break;
					if (att_src.Mesh[u].Poly[v].Indexes.Length != att_dst.Mesh[u].Poly[v].Indexes.Length)
						Console.WriteLine("Poly index count different for mesh {0}: {1} vs {2}", u, att_src.Mesh[u].Poly[v].Indexes.Length, att_dst.Mesh[u].Poly[v].Indexes.Length);
					for (int i = 0; i < att_src.Mesh[u].Poly[v].Indexes.Length; i++)
					{
						if (i >= att_dst.Mesh[u].Poly[v].Indexes.Length) break;
						if (att_src.Mesh[u].Poly[v].Indexes[i] != att_dst.Mesh[u].Poly[v].Indexes[i])
							Console.WriteLine("Mesh {0} poly {1} index {2} different: {3} vs {4}", u, v, i, att_src.Mesh[u].Poly[v].Indexes[i], att_dst.Mesh[u].Poly[v].Indexes[i]);
					}
				}

				//Compare vcolors
				if (att_src.Mesh[u].VColor == null) continue;
				if (att_src.Mesh[u].VColor.Length != att_dst.Mesh[u].VColor.Length)				
					Console.WriteLine("VColor count different for mesh {0}: {1} vs {2}", u, att_src.Mesh[u].VColor.Length, att_dst.Mesh[u].VColor.Length);
				for (int v = 0; v < att_src.Mesh[u].VColor.Length; v++)
				{
					if (v >= att_dst.Mesh[u].VColor.Length) break;
					if (att_src.Mesh[u].VColor[v].A != att_dst.Mesh[u].VColor[v].A || att_src.Mesh[u].VColor[v].R != att_dst.Mesh[u].VColor[v].R || att_src.Mesh[u].VColor[v].G != att_dst.Mesh[u].VColor[v].G || att_src.Mesh[u].VColor[v].B != att_dst.Mesh[u].VColor[v].B)
						Console.WriteLine("VColor {0} different for mesh {1}: {2} vs {3}", v, u, att_src.Mesh[u].VColor[v], att_dst.Mesh[u].VColor[v]);
				}

				//Compare UVs
				if (att_src.Mesh[u].UV == null) continue;
				bool name = false;
				int addr = int.Parse(att_src.Mesh[u].UVName.Replace("uv_", ""), System.Globalization.NumberStyles.AllowHexSpecifier);
				//List<DiffData> items = new List<DiffData>();
				for (int v = 0; v < att_src.Mesh[u].UV.Length; v++)
				{
					short src_U = (short)(att_src.Mesh[u].UV[v].U * 255f);
					short src_V = (short)(att_src.Mesh[u].UV[v].V * 255f);
					short dst_U = (short)(att_dst.Mesh[u].UV[v].U * 255f);
					short dst_V = (short)(att_dst.Mesh[u].UV[v].V * 255f);
					if (src_U != dst_U || src_V != dst_V)
					{
						if (!name)
						{
							name = true;
							Console.WriteLine("UV array {0} is different", att_src.Mesh[u].UVName);
						}
						//items.Add(new DiffData { id = v, u = dst_U, v = dst_V });
						//Console.WriteLine("{0} : {1}, {2} is {3}, {4}", v, src_U, src_V, dst_U, dst_V);
					}
				}
				/*
				if (items.Count > 0)
				{
					if (biglist.ContainsKey(addr))
					{
						//Console.WriteLine("Reused UV array at {0}", addr.ToString("X"));
					}
					else
					{
						biglist.Add(addr, items);
						uvcount++;
					}
				}*/
			}
		}

		static bool CompareCOL(COL item1, COL item2, int tryhard = 0)
		{
			if (item1.Bounds.Center.X != item2.Bounds.Center.X) return false;
			if (item1.Bounds.Center.Y != item2.Bounds.Center.Y) return false;
			if (item1.Bounds.Center.Z != item2.Bounds.Center.Z) return false;
			if (item1.Bounds.Radius != item2.Bounds.Radius)
			{
				if (tryhard < 1) return false;
				else 
					Console.WriteLine("Radius different for COL item at {0} / {1} / {2}: {3} vs {4}", item1.Bounds.Center.X, item1.Bounds.Center.Y, item1.Bounds.Center.Z, item1.Bounds.Radius, item2.Bounds.Radius);
			}
			if (item1.Flags != item2.Flags)
			{				
				if (tryhard < 1) return false;
				else
					Console.WriteLine("Flags different for COL item at {0} / {1} / {2}: {3} vs {4}", item1.Bounds.Center.X, item1.Bounds.Center.Y, item1.Bounds.Center.Z, item1.Flags.ToString("X"), item2.Flags.ToString("X"));
			}
			if (item1.SurfaceFlags != item2.SurfaceFlags)
			{
				if (tryhard < 1) return false;
				else 
					Console.WriteLine("Surface flags different for COL item at {0} / {1} / {2}: {3} vs {4}", item1.Bounds.Center.X, item1.Bounds.Center.Y, item1.Bounds.Center.Z, item1.SurfaceFlags.ToString("X"), item2.SurfaceFlags.ToString("X"));
			}
			return true;
		}

		/*
		static void SerializeDiffList(string filename)
		{
			TextWriter tw;
			if (overwrite) tw = File.CreateText(filename);
			else tw = File.AppendText(filename);
			foreach (var item in biglist)
			{
				tw.WriteLine("[{0}]", item.Key.ToString("X"));
				foreach (var item2 in item.Value)
				{
					tw.WriteLine("{0}={1},{2}", item2.id, item2.u, item2.v);
				}
				tw.WriteLine();
			}
			tw.Flush();
			tw.Close();
		}
		*/
	}
}