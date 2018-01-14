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
			RandNode opr = getOps ();

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


		//Debug.Log (questionStr);
		QTree root = QTreeBuilder.build (opdNodes, oprNodes);
		int rightAnswer = (int)root.evaluate ();
		Question question = new Question (questionStr, rightAnswer);


		//Debug.Log ("The correct answer is " + question.answer.ToString ());
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
	private static RandNode getOps ()
	{
		RandNode node = new RandNode ();
		List<QTree.Type> types = new List<QTree.Type> ();
		if (GlobalSettings.hasAdd)
			types.Add (QTree.Type.Add);
		if (GlobalSettings.hasSub)
			types.Add (QTree.Type.Sub);
		if (GlobalSettings.hasMul)
			types.Add (QTree.Type.Mul);
		if (GlobalSettings.hasDiv)
			types.Add (QTree.Type.Div);

		int i = rnd.Next (0, types.Count);
		node.type = types [i];
		return node;
	}

}

