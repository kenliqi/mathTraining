using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 **/
public class Question 
{

    public long answer;
    public string question;
	public int score;
	public string explaination;

	public Question(string question, long answer, int score, string explaination)
	{
		this.question = question;
		this.answer = answer;
		this.score = score;
		this.question = question;
	}

    public bool verify(long result)
    {
        return result == answer;
    }
}
