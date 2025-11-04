using UnityEngine;
using System.Collections.Generic;

namespace ZenoSDK
{
    // Data structure carries rendable data during a streaming zone for application to render.
    public class ZoneStreamRenderable
    {
        public Pose sharedOriginOffset;
        public string currentClientId;
        public IDictionary<string, ClientStreamRenderable> clientStreams;
        public Mesh immediateMesh;
        public ObjectDetectionResult? objectDetection;
        public int registeredImageCount;
    }

    // Data structure carries rendable data for a particular connected client during streaming.
    public class ClientStreamRenderable
    {
        public string clientId;
        public bool connected;
        public bool isHost;
        public Pose? clientPose;
        public Vector3[] points;
        public Pose[] trajectory;
    }

    // Data structure carries renderable data of a mesh.
    public class MeshRenderable
    {
        public AnchoredZoneMeshType meshType;
        public Mesh mesh;
        public Texture texture;
    }

    // Data structure carries renderable data of a marker.
    public class MarkerRenderable
    {
        public byte[] imageBytes;
        public Vector2 screenSize;
    }
}
