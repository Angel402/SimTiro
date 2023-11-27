/*using System.Collections.Generic;
using Unity.WebRTC;

namespace ServiceLocatorPath
{
    public class LocalVoiceChatServer : ILocalVoiceChatServer
    {
        public LocalVoiceChatServer()
        {
            RTCConfiguration config = new RTCConfiguration
            {
                iceServers = new List<RTCIceServer>
                {
                    new RTCIceServer
                    {
                        urls = new List<string> { "192.168.1.140:8888" }.ToArray(),
                    },
                }.ToArray(),
            };

            // Configurar RTCPeerConnection con 'config'
        
            var peerConnection = new RTCPeerConnection();
            RTCDataChannel dataChannel = peerConnection.CreateDataChannel("myChannel");
        }
    }
}*/