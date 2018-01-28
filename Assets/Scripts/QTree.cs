using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


/**
 * 
 **/
public class QTree
{
	public enum Type {Add, Sub, Mul, Div, Null};

	public double value;

	public QTree left;

	public QTree right;

	public Type type;

	public double result;

	public QTree() {

	}

	public QTree(double value, Type type, QTree left, QTree right) {
		this.value = value;
		this.type = type;
		this.left = left;
		this.right = right;
	}

	public static bool higherPriority(QTree.Type t1, QTree.Type t2) {
		return (t1 == Type.Div || t1 == Type.Mul) &&
			(t2 == Type.Add || t2 == Type.Sub || t2 == Type.Null);
	}

	public static string getOperatorStr(QTree.Type type) {
		switch (type) {
		case Type.Add:
			return "+";
		case Type.Sub:
			return "-";
		case Type.Mul:
			return "*";
		case Type.Div:
			return "/";
		}
		return "";
	}

	//intermediate node
	public QTree(Type type, QTree left, QTree right) {
		Assert.IsTrue (type != Type.Null);
		this.value = double.NaN;
		this.type = type;
		this.left = left;
		this.right = right;
	}

	//leaf node
	public QTree(double value) {
		this.value = value;
		this.type = Type.Null;
		this.left = null;
		this.right = null;
	}

	public double getResult() {
		if (result == null) {
			result = evaluate ();
		}
		return result;
	}
	//Evaluate the question tree to get the answer
	public double evaluate() {
//		Debug.Log ("Evaluation: " + value + " " + type);
		switch (type) {
		case Type.Null:
			return value;
		case Type.Add:
			return left.evaluate () + right.evaluate ();
		case Type.Sub:
			return left.evaluate () - right.evaluate ();
		case Type.Mul:
			return left.evaluate () * right.evaluate ();
		case Type.Div:
			Assert.IsTrue (right.evaluate () != 0.0d);
			return left.evaluate () / right.evaluate ();
		}

		return double.NaN;
	}

	public override string ToString() {
		return type + " " + value;
	}


}
