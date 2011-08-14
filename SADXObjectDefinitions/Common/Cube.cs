﻿using System;
using System.Collections.Generic;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using SonicRetro.SAModel.Direct3D;
using SonicRetro.SAModel.SADXLVL2;

namespace SADXObjectDefinitions.Common
{
    public class Cube : ObjectDefinition
    {
        private SonicRetro.SAModel.Object model;
        private Microsoft.DirectX.Direct3D.Mesh[] meshes;

        public override void Init(Dictionary<string, string> data, string name, Device dev)
        {
            model = ObjectHelper.LoadModel("Objects/Collision/Cube Model.ini");
            meshes = ObjectHelper.GetMeshes(model, dev);
        }

        public override float CheckHit(SETItem item, Vector3 Near, Vector3 Far, Viewport Viewport, Matrix Projection, Matrix View, MatrixStack transform)
        {
            transform.Push();
            transform.TranslateLocal(item.Position.ToVector3());
            transform.RotateXYZLocal(0, item.Rotation.Y, 0);
            transform.ScaleLocal((item.Scale.X + 10) / 5f, (item.Scale.Y + 10) / 5f, (item.Scale.Z + 10) / 5f);
            float dist = model.CheckHit(Near, Far, Viewport, Projection, View, transform, meshes);
            transform.Pop();
            return dist;
        }

        public override void Render(SETItem item, Device dev, MatrixStack transform, bool selected)
        {
            transform.Push();
            transform.TranslateLocal(item.Position.ToVector3());
            transform.RotateXYZLocal(0, item.Rotation.Y, 0);
            transform.ScaleLocal((item.Scale.X + 10) / 5f, (item.Scale.Y + 10) / 5f, (item.Scale.Z + 10) / 5f);
            model.DrawModelTree(dev, transform, null, meshes);
            if (selected)
                model.DrawModelTreeInvert(dev, transform, meshes);
            transform.Pop();
        }

        public override string Name { get { return "Solid Cube"; } }
    }
}