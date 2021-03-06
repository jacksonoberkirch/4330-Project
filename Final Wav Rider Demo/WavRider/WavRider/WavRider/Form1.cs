﻿using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WavRider
{
    public partial class Form1 : Form
    {
        WaveOutEvent waveOut;

        bool isPlaying = false; //boolean to determine if a song is currently playing
        string currentSong = null; //used in playback functions to determine the song currently being used in playback
       // List<string> songList = new List<string>();

        Mp3FileReader mp3Reader; //file reader for .mp3 files
        WaveFileReader waveReader; //file reader for .wav files
        List<string> songList = new List<string>(); //list to store song file strings
        //SongList songList = new SongList("C:\\Users\\Owner\\Desktop\\MusicStuff");


        public Form1()
        {
          /*  OpenFileDialog open = new OpenFileDialog();
            //open.Filter = "Wave File (.wav)|.wav;";
            if (open.ShowDialog() != DialogResult.OK) return;
            // DisposeWave();
            string name = open.SafeFileName; */
            groupBox2 = new GroupBox(); //group box that will show which song is currently used in playback
          //  MessageBox.Show();
            songList.Add(this.GetType().Assembly.Location.Replace("WavRider.exe", "September.wav")); //add an initial song
            currentSong = songList[0]; //set current song to beginning of list for playback
            string textSong = currentSong;
            CheckSong(songList[0]); 
            //MessageBox.Show(name);
            InitializeComponent();


            if (couter == 1)
            {

                ListViewItem item1 = new ListViewItem("September");
                item1.SubItems.Add("Earth, Wind & Fire");
                item1.SubItems.Add("September");
                item1.SubItems.Add("3:35");
                listView1.Items.AddRange(new ListViewItem[] { item1, });


            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
        /**
        * Function for play/pause. This function determines whether to play the song currently loaded for playback
        **/
        private void button7_Click(object sender, EventArgs e)
        {

            groupBox2.Text = "Playback:\n" + currentSong.Replace(this.GetType().Assembly.Location.Replace("WavRider.exe", ""), "");

            if (isPlaying == true) {
                waveOut.Pause();
                button7.Text = "►";
                isPlaying = false;
            } else if (isPlaying == false) {
                waveOut.Play();
                button7.Text = "❙❙";

                isPlaying = true;
            }
        }
        /**
        * Function for skipping songs. Sets the current song to the next song in the list, if it is the last song then the program loads the first song for playback
        */
        private void button8_Click(object sender, EventArgs e)
        {
            if (currentSong.Equals(songList[songList.Count - 1])) {
                currentSong = songList[0];

                waveOut.Stop();
                waveOut.Dispose();
                if (mp3Reader != null)
                    mp3Reader.Dispose();
                if (waveReader != null)
                    waveReader.Dispose();
                CheckSong(currentSong);
                groupBox2.Text = "Playback:\n" + currentSong.Replace(this.GetType().Assembly.Location.Replace("WavRider.exe", ""), "");

            }
            else {
                currentSong = songList[songList.IndexOf(currentSong) + 1];
                groupBox2.Text = "Playback:\n" + currentSong.Replace(this.GetType().Assembly.Location.Replace("WavRider.exe", ""), "");

                waveOut.Stop();
                waveOut.Dispose();
                if (mp3Reader != null)
                    mp3Reader.Dispose();
                if (waveReader != null)
                    waveReader.Dispose();
                CheckSong(songList[songList.IndexOf(currentSong)]);
                waveOut.Play();
                isPlaying = true;
            }
        }
        int couter =1 ;
      /*  private NAudio.Wave.Mp3FileReader wave = null;
        private NAudio.Wave.DirectSoundOut output = null;
        */
        private void button10_Click(object sender, EventArgs e) {
            OpenFileDialog open = new OpenFileDialog();
            //open.Filter = "Wave File (.wav)|.wav;";
            if (open.ShowDialog() != DialogResult.OK) return;
           // DisposeWave();
            string name = open.SafeFileName;
            songList.Add(name);
            // CheckSong(name);
            MessageBox.Show(name);


            couter++;

            if (couter == 1) {

                ListViewItem item1 = new ListViewItem("September");
                item1.SubItems.Add("Earth, Wind & Fire");
                item1.SubItems.Add("September");
                item1.SubItems.Add("3:35");
                listView1.Items.AddRange(new ListViewItem[] { item1, });


            }
            if (couter == 2) {


                ListViewItem item2 = new ListViewItem("Never Gonna Give You Up");
                item2.SubItems.Add("Rick Astley");
                item2.SubItems.Add("Whenever You Need Somebody");
                item2.SubItems.Add("3:32");
                listView1.Items.AddRange(new ListViewItem[] { item2 });
            }
            if (couter == 3) {




                ListViewItem item3 = new ListViewItem("Blues Demo");
                item3.SubItems.Add("Gil Platt");
                item3.SubItems.Add("Cover");
                item3.SubItems.Add("0:10");
                listView1.Items.AddRange(new ListViewItem[] { item3 });
            }



        }
        /**
        * Checks to see if the file is a .wav or .mp3
        **/
        private void CheckSong(string song) {
            waveOut = new WaveOutEvent();

            if (song.Contains(".mp3")) {
                mp3Reader = new Mp3FileReader(song);
                waveOut.Init(mp3Reader);
                
            } else if (song.Contains(".wav")) {
                waveReader = new WaveFileReader(song);
                waveOut.Init(waveReader);

            }
        }

        /*
        private void DisposeWave() {
            if (output != null) {
                if (output.PlaybackState == NAudio.Wave.PlaybackState.Playing) output.Stop();
                output.Dispose();
                output = null;
            }

            if (wave != null) {
                wave.Dispose();
                wave = null;

            }
            */
        /**
        * Function for rewinding. If the song has been playing for three seconds or more, it rewinds to the beginning of the song.
        * If the song has been playing for three seconds or fewer, it rewinds to the beginning of the song. If the 
        * song is the first one in the list, then it will rewind the song to the beginning, regardless of the amount of time it has been playing
        **/
        private void button9_Click(object sender, EventArgs e) {
            if (currentSong.Equals(songList[0]) && currentSong.Contains(".mp3") && mp3Reader.CurrentTime <= TimeSpan.FromSeconds(3.0))
                mp3Reader.CurrentTime = TimeSpan.FromSeconds(0.0);
            else if (currentSong.Equals(songList[0]) && currentSong.Contains(".wav") && waveReader.CurrentTime <= TimeSpan.FromSeconds(3.0))
                waveReader.CurrentTime = TimeSpan.FromSeconds(0.0);
            else if (currentSong.Equals(songList[0]) && currentSong.Contains(".mp3") && mp3Reader.CurrentTime >= TimeSpan.FromSeconds(3.0))
                mp3Reader.CurrentTime = TimeSpan.FromSeconds(0.0);
            else if (currentSong.Equals(songList[0]) && currentSong.Contains(".wav") && waveReader.CurrentTime >= TimeSpan.FromSeconds(3.0))
                waveReader.CurrentTime = TimeSpan.FromSeconds(0.0);

            else if (currentSong.Contains(".mp3") && mp3Reader.CurrentTime >= TimeSpan.FromSeconds(3.0) && currentSong != songList[0])
                mp3Reader.CurrentTime = TimeSpan.FromSeconds(0.0);
            else if (currentSong.Contains(".wav") && waveReader.CurrentTime >= TimeSpan.FromSeconds(3.0) && currentSong != songList[0])
                waveReader.CurrentTime = TimeSpan.FromSeconds(0.0);
            else {
                currentSong = songList[songList.IndexOf(currentSong) - 1];
                groupBox2.Text = "Playback:\n" + currentSong.Replace(this.GetType().Assembly.Location.Replace("WavRider.exe", ""), "");

                waveOut.Stop();
                waveOut.Dispose();
                if (mp3Reader != null)
                    mp3Reader.Dispose();
                if (waveReader != null)
                    waveReader.Dispose();
                CheckSong(songList[songList.IndexOf(currentSong)]);
                waveOut.Play();
                isPlaying = true;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e) {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {

        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            Form2 form = new Form2();

            form.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
