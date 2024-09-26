﻿using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerovoice.Recorders
{
    public class NAudioRecorder : BaseRecorder
    {
        private WaveInEvent waveInEvent = new()
        {
            WaveFormat = new WaveFormat(48000, 2),
            // 20ms frame size, discord's default
            BufferMilliseconds = 20,
        };

        public override void Start()
        {
            waveInEvent.StartRecording();
            waveInEvent.DataAvailable += WaveInEvent_DataAvailable;
        }

        public override void Stop() {
            waveInEvent.StopRecording();
            waveInEvent.DataAvailable -= WaveInEvent_DataAvailable;
        }

        public override void Dispose()
        {
            waveInEvent.Dispose();
        }

        private void WaveInEvent_DataAvailable(object? sender, WaveInEventArgs e)
        {
            OnDataAvailable(e.Buffer);
        }
    }
}