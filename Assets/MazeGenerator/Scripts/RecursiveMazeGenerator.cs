using UnityEngine;
using System.Collections;

//<summary>
//Pure recursive maze generation.
//Use carefully for large mazes.
//</summary>
public class RecursiveMazeGenerator : BasicMazeGenerator {

	public RecursiveMazeGenerator(int rows, int columns):base(rows,columns){

	}

	public override void GenerateMaze ()
	{
		VisitCell (0, 0, DIRECTION.Start);
	}

	private void VisitCell(int row, int column, DIRECTION moveMade){
		DIRECTION[] movesAvailable = new DIRECTION[4];
		int movesAvailableCount = 0;

		do{
			movesAvailableCount = 0;

			//check move right
			if(column+1 < ColumnCount && !GetMazeCell(row,column+1).IsVisited){
				movesAvailable[movesAvailableCount] = DIRECTION.Right;
				movesAvailableCount++;
			}else if(!GetMazeCell(row,column).IsVisited && moveMade != DIRECTION.Left){
				GetMazeCell(row,column).WallRight = true;
			}
			//check move forward
			if(row+1 < RowCount && !GetMazeCell(row+1,column).IsVisited){
				movesAvailable[movesAvailableCount] = DIRECTION.Front;
				movesAvailableCount++;
			}else if(!GetMazeCell(row,column).IsVisited && moveMade != DIRECTION.Back){
				GetMazeCell(row,column).WallFront = true;
			}
			//check move left
			if(column > 0 && column-1 >= 0 && !GetMazeCell(row,column-1).IsVisited){
				movesAvailable[movesAvailableCount] = DIRECTION.Left;
				movesAvailableCount++;
			}else if(!GetMazeCell(row,column).IsVisited && moveMade != DIRECTION.Right){
				GetMazeCell(row,column).WallLeft = true;
			}
			//check move backward
			if(row > 0 && row-1 >= 0 && !GetMazeCell(row-1,column).IsVisited){
				movesAvailable[movesAvailableCount] = DIRECTION.Back;
				movesAvailableCount++;
			}else if(!GetMazeCell(row,column).IsVisited && moveMade != DIRECTION.Front){
				GetMazeCell(row,column).WallBack = true;
			}

			if(movesAvailableCount == 0 && !GetMazeCell(row,column).IsVisited){
				GetMazeCell(row,column).IsGoal = true;
			}

			GetMazeCell(row,column).IsVisited = true;

			if(movesAvailableCount > 0){
				switch (movesAvailable[Random.Range(0,movesAvailableCount)]) {
				case DIRECTION.Start:
					break;
				case DIRECTION.Right:
					VisitCell(row,column+1,DIRECTION.Right);
					break;
				case DIRECTION.Front:
					VisitCell(row+1,column,DIRECTION.Front);
					break;
				case DIRECTION.Left:
					VisitCell(row,column-1,DIRECTION.Left);
					break;
				case DIRECTION.Back:
					VisitCell(row-1,column,DIRECTION.Back);
					break;
				}
			}

		}while(movesAvailableCount > 0);
	}
}
