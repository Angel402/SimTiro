/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RenderStreaming;
using Unity.WebRTC;

namespace Utilities
{
    public class VoiceChatManager : MonoBehaviour
    {
        [SerializeField] private WebRTCStreamer _streamer;
    [SerializeField] private List<RTCPeerConnection> _peerConnections = new List<RTCPeerConnection>();

    void Start()
    {
        _streamer.OnAddStream += OnAddStream;
        StartCoroutine(ConnectToPeers());
    }

    IEnumerator ConnectToPeers()
    {
        yield return new WaitForSeconds(1f); // Asegúrate de que la transmisión haya comenzado correctamente

        if (_streamer.SignalingState == SignalingState.Stable)
        {
            // Conectar con los demás participantes
            ConnectToPeer("IP_PARTICIPANTE_1");
            ConnectToPeer("IP_PARTICIPANTE_2");
        }
    }

    void ConnectToPeer(string ipAddress)
    {
        RTCConfiguration configuration = new RTCConfiguration
        {
            iceServers = new List<RTCIceServer> { new RTCIceServer { urls = new List<string> { "stun:" + ipAddress } } }
        };

        var peerConnection = new RTCPeerConnection(configuration);
        peerConnection.OnIceCandidate = candidate => SendIceCandidate(ipAddress, candidate);
        peerConnection.OnDataChannel = channel => SetupDataChannel(channel);
        _peerConnections.Add(peerConnection);

        _streamer.AddPeerConnection(peerConnection);

        var offerOptions = new RTCOfferOptions
        {
            offerToReceiveAudio = 1,
            offerToReceiveVideo = 0
        };

        peerConnection.CreateOffer(offerOptions).Then(offer =>
        {
            peerConnection.SetLocalDescription(offer);
            SendOffer(ipAddress, offer);
        });
    }

    void SendOffer(string ipAddress, RTCSessionDescription offer)
    {
        // Implementa la lógica para enviar la oferta al servidor de señalización
        // Por ejemplo, utilizando Socket.IO o cualquier otro método
    }

    void SendIceCandidate(string ipAddress, RTCIceCandidate candidate)
    {
        // Implementa la lógica para enviar el candidato ICE al servidor de señalización
        // Por ejemplo, utilizando Socket.IO o cualquier otro método
    }

    void OnAddStream(MediaStream stream) 
    {
        // Implementa la lógica para manejar la llegada de un nuevo stream de audio
        // Puedes asociar este stream con un objeto AudioSource en Unity para reproducir el audio
    }

    void SetupDataChannel(RTCDataChannel channel)
    {
        // Implementa la lógica para manejar la llegada de un nuevo canal de datos si es necesario
    }
    }
}*/