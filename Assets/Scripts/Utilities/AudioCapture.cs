/*using UnityEngine;
using NAudio.Wave;

public class AudioCapture : MonoBehaviour
{
    private WaveInEvent waveIn;
    private WaveFileWriter waveWriter;

    // Ajustes de audio
    public string microphoneName = null;
    public int sampleRate = 44100;
    public int recordingTime = 10; // Duración de la grabación en segundos
    public string outputPath = "output.wav";

    void Start()
    {
        // Configuración del micrófono
        string[] microphones = Microphone.devices;
        if (microphones.Length > 0)
        {
            microphoneName = microphones[0]; // Puedes seleccionar un micrófono específico aquí
        }
        else
        {
            Debug.LogError("No se encontraron micrófonos disponibles.");
            return;
        }

        // Inicia la captura de audio con NAudio
        waveIn = new WaveInEvent();
        waveIn.DeviceNumber = Microphone.GetDeviceCaps(microphoneName, "Number");
        waveIn.WaveFormat = new WaveFormat(sampleRate, 1); // Mono
        waveIn.DataAvailable += WaveIn_DataAvailable;

        waveWriter = new WaveFileWriter(outputPath, waveIn.WaveFormat);

        waveIn.StartRecording();
        Invoke("StopRecording", recordingTime);
    }

    private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
    {
        // Procesamiento de audio (puedes aplicar aquí cualquier procesamiento adicional)
        waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
    }

    void StopRecording()
    {
        // Detiene la grabación y libera los recursos
        waveIn.StopRecording();
        waveWriter.Close();
        waveIn.Dispose();
        waveWriter.Dispose();

        Debug.Log("Grabación finalizada. Guardada en: " + outputPath);
    }
}*/