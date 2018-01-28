

public class QTreeExt : QTree {
	public bool isVisited = false;

	public QTreeExt(QTree qt, bool isVisited) {
		this.value = qt.value;
		this.type = qt.type;
		this.left = qt.left;
		this.right = qt.right;
		this.isVisited = isVisited;
	}
}