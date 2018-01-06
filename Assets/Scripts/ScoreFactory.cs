using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ScoreFactory {

	public static Score NoAnswer() {
		return new Score (new TimeSpan(), false, true);
	}

	public static Score Answer(TimeSpan time, bool isRightAns) {
		return new Score (time, isRightAns, false);
	}


}
