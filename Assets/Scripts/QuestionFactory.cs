using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionFactory
{
	public QuestionFactory ()
	{
	}

	private static readonly System.Random rnd = new System.Random ();

	public static Question generate() {
		List<RandNode> opdNodes = new List<RandNode> ();
		List<RandNode> oprNodes = new List<RandNode> ();
		string questionStr = "";
		RandNode rn = getOpd (GlobalSettings.largestNum);
		opdNodes.Add (rn);
		questionStr += rn.number.ToString ();

		for (int i = 0; i < GlobalSettings.numberOfOperators - 1; i++) {
			RandNode opr = getOps (GlobalSettings.level);

			/** Deal with divide, we only want integer for dividen **/
			RandNode opd;
			if (opr.type == QTree.Type.Div) {
				opd = getValidDivisor (getPrevDiv(oprNodes, opdNodes, opdNodes.Count - 1));
			} else {
				opd = getOpd (GlobalSettings.largestNum);
			}

			//Add them in
			opdNodes.Add(opd);
			oprNodes.Add (opr);
			questionStr += " " + QTree.getOperatorStr (opr.type) + " " + opd.number.ToString ();
		}


		Debug.Log (questionStr);
		QTree root = QTreeBuilder.build (opdNodes, oprNodes);
		int rightAnswer = (int)root.evaluate ();
		Question question = new Question (questionStr, rightAnswer);


		Debug.Log ("The correct answer is " + question.answer.ToString ());
		return question;

	}


	private static int getPrevDiv(List<RandNode> operators, List<RandNode> operands, int i) {
		int num = operands [i].number;
		if (i == 0 || operators [i - 1].type != QTree.Type.Div)
			return num;
		else
			return getPrevDiv (operators, operands, i - 1) / num;
	}

	private static RandNode getValidDivisor(int number) {

//		Debug.Log ("Previous number for divide: " + number);

		List<int> candidates = new List<int>(getCandidates (number));
		int cou = rnd.Next (0, candidates.Count);
		HashSet<int> indexes = new HashSet<int> ();
		int divisor = 1;
		Debug.Log ("Generate number from candiates of " + candidates);
		while (cou >= 0) {
			int idx = rnd.Next (0, candidates.Count);
			if (indexes.Contains (idx))
				continue;
			indexes.Add (idx);
			divisor *= candidates [idx];
			cou--;
		}
		RandNode rNode = new RandNode();
		rNode.number = divisor;
		Debug.Log ("Generated divisor : " + divisor);
		return rNode;
	}

	private static LinkedList<int> getCandidates(int num) {
//		Debug.Log ("Get valid candidates for " + num);
		int prime = getOnePrime (num);
//		Debug.Log ("One prime " + prime);
		//Exit condition
		if (prime >= num) {
			LinkedList<int> link = new LinkedList<int> ();
			link.AddLast (prime);
			return link;
		}
		LinkedList<int> res = getCandidates (num / prime);
		res.AddLast (prime);
		return res;
	}

	private static int getOnePrime(int num) {
		foreach(int p in PrimeNumbers.primes) {
			if (num % p == 0) {
				return p;
			}
		}
		Debug.LogError ("We didn't find any prime for this number" + num);
		return num;
	}

	//Get a random operand
	private static RandNode getOpd (int max)
	{
		RandNode node = new RandNode ();
		node.number = rnd.Next (0, max + 1);
		return node;
	}


	//Get a random operator
	private static RandNode getOps (QuestionGenerator.Level level)
	{
		RandNode node = new RandNode ();
		switch (level) {
		case QuestionGenerator.Level.ADD:
			node.type = QTree.Type.Add;
			break;
		case QuestionGenerator.Level.SUB:
			node.type = QTree.Type.Sub;
			break;
		case QuestionGenerator.Level.MUL:
			node.type = QTree.Type.Mul;
			break;
		case QuestionGenerator.Level.DIV:
			node.type = QTree.Type.Div;
			break;
		case QuestionGenerator.Level.ADD_SUB:
			int i = rnd.Next (0, 2);
			node.type = i == 0 ? QTree.Type.Add : QTree.Type.Sub;
			break;
		case QuestionGenerator.Level.ADD_SUB_MUL:
			i = rnd.Next (0, 3);
			node.type = i == 0 ? QTree.Type.Add : i == 1 ? QTree.Type.Sub : QTree.Type.Mul;
			break;
		default:
			i = rnd.Next (0, 4);
			if (i == 0)
				node.type = QTree.Type.Add;
			else if (i == 1)
				node.type = QTree.Type.Sub;
			else if (i == 2)
				node.type = QTree.Type.Mul;
			else
				node.type = QTree.Type.Div;
			break;

		}
		return node;
	}

}

