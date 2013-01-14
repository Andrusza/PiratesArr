﻿using System;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace Cameras
{
    [Serializable()]
    public partial class FirstPersonCamera : ISerializable
    {
        private static readonly Vector3 worldY = new Vector3(0, 1, 0);
        private Vector3 dir;

        private Quaternion orientation;
        public Matrix view;

        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        private Vector3 eye = new Vector3();
        private Vector3 xAxis = new Vector3(1,0,0);
        private Vector3 yAxis = new Vector3(0,1,0);
        private Vector3 zAxis = new Vector3(0,0,1);

        public FirstPersonCamera(Vector3 position)
        {
            this.view = Matrix.Identity;
            this.dir = new Vector3(0, 0, 1);
            this.orientation = new Quaternion(0, 0, 0, 1);
            this.CameraTranslate(position);
        }

        public FirstPersonCamera(Vector3 position, Quaternion orientation)
        {
            this.view = Matrix.Identity;
            this.dir = new Vector3(0, 0, 1);
            this.orientation = orientation;
            this.CameraTranslate(position);
        }

        public FirstPersonCamera(SerializationInfo info, StreamingContext ctxt)
        {
            this.dir = (Vector3)info.GetValue(("Dir"), typeof(Vector3));
            this.orientation = (Quaternion)info.GetValue("Orientation", typeof(Quaternion));
            this.View = (Matrix)info.GetValue("View", typeof(Matrix));
            this.eye = (Vector3)info.GetValue("Eye", typeof(Vector3));
            this.SetViewPosition();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Dir", this.dir);
            info.AddValue("Orientation", this.orientation);
            info.AddValue("View", this.view);
            info.AddValue("Eye", this.eye);
        }
    }
}