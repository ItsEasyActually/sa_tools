﻿using System;
using System.Collections.Generic;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using SonicRetro.SAModel.Direct3D;
using SonicRetro.SAModel.SADXLVL2;

namespace SADXObjectDefinitions.Common
{
    public class SpringB : ObjectDefinition
    {
        private SonicRetro.SAModel.Object model;
        private Microsoft.DirectX.Direct3D.Mesh[] meshes;

        public override void Init(Dictionary<string, string> data, string name, Device dev)
        {
            model = ObjectHelper.LoadModel("Objects/Spring/Air Model.ini");
            meshes = ObjectHelper.GetMeshes(model, dev);
        }

        public override float CheckHit(SETItem item, Vector3 Near, Vector3 Far, Viewport Viewport, Matrix Projection, Matrix View, MatrixStack transform)
        {
            transform.Push();
            transform.TranslateLocal(item.Position.ToVector3());
            transform.RotateXYZLocal(item.Rotation.X, item.Rotation.Y, item.Rotation.Z);
            float dist = model.CheckHit(Near, Far, Viewport, Projection, View, transform, meshes);
            transform.Pop();
            return dist;
        }

        public override void Render(SETItem item, Device dev, MatrixStack transform, bool selected)
        {
            transform.Push();
            transform.TranslateLocal(item.Position.ToVector3());
            transform.RotateXYZLocal(item.Rotation.X, item.Rotation.Y, item.Rotation.Z);
            model.DrawModelTree(dev, transform, ObjectHelper.GetTextures("OBJ_REGULAR"), meshes);
            if (selected)
                model.DrawModelTreeInvert(dev, transform, meshes);
            transform.Pop();
        }

        public override string Name { get { return "Air Spring"; } }

        public override Type ObjectType
        {
            get
            {
                return typeof(SpringBSETItem);
            }
        }
    }

    public class SpringBSETItem : SETItem
    {
        public SpringBSETItem() : base() { }
        public SpringBSETItem(byte[] file, int address) : base(file, address) { }

        public float Control
        {
            get
            {
                return Scale.X;
            }
            set
            {
                Scale.X = value;
            }
        }

        public float Speed
        {
            get
            {
                return Scale.Y;
            }
            set
            {
                Scale.Y = value;
            }
        }
    }
}