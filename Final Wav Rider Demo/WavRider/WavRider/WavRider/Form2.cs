﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WavRider {
    public partial class Form2 : Form {
        public Form2() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            List<NAudio.Wave.WaveInCapabilities> sources = new List<NAudio.Wave.WaveInCapabilities>();

            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++) {
                sources.Add(NAudio.Wave.WaveIn.GetCapabilities(i));

            }

            SourceList.Items.Clear();

            foreach (var source in sources) {
                ListViewItem item = new ListViewItem(source.ProductName);
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, source.Channels.ToString()));
                SourceList.Items.Add(item);




            }
        }

        private NAudio.Wave.WaveFileReader wave = null;

        private NAudio.Wave.DirectSoundOut output = null;

        NAudio.Wave.WaveFileWriter waveWritter = null;

        private NAudio.Wave.WaveIn sourceStream = null;
        private NAudio.Wave.DirectSoundOut waveOut = null;

        private void button2_Click(object sender, EventArgs e) {
            if (SourceList.SelectedItems.Count == 0) return;

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Wave File (*.wav)|*.wav;";
            if (save.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;



            int deviceNumber = SourceList.SelectedItems[0].Index;

            sourceStream = new NAudio.Wave.WaveIn();
            sourceStream.DeviceNumber = deviceNumber;
            sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(deviceNumber).Channels);

            sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceStream_DateAvailable);
            waveWritter = new NAudio.Wave.WaveFileWriter(save.FileName, sourceStream.WaveFormat);

            sourceStream.StartRecording();
        }

        private void button3_Click(object sender, EventArgs e) {
            if (waveOut != null) {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;

            }



            if (sourceStream != null) {
                sourceStream.StopRecording();
                sourceStream.Dispose();
                sourceStream = null;

            }

            if (waveWritter != null) {
                waveWritter.Dispose();
                waveWritter = null;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void sourceStream_DateAvailable(object sender, NAudio.Wave.WaveInEventArgs e) {
            if (waveWritter == null) return;

            waveWritter.WriteData(e.Buffer, 0, e.BytesRecorded);
            waveWritter.Flush();
        }
    }
}
