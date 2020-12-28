﻿using AAV.WPF.AltBpr;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

await UnitTest();

static async Task UnitTest()
{
  var freqs = new[] { 1000, 5000, 3000, 10000, 100 };
  var durtn = .01;
  await ChimerAlt.PlayFreqListOld(freqs, durtn);
  await ChimerAlt.PlayFreqListNew(freqs, durtn);
  return;

  var freqDurnList = new List<int[]>();
  ChimerAlt.connectTheDots_New(20, 20 * 16, freqDurnList, .200);

  connectTheDots(20, 20 * 16, freqDurnList, .200);
  //connectTheDots(20 * 16, 20, freqDurnList, .200);
  //connectTheDots(20, 20 * 16 * 16, freqDurnList, .200 + .200);
  //connectTheDots(20, 20 * 16 * 16 * 2, freqDurnList, .200 + .200 + .050);
  //connectTheDots(20, 20 * 16 * 16 * 16, freqDurnList, .200 + .200 + .200);
}
static void connectTheDots(double freqA, double freqB, List<int[]> freqDurnList, double durationSec = 1, double durnMultr = 1, double frMultr = 1.02)
{
  const double stepDurnSec = 0.05;
  var stepsTtlCnt = (int)(durationSec / stepDurnSec);

  var stepMultiplier = Math.Pow(freqB / freqA, 1.0 / stepsTtlCnt);
  var freq = freqA;

  for (int i = 0; i <= stepsTtlCnt; i++)
  {
    Console.Write($"  {1 + i,4}.  Playing {freq,14:N1} hz  for {stepDurnSec} sec \n");
    freq *= stepMultiplier;
  }
  Console.Write($"  \n");
}