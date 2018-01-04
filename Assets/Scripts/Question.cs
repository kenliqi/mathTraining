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

	public Question(string question, long answer)
	{
		this.question = question;
		this.answer = answer;
	}

    public bool verify(long result)
    {
        return result == answer;
    }
}
