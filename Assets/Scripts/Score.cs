using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Score
{
	public bool noAnswer { get; }

	public TimeSpan time { get; }

	public bool correctAns { get; }

	public Score() {
	}

	public Score (TimeSpan time, bool correctAns, bool noAnswer)
	{
		this.time = time;
		this.correctAns = correctAns;
		this.noAnswer = noAnswer;
	}
}
