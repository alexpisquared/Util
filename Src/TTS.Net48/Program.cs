﻿using System;
using System.Diagnostics;
using System.Speech.Synthesis;

namespace TTS.Net48
{
  class Program
  {
    static void Main(string[] args)
    {
      var nts = "Command line is empty.";

      try
      {
        var synth = new SpeechSynthesizer();
        var instVoices = synth.GetInstalledVoices();

        synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Child);
        synth.Rate = 5;

        if (args.Length > 1)
          nts = args[1];
        else
        {
          synth.Speak($"Only {instVoices.Count} voices installed:");
          foreach (var voice in instVoices)
          {
            Debug.WriteLine(voice.VoiceInfo.Name);
            synth.Speak(voice.VoiceInfo.Name);
          }
        }

        synth.Speak(nts);
      }
      catch (Exception ex) { Debug.WriteLine($"\r\n\nExeption in {ex.Source}:\r\n {ex.Message}\n\n\n"); throw; }
    }
  }
}