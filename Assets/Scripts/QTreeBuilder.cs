using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;


public class QTreeBuilder
{
	public static QTree build (List<RandNode> operands, List<RandNode> operators)
	{
		
		//operands stack is always having 1 more element than operators
		Stack<QTree> opdStack = new Stack<QTree> ();
		Stack<QTree> oprStack = new Stack<QTree> ();

//		foreach (RandNode rn in operands)
//			Debug.Log (rn.number);
//		foreach (RandNode rn in operators)
//			Debug.Log (rn.type);
//
//		Debug.Log ("===================");
//
		List<RandNode>.Enumerator opdItr = operands.GetEnumerator ();
		opdItr.MoveNext ();
		opdStack.Push (new QTree (opdItr.Current.number));
		opdItr.MoveNext ();

		foreach (RandNode node in operators) {

			//keep poping if the next operator is not having higher precedence
			while (oprStack.Count > 0 && !QTree.higherPriority (node.type, oprStack.Peek ().type)) {
				QTree opr = oprStack.Pop ();
				opr.right = opdStack.Pop ();
				opr.left = opdStack.Pop ();

//				Debug.Log (opr.type + " | " + opr.left.value + " | " + opr.right.value);

				opdStack.Push (opr);
			}

			//pushing a new operator in stack
			oprStack.Push (new QTree (node.type, null, null));
			opdStack.Push (new QTree (opdItr.Current.number));
			opdItr.MoveNext ();

		}

		//drain the oprStack
		while (oprStack.Count > 0) {
			QTree opr = oprStack.Pop ();
			opr.right = opdStack.Pop ();
			opr.left = opdStack.Pop ();

//			Debug.Log (opr.type + " | " + opr.left.value + " | " + opr.right.value);

			opdStack.Push (opr);
		}
		return opdStack.Pop ();

	}

	
}
