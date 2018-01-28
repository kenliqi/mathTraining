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

		int score = getScore (root);
		string explaination = getExplaination (root);
		Debug.Log ("Explaination " + explaination + ", " + score);
		Question question = new Question (questionStr, rightAnswer, score, explaination);


		//Debug.Log ("The correct answer is " + question.answer.ToString ());
		return question;

	}

	/*
5 + 6 * 3 / 2 - 3
=5 + 18 / 2 - 3
=5 + 9 -3
=14 - 3
=11
	 *  BFS to a stack, then pop out
	 * 
	 */


	private static string getExplaination(QTree root) {
		Stack<string> exp = new Stack<string> ();
		//bfs
		Stack<QTreeExt> nodes = new Stack<QTreeExt>();
		nodes.Push (new QTreeExt(root, false));
		while (nodes.Count > 0) {
			Stack<QTreeExt> nextLevel = new Stack<QTreeExt> ();
			string levelRes = "";
			while (nodes.Count > 0) {
				QTreeExt qt = nodes.Pop ();
				if (qt.isVisited) {
					levelRes += QTree.getOperatorStr(qt.type);
				} else {
					levelRes += qt.evaluate();
					if (qt.left != null && qt.right != null) {
						nextLevel.Push (new QTreeExt(qt.left, false));
						nextLevel.Push (new QTreeExt(qt, true)); //make sure we don't visit this again
							nextLevel.Push (new QTreeExt(qt.right, false));
					}
				}

			}
			nodes = nextLevel;
			exp.Push (levelRes);
		}

		//pop the result stack
		string result = " ";
		while (exp.Count > 0) {
			result += exp.Pop ();
			result += "\n=";
		}

		return result;
	}


	private static string getOperatorStr(QTree.Type type) {
		switch (type) {
		case QTree.Type.Add:
			return "+";
		case QTree.Type.Sub:
			return "-";
		case QTree.Type.Mul:
			return "*";
		case QTree.Type.Div:
			return "/";
		}
		return "";
	}


	private static int getOperatorScore(QTree.Type type) {
		switch(type) {
		case QTree.Type.Add:
		case QTree.Type.Sub:
			return 1;
		case QTree.Type.Mul:
			return 2;
		case QTree.Type.Div:
			return 3;
		}
		return 1;
	}
	private static int getScore(QTree root){
		if (root.type == null || root.type == QTree.Type.Null) {
			return (int) (root.value / 20) + 1; //every 20, the score is increase by 1
		} else {
			int oprScore = getOperatorScore (root.type);
			int leftScore = getScore (root.left);
			int rightScore = getScore (root.right);
			return oprScore * (leftScore + rightScore);
		}

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

